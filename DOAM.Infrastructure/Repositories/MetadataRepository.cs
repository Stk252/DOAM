using DOAM.Infrastructure.Repositories.Interfaces;
using DOAM.Infrastructure.DB;
using System.Data.Entity;
using DOAM.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOAM.Infrastructure.Repositories
{
    public class MetadataRepository : Repository<Metadata>, IMetadataRepository
    {
        public MetadataRepository(DOAMDbContext context)
            : base(context)
        {
        }

        public IEnumerable<Metadata> GetFullMetadatas()
        {
            return Context.Metadatas.Include(md => md.MetadataGroup).ToList();
        }
    }
}
