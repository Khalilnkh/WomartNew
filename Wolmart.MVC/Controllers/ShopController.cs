using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Wolmart.MVC.DAL;
using Wolmart.MVC.Models;
using Wolmart.MVC.ViewModels;
using Wolmart.MVC.ViewModels.Shops;

namespace Wolmart.MVC.Controllers
{
    public class ShopController : Controller
    {
        private readonly AppDbContext _context;

        public ShopController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(int page, int? category, int? brand, int? size, int? color, int? minPrice, int? maxPrice)
        {
            IQueryable<Product> products = _context.Products.Include(x=>x.Feedbacks).Include(x=>x.ProductSizes).Include(x=>x.ProductColors).OrderBy(x=>x.ID);

            if (category.HasValue)
            {
                products = products.Where(p => p.CategoryID == category);
            }

            if (brand.HasValue)
            {
                products = products.Where(p => p.BrandID == brand);
            }

            if (size.HasValue)
            {
                products = products.Where(p => p.ProductSizes.Any(x => x.SizeID == size));
            }


            if (color.HasValue)
            {
                products = products.Where(p => p.ProductColors.Any(x => x.ColorID == color));
            }

            if (minPrice.HasValue && maxPrice.HasValue)
            {
                products = products.Where(p => p.ProductColors.All(x => x.Price >= minPrice && x.Price <= maxPrice));
            }

            var pagedList = PagiNationList<Product>.Create(products, page, 12);

            ViewBag.Brands = _context.Brands.ToList();
            ViewBag.Categories = _context.Categories.ToList();
            ViewBag.Colors = _context.Colors.ToList();
            ViewBag.Brands = _context.Brands.ToList();
            ViewBag.Sizes = _context.Sizes.ToList();

            return View(pagedList);
        }

        public async Task<IActionResult> Detail(int? ID)
        {
            if (ID == null) return Ok();

            ShopVM shopVM;

            var product = await _context.Products
                .Include(x => x.ProductImages)
                .Include(x => x.Feedbacks)
                .Include(x => x.ProductDescriptions)
                .Include(x => x.ProductColors)
                .ThenInclude(x => x.Color)
                .Include(x => x.Brand)
                .Include(x => x.Category)
                .FirstOrDefaultAsync(x => x.ID == ID);

            if (product != null)
            {
                shopVM = new ShopVM
                {
                    Product = product,
                    Colors = await _context.Colors.ToListAsync(),
                    Sizes = await _context.Sizes.ToListAsync(),
                    Feedbacks = await _context.Feedbacks.Where(x => x.ProductID == ID).ToListAsync(),
                    Description = await _context.ProductDescriptions.Where(x => x.ProductID == ID).ToListAsync(),
                    Specifications = await _context.ProductSpecifications.Where(x => x.ProductID == ID).ToListAsync(),
                    Products = await _context.Products.Include(x=>x.ProductColors).Include(x=>x.Feedbacks).Where(x => x.CategoryID == product.CategoryID).ToListAsync()
                };
            }
            else
            {
                return NotFound();
            }

            return View(shopVM);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Feedback(int? ID, Feedback feedback)
        {
            feedback.CreatedAt = DateTime.Now;

            await _context.Feedbacks.AddAsync(feedback);
            await _context.SaveChangesAsync();

            return RedirectToAction("index", "shop");
        }

    }
}
