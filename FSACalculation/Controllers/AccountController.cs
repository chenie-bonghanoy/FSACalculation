using FSACalculation.Data.Entities;
using FSACalculation.ViewModels;
using Microsoft.AspNetCore.Http;
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
        private readonly IConfiguration _config;

        public AccountController(ILogger<AccountController> logger, 
            SignInManager<UserLogin> signInManager, 
            UserManager<UserLogin> userManager, 
            IConfiguration config)
        {
            _logger = logger;
            _signInManager = signInManager;
            _userManager = userManager;
            _config = config;
        }
        public IActionResult Login()
        {
            if (this.User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Claims", "Home");
            }
            return View();
            //return RedirectToAction("Logout");
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(viewModel.UserName);

                if (user != null)
                {
                    //var result = await _signInManager.CheckPasswordSignInAsync(user, viewModel.Password, false);
                    var result = await _signInManager.PasswordSignInAsync(viewModel.UserName, viewModel.Password, true, false);

                    if (result.Succeeded)
                    {
                        // Create the Token
                        var claims = new[]
                        {
                              new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                              new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                              new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName)
                        };

                        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));

                        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                        var token = new JwtSecurityToken(
                          _config["Tokens:Issuer"],
                          _config["Tokens:Audience"],
                          claims,
                          expires: DateTime.UtcNow.AddMinutes(60),
                          signingCredentials: creds);

                        TempData["JWToken"] = new JwtSecurityTokenHandler().WriteToken(token);

                        if (user.isAdmin != 1)
                        {
                            return RedirectToAction("Claims", "Home");
                        }
                        return RedirectToAction("AdminApproval", "Home");

                    }

                    ModelState.AddModelError("", "Failed to Login");

                }
            }

            return View(viewModel);
        }
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            TempData["empId"] = 0;
            return RedirectToAction("Claims", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> CreateToken([FromBody] LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(model.UserName);

                if (user != null)
                {
                    var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);

                    if (result.Succeeded)
                    {
                        // Create the Token
                        var claims = new[]
                        {
                              new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                              new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                              new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName)
                            };
                        var a = _config["Tokens:Key"];
                        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));

                        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                        var token = new JwtSecurityToken(
                          _config["Tokens:Issuer"],
                          _config["Tokens:Audience"],
                          claims,
                          expires: DateTime.UtcNow.AddMinutes(60),
                          signingCredentials: creds);

                        var results = new
                        {
                            token = new JwtSecurityTokenHandler().WriteToken(token),
                            expiration = token.ValidTo
                        };

                        return Created("", results);
                    }
                }
            }
            return BadRequest();
        }
    }
}
