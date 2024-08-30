using MarketPortal.Data.Models;
using MarketPortal.Models;
using MarketPortal.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MarketPortal.Controllers
{
    public class UsersController : Controller
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly IUserService _service;

        public UsersController(IUserService service, SignInManager<User> signInManager, UserManager<User> userManager)
        {
            _service = service;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View(new LoginViewModel());
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string email, string password)
        {
            User user = await _service.Login(email, password);
            await _signInManager.SignInAsync(user, true);
            return RedirectToAction("Index", "Home");
        }
    }
}
