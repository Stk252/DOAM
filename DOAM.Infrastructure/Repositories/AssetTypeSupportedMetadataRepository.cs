using DOAM.Infrastructure.Repositories.Interfaces;
using DOAM.Infrastructure.DB;
using DOAM.Infrastructure.Repositories;
using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOAM.Infrastructure.Repositories
{
    public class AssetTypeSupportedMetadataRepository : Repository<AssetTypeSupportedMetadata>, IAssetTypeSupportedMetadataRepository
    {
        public AssetTypeSupportedMetadataRepository(DOAMDbContext context)
            : base(context)
        {
        }

        public IEnumerable<int> GetAssetTypeSupportedMetadasIdsForGivenAssetType(int id)
        {
            return Context.AssetTypeSupportedMetadatas
                .Where(atsm => atsm.AssetTypeId == id)
                .Select(atsm => atsm.MetadataId)
                .ToList();
        }

        public IEnumerable<AssetTypeSupportedMetadata> GetAssetTypeSupportedMetadasForGivenAssetType(int id)
        {
            return Context.AssetTypeSupportedMetadatas
                .Where(atsm => atsm.AssetTypeId == id)
                .ToList();
        }

        public IEnumerable<AssetTypeSupportedMetadata> GetFullAssetTypeSupportedMetadasForGivenAssetType(int id)
        {
            return Context.AssetTypeSupportedMetadatas
                .Where(atsm => atsm.AssetTypeId == id)
                .Include(atsm => atsm.Metadata.MetadataGroup)
                .ToList();
        }
    }
}
