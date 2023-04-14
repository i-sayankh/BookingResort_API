using BookingResort_ResortAPI.Data;
using BookingResort_ResortAPI.Models;
using BookingResort_ResortAPI.Models.DTO;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookingResort_ResortAPI.Controllers
{
	[Route("api/ResortAPI")]
	[ApiController]
	public class ResortAPIController : ControllerBase
	{
		private readonly ApplicationDbContext _db;
		public ResortAPIController(ApplicationDbContext db)
		{
			_db = db;
		}

		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public ActionResult<IEnumerable<ResortDTO>> GetResorts()
		{
			return Ok(_db.Resorts.ToList());
		}

		[HttpGet("{id:int}", Name = "GetResort")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		//[ProducesResponseType(200, Type = typeof(ResortDTO))]		
		public ActionResult<ResortDTO> GetResort(int id)
		{
			if (id == 0)
			{
				return BadRequest();
			}
			var resort = _db.Resorts.FirstOrDefault(u => u.Id == id);
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
		public ActionResult<ResortDTO> CreateResort([FromBody] ResortDTO resortDTO)
		{
			if (_db.Resorts.FirstOrDefault(u => u.Name.ToLower() == resortDTO.Name.ToLower()) != null)
			{
				ModelState.AddModelError("customError", "Resort Already Exists!!");
				return BadRequest(ModelState);
			}
			if (resortDTO == null)
			{
				return BadRequest(resortDTO);
			}
			if (resortDTO.Id > 0)
			{
				return StatusCode(StatusCodes.Status500InternalServerError);
			}
			Resort model = new Resort()
			{
				Amenity = resortDTO.Amenity,
				Details = resortDTO.Details,
				Id = resortDTO.Id,
				ImageURL = resortDTO.ImageURL,
				Name = resortDTO.Name,
				Occupancy = resortDTO.Occupancy,
				Rate = resortDTO.Rate,
				Sqft = resortDTO.Sqft
			};
			_db.Resorts.Add(model);
			_db.SaveChanges();

			return CreatedAtRoute("GetResort", new { id = resortDTO.Id }, resortDTO);
		}

		[HttpDelete("{id:int}", Name = "DeleteResort")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public IActionResult DeleteResort(int id)
		{
			if (id == 0)
			{
				return BadRequest();
			}
			var resort = _db.Resorts.FirstOrDefault(u => u.Id == id);
			if (resort == null)
			{
				return NotFound();
			}
			_db.Resorts.Remove(resort);
			_db.SaveChanges();
			return NoContent();
		}

		[HttpPut("{id:int}", Name = "UpdateResort")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public IActionResult UpdateResort(int id, [FromBody] ResortDTO resortDTO)
		{
			if (resortDTO == null || id != resortDTO.Id)
			{
				return BadRequest();
			}
			//var resort = _db.Resorts.FirstOrDefault(u => u.Id == id);
			//resort.Name = resortDTO.Name;
			//resort.Sqft = resortDTO.Sqft;
			//resort.Occupancy = resortDTO.Occupancy;

			Resort model = new Resort()
			{
				Amenity = resortDTO.Amenity,
				Details = resortDTO.Details,
				Id = resortDTO.Id,
				ImageURL = resortDTO.ImageURL,
				Name = resortDTO.Name,
				Occupancy = resortDTO.Occupancy,
				Rate = resortDTO.Rate,
				Sqft = resortDTO.Sqft
			};
			_db.Resorts.Update(model);
			_db.SaveChanges();
			return NoContent();
		}

		[HttpPatch("{id:int}", Name = "UpdatePartialResort")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public IActionResult UpdatePartialResort(int id, JsonPatchDocument<ResortDTO> patchDTO)
		{
			if (patchDTO == null || id == 0)
			{
				return BadRequest();
			}
			var resort = _db.Resorts.AsNoTracking().FirstOrDefault(u => u.Id == id);

			ResortDTO resortDTO = new ResortDTO()
			{
				Amenity= resort.Amenity,
				Details = resort.Details,
				Id = resort.Id,
				ImageURL = resort.ImageURL,
				Name = resort.Name,
				Occupancy= resort.Occupancy,
				Rate= resort.Rate,
				Sqft= resort.Sqft
			};

			if (resort == null)
			{
				return BadRequest();
			}
			patchDTO.ApplyTo(resortDTO, ModelState);

			Resort model = new Resort()
			{
				Amenity = resortDTO.Amenity,
				Details = resortDTO.Details,
				Id = resortDTO.Id,
				ImageURL = resortDTO.ImageURL,
				Name = resortDTO.Name,
				Occupancy = resortDTO.Occupancy,
				Rate = resortDTO.Rate,
				Sqft = resortDTO.Sqft
			};
			_db.Resorts.Update(model);
			_db.SaveChanges();

			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			return NoContent();
		}
	}
}
