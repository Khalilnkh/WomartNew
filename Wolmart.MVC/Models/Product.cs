using System.ComponentModel.DataAnnotations;

namespace Wolmart.MVC.Models
{
    public class Product : BaseEntity
    {
        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int Count { get; set; }
        public string MainImage { get; set; }
        public string HoverImage { get; set; }

        [Required]
        public int BrandID { get; set; }
        public Brand Brand { get; set; }

        [Required]
        public int CategoryID { get; set; }
        public Category Category { get; set; }

        public List<ProductColor> ProductColors { get; set; }
        public List<ProductSize> ProductSizes { get; set; }
        public List<Feedback> Feedbacks { get; set; }
        public List<ProductImage> ProductImages { get; set; }
        public List<ProductDescription> ProductDescriptions { get; set; }
        public List<ProductSpecification> ProductSpecifications { get; set; }
    }
}
