using DOAM.Application.ViewModels.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOAM.Application.Services.Interfaces
{
    public interface IUsersControllerService
    {
        IndexViewModel GetIndexViewModel();
        void UpdateUserRole(IndexViewModel indexViewModel);
        void DeleteUser(string id);
    }
}
