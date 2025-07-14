using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using TaxCalculator.App.Repository.Entities;
using Microsoft.EntityFrameworkCore;

namespace TaxCalculator.App.Repository.DbContexts;

public class AppDbContext : DbContext
{
	public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

	public DbSet<TaxBandEntity> TaxBands { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<TaxBandEntity>().HasData(
			new TaxBandEntity { Id = 1, LowerLimit = 0, UpperLimit = 5000, Rate = 0 },
			new TaxBandEntity { Id = 2, LowerLimit = 5000, UpperLimit = 20000, Rate = 20 },
			new TaxBandEntity { Id = 3, LowerLimit = 20000, UpperLimit = null, Rate = 40 }
		);
	}


}