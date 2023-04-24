using System.ComponentModel.DataAnnotations;

namespace BookingResort_ResortAPI.Models.DTO
{
	public class ResortNumberDTO
	{
		[Required]
		public int ResortNo { get; set; }
		[Required]
		public int ResortId { get; set; }
		public string SpecialDetails { get; set; }
		public ResortDTO Resort { get; set; }
	}
}
