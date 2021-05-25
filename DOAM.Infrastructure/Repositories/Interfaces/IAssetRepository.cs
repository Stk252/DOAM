using DOAM.Infrastructure.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOAM.Infrastructure.Repositories.Interfaces
{
    public interface IAssetRepository : IRepository<Asset>
    {
        IEnumerable<Asset> GetAssets(int pageIndex, int pageSize);
        IEnumerable<Asset> GetLatestAssets(int count);
        //IEnumerable<Asset> GetFullAssets(int pageIndex, int pageSize);
        IEnumerable<Asset> GetFullAssets();
        IEnumerable<Asset> GetFullAssetsForGivenAssetType(int assetTypeId);
        IEnumerable<Asset> GetFullAssetsForGivenMimeType(int mimeTypeId);
        IEnumerable<Asset> GetFullAssetsForGivenMetadata(int metadataId);
        IEnumerable<Asset> GetFullAssetsForGivenMetadataGroup(int metadataGroupId);
        IEnumerable<Asset> GetFullAssetsForGivenTag(int tagId);
        IEnumerable<Asset> GetFullAssetsForGivenIds(List<int> assetIds);
        Asset GetFullAsset(int id);
        IEnumerable<int> GetAssetIdsForAssetType(int assetTypeId);
        IEnumerable<int> GetAssetIdsForMimeType(int mimeTypeId);
        IEnumerable<int> GetAssetIdsForTag(int tagId);
        IEnumerable<int> GetAssetIdsForMetadata(int metadataId);
        IEnumerable<int> GetAssetIdsForMetadataGroup(int metadataGroupId);
    }
}
