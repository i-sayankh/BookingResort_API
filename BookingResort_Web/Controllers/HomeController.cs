using AutoMapper;
using BookingResort_Utility;
using BookingResort_Web.Models;
using BookingResort_Web.Models.DTO;
using BookingResort_Web.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;

namespace BookingResort_Web.Controllers
{
	public class HomeController : Controller
	{
        private readonly IResortService _resortService;
        private readonly IMapper _mapper;

        public HomeController(IResortService resortService, IMapper mapper)
        {
            _resortService = resortService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            List<ResortDTO> list = new();
            var response = await _resortService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<ResortDTO>>(Convert.ToString(response.Result));
            }
            return View(list);
        }
	}
}