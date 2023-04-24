using System.ComponentModel.DataAnnotations;

namespace BookingResort_Web.Models.DTO
{
	public class ResortCreateDTO
	{
		[Required]
		[MaxLength(30)]
		public string Name { get; set; }
		public string? Details { get; set; }
		[Required]
		public double Rate { get; set; }
		public int Occupancy { get; set; }
		public int Sqft { get; set; }
		public string? ImageURL { get; set; }
		public string? Amenity { get; set; }
	}
}
