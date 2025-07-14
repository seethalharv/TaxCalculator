using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxCalculator.App.Repository.Entities
{
	public class TaxBandEntity
	{
		public int Id { get; set; }
		public int LowerLimit { get; set; }
		public int? UpperLimit { get; set; }
		public int Rate { get; set; } 
	}
}
