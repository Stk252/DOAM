using DOAM.Infrastructure.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOAM.Infrastructure.Repositories.Interfaces
{
    public interface IAssetTypeSupportedMetadataRepository : IRepository<AssetTypeSupportedMetadata>
    {
        IEnumerable<int> GetAssetTypeSupportedMetadasIdsForGivenAssetType(int id);
        IEnumerable<AssetTypeSupportedMetadata> GetAssetTypeSupportedMetadasForGivenAssetType(int id);
        IEnumerable<AssetTypeSupportedMetadata> GetFullAssetTypeSupportedMetadasForGivenAssetType(int id);
    }
}
