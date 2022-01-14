using ProjectLex.InventoryManagement.Database.Models;
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

        public User User => _user;
        public string UserID => _user.UserID.ToString();
        public string RoleID => _user.RoleID.ToString();
        public RoleViewModel Role
        {
            get
            {
                if(_user.Role != null)
                {
                    return new RoleViewModel(_user.Role);
                }
                return null;
            }
        }

        public string UserUsername => _user.UserUsername;
        public string UserPassword => _user.UserPassword;


        public UserViewModel(User user)
        {
            _user = user;
        }

        
    }
}
