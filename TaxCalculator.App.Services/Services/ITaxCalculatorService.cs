using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxCalculator.App.Core.Models;

namespace TaxCalculator.App.Services.Services
{
	public interface ITaxCalculatorService
	{
		TaxResult Calculate(int salary);
	}
}
