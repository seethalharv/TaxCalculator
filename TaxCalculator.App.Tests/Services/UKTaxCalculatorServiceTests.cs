using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxCalculator.App.Core.Models;
using TaxCalculator.App.Services.Services;

namespace TaxCalculator.App.Tests.services
{
	[TestClass]
	public class UKTaxCalculatorServiceTests
	{
		private UKTaxCalculatorService _service;

		[TestInitialize]
		public void Setup()
		{
			var bands = new List<TaxBand>
			{
				new TaxBand { LowerLimit = 0, UpperLimit = 5000, TaxRate = 0 },
				new TaxBand { LowerLimit = 5000, UpperLimit = 20000, TaxRate = 20 },
				new TaxBand { LowerLimit = 20000, UpperLimit = null, TaxRate = 40 }
			};

			_service = new UKTaxCalculatorService(bands);
		}

		[TestMethod]
		public void Calculate_ReturnsZeroTax_ForSalaryBelow5000()
		{
			var result = _service.Calculate(4000);

			Assert.AreEqual(4000, result.NetAnnual);
			Assert.AreEqual(0, result.AnnualTax);
		}

		[TestMethod]
		public void Calculate_ReturnsCorrectTax_ForSalaryWithin20PercentBand()
		{
			var result = _service.Calculate(15000);

			// Taxable: 15000 - 5000 = 10000 @ 20%
			decimal expectedTax = 10000 * 0.20m;
			decimal expectedNet = 15000 - expectedTax;

			Assert.AreEqual(decimal.Round(expectedTax, 2), result.AnnualTax);
			Assert.AreEqual(decimal.Round(expectedNet, 2), result.NetAnnual);
		}
		[TestMethod]
		public void Calculate_ReturnsCorrectTax_ForSalaryCrossingInto40PercentBand()
		{
			var result = _service.Calculate(30000);

			/*
             - First 5000: 0%
             - 5001–20000: 15,000 @ 20%
             - 20001–30000: 10,000 @ 40%
            */

			decimal tax20 = (20000 - 5000) * 0.20m;
			decimal tax40 = (30000 - 20000) * 0.40m;
			decimal totalTax = tax20 + tax40;
			decimal expectedNet = 30000 - totalTax;

			Assert.AreEqual(decimal.Round(totalTax, 2), result.AnnualTax);
			Assert.AreEqual(decimal.Round(expectedNet, 2), result.NetAnnual);
		}

		[TestMethod]
		public void Calculate_ReturnsCorrectTax_ForHighSalary()
		{
			var result = _service.Calculate(100000);

			/*
             - First 5000: 0%
             - 5001–20000: 15,000 @ 20%
             - 20001–100000: 80,000 @ 40%
            */

			decimal tax20 = (20000 - 5000) * 0.20m;
			decimal tax40 = (100000 - 20000) * 0.40m;
			decimal totalTax = tax20 + tax40;
			decimal expectedNet = 100000 - totalTax;

			Assert.AreEqual(decimal.Round(totalTax, 2), result.AnnualTax);
			Assert.AreEqual(decimal.Round(expectedNet, 2), result.NetAnnual);
		}

	}

}
