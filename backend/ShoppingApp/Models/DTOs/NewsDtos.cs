namespace ShoppingApp.Models.DTOs
{
    public class AddNewsDto
    {
        public string Title { get; set; } = string.Empty;
        public string ShortDescription { get; set; } = string.Empty;
        public IFormFile Image { get; set; }
        public string Content { get; set; } = string.Empty;
    }
    public class UpdateNewsDto
    {
        public string? Title { get; set; } = string.Empty;
        public string? ShortDescription { get; set; } = string.Empty;
        public IFormFile? Image { get; set; }
        public string? Content { get; set; } = string.Empty;
    }
}