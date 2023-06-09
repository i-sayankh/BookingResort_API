﻿using AutoMapper;
using BookingResort_Utility;
using BookingResort_Web.Models;
using BookingResort_Web.Models.DTO;
using BookingResort_Web.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Reflection;

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
			var response = await _resortService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
			if(response != null && response.IsSuccess)
			{
				list = JsonConvert.DeserializeObject<List<ResortDTO>>(Convert.ToString(response.Result));
			}
			return View(list);
		}

        [Authorize(Roles ="admin")]
        public async Task<IActionResult> CreateResort()
        {            
            return View();
        }

		[HttpPost]
		[ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> CreateResort(ResortCreateDTO model)
        {
            if(ModelState.IsValid)
			{
                var response = await _resortService.CreateAsync<APIResponse>(model, HttpContext.Session.GetString(SD.SessionToken));
                if (response != null && response.IsSuccess)
				{
                    TempData["success"] = "Resort Created Successfully";
                    return RedirectToAction(nameof(IndexResort));
                }
            }
            TempData["error"] = "Error Encountered";
            return View(model);
        }

        [Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdateResort(int resortId)
        {
            var response = await _resortService.GetAsync<APIResponse>(resortId, HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                ResortDTO model = JsonConvert.DeserializeObject<ResortDTO>(Convert.ToString(response.Result));
                return View(_mapper.Map<ResortUpdateDTO>(model));
            }
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdateResort(ResortUpdateDTO model)
        {
            if (ModelState.IsValid)
            {
                var response = await _resortService.UpdateAsync<APIResponse>(model, HttpContext.Session.GetString(SD.SessionToken));
                if (response != null && response.IsSuccess)
                {
                    TempData["success"] = "Resort Updated Successfully";
                    return RedirectToAction(nameof(IndexResort));
                }
            }
            TempData["error"] = "Error Encountered";
            return View(model);
        }

        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteResort(int resortId)
        {
            var response = await _resortService.GetAsync<APIResponse>(resortId, HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                ResortDTO model = JsonConvert.DeserializeObject<ResortDTO>(Convert.ToString(response.Result));
                return View(model);
            }
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteResort(ResortDTO model)
        {
            var response = await _resortService.DeleteAsync<APIResponse>(model.Id, HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                TempData["success"] = "Resort Deleted Successfully";
                return RedirectToAction(nameof(IndexResort));
            }
            TempData["error"] = "Error Encountered";
            return View(model);
        }
    }
}
