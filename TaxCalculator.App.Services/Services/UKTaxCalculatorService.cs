using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using TaxCalculator.App.Core.Models;

namespace TaxCalculator.App.Services.Services
{
	public class UKTaxCalculatorService : ITaxCalculatorService
	{
		//The bands are set in app settings and injected via DI
		private readonly List<TaxBand> _bands;
		public UKTaxCalculatorService(IEnumerable<TaxBand> bands)
		{
			// Ensure bands are ordered by LowerLimit ascending
			_bands = bands.OrderBy(b => b.LowerLimit).ToList();
		}
		public TaxResult Calculate(int salary)
		{
			decimal totalTax = 0;

			foreach (var band in _bands)
			{
				if (salary <= band.LowerLimit)
					continue;

				int bandLower = band.LowerLimit;
				int bandUpper = band.UpperLimit ?? int.MaxValue;

				int taxableInBand = Math.Min(salary, bandUpper) - bandLower;

				if (taxableInBand > 0)
				{
					decimal bandTax = taxableInBand * (band.TaxRate / 100m);
					totalTax += bandTax;
				}
			}

			decimal netAnnual = salary - totalTax;

			return new TaxResult
			{
				GrossAnnual = salary,
				GrossMonthly = Math.Round(salary / 12m, 2),
				NetAnnual = Math.Round(netAnnual, 2),
				NetMonthly = Math.Round(netAnnual / 12m, 2),
				AnnualTax = Math.Round(totalTax, 2),
				MonthlyTax = Math.Round(totalTax / 12m, 2)
			};
		}
	}
}

