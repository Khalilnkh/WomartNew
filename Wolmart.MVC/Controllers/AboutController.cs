using Microsoft.AspNetCore.Mvc;

namespace Wolmart.MVC.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
