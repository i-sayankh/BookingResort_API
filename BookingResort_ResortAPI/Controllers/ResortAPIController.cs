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

		[HttpPost]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public ActionResult<ResortDTO> CreateResort([FromBody]ResortDTO resort)
		{
			if(resort == null)
			{
				return BadRequest(resort);
			}
			if(resort.Id > 0)
			{
				return StatusCode(StatusCodes.Status500InternalServerError);
			}
			resort.Id = ResortStore.resortList.OrderByDescending(u=>u.Id).FirstOrDefault().Id+1;
			ResortStore.resortList.Add(resort);

			return Ok(resort);
		}
	}
}
