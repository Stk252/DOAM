using DOAM.Infrastructure.Repositories.Interfaces;
using DOAM.Infrastructure.DB;
using DOAM.Infrastructure.Repositories;
using System.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOAM.Infrastructure.Repositories
{
    public class MimeTypeRepository : Repository<MimeType>, IMimeTypeRepository
    {
        public MimeTypeRepository(DOAMDbContext context)
            : base(context)
        {
        }

        public IEnumerable<MimeType> GetFullMimeTypes()
        {
            return Context.MimeTypes.Include(mt => mt.AssetType).ToList();
        }
    }
}
