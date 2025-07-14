using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaxCalculator.App.Repository.DbContexts;
using TaxCalculator.App.Repository.Entities;
using TaxCalculator.App.Repository.Implementations;

namespace TaxCalculator.App.Tests.Repository
{
	/// <summary>
	/// Test class to validate the functionality of the TaxBandRepository.	
	/// </summary>
	[TestClass]
	public class TaxBandRepositoryTests
	{
		private async Task<AppDbContext> CreateContextWithDataAsync(string dbName, bool seedData = true)
		{
			var options = new DbContextOptionsBuilder<AppDbContext>()
				.UseInMemoryDatabase(databaseName: dbName)
				.Options;

			var context = new AppDbContext(options);

			if (seedData)
			{
				context.TaxBands.AddRange(new List<TaxBandEntity>
				{
					new TaxBandEntity { Id = 1, LowerLimit = 0, UpperLimit = 5000, Rate = 0 },
					new TaxBandEntity { Id = 2, LowerLimit = 5000, UpperLimit = 20000, Rate = 20 },
					new TaxBandEntity { Id = 3, LowerLimit = 20000, UpperLimit = null, Rate = 40 }
				});

				await context.SaveChangesAsync();
			}

			return context;
		}

		[TestMethod]
		public async Task GetAllAsync_ShouldReturnAllTaxBands()
		{
			// Arrange
			var context = await CreateContextWithDataAsync("TaxBandDb_WithData");
			var repository = new TaxBandRepository(context);

			// Act
			var result = await repository.GetAllAsync();

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(3, result.Count);
			Assert.IsTrue(result.Exists(b => b.Rate == 20));
		}

		[TestMethod]
		public async Task GetAllAsync_ShouldReturnEmptyList_WhenNoDataExists()
		{
			// Arrange
			var context = await CreateContextWithDataAsync("TaxBandDb_Empty", seedData: false);
			var repository = new TaxBandRepository(context);

			// Act
			var result = await repository.GetAllAsync();

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(0, result.Count);
		}
	}
}
