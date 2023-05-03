﻿using BookingResort_ResortAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BookingResort_ResortAPI.Data
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
		{

		}
		public DbSet<Resort> Resorts { get; set; }
		public DbSet<ResortNumber> ResortNumbers { get; set; }
		public DbSet<LocalUser> LocalUsers { get; set; }
	}
}
