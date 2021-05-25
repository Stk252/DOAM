using DOAM.Infrastructure.Repositories.Interfaces;
using DOAM.Infrastructure.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace DOAM.Infrastructure.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        // Protected because it's to be used in derived repository classes
        protected readonly DOAMDbContext Context;



        public Repository(DOAMDbContext context)
        {
            Context = context;
        }



        public TEntity Get(int id)
        {
            return Context.Set<TEntity>().Find(id);
        }


        public IEnumerable<TEntity> GetAll()
        {
            return Context.Set<TEntity>().ToList();
        }


        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return Context.Set<TEntity>().Where(predicate);
        }



        public void Add(TEntity entity)
        {
            Context.Set<TEntity>().Add(entity);
        }

        // For some reason Enumerables don't work: IDENTITY isn't generated and as such the entities don't get added.
        public void AddRange(TEntity[] entities)
        {
            Context.Set<TEntity>().AddRange(entities);
        }


        public void Remove(TEntity entity)
        {
            Context.Set<TEntity>().Remove(entity);
        }


        public void RemoveRange(TEntity[] entities)
        {
            Context.Set<TEntity>().RemoveRange(entities);
        }
    }
}
