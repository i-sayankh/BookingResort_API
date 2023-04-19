using System.ComponentModel.DataAnnotations;

namespace BookingResort_ResortAPI.Models.DTO
{
	public class ResortNumberUpdateDTO
	{
		[Required]
		public int ResortNo { get; set; }
		[Required]
		public int ResortId { get; set; }
		public string SpecialDetails { get; set; }
	}
}
