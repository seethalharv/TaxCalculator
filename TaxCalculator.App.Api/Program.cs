using TaxCalculator.App.Api;
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
builder.WebHost.UseWebRoot("AngularClient");

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
app.UseStaticFiles(); 
app.MapFallbackToFile("index.html");

app.MapControllers();

app.Run();
