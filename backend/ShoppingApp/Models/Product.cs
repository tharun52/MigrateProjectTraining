using System.ComponentModel.DataAnnotations;

namespace ShoppingApp.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;
        public Nullable<double> Price { get; set; }
        public Nullable<int> UserId { get; set; }
        public Nullable<int> CategoryId { get; set; }
        public Nullable<int> ColorId { get; set; }
        public Nullable<int> ModelId { get; set; }
        public Nullable<int> Storage { get; set; }
        public Nullable<System.DateTime> SellStartDate { get; set; }
        public Nullable<System.DateTime> SellEndDate { get; set; }
        public int IsNew { get; set; }   
    }
}