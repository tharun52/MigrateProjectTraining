using System.ComponentModel.DataAnnotations;

namespace ShoppingApp.Models
{
    public class Model
    {
        [Key]
        public int ModelId { get; set; }
        public string Model1 { get; set; } = string.Empty;
        public virtual ICollection<Product>? Products { get; set; }
    }
}