using Microsoft.ApplicationInsights;
using Microsoft.Extensions.Logging;
using TaxCalculator.App.Core.Models;
using TaxCalculator.App.Repository.Interfaces;

namespace TaxCalculator.App.Services.Services
{
	/// <summary>
	/// Provides functionality to calculate income tax for the UK based on specified tax bands.
	/// </summary>
	/// <remarks>This service calculates the total tax, net income, and other related values for a given annual
	/// salary using a set of tax bands. The tax bands, which define the tax rates and thresholds, are injected into the
	/// service and must be ordered by their lower limits in ascending order.</remarks>
	public class UKTaxCalculatorService : ITaxCalculatorService
	{
		private readonly ILogger<UKTaxCalculatorService> _logger;
		private readonly TelemetryClient _telemetry;
		private readonly ITaxBandRepository _taxBandRepository;

		public UKTaxCalculatorService(ILogger<UKTaxCalculatorService> logger,
	                                 TelemetryClient telemetry,
									 ITaxBandRepository taxBandRepository)
		{
			_taxBandRepository = taxBandRepository ?? throw new ArgumentNullException(nameof(taxBandRepository));
			_logger = logger ?? throw new ArgumentNullException(nameof(logger));
			_telemetry = telemetry ?? throw new ArgumentNullException(nameof(telemetry));
		}
		public async Task<TaxResult> Calculate(decimal salary)
		{
			if (salary <= 0)
				throw new ArgumentException("Salary must be a positive integer.", nameof(salary));
			

			try
			{
				//Ideally if we are going to expose the entity to the UI layer , then map it to the model.
				//Good practice to only expose the necessary information to the UI layer.
				//skipping that here since we are not sending bands to the UI layer.
				var bands = await _taxBandRepository.GetAllAsync();

				//Should be ordered by LowerLimit in ascending order for the loop to work correctly.
				bands = bands.OrderBy(b => b.LowerLimit).ToList();
				decimal totalTax = 0;

				//Loop through each tax band and calculate the tax
				foreach (var band in bands)
				{
					if (salary <= band.LowerLimit)
						continue;

					int bandLower = band.LowerLimit;
					int bandUpper = band.UpperLimit ?? int.MaxValue;

					decimal taxableInBand = Math.Min(salary, bandUpper) - bandLower;

					if (taxableInBand > 0)
					{
						decimal bandTax = taxableInBand * (band.Rate / 100m);
						totalTax += bandTax;
					}
				}

				// Calculate net annual income after tax
				decimal netAnnual = salary - totalTax;

				_logger.LogInformation("Tax calculated successfully for salary {Salary}", salary);
				_telemetry.TrackEvent("TaxCalculationSuccess", new Dictionary<string, string> { { "Salary", salary.ToString() } });

				// Return the tax result with all calculated values and set isSuccess to true or else false.
				return new TaxResult
				{
					GrossAnnual = salary,
					GrossMonthly = Math.Round(salary / 12m, 2),
					NetAnnual = Math.Round(netAnnual, 2),
					NetMonthly = Math.Round(netAnnual / 12m, 2),
					AnnualTax = Math.Round(totalTax, 2),
					MonthlyTax = Math.Round(totalTax / 12m, 2),
					IsSuccess = true
				};
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Unhandled exception during tax calculation.");
				_telemetry.TrackException(ex);
				return new TaxResult() { IsSuccess = false };
			}
		}
	}
}

