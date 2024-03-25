namespace Wolmart.MVC.Models
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        public string Image { get; set; }
        public string Icon { get; set; }
        public List<Subcategory> Subcategories { get; set; }
    }
}
