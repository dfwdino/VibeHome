using VibeHome.Domain.Entities.Recipes;

namespace VibeHome.Application.Interfaces
{
    public interface IRecipeIngredientService
    {
        Task<IEnumerable<RecipeIngredient>> GetAllAsync();
        Task<IEnumerable<RecipeIngredient>> GetByRecipeIdAsync(int recipeId);
        Task<RecipeIngredient?> GetByIdAsync(int id);
        Task AddAsync(RecipeIngredient recipeIngredient);
        Task UpdateAsync(RecipeIngredient recipeIngredient);
        Task DeleteAsync(int id);
    }
}
