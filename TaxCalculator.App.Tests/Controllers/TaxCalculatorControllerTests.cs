using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using TaxCalculator.App.Api.Controllers;
using TaxCalculator.App.Core.Models;
using TaxCalculator.App.Services.Services;

namespace TaxCalculator.App.Tests.Controllers;

[TestClass]
public class TaxCalculatorControllerTests
{
	private Mock<ITaxCalculatorService> _mockService = new();
	private TaxCalculatorController _controller = null!;

	[TestInitialize]
	public void Setup()
	{
		_mockService = new Mock<ITaxCalculatorService>();
		var _logger = new Mock<ILogger<TaxCalculatorController>>();
		var telemetryConfig = TelemetryConfiguration.CreateDefault();
		var telemetryClient = new TelemetryClient(telemetryConfig);
		_controller = new TaxCalculatorController(_mockService.Object, _logger.Object, telemetryClient);
	}

	[TestMethod]
	public void Calculate_ReturnsBadRequest_WhenInputIsNull()
	{
		// Act
		var result = _controller.Calculate(null);

		// Assert
		Assert.IsInstanceOfType(result.Result, typeof(BadRequestObjectResult));
	}

	[TestMethod]
	public void Calculate_ReturnsBadRequest_WhenSalaryIsInvalid()
	{
		// Arrange
		var input = new TaxInput { Salary = 0 };

		// Act
		var result = _controller.Calculate(input);

		// Assert
		Assert.IsInstanceOfType(result.Result, typeof(BadRequestObjectResult));
	}

	[TestMethod]
	public void Calculate_ReturnsOk_WithCorrectResult()
	{
		// Arrange
		var input = new TaxInput { Salary = 50000 };
		var expectedResult = new TaxResult { NetAnnual = 40000, AnnualTax = 10000, IsSuccess = true };

		_mockService.Setup(s => s.Calculate(50000)).ReturnsAsync(expectedResult);

		// Act
		var result = _controller.Calculate(input);

		// Assert
		var okResult = result.Result as OkObjectResult;
		Assert.IsNotNull(okResult);
		Assert.IsInstanceOfType(okResult.Value, typeof(TaxResult));
		var actualResult = okResult.Value as TaxResult;

		Assert.AreEqual(expectedResult.NetAnnual, actualResult?.NetAnnual);
		Assert.AreEqual(expectedResult.AnnualTax, actualResult?.AnnualTax);
	}
}
