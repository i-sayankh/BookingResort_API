using AutoMapper;
using BookingResort_ResortAPI.Data;
using BookingResort_ResortAPI.Models;
using BookingResort_ResortAPI.Models.DTO;
using BookingResort_ResortAPI.Repository.IRepository;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookingResort_ResortAPI.Controllers
{
	[Route("api/ResortAPI")]
	[ApiController]
	public class ResortAPIController : ControllerBase
	{
		private readonly IResortRepository _dbResort;
		private readonly IMapper _mapper;
		public ResortAPIController(IResortRepository dbResort, IMapper mapper)
		{
			_dbResort = dbResort;
			_mapper = mapper;
		}

		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult<IEnumerable<ResortDTO>>> GetResorts()
		{
			IEnumerable<Resort> resortList = await _dbResort.GetAllAsync();
			return Ok(_mapper.Map<List<ResortDTO>>(resortList));
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
			var resort = await _dbResort.GetAsync(u => u.Id == id);
			if (resort == null)
			{
				return NotFound();
			}
			return Ok(_mapper.Map<ResortDTO>(resort));
		}

		[HttpPost]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<ActionResult<ResortDTO>> CreateResort([FromBody] ResortCreateDTO createDTO)
		{
			if (await _dbResort.GetAsync(u => u.Name.ToLower() == createDTO.Name.ToLower()) != null)
			{
				ModelState.AddModelError("customError", "Resort Already Exists!!");
				return BadRequest(ModelState);
			}
			if (createDTO == null)
			{
				return BadRequest(createDTO);
			}
			//if (resortDTO.Id > 0)
			//{
			//	return StatusCode(StatusCodes.Status500InternalServerError);
			//}

			Resort model = _mapper.Map<Resort>(createDTO);
			
			//Resort model = new Resort()
			//{
			//	Amenity = createDTO.Amenity,
			//	Details = createDTO.Details,
			//	ImageURL = createDTO.ImageURL,
			//	Name = createDTO.Name,
			//	Occupancy = createDTO.Occupancy,
			//	Rate = createDTO.Rate,
			//	Sqft = createDTO.Sqft
			//};

			await _dbResort.CreateAsync(model);
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
			var resort = await _dbResort.GetAsync(u => u.Id == id);
			if (resort == null)
			{
				return NotFound();
			}
			await _dbResort.RemoveAsync(resort);
			return NoContent();
		}

		[HttpPut("{id:int}", Name = "UpdateResort")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<IActionResult> UpdateResort(int id, [FromBody] ResortUpdateDTO updateDTO)
		{
			if (updateDTO == null || id != updateDTO.Id)
			{
				return BadRequest();
			}

			Resort model = _mapper.Map<Resort>(updateDTO);
			
			await _dbResort.UpdateAsync(model);
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

			var resort = await _dbResort.GetAsync(u => u.Id == id, tracked:false);
			ResortUpdateDTO resortDTO = _mapper.Map<ResortUpdateDTO>(resort);			

			if (resort == null)
			{
				return BadRequest();
			}

			patchDTO.ApplyTo(resortDTO, ModelState);
			Resort model = _mapper.Map<Resort>(resortDTO);
			await _dbResort.UpdateAsync(model);

			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			return NoContent();
		}
	}
}
