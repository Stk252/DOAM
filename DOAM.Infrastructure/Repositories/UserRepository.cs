using DOAM.Infrastructure.DB;
using DOAM.Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOAM.Infrastructure.Repositories
{
    public class UserRepository : Repository<AspNetUser>, IUserRepository
    {
        public UserRepository(DOAMDbContext context)
            : base(context)
        {
        }

        public IEnumerable<AspNetUser> GetFullUsers()
        {
            return Context.AspNetUsers.Include(u => u.AspNetRoles).ToList();
        }

        public AspNetUser GetUser(string id)
        {
            return Context.AspNetUsers
                .Where(u => u.Id == id)
                .Include(u => u.AspNetRoles)
                .SingleOrDefault(u => u.Id == id);


        }
    }
}
