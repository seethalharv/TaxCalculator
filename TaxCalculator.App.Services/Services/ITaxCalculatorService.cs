using TaxCalculator.App.Core.Models;

namespace TaxCalculator.App.Services.Services
{
	/// <summary>
	/// Interface for tax calculation services.
	/// </summary>
	public interface ITaxCalculatorService
	{
		TaxResult Calculate(decimal salary);
	}
}
