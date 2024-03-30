using Microsoft.AspNetCore.Mvc;
using Wolmart.MVC.DAL;
using Wolmart.MVC.Models;

namespace Wolmart.MVC.Controllers
{
    public class ContactController : Controller
    {
        private readonly AppDbContext _context;
        public ContactController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Contact(Message message)
        {
            if (!ModelState.IsValid) return View();

            string trimmedFullname = message.Fullname.Trim();
            string trimmedEmail = message.Email.Trim();
            string trimmedText = message.Text.Trim();

            message.Fullname = trimmedFullname;
            message.Email = trimmedEmail;
            message.Text = trimmedText;
            message.CreatedAt = DateTime.Now;

            await _context.Messages.AddAsync(message);
            await _context.SaveChangesAsync();
        
            return RedirectToAction("Index");   
        }
    }
}
