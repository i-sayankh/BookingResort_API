using BookingResort_Web.Models;
using BookingResort_Web.Models.DTO;
using BookingResort_Web.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace BookingResort_Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpGet]
        public IActionResult Login()
        {
            LoginRequestDTO obj = new();
            return View(obj);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Login(LoginRequestDTO obj)
        {
            return View(obj);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegistrationRequestDTO obj)
        {
            APIResponse result = await _authService.RegisterAsync<APIResponse>(obj);
            if(result != null && result.IsSuccess)
            {
                return RedirectToAction("Login");
            }
            return View();
        }
        public async Task<IActionResult> Logout()
        {
            return View();
        }
        public IActionResult AccessDenied()
        {
            return View();
        }

    }
}
