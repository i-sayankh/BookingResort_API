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
                resortNumberVM.ResortList = JsonConvert.DeserializeObject<List<ResortDTO>>
                    (Convert.ToString(response.Result)).Select(i=>new SelectListItem
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
                else
                {
                    if(response.ErrorMessages.Count > 0)
                    {
                        ModelState.AddModelError("ErrorMessages", response.ErrorMessages.FirstOrDefault());
                    }
                }
            }

            var resp = await _resortService.GetAllAsync<APIResponse>();
            if (resp != null && resp.IsSuccess)
            {
                model.ResortList = JsonConvert.DeserializeObject<List<ResortDTO>>
                    (Convert.ToString(resp.Result)).Select(i => new SelectListItem
                    {
                        Text = i.Name,
                        Value = i.Id.ToString(),
                    });
            }
            return View(model);
        }

        public async Task<IActionResult> UpdateResortNumber(int resortNo)
        {
            ResortNumberUpdateVM resortNumberVM = new();
            var response = await _resortNumberService.GetAsync<APIResponse>(resortNo);
            if (response != null && response.IsSuccess)
            {
                ResortNumberDTO model = JsonConvert.DeserializeObject<ResortNumberDTO>(Convert.ToString(response.Result));
                resortNumberVM.ResortNumber = _mapper.Map<ResortNumberUpdateDTO>(model);
            }

            response = await _resortService.GetAllAsync<APIResponse>();
            if (response != null && response.IsSuccess)
            {
                resortNumberVM.ResortList = JsonConvert.DeserializeObject<List<ResortDTO>>
                    (Convert.ToString(response.Result)).Select(i => new SelectListItem
                    {
                        Text = i.Name,
                        Value = i.Id.ToString(),
                    });
                return View(resortNumberVM);
            }

            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateResortNumber(ResortNumberUpdateVM model)
        {
            if (ModelState.IsValid)
            {
                var response = await _resortNumberService.UpdateAsync<APIResponse>(model.ResortNumber);
                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(IndexResortNumber));
                }
                else
                {
                    if (response.ErrorMessages.Count > 0)
                    {
                        ModelState.AddModelError("ErrorMessages", response.ErrorMessages.FirstOrDefault());
                    }
                }
            }

            var resp = await _resortService.GetAllAsync<APIResponse>();
            if (resp != null && resp.IsSuccess)
            {
                model.ResortList = JsonConvert.DeserializeObject<List<ResortDTO>>
                    (Convert.ToString(resp.Result)).Select(i => new SelectListItem
                    {
                        Text = i.Name,
                        Value = i.Id.ToString(),
                    });
            }
            return View(model);
        }

        public async Task<IActionResult> DeleteResortNumber(int resortNo)
        {
            ResortNumberDeleteVM resortNumberVM = new();
            var response = await _resortNumberService.GetAsync<APIResponse>(resortNo);
            if (response != null && response.IsSuccess)
            {
                ResortNumberDTO model = JsonConvert.DeserializeObject<ResortNumberDTO>(Convert.ToString(response.Result));
                resortNumberVM.ResortNumber = model;
            }

            response = await _resortService.GetAllAsync<APIResponse>();
            if (response != null && response.IsSuccess)
            {
                resortNumberVM.ResortList = JsonConvert.DeserializeObject<List<ResortDTO>>
                    (Convert.ToString(response.Result)).Select(i => new SelectListItem
                    {
                        Text = i.Name,
                        Value = i.Id.ToString(),
                    });
                return View(resortNumberVM);
            }

            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteResortNumber(ResortNumberDeleteVM model)
        {
            var response = await _resortNumberService.DeleteAsync<APIResponse>(model.ResortNumber.ResortNo);
            if (response != null && response.IsSuccess)
            {
                return RedirectToAction(nameof(IndexResortNumber));
            }
            return View(model);
        }
    }
}
