using System.ComponentModel.DataAnnotations;

namespace BookingResort_ResortAPI.Models.DTO
{
	public class ResortDTO
	{
		public int Id { get; set; }
		[Required]
		[MaxLength(30)]
		public string Name { get; set; }
	}
}
