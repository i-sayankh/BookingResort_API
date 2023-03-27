using BookingResort_ResortAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookingResort_ResortAPI.Controllers
{
	[Route("api/ResortAPI")]
	[ApiController]
	public class ResortAPIController : ControllerBase
	{
		[HttpGet]
		public IEnumerable<Resort> GetResorts()
		{ 
			return new List<Resort> { 
				new Resort{ Id = 1, Name = "Pool View"},
				new Resort{ Id = 2, Name = "Beach View"}
			};
		}
	}
}
