using System.ComponentModel.DataAnnotations;

namespace BookingResort_ResortAPI.Models.DTO
{
	public class ResortNumberUpdateDTO
	{
		[Required]
		public int ResortNo { get; set; }
		public string SpecialDetails { get; set; }
	}
}
