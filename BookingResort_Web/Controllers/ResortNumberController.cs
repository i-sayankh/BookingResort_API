using AutoMapper;
using BookingResort_Web.Models;
using BookingResort_Web.Models.DTO;
using BookingResort_Web.Services;
using BookingResort_Web.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BookingResort_Web.Controllers
{
    public class ResortNumberController : Controller
    {
        private readonly IResortNumberService _resortNumberService;
        private readonly IMapper _mapper;

        public ResortNumberController(IResortNumberService resortNumberService, IMapper mapper)
        {
            _resortNumberService = resortNumberService;
            _mapper = mapper;
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
    }
}
