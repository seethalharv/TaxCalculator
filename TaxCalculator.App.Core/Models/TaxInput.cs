using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxCalculator.App.Core.Models
{
	/// <summary>
	/// This class represents the input model for tax calculation.
	/// Leaving this as class and not just a record to allow for future extensibility.
	/// </summary>
	public class TaxInput
	{
		[Required(ErrorMessage = "Salary is required.")]
		[Range(1, int.MaxValue, ErrorMessage = "Salary must be a positive number.")]
		public int Salary { get; set; }
		//This could potentially take more inputs like , year or tax exemptions and such to further customize the calculation 
		// for example number of children, marital status, etc.
	}
}
