using ShoppingApp.Models;

namespace ShoppingApp.Interfaces
{
    public interface IModelService
    {
        public Task<Model> AddModelAsync(string name);
        public Task<IEnumerable<Model>> GetModelsAsync();
        public Task<Model> DeleteModelAsync(int modelId);        
    }
}