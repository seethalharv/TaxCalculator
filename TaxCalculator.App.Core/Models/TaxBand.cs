using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxCalculator.App.Core.Models
{
	public class TaxBand
	{
		public int LowerLimit { get; set; }
		public int? UpperLimit { get; set; } // null = no upper cap
		public int TaxRate { get; set; } // percentage
	}
}
