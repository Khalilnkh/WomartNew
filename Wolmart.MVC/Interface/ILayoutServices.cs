using Wolmart.MVC.Models;

namespace Wolmart.MVC.Interface
{
    public interface ILayoutServices
    {
        Task<List<Category>> GetCategories();
        Task<List<Subcategory>> GetSubcategories();
    }
}
