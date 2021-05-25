using DOAM.Infrastructure.Repositories.Interfaces;
using DOAM.Infrastructure.DB;
using DOAM.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;

namespace DOAM.Infrastructure.Repositories
{
    public class AssetTypeRepository : Repository<AssetType>, IAssetTypeRepository
    {
        public AssetTypeRepository(DOAMDbContext context)
            : base(context)
        {
        }

        public IEnumerable<AssetType> GetFullAssetTypes()
        {
            return Context.AssetTypes
                .Include(at => at.MimeTypes)
                .ToList();
        }
    }
}
