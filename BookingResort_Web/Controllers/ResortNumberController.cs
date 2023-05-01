using AutoMapper;
using BookingResort_Web.Models;
using BookingResort_Web.Models.DTO;
using BookingResort_Web.Models.VM;
using BookingResort_Web.Services;
using BookingResort_Web.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace BookingResort_Web.Controllers
{
    public class ResortNumberController : Controller
    {
        private readonly IResortNumberService _resortNumberService;
        private readonly IResortService _resortService;
        private readonly IMapper _mapper;

        public ResortNumberController(IResortNumberService resortNumberService, IMapper mapper, IResortService resortService)
        {
            _resortNumberService = resortNumberService;
            _mapper = mapper;
            _resortService = resortService;
        }

        public async Task<IActionResult> IndexResortNumber()
        {
            List<ResortNumberDTO> list = new();
            var response = await _resortNumberService.GetAllAsync<APIResponse>();
            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<ResortNumberDTO>>(Convert.ToString(response.Result));
            }
            return View(list);
        }

        public async Task<IActionResult> CreateResortNumber()
        {
            ResortNumberCreateVM resortNumberVM = new();
            var response = await _resortService.GetAllAsync<APIResponse>();
            if (response != null && response.IsSuccess)
            {
                resortNumberVM.ResortList = JsonConvert.DeserializeObject<List<ResortDTO>>(Convert.ToString(response.Result)).Select(i=>new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString(),
                });
            }
            return View(resortNumberVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateResortNumber(ResortNumberCreateVM model)
        {
            if (ModelState.IsValid)
            {
                var response = await _resortNumberService.CreateAsync<APIResponse>(model.ResortNumber);
                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(IndexResortNumber));
                }
            }
            return View(model);
        }

        //public async Task<IActionResult> UpdateResort(int resortId)
        //{
        //    var response = await _resortService.GetAsync<APIResponse>(resortId);
        //    if (response != null && response.IsSuccess)
        //    {
        //        ResortDTO model = JsonConvert.DeserializeObject<ResortDTO>(Convert.ToString(response.Result));
        //        return View(_mapper.Map<ResortUpdateDTO>(model));
        //    }
        //    return NotFound();
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> UpdateResort(ResortUpdateDTO model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var response = await _resortService.UpdateAsync<APIResponse>(model);
        //        if (response != null && response.IsSuccess)
        //        {
        //            return RedirectToAction(nameof(IndexResort));
        //        }
        //    }
        //    return View(model);
        //}

        //public async Task<IActionResult> DeleteResort(int resortId)
        //{
        //    var response = await _resortService.GetAsync<APIResponse>(resortId);
        //    if (response != null && response.IsSuccess)
        //    {
        //        ResortDTO model = JsonConvert.DeserializeObject<ResortDTO>(Convert.ToString(response.Result));
        //        return View(model);
        //    }
        //    return NotFound();
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteResort(ResortDTO model)
        //{
        //    var response = await _resortService.DeleteAsync<APIResponse>(model.Id);
        //    if (response != null && response.IsSuccess)
        //    {
        //        return RedirectToAction(nameof(IndexResort));
        //    }
        //    return View(model);
        //}
    }
}
