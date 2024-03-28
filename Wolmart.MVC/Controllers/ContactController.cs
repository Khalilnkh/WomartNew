using Microsoft.AspNetCore.Mvc;

namespace Wolmart.MVC.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
