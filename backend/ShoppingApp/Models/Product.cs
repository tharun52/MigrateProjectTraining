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
        public Nullable<int> IsNew { get; set; }    
        public virtual Category? Category { get; set; }
        public virtual Color? Color { get; set; }
        public virtual Model? Model { get; set; }
        public virtual ICollection<OrderDetail>? OrderDetails { get; set; }
        public virtual User? User { get; set; }
    }
}