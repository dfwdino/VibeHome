using VibeHome.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace VibeHome.Application.Interfaces
{
    public interface IWeightTrainingSessionService
    {
        Task<IEnumerable<WeightTrainingSession>> GetAllAsync();
        Task<WeightTrainingSession?> GetByIdAsync(int id);
        Task AddAsync(WeightTrainingSession session);
        Task UpdateAsync(WeightTrainingSession session);
        Task DeleteAsync(int id);
    }
} 