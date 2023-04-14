using System.ComponentModel.DataAnnotations;

namespace BookingResort_ResortAPI.Models.DTO
{
	public class ResortUpdateDTO
	{
		[Required]
		public int Id { get; set; }
		[Required]
		[MaxLength(30)]
		public string Name { get; set; }
		public string Details { get; set; }
		[Required]
		public double Rate { get; set; }
		[Required]
		public int Occupancy { get; set; }
		[Required]
		public int Sqft { get; set; }
		[Required]
		public string ImageURL { get; set; }
		[Required]
		public string Amenity { get; set; }
	}
}
