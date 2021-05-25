using DOAM.Infrastructure.DB;
using DOAM.Infrastructure.Repositories;
using DOAM.Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOAM.Infrastructure.Repositories
{
    public class AssetIssueRepository : Repository<AssetIssue>, IAssetIssueRepository
    {
        public AssetIssueRepository(DOAMDbContext context)
            : base(context)
        {
        }
    }
}
