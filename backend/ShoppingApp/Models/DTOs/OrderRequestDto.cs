namespace ShoppingApp.Models.DTOs
{
    public class OrderRequestDto
    {
        public string PaymentType { get; set; } = string.Empty;
        public string CustomerName { get; set; } = string.Empty;
        public string CustomerPhone { get; set; } = string.Empty;
        public string CustomerEmail { get; set; } = string.Empty;
        public string CustomerAddress { get; set; } = string.Empty;
        public Cart[] CartItems { get; set; } = Array.Empty<Cart>();
    }
}