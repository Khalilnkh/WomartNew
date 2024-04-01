namespace Wolmart.MVC.Models
{
    public class FeedbackImage : BaseEntity
    {
        public string Image { get; set; }
        public int ProductID { get; set; }
        public Product Product { get; set; }
    }
}
