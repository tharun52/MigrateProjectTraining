using ShoppingApp.Interfaces;
using ShoppingApp.Interfaces.TrueVote.Interfaces;
using ShoppingApp.Models;

namespace ShoppingApp.Services
{
    public class ColorService : IColorService
    {
        private readonly IRepository<int, Color> _ColorRepository;

        public ColorService(IRepository<int, Color> ColorRepository)
        {
            _ColorRepository = ColorRepository;
        }
        public async Task<Color> AddColorAsync(string name)
        {
            var colors = await _ColorRepository.GetAll();
            if (colors.SingleOrDefault(c => c.Color1 == name) != null)
            {
                throw new Exception("Color Already Exists");
            }
            var newColor = new Color { Color1 = name };
            return await _ColorRepository.Add(newColor);
        }
        public async Task<IEnumerable<Color>> GetColorsAsync()
        {
            var colors = await _ColorRepository.GetAll();
            if (colors == null)
            {
                throw new Exception("No colors in the db");
            }
            return colors.OrderBy(c => c.Color1);
        }
        public async Task<Color> EditColorAsync(Color updateColor)
        {
            var Color = await _ColorRepository.Get(updateColor.ColorId);
            if (Color == null)
            {
                throw new Exception($"No Color exists with the Id: {updateColor.ColorId}");
            }

            Color.Color1 = updateColor.Color1;

            return await _ColorRepository.Update(updateColor.ColorId, Color);
        }

        public async Task<Color> DeleteColorAsync(int ColorId)
        {
            var Color = await _ColorRepository.Get(ColorId);
            if (Color == null)
            {
                throw new Exception($"No Color exists with the Id : {ColorId}");
            }
            return await _ColorRepository.Delete(ColorId);
        }
    }
}