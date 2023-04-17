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
		public async Task Create(Resort entity)
		{
			await _db.Resorts.AddAsync(entity);
			await Save();
		}

		public async Task<Resort> Get(Expression<Func<Resort, bool>> filter = null, bool tracked = true)
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

		public async Task<List<Resort>> GetAll(Expression<Func<Resort, bool>> filter = null)
		{
			IQueryable<Resort> query = _db.Resorts;
			if(filter != null)
			{
				query = query.Where(filter);
			}
			return await query.ToListAsync();
		}

		public async Task Remove(Resort entity)
		{
			_db.Resorts.Remove(entity);
			await Save();
		}

		public async Task Save()
		{
			await _db.SaveChangesAsync();
		}
	}
}
