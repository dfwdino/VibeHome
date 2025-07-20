using KidsChore.Application.Interfaces;
using KidsChore.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KidsChore.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _repo;
        public UserService(IRepository<User> repo)
        {
            _repo = repo;
        }
        public Task<IEnumerable<User>> GetAllAsync() => _repo.GetAllAsync();
        public Task<User?> GetByIdAsync(int id) => _repo.GetByIdAsync(id);
        public async Task<User?> GetByUsernameAsync(string username)
        {
            var users = await _repo.GetAllAsync();
            return users.FirstOrDefault(u => u.Username.Equals(username, System.StringComparison.OrdinalIgnoreCase) && !u.IsDeleted);
        }
        public Task AddAsync(User user) => _repo.AddAsync(user);
        public Task UpdateAsync(User user) => _repo.UpdateAsync(user);
        public Task DeleteAsync(int id) => _repo.DeleteAsync(id);
        public async Task<User?> AuthenticateAsync(string username, string password)
        {
            var user = await GetByUsernameAsync(username);
            if (user == null) return null;
            // For demo: store password as hash, compare directly (in production, use a secure hash)
            if (user.PasswordHash == password) return user;
            return null;
        }
    }
} 