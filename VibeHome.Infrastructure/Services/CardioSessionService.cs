using VibeHome.Application.Interfaces;
using VibeHome.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace VibeHome.Infrastructure.Services
{
    public class CardioSessionService : ICardioSessionService
    {
        private readonly IRepository<CardioSession> _repo;

        public CardioSessionService(IRepository<CardioSession> repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<CardioSession>> GetAllAsync()
        {
            return await _repo.GetAllAsync();
        }

        public async Task<CardioSession?> GetByIdAsync(int id)
        {
            return await _repo.GetByIdAsync(id);
        }

        public async Task AddAsync(CardioSession session)
        {
            // Add validation logic here if needed
            await _repo.AddAsync(session);
        }

        public async Task UpdateAsync(CardioSession session)
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