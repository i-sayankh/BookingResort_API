using BookingResort_ResortAPI.Models;
using System.Linq.Expressions;

namespace BookingResort_ResortAPI.Repository.IRepository
{
	public interface IResortRepository
	{
		Task<List<Resort>> GetAll(Expression<Func<Resort>> filter = null);
		Task<Resort> Get(Expression<Func<Resort>> filter = null, bool tracked=true);
		Task Create(Resort entity);
		Task Remove(Resort entity);
		Task Save();
	}
}
