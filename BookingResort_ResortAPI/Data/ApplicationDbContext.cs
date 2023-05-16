using BookingResort_ResortAPI.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BookingResort_ResortAPI.Data
{
	public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
		{

		}
		public DbSet<ApplicationUser> ApplicationUsers { get; set; }
		public DbSet<Resort> Resorts { get; set; }
		public DbSet<ResortNumber> ResortNumbers { get; set; }
		public DbSet<LocalUser> LocalUsers { get; set; }
	}
}
