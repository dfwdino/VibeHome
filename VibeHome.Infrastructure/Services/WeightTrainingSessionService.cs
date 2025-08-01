using VibeHome.Application.Interfaces;
using VibeHome.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace VibeHome.Infrastructure.Services
{
    public class WeightTrainingSessionService : IWeightTrainingSessionService
    {
        private readonly IRepository<WeightTrainingSession> _repo;

        public WeightTrainingSessionService(IRepository<WeightTrainingSession> repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<WeightTrainingSession>> GetAllAsync()
        {
            return await _repo.GetAllAsync();
        }

        public async Task<WeightTrainingSession?> GetByIdAsync(int id)
        {
            return await _repo.GetByIdAsync(id);
        }

        public async Task AddAsync(WeightTrainingSession session)
        {
            // Add validation logic here if needed
            await _repo.AddAsync(session);
        }

        public async Task UpdateAsync(WeightTrainingSession session)
        {
            // Add validation logic here if needed
            await _repo.UpdateAsync(session);
        }

        public async Task DeleteAsync(int id)
        {
            await _repo.DeleteAsync(id);
        }
    }
} 