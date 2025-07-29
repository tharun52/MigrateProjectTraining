using System.ComponentModel.DataAnnotations;

namespace ShoppingApp.Models
{
    public class OrderDetail
    {
        [Key]
        public int OrderID { get; set; }
        public int ProductID { get; set; }
        public Nullable<double> Price { get; set; }
        public Nullable<int> Quantity { get; set; }
    
        public virtual Order? Order { get; set; }
        public virtual Product? Product { get; set; }
    }
}