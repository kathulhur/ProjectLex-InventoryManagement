using ProjectLex.InventoryManagement.Desktop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLex.InventoryManagement.Desktop.ViewModels
{
    public class UserViewModel : ViewModelBase
    {
        private readonly User _user;
        private readonly Role _role;
        public string UserID => _user.UserID;
        public string RoleID => _user.RoleID;

        public RoleViewModel Role => new RoleViewModel(_role);
        public string UserUsername => _user.UserUsername;
        public string UserPassword => _user.UserPassword;


        public UserViewModel(User user, Role role)
        {
            _user = user;
            _role = role;
        }
    }
}
