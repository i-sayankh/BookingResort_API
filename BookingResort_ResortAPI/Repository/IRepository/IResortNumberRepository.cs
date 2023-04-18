using BookingResort_ResortAPI.Models;
using System.Linq.Expressions;

namespace BookingResort_ResortAPI.Repository.IRepository
{
	public interface IResortNumberRepository : IRepository<ResortNumber>
	{		
		Task<ResortNumber> UpdateAsync(ResortNumber entity);		
	}
}
