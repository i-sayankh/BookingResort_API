using AutoMapper;
using BookingResort_ResortAPI.Data;
using BookingResort_ResortAPI.Models;
using BookingResort_ResortAPI.Models.DTO;
using BookingResort_ResortAPI.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace BookingResort_ResortAPI.Controllers
{
	[Route("api/ResortAPI")]
	[ApiController]
	public class ResortAPIController : ControllerBase
	{
		protected APIResponse _response;
		private readonly IResortRepository _dbResort;
		private readonly IMapper _mapper;
		public ResortAPIController(IResortRepository dbResort, IMapper mapper)
		{
			_dbResort = dbResort;
			_mapper = mapper;
			this._response= new();
		}

		[HttpGet]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult<APIResponse>> GetResorts()
		{
			try
			{
				IEnumerable<Resort> resortList = await _dbResort.GetAllAsync();
				_response.Result = _mapper.Map<List<ResortDTO>>(resortList);
				_response.StatusCode = HttpStatusCode.OK;
				return Ok(_response);
			}
			catch (Exception ex)
			{
				_response.IsSuccess= false;
				_response.ErrorMessages = new List<string> { ex.Message };
			}
			return _response;
		}

		[HttpGet("{id:int}", Name = "GetResort")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		//[ProducesResponseType(200, Type = typeof(ResortDTO))]		
		public async Task<ActionResult<APIResponse>> GetResort(int id)
		{
			try
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
				_response.Result = _mapper.Map<ResortDTO>(resort);
				_response.StatusCode = HttpStatusCode.OK;
				return Ok(_response);
			}
			catch (Exception ex)
			{
				_response.IsSuccess = false;
				_response.ErrorMessages = new List<string> { ex.Message };
			}
			return _response;

		}

		[HttpPost]
        [Authorize(Roles = "admin")]
        [ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<ActionResult<APIResponse>> CreateResort([FromBody] ResortCreateDTO createDTO)
		{
			try
			{
				if (await _dbResort.GetAsync(u => u.Name.ToLower() == createDTO.Name.ToLower()) != null)
				{
					ModelState.AddModelError("ErrorMessages", "Resort Already Exists!!");
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

				Resort resort = _mapper.Map<Resort>(createDTO);

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

				await _dbResort.CreateAsync(resort);
				_response.Result = _mapper.Map<ResortDTO>(resort);
				_response.StatusCode = HttpStatusCode.Created;
				return CreatedAtRoute("GetResort", new { id = resort.Id }, _response);
			}
			catch (Exception ex)
			{
				_response.IsSuccess = false;
				_response.ErrorMessages = new List<string> { ex.Message };
			}
			return _response;
		}

		[HttpDelete("{id:int}", Name = "DeleteResort")]
        [Authorize(Roles = "admin")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult<APIResponse>> DeleteResort(int id)
		{
			try
			{
				if (id == 0)
				{
					_response.StatusCode = HttpStatusCode.BadRequest;
					return BadRequest(_response);
				}
				var resort = await _dbResort.GetAsync(u => u.Id == id);
				if (resort == null)
				{
					_response.StatusCode = HttpStatusCode.NotFound;
					return NotFound(_response);
				}
				await _dbResort.RemoveAsync(resort);
				_response.StatusCode = HttpStatusCode.NoContent;
				_response.IsSuccess = true;
				return Ok(_response);
			}
			catch (Exception ex)
			{
				_response.IsSuccess = false;
				_response.ErrorMessages = new List<string> { ex.Message };
			}
			return _response;

		}

		[HttpPut("{id:int}", Name = "UpdateResort")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<APIResponse>> UpdateResort(int id, [FromBody] ResortUpdateDTO updateDTO)
		{
			try
			{
				if (updateDTO == null || id != updateDTO.Id)
				{
					_response.StatusCode=HttpStatusCode.BadRequest;
					return BadRequest(_response);
				}

				Resort model = _mapper.Map<Resort>(updateDTO);

				await _dbResort.UpdateAsync(model);
				_response.StatusCode = HttpStatusCode.NoContent;
				_response.IsSuccess = true;
				return Ok(_response);
			}
			catch (Exception ex)
			{
				_response.IsSuccess = false;
				_response.ErrorMessages = new List<string> { ex.Message };
			}
			return _response;
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
