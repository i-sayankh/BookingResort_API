using BookingResort_ResortAPI.Data;
using BookingResort_ResortAPI.Models;
using BookingResort_ResortAPI.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace BookingResort_ResortAPI.Repository
{
	public class ResortRepository : Repository<Resort>, IResortRepository
	{
		private readonly ApplicationDbContext _db;
		public ResortRepository(ApplicationDbContext db) : base(db) 
		{
			_db = db;
		}	

		public async Task<Resort> UpdateAsync(Resort entity)
		{
			entity.UpdatedDate = DateTime.Now;
			_db.Resorts.Update(entity);
			await _db.SaveChangesAsync();
			return entity;
		}
	}
}
