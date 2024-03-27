using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Wolmart.MVC.Models;
using Wolmart.MVC.ViewModels.Account;

namespace Wolmart.MVC.Controllers;

public class HomeController : Controller
{
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;

    public HomeController(RoleManager<IdentityRole> roleManager, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
    {
        _roleManager = roleManager;
        _userManager = userManager;
        _signInManager = signInManager;
    }
    public IActionResult Index()
    {
        if (TempData["ErrorMessage"] != null)
        {
            ModelState.AddModelError("", TempData["ErrorMessage"].ToString());
        }

        if (TempData["ModelStateErrors"] != null)
        {
            ModelState.AddModelError("", TempData["ModelStateErrors"].ToString());
        }

        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }
}
