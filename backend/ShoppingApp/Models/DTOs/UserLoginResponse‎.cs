namespace ShoppingApp.Models.DTOs
{
    public class UserLoginResponse
    {
        public int UserId { get; set; }
        public string Username { get; set; } = string.Empty;
        public string? Token { get; set; }
        public string? Role { get; set; }
        public string RefreshToken { get; set; } = string.Empty;
    }
}