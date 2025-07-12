namespace TaxCalculator.App.Core.Models
{
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
