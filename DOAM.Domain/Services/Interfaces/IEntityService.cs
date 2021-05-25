using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOAM.Domain.Services.Interfaces
{
    public interface IEntityService<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> GetAll();
        TEntity Get(int id);
        void Add(TEntity entity);
        void AddRange(List<TEntity> entities);
        void Delete(int id);
    }
}
