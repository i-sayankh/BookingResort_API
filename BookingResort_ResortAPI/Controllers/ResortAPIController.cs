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
		public async Task<ActionResult<IEnumerable<ResortDTO>>> GetResorts()
		{
			return Ok(await _db.Resorts.ToListAsync());
		}

		[HttpGet("{id:int}", Name = "GetResort")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		//[ProducesResponseType(200, Type = typeof(ResortDTO))]		
		public async Task<ActionResult<ResortDTO>> GetResort(int id)
		{
			if (id == 0)
			{
				return BadRequest();
			}
			var resort = await _db.Resorts.FirstOrDefaultAsync(u => u.Id == id);
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
		public async Task<ActionResult<ResortDTO>> CreateResort([FromBody] ResortCreateDTO resortDTO)
		{
			if (await _db.Resorts.FirstOrDefaultAsync(u => u.Name.ToLower() == resortDTO.Name.ToLower()) != null)
			{
				ModelState.AddModelError("customError", "Resort Already Exists!!");
				return BadRequest(ModelState);
			}
			if (resortDTO == null)
			{
				return BadRequest(resortDTO);
			}
			//if (resortDTO.Id > 0)
			//{
			//	return StatusCode(StatusCodes.Status500InternalServerError);
			//}
			Resort model = new Resort()
			{
				Amenity = resortDTO.Amenity,
				Details = resortDTO.Details,
				ImageURL = resortDTO.ImageURL,
				Name = resortDTO.Name,
				Occupancy = resortDTO.Occupancy,
				Rate = resortDTO.Rate,
				Sqft = resortDTO.Sqft
			};
			await _db.Resorts.AddAsync(model);
			await _db.SaveChangesAsync();

			return CreatedAtRoute("GetResort", new { id = model.Id }, model);
		}

		[HttpDelete("{id:int}", Name = "DeleteResort")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<IActionResult> DeleteResort(int id)
		{
			if (id == 0)
			{
				return BadRequest();
			}
			var resort = await _db.Resorts.FirstOrDefaultAsync(u => u.Id == id);
			if (resort == null)
			{
				return NotFound();
			}
			_db.Resorts.Remove(resort);
			await _db.SaveChangesAsync();
			return NoContent();
		}

		[HttpPut("{id:int}", Name = "UpdateResort")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<IActionResult> UpdateResort(int id, [FromBody] ResortUpdateDTO resortDTO)
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
			await _db.SaveChangesAsync();
			return NoContent();
		}

		[HttpPatch("{id:int}", Name = "UpdatePartialResort")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<IActionResult> UpdatePartialResort(int id, JsonPatchDocument<ResortUpdateDTO> patchDTO)
		{
			if (patchDTO == null || id == 0)
			{
				return BadRequest();
			}
			var resort = await _db.Resorts.AsNoTracking().FirstOrDefaultAsync(u => u.Id == id);

			ResortUpdateDTO resortDTO = new()
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
			await _db.SaveChangesAsync();

			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			return NoContent();
		}
	}
}
