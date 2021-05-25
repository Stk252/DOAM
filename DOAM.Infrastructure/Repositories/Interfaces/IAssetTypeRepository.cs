using DOAM.Infrastructure.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOAM.Infrastructure.Repositories.Interfaces
{
    public interface IAssetTypeRepository : IRepository<AssetType>
    {
        IEnumerable<AssetType> GetFullAssetTypes();
    }
}
