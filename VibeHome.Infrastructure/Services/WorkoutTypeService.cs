using VibeHome.Application.Interfaces;
using VibeHome.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace VibeHome.Infrastructure.Services
{
    public class WorkoutTypeService : IWorkoutTypeService
    {
        private readonly IRepository<WorkoutType> _repo;

        public WorkoutTypeService(IRepository<WorkoutType> repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<WorkoutType>> GetAllAsync()
        {
            return await _repo.GetAllAsync();
        }

        public async Task<WorkoutType?> GetByIdAsync(int id)
        {
            return await _repo.GetByIdAsync(id);
        }

        public async Task AddAsync(WorkoutType type)
        {
            // Add validation logic here if needed
            await _repo.AddAsync(type);
        }

        public async Task UpdateAsync(WorkoutType type)
        {
            // Add validation logic here if needed
            await _repo.UpdateAsync(type);
        }

        public async Task DeleteAsync(int id)
        {
            await _repo.DeleteAsync(id);
        }
    }
} 