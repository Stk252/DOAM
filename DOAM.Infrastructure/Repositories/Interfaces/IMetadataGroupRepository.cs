using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DOAM.Infrastructure.DB;
using System.Threading.Tasks;

namespace DOAM.Infrastructure.Repositories.Interfaces
{
    public interface IMetadataGroupRepository : IRepository<MetadataGroup>
    {
        IEnumerable<MetadataGroup> GetFullMetadataGroups();
    }
}
