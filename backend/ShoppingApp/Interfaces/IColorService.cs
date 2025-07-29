using ShoppingApp.Models;

namespace ShoppingApp.Interfaces
{
    public interface IColorService
    {
        public Task<Color> AddColorAsync(string name);
        public Task<IEnumerable<Color>> GetColorsAsync();
        public Task<Color> EditColorAsync(Color updateColor);
        public Task<Color> DeleteColorAsync(int ColorId);
    }
}