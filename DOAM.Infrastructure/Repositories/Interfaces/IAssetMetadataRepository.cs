using DOAM.Infrastructure.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOAM.Infrastructure.Repositories.Interfaces
{
    public interface IAssetMetadataRepository : IRepository<AssetMetadata>
    {
        IEnumerable<AssetMetadata> GetAssetMetadatasForGivenAsset(int id);
    }
}
