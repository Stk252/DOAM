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
    public class AssetSuggestionRepository : Repository<AssetSuggestion>, IAssetSuggestionRepository
    {
        public AssetSuggestionRepository(DOAMDbContext context)
            : base(context)
        {
        }
    }
}
