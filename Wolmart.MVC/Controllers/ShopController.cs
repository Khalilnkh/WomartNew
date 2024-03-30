using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Wolmart.MVC.DAL;
using Wolmart.MVC.Models;
using Wolmart.MVC.ViewModels;
using Wolmart.MVC.ViewModels.Cart;
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

        public IActionResult Index(int page, string q,int? categoryID, int? category, int? brand, int? size, int? color, int? minPrice, int? maxPrice,int? show, string orderby)
        {
            IQueryable<Product> products = _context.Products.Include(x=>x.Feedbacks).Include(x=>x.ProductSizes).Include(x=>x.ProductColors).OrderBy(x=>x.ID);

            if (categoryID.HasValue)
            {
                products = products.Where(x => x.CategoryID == categoryID);
            }

            ViewBag.SearchTerm = q;
            
            if (!string.IsNullOrWhiteSpace(q))
            {
                products = products.Where(x => x.Name.Contains(q.Trim()) && (!categoryID.HasValue || x.CategoryID == categoryID));
            }

            switch (orderby)
            {
                case "rating":
                    products = products.OrderByDescending(x => x.Feedbacks.Count());
                    break;
                case "date":
                    products = products.OrderBy(x => x.CreatedAt);
                    break;
                case "price-low":
                    products = products.OrderBy(x => x.ProductColors.Min(pc => pc.DiscountedPrice != null ? pc.DiscountedPrice : pc.Price));
                    break;
                case "price-high":
                    products = products.OrderByDescending(x => x.ProductColors.Max(pc => pc.DiscountedPrice != null ? pc.DiscountedPrice : pc.Price));
                    break;

                default:
                    break;

            }

            switch(show)
            {
                case 9:
                    products = products.Take(9);
                    break;
                case 12:
                    products = products.Take(12);
                    break;
                case 24:
                    products = products.Take(24);
                    break;
                case 36:
                    products = products.Take(36);
                    break;

                default:
                    break;
            }

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
                products = products.Where(p => p.ProductColors.All(color =>
                    (color.DiscountedPrice ?? color.Price) >= minPrice &&
                    (color.DiscountedPrice ?? color.Price) <= maxPrice) ||
                    (p.ProductColors.Any(color => color.DiscountedPrice.HasValue && color.Price >= minPrice && color.Price <= maxPrice)));
            }


            var pagedList = PagiNationList<Product>.Create(products, page, show ?? 12);

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
                string cart = Request.Cookies["cart"];
                List<CartVM> cartVMs = null;

                if (!string.IsNullOrWhiteSpace(cart))
                {
                    cartVMs = JsonConvert.DeserializeObject<List<CartVM>>(cart);
                }
                else
                {
                    cartVMs = new List<CartVM>();
                }

                shopVM = new ShopVM
                {
                    Product = product,
                    Colors = await _context.Colors.ToListAsync(),
                    Sizes = await _context.Sizes.ToListAsync(),
                    Feedbacks = await _context.Feedbacks.Where(x => x.ProductID == ID).ToListAsync(),
                    Description = await _context.ProductDescriptions.Where(x => x.ProductID == ID).ToListAsync(),
                    Specifications = await _context.ProductSpecifications.Where(x => x.ProductID == ID).ToListAsync(),
                    Products = await _context.Products.Include(x=>x.ProductColors).Include(x=>x.Feedbacks).Where(x => x.CategoryID == product.CategoryID).ToListAsync(),
                    CartVMs = cartVMs 
                };

                foreach (CartVM item in shopVM.CartVMs)
                {
                    Product products = await _context.Products.Include(x => x.ProductColors).FirstOrDefaultAsync(x => x.ID == item.ProductID);

                    item.Image = products.MainImage;
                    item.Title = products.Name;
                    item.Price = products.ProductColors.Min(x => x.Price);
                }
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
