using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace CalendarApp.Data.Repositories
{
    public class GenericRepository<TEntity, TContext> : IDisposable 
        where TEntity : class 
        where TContext : DbContext
    {
        protected readonly TContext DbContext;
        protected readonly DbSet<TEntity> DbSet;

        public GenericRepository(TContext context)
        {
            DbContext = context;
            DbSet = context.Set<TEntity>();
        }

        public virtual IQueryable<TEntity> GetAll()
        {
            return DbSet;
        }

        public virtual TEntity GetById(object key)
        {
            return DbSet.Find(key);
        }

        public virtual async Task<TEntity> GetByIdAsync(object key)
        {
            return await DbSet.FindAsync(key);
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    DbContext.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
