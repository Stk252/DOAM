using DOAM.Infrastructure.Repositories.Interfaces;
using DOAM.Infrastructure.DB;
using DOAM.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOAM.Infrastructure.Repositories
{
    public class TagRepository : Repository<Tag>, ITagRepository
    {
        public TagRepository(DOAMDbContext context)
            : base(context)
        {
        }
    }
}
