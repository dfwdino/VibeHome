using VibeHome.Application.Interfaces;
using VibeHome.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace VibeHome.Infrastructure.Services
{
    public class CardioTypeService : ICardioTypeService
    {
        private readonly IRepository<CardioType> _repo;

        public CardioTypeService(IRepository<CardioType> repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<CardioType>> GetAllAsync()
        {
            return await _repo.GetAllAsync();
        }

        public async Task<CardioType?> GetByIdAsync(int id)
        {
            return await _repo.GetByIdAsync(id);
        }

        public async Task AddAsync(CardioType type)
        {
            // Add validation logic here if needed
            await _repo.AddAsync(type);
        }

        public async Task UpdateAsync(CardioType type)
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