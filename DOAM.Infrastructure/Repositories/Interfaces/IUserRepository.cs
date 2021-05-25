using DOAM.Infrastructure.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOAM.Infrastructure.Repositories.Interfaces
{
    public interface IUserRepository : IRepository<AspNetUser>
    {
        IEnumerable<AspNetUser> GetFullUsers();
        AspNetUser GetUser(string id);
    }
}
