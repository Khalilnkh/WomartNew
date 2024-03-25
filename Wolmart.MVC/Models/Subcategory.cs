namespace Wolmart.MVC.Models
{
    public class Subcategory : BaseEntity
    {
            public string Name { get; set; }
            public int ParentCategoryID { get; set; }
            public int? ParentSubcategoryID { get; set; } 
            public Category ParentCategory { get; set; }
            public Subcategory ParentSubcategory { get; set; } 
    }

}
