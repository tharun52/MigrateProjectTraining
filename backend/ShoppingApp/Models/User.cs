using System.ComponentModel.DataAnnotations;

namespace ShoppingApp.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;   
        public string Role { get; set; } = string.Empty;   

        public virtual ICollection<News>? News { get; set; }
        public virtual ICollection<Product>? Products { get; set; }
    }
}