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
		[ProducesResponseType(StatusCodes.Status200OK)]
		public ActionResult<IEnumerable<ResortDTO>> GetResorts()
		{
			return Ok(ResortStore.resortList);
		}

		[HttpGet("id:int")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		//[ProducesResponseType(200, Type = typeof(ResortDTO))]		
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
