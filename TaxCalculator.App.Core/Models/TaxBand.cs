using System.Diagnostics.CodeAnalysis;

namespace TaxCalculator.App.Core.Models;

/// <summary>
/// Represents a tax band with defined lower and optional upper income limits, and an associated tax rate.	
/// </summary>
/// <remarks>A tax band defines a range of income and the percentage tax rate applied to that range.  The see
/// cref=UpperLimit property is nullable, indicating that the tax band may have no upper cap.</remarks>
[ExcludeFromCodeCoverage]
public class TaxBand
{
	public int LowerLimit { get; set; }
	public int? UpperLimit { get; set; } // null = no upper cap
	public int TaxRate { get; set; } // percentage
}
