namespace TaxCalculator.App.Api
{
	public class Startup
	{
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddControllers();
			services.AddSwaggerGen();
			services.AddEndpointsApiExplorer();
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseRouting();
			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}

}
