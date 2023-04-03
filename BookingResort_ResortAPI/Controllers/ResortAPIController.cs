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

		[HttpGet("{id:int}", Name ="GetResort")]
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
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public ActionResult<ResortDTO> CreateResort([FromBody]ResortDTO resortDTO)
		{
			if (ResortStore.resortList.FirstOrDefault(u => u.Name.ToLower() == resortDTO.Name.ToLower())!=null) 
			{
				ModelState.AddModelError("customError", "Resort Already Exists!!");
				return BadRequest(ModelState);
			}
			if(resortDTO == null)
			{
				return BadRequest(resortDTO);
			}
			if(resortDTO.Id > 0)
			{
				return StatusCode(StatusCodes.Status500InternalServerError);
			}
			resortDTO.Id = ResortStore.resortList.OrderByDescending(u=>u.Id).FirstOrDefault().Id+1;
			ResortStore.resortList.Add(resortDTO);

			return CreatedAtRoute("GetResort", new { id = resortDTO.Id }, resortDTO);
		}

		[HttpDelete("{id:int}", Name ="DeleteResort")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public IActionResult DeleteResort(int id)
		{
			if (id == 0)
			{
				return BadRequest();
			}
			var resort = ResortStore.resortList.FirstOrDefault(u => u.Id == id);
			if(resort == null)
			{
				return NotFound();
			}
			ResortStore.resortList.Remove(resort);
			return NoContent();
		}

		[HttpPut("{id:int}", Name = "UpdateResort")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public IActionResult UpdateResort(int id, [FromBody]ResortDTO resortDTO)
		{
			if(resortDTO == null || id != resortDTO.Id)
			{
				return BadRequest();
			}
			var resort = ResortStore.resortList.FirstOrDefault(u => u.Id == id);
			resort.Name = resortDTO.Name;
			resort.Sqft = resortDTO.Sqft;
			resort.Occupancy = resortDTO.Occupancy;
			return NoContent();
		}
	}
}
