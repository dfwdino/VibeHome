using VibeHome.Application.Interfaces;
using VibeHome.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace VibeHome.Infrastructure.Services
{
    public class WorkoutLocationService : IWorkoutLocationService
    {
        private readonly IRepository<WorkoutLocation> _repo;

        public WorkoutLocationService(IRepository<WorkoutLocation> repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<WorkoutLocation>> GetAllAsync()
        {
            return await _repo.GetAllAsync();
        }

        public async Task<WorkoutLocation?> GetByIdAsync(int id)
        {
            return await _repo.GetByIdAsync(id);
        }

        public async Task AddAsync(WorkoutLocation location)
        {
            // Add validation logic here if needed
            await _repo.AddAsync(location);
        }

        public async Task UpdateAsync(WorkoutLocation location)
        {
            // Add validation logic here if needed
            await _repo.UpdateAsync(location);
        }

        public async Task DeleteAsync(int id)
        {
            await _repo.DeleteAsync(id);
        }
    }
} 