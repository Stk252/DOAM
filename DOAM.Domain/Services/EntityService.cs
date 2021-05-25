using DOAM.Domain.Services.Interfaces;
using DOAM.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOAM.Domain.Services
{
    public class EntityService<TEntity> : IEntityService<TEntity> where TEntity : class
    {
        public IEnumerable<TEntity> GetAll()
        {
            using (UnitOfWork<TEntity> unitOfWork = new UnitOfWork<TEntity>())
            {
                return unitOfWork.Entities.GetAll();
            }
        }

        public TEntity Get(int id)
        {
            using (UnitOfWork<TEntity> unitOfWork = new UnitOfWork<TEntity>())
            {
                return unitOfWork.Entities.Get(id);
            }
        }

        public void Add(TEntity entity)
        {
            using (UnitOfWork<TEntity> unitOfWork = new UnitOfWork<TEntity>())
            {
                unitOfWork.Entities.Add(entity);
                unitOfWork.Complete();
            }
        }


        public void AddRange(List<TEntity> entities)
        {

            using (UnitOfWork<TEntity> unitOfWork = new UnitOfWork<TEntity>())
            {
                TEntity[] entitiesArray = entities.ToArray();
                unitOfWork.Entities.AddRange(entitiesArray);
                unitOfWork.Complete();
            }
        }
        

        public void Delete(int id)
        {
            using (UnitOfWork<TEntity> unitOfWork = new UnitOfWork<TEntity>())
            {
                var entity = unitOfWork.Entities.Get(id);

                if (entity != null)
                {
                    unitOfWork.Entities.Remove(entity);
                    unitOfWork.Complete();
                }
            }
        }

        public void RemoveRange(TEntity[] entities)
        {

            using (UnitOfWork<TEntity> unitOfWork = new UnitOfWork<TEntity>())
            {
                unitOfWork.Entities.RemoveRange(entities);
                unitOfWork.Complete();
            }
        }

    }
}
