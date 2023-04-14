using BookingResort_ResortAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BookingResort_ResortAPI.Data
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
		{

		}
		public DbSet<Resort> Resorts { get; set; }

		//protected override void OnModelCreating(ModelBuilder modelBuilder)
		//{
		//	modelBuilder.Entity<Resort>().HasData(
		//		new Resort
		//		{
		//			Id = 1,
		//			Name = "Royal Resort",
		//			Details = "Fusce 11 tincidunt maximus leo, sed scelerisque massa auctor sit amet. Donec ex mauris, hendrerit quis nibh ac, efficitur fringilla enim.",
		//			ImageURL = "https://dotnetmasteryimages.blob.core.windows.net/blueResortimages/Resort3.jpg",
		//			Occupancy = 4,
		//			Rate = 200,
		//			Sqft = 550,
		//			Amenity = "",
		//			CreatedDate = DateTime.Now
		//		},
		//	  new Resort
		//	  {
		//		  Id = 2,
		//		  Name = "Premium Pool Resort",
		//		  Details = "Fusce 11 tincidunt maximus leo, sed scelerisque massa auctor sit amet. Donec ex mauris, hendrerit quis nibh ac, efficitur fringilla enim.",
		//		  ImageURL = "https://dotnetmasteryimages.blob.core.windows.net/blueResortimages/Resort1.jpg",
		//		  Occupancy = 4,
		//		  Rate = 300,
		//		  Sqft = 550,
		//		  Amenity = "",
		//		  CreatedDate = DateTime.Now
		//	  },
		//	  new Resort
		//	  {
		//		  Id = 3,
		//		  Name = "Luxury Pool Resort",
		//		  Details = "Fusce 11 tincidunt maximus leo, sed scelerisque massa auctor sit amet. Donec ex mauris, hendrerit quis nibh ac, efficitur fringilla enim.",
		//		  ImageURL = "https://dotnetmasteryimages.blob.core.windows.net/blueResortimages/Resort4.jpg",
		//		  Occupancy = 4,
		//		  Rate = 400,
		//		  Sqft = 750,
		//		  Amenity = "",
		//		  CreatedDate = DateTime.Now
		//	  },
		//	  new Resort
		//	  {
		//		  Id = 4,
		//		  Name = "Diamond Resort",
		//		  Details = "Fusce 11 tincidunt maximus leo, sed scelerisque massa auctor sit amet. Donec ex mauris, hendrerit quis nibh ac, efficitur fringilla enim.",
		//		  ImageURL = "https://dotnetmasteryimages.blob.core.windows.net/blueResortimages/Resort5.jpg",
		//		  Occupancy = 4,
		//		  Rate = 550,
		//		  Sqft = 900,
		//		  Amenity = "",
		//		  CreatedDate = DateTime.Now
		//	  },
		//	  new Resort
		//	  {
		//		  Id = 5,
		//		  Name = "Diamond Pool Resort",
		//		  Details = "Fusce 11 tincidunt maximus leo, sed scelerisque massa auctor sit amet. Donec ex mauris, hendrerit quis nibh ac, efficitur fringilla enim.",
		//		  ImageURL = "https://dotnetmasteryimages.blob.core.windows.net/blueResortimages/Resort2.jpg",
		//		  Occupancy = 4,
		//		  Rate = 600,
		//		  Sqft = 1100,
		//		  Amenity = "",
		//		  CreatedDate = DateTime.Now
		//	  });
		//}
	}
}
