global using RouteSummitTask.BLL.Interfaces;
global using RouteSummitTask.DAL.Context;
using Microsoft.EntityFrameworkCore;

namespace RouteSummitTask.BLL.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly DbSet<T> _db;

        public GenericRepository(OrderManagementDbContext context)
        {
            _db = context.Set<T>();
        }

        public async Task Add(T entity)
            => await _db.AddAsync(entity);

        public async Task<IEnumerable<T>> GetAllAsync(params string[] includeProperties)
        {
            var query = _db.AsNoTracking();
            query = includeProperties.Aggregate(query,
               (current, includeProperty) => current.Include(includeProperty));
            return await query.ToListAsync();
        }
        public async Task<T?> GetByIdAsync(int id)
            => await _db.FindAsync(id);

        public void Update(T entity)
            => _db.Update(entity);
        public void Delete(T entity)
            => _db.Remove(entity);
    }
}
