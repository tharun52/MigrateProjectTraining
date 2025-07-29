using ShoppingApp.Interfaces;
using ShoppingApp.Interfaces.TrueVote.Interfaces;
using ShoppingApp.Models;

namespace ShoppingApp.Services
{
    public class ModelService : IModelService
    {
        private readonly IRepository<int, Model> _modelRepository;

        public ModelService(IRepository<int, Model> modelRepository)
        {
            _modelRepository = modelRepository;
        }
        public async Task<Model> AddModelAsync(string name)
        {
            var models = await _modelRepository.GetAll();
            if (models.SingleOrDefault(c => c.Model1 == name) != null)
            {
                throw new Exception("Model Already Exists");
            }
            var newmodel = new Model { Model1 = name };
            return await _modelRepository.Add(newmodel);
        }
        public async Task<IEnumerable<Model>> GetModelsAsync()
        {
            var models = await _modelRepository.GetAll();
            if (models == null)
            {
                throw new Exception("No models in the db");
            }
            return models.OrderBy(c => c.Model1);
        }
        public async Task<Model> DeleteModelAsync(int modelId)
        {
            var model = await _modelRepository.Get(modelId);
            if (model == null)
            {
                throw new Exception($"No model exists with the Id : {modelId}");
            }
            return await _modelRepository.Delete(modelId);
        }
    }
}