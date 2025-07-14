using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxCalculator.App.Repository.Entities;

namespace TaxCalculator.App.Repository.Interfaces
{
	/// <summary>
	/// Defines a repository for accessing and managing tax band data.
	/// </summary>
	/// <remarks>This interface provides methods for retrieving tax band information.  Implementations of this
	/// interface are responsible for interacting with the underlying data source.</remarks>
	public interface ITaxBandRepository
	{
		Task<List<TaxBandEntity>> GetAllAsync();
	}
}
