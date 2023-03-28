using BookingResort_ResortAPI.Data;
using BookingResort_ResortAPI.Models;
using BookingResort_ResortAPI.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace BookingResort_ResortAPI.Controllers
{
	[Route("api/ResortAPI")]
	[ApiController]
	public class ResortAPIController : ControllerBase
	{
		[HttpGet]
		public ActionResult<IEnumerable<ResortDTO>> GetResorts()
		{
			return Ok(ResortStore.resortList);
		}

		[HttpGet("id:int")]
		public ActionResult<ResortDTO> GetResort(int id)
		{
			if(id==0)
			{
				return BadRequest();
			}
			var resort = ResortStore.resortList.FirstOrDefault(u => u.Id == id);
			if (resort == null)
			{
				return NotFound();
			}
			return Ok(resort);
		}
	}
}
