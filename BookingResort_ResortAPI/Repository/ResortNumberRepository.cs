using BookingResort_ResortAPI.Data;
using BookingResort_ResortAPI.Models;
using BookingResort_ResortAPI.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace BookingResort_ResortAPI.Repository
{
	public class ResortNumberRepository : Repository<ResortNumber>, IResortNumberRepository
	{
		private readonly ApplicationDbContext _db;
		public ResortNumberRepository(ApplicationDbContext db) : base(db) 
		{
			_db = db;
		}	

		public async Task<ResortNumber> UpdateAsync(ResortNumber entity)
		{
			entity.UpdatedDate = DateTime.Now;
			_db.ResortNumbers.Update(entity);
			await _db.SaveChangesAsync();
			return entity;
		}
	}
}
