using DOAM.Infrastructure.DB;
using DOAM.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOAM.Domain.Services
{
    public class UserService : EntityService<AspNetUser>
    {
        public IEnumerable<AspNetUser> GetFullUsers()
        {
            using (UnitOfWork<AspNetUser> unitOfWork = new UnitOfWork<AspNetUser>())
            {
                return unitOfWork.Users.GetFullUsers();
            }

        }

        public void UpdateUserRole(string userId, string newRole)
        {
            using (UnitOfWork<AspNetUser> unitOfWork = new UnitOfWork<AspNetUser>())
            {
                var user = unitOfWork.Users.GetUser(userId);
                user.AspNetRoles.FirstOrDefault().Name = newRole;

                unitOfWork.Complete();
            }
        }

        public void RemoveUser(string userId)
        {
            using (UnitOfWork<AspNetUser> unitOfWork = new UnitOfWork<AspNetUser>())
            {
                var user = unitOfWork.Users.GetUser(userId);

                if (user != null)
                    unitOfWork.Users.Remove(user);

                unitOfWork.Complete();
            }
        }
    }
}
