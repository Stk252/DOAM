using DOAM.Infrastructure.DB;
using DOAM.Infrastructure.Repositories.Interfaces;
using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOAM.Infrastructure.Repositories
{
    public class AssetMetadataRepository : Repository<AssetMetadata>, IAssetMetadataRepository
    {
        public AssetMetadataRepository(DOAMDbContext context)
            : base(context)
        {
        }

        public IEnumerable<AssetMetadata> GetAssetMetadatasForGivenAsset(int id)
        {
            return Context.AssetMetadatas
                .Include(am => am.Metadata.MetadataGroup)
                .Where(at => at.AssetId == id)
                .ToList();
        }
    }
}
