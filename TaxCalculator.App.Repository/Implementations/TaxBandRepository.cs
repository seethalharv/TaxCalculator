using Microsoft.EntityFrameworkCore;
using TaxCalculator.App.Repository.DbContexts;
using TaxCalculator.App.Repository.Entities;
using TaxCalculator.App.Repository.Interfaces;

namespace TaxCalculator.App.Repository.Implementations
{
	/// <summary>
	/// Provides methods for accessing and retrieving tax band data from the database.
	/// </summary>
	/// <remarks>This repository is responsible for interacting with the underlying data store to retrieve tax band
	/// information. It implements the <see cref="ITaxBandRepository"/> interface.</remarks>
	public class TaxBandRepository : ITaxBandRepository
	{
		private readonly AppDbContext _context;

		public TaxBandRepository(AppDbContext context)
		{
			_context = context;
		}

		/// <summary>
		/// Asynchronously retrieves all tax band entities from the data source.
		/// </summary>
		/// <remarks>This method queries the underlying data source to retrieve all tax band entities.  The returned
		/// list will be empty if no tax bands are found.</remarks>
		/// <returns>A task that represents the asynchronous operation. The task result contains a list of <see cref="TaxBandEntity"/>
		/// objects.</returns>
		public async Task<List<TaxBandEntity>> GetAllAsync()
		{
			return await _context.TaxBands.ToListAsync();
		}
	}
}
