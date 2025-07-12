using TaxCalculator.App.Core.Models;
using TaxCalculator.App.Services.Services;

var builder = WebApplication.CreateBuilder(args);

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
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
