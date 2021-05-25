using DOAM.Application.Services.Interfaces;
using DOAM.Application.Services.Validation;
using DOAM.Application.ViewModels.Users;
using DOAM.Domain.Constants;
using DOAM.Domain.Services;
using DOAM.Infrastructure.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOAM.Application.Services
{
    public class UsersControllerService : IUsersControllerService
    {
        private readonly UserService _userService;
        private readonly IValidationDictionary _validatonDictionary;

        public UsersControllerService(IValidationDictionary validationDictionary)
        {
            _userService = new UserService();
            _validatonDictionary = validationDictionary;
        }

        public IndexViewModel GetIndexViewModel()
        {
            List<AspNetUser> users = _userService.GetFullUsers().ToList();

            if (users.Count == 0) return null;

            var indexViewModel = new IndexViewModel()
            {
                Users = users
            };

            return indexViewModel;
        }

        public void UpdateUserRole(IndexViewModel indexViewModel)
        {
            _userService.UpdateUserRole(indexViewModel.UserId, indexViewModel.NewRole);
        }

        public void DeleteUser(string id)
        {
            _userService.RemoveUser(id);
        }

    }
}
