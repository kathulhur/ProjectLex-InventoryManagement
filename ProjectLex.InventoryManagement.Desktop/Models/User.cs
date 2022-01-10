using ProjectLex.InventoryManagement.Database.DTOs;
using ProjectLex.InventoryManagement.Desktop.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLex.InventoryManagement.Desktop.Models
{
    public class User
    {
        public string UserID { get; }
        public string RoleID { get; }
        public string UserUsername { get; }
        public string UserPassword { get; }

        public User(UserDTO userDTO)
        {
            UserID = userDTO.UserID.ToString();
            RoleID = userDTO.RoleId.ToString();
            UserUsername = userDTO.UserUsername;
            UserPassword = userDTO.UserPassword;
        }

        public User(CreateUserViewModel createUserViewModel)
        {
            UserID = Guid.NewGuid().ToString();
            RoleID = createUserViewModel.RoleID;
            UserUsername = createUserViewModel.UserUsername;
            UserPassword = createUserViewModel.UserPassword;
        }

        public User(ModifyUserViewModel modifyUserViewModel)
        {
            UserID = modifyUserViewModel.UserID;
            RoleID = modifyUserViewModel.RoleID;
            UserUsername = modifyUserViewModel.UserUsername;
            UserPassword = modifyUserViewModel.UserPassword;
        }

        public User(UserViewModel userViewModel)
        {
            UserID = userViewModel.UserID;
            RoleID = userViewModel.RoleID;
            UserUsername = userViewModel.UserUsername;
            UserPassword = userViewModel.UserPassword;
        }

    }
}
