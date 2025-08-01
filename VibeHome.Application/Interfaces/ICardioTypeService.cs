using VibeHome.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace VibeHome.Application.Interfaces
{
    public interface ICardioTypeService
    {
        Task<IEnumerable<CardioType>> GetAllAsync();
        Task<CardioType?> GetByIdAsync(int id);
        Task AddAsync(CardioType cardioType);
        Task UpdateAsync(CardioType cardioType);
        Task DeleteAsync(int id);
    }
} 