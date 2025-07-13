using System.Diagnostics.CodeAnalysis;

namespace TaxCalculator.App.Core.Models
{
	/// <summary>
	/// Represents the result of a tax calculation, including gross income, net income, and tax amounts on both an annual
	/// and monthly basis.
	/// </summary>
	/// <remarks>This class provides a structured way to store and access the results of a tax computation. It
	/// includes properties for gross and net income, as well as the corresponding tax amounts. Additionally, the tax year
	/// is included to indicate the fiscal year for which the calculation applies.</remarks>
	[ExcludeFromCodeCoverageAttribute]
	public class TaxResult
	{
		public decimal GrossAnnual { get; set; }
		public decimal GrossMonthly { get; set; }
		public decimal NetAnnual { get; set; }
		public decimal NetMonthly { get; set; }
		public decimal AnnualTax { get; set; }
		public decimal MonthlyTax { get; set; }
		public string? TaxYear { get; set; } = DateTime.Now.Year.ToString();
	}
}
