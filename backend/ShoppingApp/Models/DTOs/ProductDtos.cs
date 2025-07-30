namespace ShoppingApp.Models.DTOs
{
    public class ProductAddDto
    {
        public string ProductName { get; set; } = string.Empty;
        public IFormFile? Image { get; set; }
        public Nullable<double> Price { get; set; }
        public Nullable<int> CategoryId { get; set; }
        public Nullable<int> ColorId { get; set; }
        public Nullable<int> ModelId { get; set; }
        public Nullable<int> Storage { get; set; }
        public Nullable<System.DateTime> SellStartDate { get; set; }
        public Nullable<System.DateTime> SellEndDate { get; set; }
        public Nullable<int> IsNew { get; set; } = 0;
    }
    public class ProductGetDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string? Image { get; set; }
        public string? Category { get; set; }
        public string? Color { get; set; }
        public string? Model { get; set; }
        public int? Storage { get; set; }
        public DateTime? SellStartDate { get; set; }
        public DateTime? SellEndDate { get; set; }
        public int? IsNew { get; set; }
    }
}