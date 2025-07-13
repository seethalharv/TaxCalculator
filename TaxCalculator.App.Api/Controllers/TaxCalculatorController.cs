using Microsoft.ApplicationInsights;
using Microsoft.AspNetCore.Mvc;
using TaxCalculator.App.Core.Models;
using TaxCalculator.App.Services.Services;

namespace TaxCalculator.App.Api.Controllers
{
	/// <summary>
	/// Provides endpoints for calculating taxes based on user input.
	/// </summary>
	/// <remarks>This controller handles tax calculation requests by exposing an API endpoint that accepts input
	/// data, validates it, and returns the calculated tax result.</remarks>
	[ApiController]
	[Route("api/taxCalculator")]
	public class TaxCalculatorController : ControllerBase
	{
		private readonly ITaxCalculatorService _calculator;
		private readonly ILogger<TaxCalculatorController> _logger;
		private readonly TelemetryClient _telemetry;

		public TaxCalculatorController(ITaxCalculatorService calculator, ILogger<TaxCalculatorController> logger,
			TelemetryClient telemetry)
		{
			_calculator = calculator;
			_logger = logger;
			_telemetry = telemetry ?? throw new ArgumentNullException(nameof(telemetry));
		}

		/// <summary>
		/// Calculates the tax based on the provided salary input.
		/// </summary>
		/// <remarks>This method validates the input model state and salary before performing the tax calculation.  If
		/// the input is invalid, a bad request response is returned with the relevant error details.</remarks>
		/// <param name="input">The tax input containing the salary for which the tax is to be calculated. Must not be null, and the salary must
		/// be greater than zero.</param>
		/// <returns>An <see cref="ActionResult{T}"/> containing the calculated tax result if the input is valid.  Returns a <see
		/// cref="BadRequestObjectResult"/> with validation errors if the input is invalid.</returns>
		[HttpPost("calculate")]
		public ActionResult<TaxResult> Calculate([FromBody] TaxInput? input)
		{
			_telemetry.TrackEvent("TaxCalculationRequest", new Dictionary<string, string>
			{
				{ "Salary", input?.Salary.ToString() ?? "null" }
			});
			_logger.LogInformation("Received tax calculation request for salary: {Salary}", input?.Salary);
			if (!ModelState.IsValid)
			{
				var errors = ModelState.Values
					.SelectMany(v => v.Errors)
					.Select(e => e.ErrorMessage)
					.ToList();
				_logger.LogWarning("Invalid model state: {Errors}", string.Join(", ", errors));
				return BadRequest(new { Errors = errors });
			}

			if (input == null || input.Salary <= 0)
				return BadRequest("Invalid input");

			var result = _calculator.Calculate(input.Salary);
			_telemetry.TrackEvent("TaxCalculationSuccess", new Dictionary<string, string>
		    {
			    { "Salary", input.Salary.ToString() },
			    { "AnnualTax", result.AnnualTax.ToString("F2") }
		    });
			_logger.LogInformation("Tax calculation successful for salary: {Salary}", input.Salary);
			return Ok(result);
		}
	}
}
