using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.Extensions.FileProviders;
using TaxCalculator.App.Core.Models;
using TaxCalculator.App.Services.Services;

try
{
	var options = new WebApplicationOptions
	{
		WebRootPath = "AngularClient"  // This is needed to serve static files from the Angular client. 
	};
	var builder = WebApplication.CreateBuilder(options);

	var taxBands = builder.Configuration
		.GetSection("TaxBands")
		.Get<List<TaxBand>>();
	if (taxBands == null || !taxBands.Any())
	{
		throw new InvalidOperationException("TaxBands configuration section is missing or empty.");
	}
	builder.Services.AddSingleton<IEnumerable<TaxBand>>(taxBands);

	// Add services to the container.

	builder.Services.AddControllers();
	// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
	builder.Services.AddEndpointsApiExplorer();
	builder.Services.AddSwaggerGen();
	builder.Services.AddScoped<ITaxCalculatorService, UKTaxCalculatorService>();
	builder.Services.AddApplicationInsightsTelemetry();
	builder.Services.AddSingleton<TelemetryClient>();

	//Add CORS policy
	builder.Services.AddCors(options =>
	{
		options.AddDefaultPolicy(policy =>
		{
			policy
				.WithOrigins("http://localhost:4200")
				.AllowAnyMethod()
				.AllowAnyHeader();
		});
	});

	var app = builder.Build();
	app.UseCors();
	// Configure the HTTP request pipeline.
	if (app.Environment.IsDevelopment())
	{
		app.UseSwagger();
		app.UseSwaggerUI();
	}

	app.UseHttpsRedirection();
	app.UseDefaultFiles();
	app.UseStaticFiles(new StaticFileOptions
	{
		FileProvider = new PhysicalFileProvider(
			Path.Combine(Directory.GetCurrentDirectory(), "AngularClient", "browser")),
		RequestPath = ""
	});
	app.MapFallbackToFile("index.html");
	app.UseRouting();
	app.MapControllers();

	app.Run();
}
catch (Exception ex)
{
	Console.WriteLine($"An error occurred while starting the application: {ex.Message}");
	Console.WriteLine(ex.StackTrace);

	File.WriteAllText(
		Path.Combine(Directory.GetCurrentDirectory(), "startup-error.txt"),
		ex.ToString());

	// Log to Application Insights
	var telemetryClient = new TelemetryClient(TelemetryConfiguration.CreateDefault());
	telemetryClient.TrackException(ex);
	telemetryClient.Flush();
	Thread.Sleep(5000);

	throw;
}
