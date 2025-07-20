using KidsChore.Application.Interfaces;
using KidsChore.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace KidsChore.Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly KidsChoreDbContext _context;
        private readonly DbSet<T> _dbSet;

        public Repository(KidsChoreDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            // Get the primary key property name using EF Core metadata
            var entityType = _context.Model.FindEntityType(typeof(T));
            var key = entityType.FindPrimaryKey();
            var keyProperty = key.Properties.First();
            var keyName = keyProperty.Name;
            var keyValue = typeof(T).GetProperty(keyName)?.GetValue(entity);

            // Check if an entity with the same key is already tracked
            var trackedEntity = _context.ChangeTracker.Entries<T>()
                .FirstOrDefault(e => e.Property(keyName).CurrentValue.Equals(keyValue));
            if (trackedEntity != null)
            {
                trackedEntity.State = EntityState.Detached;
            }
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity != null)
            {
                var prop = typeof(T).GetProperty("IsDeleted");
                if (prop != null)
                {
                    prop.SetValue(entity, true);
                    _dbSet.Update(entity);
                    await _context.SaveChangesAsync();
                }
            }
        }
    }
} 