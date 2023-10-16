using FSACalculation.Entities;
using FSACalculation.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FSACalculation.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        private readonly SignInManager<UserLogin> _signInManager;
        private readonly UserManager<UserLogin> _userManager;

        public AccountController(ILogger<AccountController> logger, SignInManager<UserLogin> signInManager, UserManager<UserLogin> userManager)
        {
            _logger = logger;
            _signInManager = signInManager;
            this._userManager = userManager;
        }
        public IActionResult Login()
        {
            if (this.User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Claims", "Home");
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(viewModel.UserName, viewModel.Password, true, false);

                if (result.Succeeded)
                {
                    var check = _userManager.FindByNameAsync(viewModel.UserName).Result;
                    if (check.isAdmin != 1)
                    {
                        return RedirectToAction("Claims", "Home");
                    }
                    return RedirectToAction("AdminApproval", "Home");
                }
            }
            ModelState.AddModelError("", "Failed to Login");


            return View(viewModel);
        }
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            TempData["empId"] = 0;
            return RedirectToAction("Claims", "Home");
        }
    }
}
