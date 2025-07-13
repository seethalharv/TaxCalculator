using Microsoft.AspNetCore.Mvc;
using TaxCalculator.App.Core.Models;
using TaxCalculator.App.Services.Services;

namespace TaxCalculator.App.Api.Controllers
{
	[ApiController]
	[Route("api/taxCalculator")]
	public class TaxCalculatorController : ControllerBase
	{
		private readonly ITaxCalculatorService _calculator;

		public TaxCalculatorController(ITaxCalculatorService calculator)
		{
			_calculator = calculator;
		}
		[HttpPost("calculate")]
		public ActionResult<TaxResult> Calculate([FromBody] TaxInput input)
		{

			if (!ModelState.IsValid)
			{
				var errors = ModelState.Values
					.SelectMany(v => v.Errors)
					.Select(e => e.ErrorMessage)
					.ToList();

				return BadRequest(new { Errors = errors });
			}

			if (input == null || input.Salary <= 0)
				return BadRequest("Invalid input");

			var result = _calculator.Calculate(input.Salary);
			return Ok(result);
		}
	}
}
