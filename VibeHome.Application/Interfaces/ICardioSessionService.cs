using VibeHome.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace VibeHome.Application.Interfaces
{
    public interface ICardioSessionService
    {
        Task<IEnumerable<CardioSession>> GetAllAsync();
        Task<CardioSession?> GetByIdAsync(int id);
        Task AddAsync(CardioSession session);
        Task UpdateAsync(CardioSession session);
        Task DeleteAsync(int id);
    }
} 