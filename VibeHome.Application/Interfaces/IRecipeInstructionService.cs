using VibeHome.Domain.Entities.Recipes;

namespace VibeHome.Application.Interfaces
{
    public interface IRecipeInstructionService
    {
        Task<IEnumerable<RecipeInstruction>> GetAllAsync();
        Task<IEnumerable<RecipeInstruction>> GetByRecipeIdAsync(int recipeId);
        Task<RecipeInstruction?> GetByIdAsync(int id);
        Task AddAsync(RecipeInstruction recipeInstruction);
        Task UpdateAsync(RecipeInstruction recipeInstruction);
        Task DeleteAsync(int id);
    }
}
