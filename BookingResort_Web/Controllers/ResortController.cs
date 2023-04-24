using AutoMapper;
using BookingResort_Web.Models;
using BookingResort_Web.Models.DTO;
using BookingResort_Web.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace BookingResort_Web.Controllers
{
	public class ResortController : Controller
	{
		private readonly IResortService _resortService;
		private readonly IMapper _mapper;

		public ResortController(IResortService resortService, IMapper mapper)
		{
			_resortService = resortService;
			_mapper = mapper;
		}

		public async Task<IActionResult> IndexResort()
		{
			List<ResortDTO> list = new();
			var response = await _resortService.GetAllAsync<APIResponse>();
			if(response != null && response.IsSuccess)
			{
				list = JsonConvert.DeserializeObject<List<ResortDTO>>(Convert.ToString(response.Result));
			}
			return View(list);
		}

        public async Task<IActionResult> CreateResort()
        {            
            return View();
        }

		[HttpPost]
		[ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateResort(ResortCreateDTO model)
        {
            if(ModelState.IsValid)
			{
                var response = await _resortService.CreateAsync<APIResponse>(model);
                if (response != null && response.IsSuccess)
				{
                    return RedirectToAction(nameof(IndexResort));
                }
            }
            return View(model);
        }
    }
}
