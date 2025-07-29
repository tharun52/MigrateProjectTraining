using System.ComponentModel.DataAnnotations;

namespace ShoppingApp.Models
{
    public class Color
    {
        [Key]
        public int ColorId { get; set; }
        public string Color1 { get; set; } = string.Empty;
        public virtual ICollection<Product>? Products { get; set; }
    }
}