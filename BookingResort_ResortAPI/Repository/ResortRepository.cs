using BookingResort_ResortAPI.Data;
using BookingResort_ResortAPI.Models;
using BookingResort_ResortAPI.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace BookingResort_ResortAPI.Repository
{
	public class ResortRepository : IResortRepository
	{
		private readonly ApplicationDbContext _db;
		public ResortRepository(ApplicationDbContext db)
		{
			_db = db;
		}
		public async Task CreateAsync(Resort entity)
		{
			await _db.Resorts.AddAsync(entity);
			await SaveAsync();
		}

		public async Task<Resort> GetAsync(Expression<Func<Resort, bool>> filter = null, bool tracked = true)
		{
			IQueryable<Resort> query = _db.Resorts;
			if(!tracked)
			{
				query = query.AsNoTracking();
			}
			if (filter != null)
			{
				query = query.Where(filter);
			}
			return await query.FirstOrDefaultAsync();
		}

		public async Task<List<Resort>> GetAllAsync(Expression<Func<Resort, bool>> filter = null)
		{
			IQueryable<Resort> query = _db.Resorts;
			if(filter != null)
			{
				query = query.Where(filter);
			}
			return await query.ToListAsync();
		}

		public async Task RemoveAsync(Resort entity)
		{
			_db.Resorts.Remove(entity);
			await SaveAsync();
		}

		public async Task SaveAsync()
		{
			await _db.SaveChangesAsync();
		}

		public async Task UpdateAsync(Resort entity)
		{
			_db.Resorts.Update(entity);
			await SaveAsync();
		}
	}
}
