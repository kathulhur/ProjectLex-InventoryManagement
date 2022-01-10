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
        public string UserID { get; }
        public string RoleID { get; }
        public RoleViewModel Role { get; }
        public string UserUsername { get; }
        public string UserPassword { get; }


        public UserViewModel(User user, Role role)
        {
            UserID = user.UserID;
            RoleID = user.RoleID;
            UserUsername = user.UserUsername;
            UserPassword = user.UserPassword;
            Role = new RoleViewModel(role);
        }

        public UserViewModel(User user, RoleViewModel role)
        {
            UserID = user.UserID;
            RoleID = user.RoleID;
            UserUsername = user.UserUsername;
            UserPassword = user.UserPassword;
            Role = role;
        }

        public UserViewModel(User user)
        {
            UserID = user.UserID;
            RoleID = user.RoleID;
            UserUsername = user.UserUsername;
            UserPassword = user.UserPassword;

        }
    }
}
