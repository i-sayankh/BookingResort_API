using AutoMapper;
using BookingResort_ResortAPI.Data;
using BookingResort_ResortAPI.Models;
using BookingResort_ResortAPI.Models.DTO;
using BookingResort_ResortAPI.Repository.IRepository;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace BookingResort_ResortAPI.Controllers
{
	[Route("api/ResortNumberAPI")]
	[ApiController]
	public class ResortNumberAPIController : ControllerBase
	{
		protected APIResponse _response;
		private readonly IResortNumberRepository _dbResortNumber;
		private readonly IMapper _mapper;
		public ResortNumberAPIController(IResortNumberRepository dbResortNumber, IMapper mapper)
		{
			_dbResortNumber = dbResortNumber;
			_mapper = mapper;
			this._response= new();
		}

		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult<APIResponse>> GetResortNumbers()
		{
			try
			{
				IEnumerable<ResortNumber> resortNumberList = await _dbResortNumber.GetAllAsync();
				_response.Result = _mapper.Map<List<ResortNumberDTO>>(resortNumberList);
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

		[HttpGet("{id:int}", Name = "GetResortNumber")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]		
		public async Task<ActionResult<APIResponse>> GetResortNumber(int id)
		{
			try
			{
				if (id == 0)
				{
					return BadRequest();
				}
				var resortNumber = await _dbResortNumber.GetAsync(u => u.ResortNo == id);
				if (resortNumber == null)
				{
					return NotFound();
				}
				_response.Result = _mapper.Map<ResortNumberDTO>(resortNumber);
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
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<ActionResult<APIResponse>> CreateResortNumber([FromBody] ResortNumberCreateDTO createDTO)
		{
			try
			{
				if (await _dbResortNumber.GetAsync(u => u.ResortNo == createDTO.ResortNo) != null)
				{
					ModelState.AddModelError("customError", "Resort Number Already Exists!!");
					return BadRequest(ModelState);
				}
				if (createDTO == null)
				{
					return BadRequest(createDTO);
				}

				ResortNumber resortNumber = _mapper.Map<ResortNumber>(createDTO);

				await _dbResortNumber.CreateAsync(resortNumber);
				_response.Result = _mapper.Map<ResortNumberDTO>(resortNumber);
				_response.StatusCode = HttpStatusCode.Created;
				return CreatedAtRoute("GetResort", new { id = resortNumber.ResortNo }, _response);
			}
			catch (Exception ex)
			{
				_response.IsSuccess = false;
				_response.ErrorMessages = new List<string> { ex.Message };
			}
			return _response;
		}

		[HttpDelete("{id:int}", Name = "DeleteResortNumber")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult<APIResponse>> DeleteResortNumber(int id)
		{
			try
			{
				if (id == 0)
				{
					_response.StatusCode = HttpStatusCode.BadRequest;
					return BadRequest(_response);
				}
				var resortNumber = await _dbResortNumber.GetAsync(u => u.ResortNo == id);
				if (resortNumber == null)
				{
					_response.StatusCode = HttpStatusCode.NotFound;
					return NotFound(_response);
				}
				await _dbResortNumber.RemoveAsync(resortNumber);
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

		[HttpPut("{id:int}", Name = "UpdateResortNumber")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<ActionResult<APIResponse>> UpdateResortNumber(int id, [FromBody] ResortNumberUpdateDTO updateDTO)
		{
			try
			{
				if (updateDTO == null || id != updateDTO.ResortNo)
				{
					_response.StatusCode=HttpStatusCode.BadRequest;
					return BadRequest(_response);
				}

				ResortNumber resortNumber = _mapper.Map<ResortNumber>(updateDTO);

				await _dbResortNumber.UpdateAsync(resortNumber);
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
	}
}
