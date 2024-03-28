using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Wolmart.MVC.Models;
using Wolmart.MVC.ViewModels.Account;

namespace Wolmart.MVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(RoleManager<IdentityRole> roleManager, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            if (!ModelState.IsValid)
            {
                List<string> errorMessages = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                TempData["ModelStateErrors"] = errorMessages;

                return RedirectToAction("Index", "Home");
            }


            AppUser appUser = new AppUser
            {
                FirstName = registerVM.FirstName,
                LastName = registerVM.LastName,
                UserName = registerVM.Username,
                Email = registerVM.Email,
            };

            IdentityResult result = await _userManager.CreateAsync(appUser, registerVM.Password);

            if (!result.Succeeded)
            {
                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                    TempData["ModelStateErrors"] = error.Description;
                }
                return RedirectToAction("Index", "Home");
            }

            await _userManager.AddToRoleAsync(appUser, "Member");

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            AppUser appUser = await _userManager.Users.FirstOrDefaultAsync(u => u.NormalizedEmail == loginVM.LoginEmail.Trim().ToUpperInvariant());

            if (appUser == null)
            {
                TempData["ErrorMessage"] = "Email or password is incorrect"; 
                return RedirectToAction("Index", "Home");
            }

            if (!await _userManager.CheckPasswordAsync(appUser, loginVM.LoginPassword))
            {
                TempData["ErrorMessage"] = "Email or password is incorrect";
                return RedirectToAction("Index", "Home");
            }


            await _signInManager.SignInAsync(appUser, loginVM.RemindMe);

            return RedirectToAction("index", "home");
        }

        [HttpGet("my-account")]
        public IActionResult Profile()
        {
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("index", "home");
        }
    }
}
