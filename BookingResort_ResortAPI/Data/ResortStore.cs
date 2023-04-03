using BookingResort_ResortAPI.Models.DTO;

namespace BookingResort_ResortAPI.Data
{
	public static class ResortStore
	{
		public static List<ResortDTO> resortList = new List<ResortDTO> {
				new ResortDTO{ Id = 1, Name = "Pool View", Occupancy=3, Sqft=1000},
				new ResortDTO{ Id = 2, Name = "Beach View", Occupancy=3, Sqft=1000}
			};
	}
}
