using BookingResort_ResortAPI.Models;
using System.Linq.Expressions;

namespace BookingResort_ResortAPI.Repository.IRepository
{
	public interface IResortRepository
	{
		Task<List<Resort>> GetAllAsync(Expression<Func<Resort, bool>> filter = null);
		Task<Resort> GetAsync(Expression<Func<Resort, bool>> filter = null, bool tracked=true);
		Task CreateAsync(Resort entity);
		Task UpdateAsync(Resort entity);
		Task RemoveAsync(Resort entity);
		Task SaveAsync();
	}
}
