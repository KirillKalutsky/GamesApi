using GamesApi.Models;
using Microsoft.EntityFrameworkCore;

namespace GamesApi.DB.Repositories
{
    public abstract class Repository<T> where T : class
    {
        protected DbContext context;
        protected DbSet<T> set;
        public Repository(DbContext context)
        {
            this.context = context;
            set = context.Set<T>();
        }

        public virtual async Task CreateAsync(T obj)
        {
            await set.AddAsync(obj);
            await SaveAsync();
        }

        public virtual async Task<bool> DeleteAsync(Guid id)
        {
            var developer = await set.FindAsync(id);

            if (developer == null)
                return false;

            set.Remove(developer);
            await SaveAsync();

            return true;
        }

        public abstract Task<PageList<T>> GetAllAsync(int currentPage, int pageSize);

        public virtual async Task<T> ReadAsync(Guid id) =>
            await set.FindAsync(id);

        public abstract Task UpdateAsync(T obj);

        public virtual async Task SaveAsync() =>
            await context.SaveChangesAsync();
    }
}
