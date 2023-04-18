using BookingResort_ResortAPI.Models;
using System.Linq.Expressions;

namespace BookingResort_ResortAPI.Repository.IRepository
{
	public interface IResortRepository : IRepository<Resort>
	{		
		Task<Resort> UpdateAsync(Resort entity);		
	}
}
