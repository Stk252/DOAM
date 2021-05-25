using DOAM.Infrastructure.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;
using DOAM.Infrastructure.Repositories;
using DOAM.Infrastructure.Repositories.Interfaces;

namespace DOAM.Infrastructure.Repositories
{
    public class AssetRepository : Repository<Asset>, IAssetRepository
    {
        public AssetRepository(DOAMDbContext context)
            :base(context)
        {
        }
        public IEnumerable<Asset> GetLatestAssets(int count)
        {
            return Context.Assets
                .Include(a => a.AssetTags.Select(at => at.Tag))
                .Include(a => a.MimeType.AssetType)
                .OrderByDescending(a => a.DateAdded)
                .Take(count).ToList();
        }


        //public IEnumerable<Asset> GetFullAssets(int pageIndex, int pageSize)
        public IEnumerable<Asset> GetFullAssets()
        {
            return Context.Assets
                .Include(a => a.AssetTags.Select(at => at.Tag))
                .Include(a => a.MimeType.AssetType)
                .OrderByDescending(a => a.DateAdded)
                .ToList();
        }

        public Asset GetFullAsset(int id)
        {
            return Context.Assets
                .Include(a => a.MimeType.AssetType)
                .Include(a => a.AssetTags.Select(at => at.Tag))
                .Include(a => a.AssetMetadatas.Select(am => am.Metadata.MetadataGroup))
                .SingleOrDefault(a => a.AssetId == id);
                
        }

        public IEnumerable<Asset> GetAssets(int pageIndex, int pageSize)
        {
            return Context.Assets
                .Include(a => a.AssetTags.Select(at => at.Tag))
                .Include(a => a.MimeType.AssetType)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize);
        }

        

        public IEnumerable<Asset> GetFullAssetsForGivenAssetType(int assetTypeId)
        {
            return Context.Assets
                .Where(a => a.MimeType.AssetTypeId == assetTypeId)
                .Include(a => a.MimeType.AssetType)
                .Include(a => a.AssetTags.Select(at => at.Tag))
                .Include(a => a.AssetMetadatas.Select(am => am.Metadata.MetadataGroup));
        }

        public IEnumerable<Asset> GetFullAssetsForGivenMimeType(int mimeTypeId)
        {
            return Context.Assets
                .Where(a => a.MimeType.MimeTypeId == mimeTypeId)
                .Include(a => a.MimeType.AssetType)
                .Include(a => a.AssetTags.Select(at => at.Tag))
                .Include(a => a.AssetMetadatas.Select(am => am.Metadata.MetadataGroup));
        }

        public IEnumerable<Asset> GetFullAssetsForGivenMetadata(int metadataId)
        {
            return Context.Assets
                .Where(a => a.AssetMetadatas.Any(am => am.MetadataId == metadataId))
                .Include(a => a.MimeType.AssetType)
                .Include(a => a.AssetTags.Select(at => at.Tag))
                .Include(a => a.AssetMetadatas.Select(am => am.Metadata.MetadataGroup));
        }

        public IEnumerable<Asset> GetFullAssetsForGivenMetadataGroup(int metadataGroupId)
        {
            return Context.Assets
                .Where(a => a.AssetMetadatas.Any(am => am.Metadata.MetadataGroupId == metadataGroupId))
                .Include(a => a.MimeType.AssetType)
                .Include(a => a.AssetTags.Select(at => at.Tag))
                .Include(a => a.AssetMetadatas.Select(am => am.Metadata.MetadataGroup));
        }

        public IEnumerable<Asset> GetFullAssetsForGivenTag(int tagId)
        {
            return Context.Assets
                .Where(a => a.AssetTags.Any(at => at.TagId == tagId))
                .Include(a => a.MimeType.AssetType)
                .Include(a => a.AssetTags.Select(at => at.Tag))
                .Include(a => a.AssetMetadatas.Select(am => am.Metadata.MetadataGroup));
        }

        public IEnumerable<Asset> GetFullAssetsForGivenIds(List<int> assetIds)
        {
            return Context.Assets
                .Where(a => assetIds.Contains(a.AssetId))
                .Include(a => a.MimeType.AssetType)
                .Include(a => a.AssetTags.Select(at => at.Tag))
                .Include(a => a.AssetMetadatas.Select(am => am.Metadata.MetadataGroup));
        }

        public IEnumerable<int> GetAssetIdsForAssetType(int assetTypeId)
        {
            return Context.Assets
                .Where(a => a.MimeType.AssetTypeId == assetTypeId)
                .Select(a => a.AssetId);
        }

        public IEnumerable<int> GetAssetIdsForMimeType(int mimeTypeId)
        {
            return Context.Assets
                .Where(a => a.MimeTypeId == mimeTypeId)
                .Select(a => a.AssetId);
        }

        public IEnumerable<int> GetAssetIdsForTag(int tagId)
        {
            return Context.Assets
                .Where(a => a.AssetTags.Any(at => at.TagId == tagId))
                .Select(a => a.AssetId);
        }

        public IEnumerable<int> GetAssetIdsForMetadata(int metadataId)
        {
            return Context.Assets
                .Where(a => a.AssetMetadatas.Any(am => am.MetadataId == metadataId))
                .Select(a => a.AssetId);
        }

        public IEnumerable<int> GetAssetIdsForMetadataGroup(int metadataGroupId)
        {
            return Context.Assets
                .Where(a => a.AssetMetadatas.Any(at => at.Metadata.MetadataGroupId == metadataGroupId))
                .Select(a => a.AssetId);
        }

        
    }
}
