using VibeHome.Application.Interfaces;
using VibeHome.Domain.Entities;

namespace VibeHome.Infrastructure.Services
{
    public class RecipeInstructionService : IRecipeInstructionService
    {
        private readonly IRepository<RecipeInstruction> _repo;

        public RecipeInstructionService(IRepository<RecipeInstruction> repo) { _repo = repo; }

        public async Task<IEnumerable<RecipeInstruction>> GetAllAsync()
        {
            var items = await _repo.GetAllAsync();
            return items.OrderBy(ri => ri.StepNumber);
        }

        public async Task<IEnumerable<RecipeInstruction>> GetByRecipeIdAsync(int recipeId)
        {
            var items = await _repo.GetAllAsync();
            return items
                .Where(ri => ri.RecipeId == recipeId && !ri.IsDeleted)
                .OrderBy(ri => ri.StepNumber);
        }

        public Task<RecipeInstruction?> GetByIdAsync(int id) => _repo.GetByIdAsync(id);
        public Task AddAsync(RecipeInstruction recipeInstruction) => _repo.AddAsync(recipeInstruction);
        public Task UpdateAsync(RecipeInstruction recipeInstruction) => _repo.UpdateAsync(recipeInstruction);
        public Task DeleteAsync(int id) => _repo.DeleteAsync(id);
    }
}
