using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookingResort_ResortAPI.Models
{
	public class ResortNumber
	{
		[Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
		public int ResortNo { get; set; }
		public string SpecialDetails { get; set; }
		public DateTime Createddate { get; set; }
		public DateTime Updateddate { get; set;}
	}
}
