using VibeHome.Application.Interfaces;
using VibeHome.Domain.Entities;
using VibeHome.Application.Services;
using VibeHome.Application.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace VibeHome.Infrastructure.Services
{
    public class JournalEntryService : IJournalEntryService
    {
        private readonly IRepository<JournalEntry> _repository;
        public JournalEntryService(IRepository<JournalEntry> repository)
        {
            _repository = repository;
        }
        public async Task<IEnumerable<JournalEntry>> GetAllAsync() => await _repository.GetAllAsync();
        public async Task<JournalEntry?> GetByIdAsync(int id) => await _repository.GetByIdAsync(id);
        public async Task AddAsync(JournalEntry entry) => await _repository.AddAsync(entry);
        public async Task UpdateAsync(JournalEntry entry) => await _repository.UpdateAsync(entry);
        public async Task DeleteAsync(int id) => await _repository.DeleteAsync(id);
    }
}
