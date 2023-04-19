using static BookingResort_Utility.SD;

namespace BookingResort_Web.Models
{
	public class APIRequest
	{
		public ApiType ApiType { get; set; } = ApiType.GET; 
		public string Url { get; set; }
		public Object Data { get; set; }
	}
}
