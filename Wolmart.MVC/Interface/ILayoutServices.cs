using Wolmart.MVC.Models;
using Wolmart.MVC.ViewModels.Cart;

namespace Wolmart.MVC.Interface
{
    public interface ILayoutServices
    {
        Task<List<Category>> GetCategories();
        Task<List<Subcategory>> GetSubcategories();
        Task<List<CartVM>> GetCart();
    }
}
