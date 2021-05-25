using DOAM.Infrastructure.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOAM.Infrastructure.Repositories.Interfaces
{
    public interface IAssetTagRepository : IRepository<AssetTag>
    {
        IEnumerable<int> GetAssetTagsIdsForGivenAsset(int id);
        IEnumerable<AssetTag> GetAssetTagsForGivenAsset(int id);
    }
}
