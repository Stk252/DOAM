using DOAM.Infrastructure.Repositories.Interfaces;
using DOAM.Infrastructure.DB;
using DOAM.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOAM.Infrastructure.Repositories
{
    public class AssetTagRepository : Repository<AssetTag>, IAssetTagRepository
    {
        public AssetTagRepository(DOAMDbContext context)
            : base(context)
        {
        }

        public IEnumerable<AssetTag> GetAssetTagsForGivenAsset(int id)
        {
            return Context.AssetTags.Where(at => at.AssetId == id).ToList();
        }

        public IEnumerable<int> GetAssetTagsIdsForGivenAsset(int id)
        {
            return Context.AssetTags
                .Where(at => at.AssetId == id)
                .Select(at => at.TagId)
                .ToList();
        }


    }
}
