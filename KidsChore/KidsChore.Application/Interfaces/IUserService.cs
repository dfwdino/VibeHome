using KidsChore.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KidsChore.Application.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllAsync();
        Task<User?> GetByIdAsync(int id);
        Task<User?> GetByUsernameAsync(string username);
        Task AddAsync(User user);
        Task UpdateAsync(User user);
        Task DeleteAsync(int id); // Soft delete
        Task<User?> AuthenticateAsync(string username, string password);
    }
} 