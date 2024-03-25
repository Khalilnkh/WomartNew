using System.ComponentModel.DataAnnotations;

namespace Wolmart.MVC.Models
{
    public class Feedback
    {
        public int ID { get; set; }

        [DisplayFormat(DataFormatString = "{0:MMM dd, yyyy}")]
        public DateTime? CreatedAt { get; set; }
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Text { get; set; }
        public string Image { get; set; }
        [Required]
        public int Rating { get; set; }
        public int ProductID { get; set; }
        public Product Product { get; set; }
    }
}
