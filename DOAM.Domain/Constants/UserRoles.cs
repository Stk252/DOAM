using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOAM.Domain.Constants
{
    public static class UserRoles
    {
        public const string ROLE_ADMIN = "Admin";
        public const string ROLE_TEAM = "Team";
        public const string ROLE_ADMIN_OR_TEAM = ROLE_ADMIN + ", " + ROLE_TEAM;
        public const string ROLE_MEMBER = "Member";
    }
}
