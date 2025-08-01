using VibeHome.Application.Interfaces;
using VibeHome.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace VibeHome.Infrastructure.Services
{
    public class ChoreCompletionService : IChoreCompletionService
    {
        private readonly IRepository<ChoreCompletion> _repo;
        public ChoreCompletionService(IRepository<ChoreCompletion> repo)
        {
            _repo = repo;
        }
        public Task<IEnumerable<ChoreCompletion>> GetAllAsync() => _repo.GetAllAsync();
        public Task<ChoreCompletion?> GetByIdAsync(int id) => _repo.GetByIdAsync(id);
        public Task AddAsync(ChoreCompletion completion) => _repo.AddAsync(completion);
        public Task UpdateAsync(ChoreCompletion completion) => _repo.UpdateAsync(completion);
        public Task DeleteAsync(int id) => _repo.DeleteAsync(id);
    }
} 