using DOAM.Infrastructure.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOAM.Application.ViewModels.Users
{
    public class IndexViewModel
    {
        public List<AspNetUser> Users { get; set; }
        public string NewRole { get; set; }
        public string UserId { get; set; }
    }
}
