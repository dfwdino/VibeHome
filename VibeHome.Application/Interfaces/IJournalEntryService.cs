using VibeHome.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace VibeHome.Application.Interfaces
{
    public interface IJournalEntryService
    {
        Task<IEnumerable<JournalEntry>> GetAllAsync();
        Task<JournalEntry?> GetByIdAsync(int id);
        Task AddAsync(JournalEntry entry);
        Task UpdateAsync(JournalEntry entry);
        Task DeleteAsync(int id);
    }
}
