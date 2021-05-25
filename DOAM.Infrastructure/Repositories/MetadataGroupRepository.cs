using DOAM.Infrastructure.Repositories.Interfaces;
using DOAM.Infrastructure.DB;
using DOAM.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOAM.Infrastructure.Repositories
{
   
    public class MetadataGroupRepository : Repository<MetadataGroup>, IMetadataGroupRepository
    {
        public MetadataGroupRepository(DOAMDbContext context)
            : base(context)
        {
        }

        public IEnumerable<MetadataGroup> GetFullMetadataGroups()
        {
            return Context.MetadataGroups.Include(mdg => mdg.Metadatas).ToList();
        }
    }
}
