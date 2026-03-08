using VibeHome.Domain.Entities;

namespace VibeHome.Application.Interfaces
{
    public interface IRecipeFavoriteService
    {
        Task<IEnumerable<RecipeFavorite>> GetAllAsync();
        Task<RecipeFavorite?> GetByIdAsync(int id);
        Task AddAsync(RecipeFavorite recipeFavorite);
        Task UpdateAsync(RecipeFavorite recipeFavorite);
        Task DeleteAsync(int id);
    }
}
