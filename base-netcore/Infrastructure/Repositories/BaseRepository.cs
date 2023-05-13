using Microsoft.EntityFrameworkCore;
namespace base_netcore.Infrastructure.Repositories
{
    public interface IBaseRepository<T> where T : class
    {
        List<T> GetAll(); 
        T Add(T entity);
        T Update(T entity);
        T Get(int id);
        T Delete(int id);
    }

    public abstract class BaseRepository<TEntity, TContext>
                 where TEntity : class
                 where TContext : DbContext
    {
        private readonly TContext context;
        public BaseRepository() { }
        public BaseRepository(TContext context)
        {
            this.context = context;
        }

        public TEntity Add(TEntity entity)
        {
            context.Set<TEntity>().Add(entity); 
            return entity;
        }
         
        public List<TEntity> GetAll()
        {
            return context.Set<TEntity>().ToList();
        }

        public TEntity Update(TEntity entity)
        {
            context.Entry(entity).State = EntityState.Modified; 
            return entity;
        }

        public TEntity Get(int id)
        {
            return context.Set<TEntity>().Find(id);
        }

        public TEntity Delete(int id)
        {
            var entity = context.Set<TEntity>().Find(id);
            if (entity == null)
            {
                return entity;
            }
            context.Set<TEntity>().Remove(entity); 
            return entity;
        }
    }
}
