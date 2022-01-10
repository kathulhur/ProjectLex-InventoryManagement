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
        public Role Role { get; }
        public string UserUsername { get; }
        public string UserPassword { get; }

        public User(UserDTO userDTO)
        {
            UserID = userDTO.UserID.ToString();
            RoleID = userDTO.RoleId.ToString();
            Role = new Role(userDTO.Role);
            UserUsername = userDTO.UserUsername;
            UserPassword = userDTO.UserPassword;
        }

        public User(CreateUserViewModel createUserViewModel)
        {
            UserID = Guid.NewGuid().ToString();
            RoleID = createUserViewModel.RoleID;
            Role = new Role(createUserViewModel.Role);
            UserUsername = createUserViewModel.UserUsername;
            UserPassword = createUserViewModel.UserPassword;
        }

        public User(ModifyUserViewModel modifyUserViewModel)
        {
            UserID = modifyUserViewModel.UserID;
            RoleID = modifyUserViewModel.RoleID;
            Role = new Role(modifyUserViewModel.Role);
            UserUsername = modifyUserViewModel.UserUsername;
            UserPassword = modifyUserViewModel.UserPassword;
        }

        public User(UserViewModel brandViewModel)
        {
            UserID = brandViewModel.UserID;
            RoleID = brandViewModel.RoleID;
            UserUsername = brandViewModel.UserUsername;
            UserPassword = brandViewModel.UserPassword;
        }

    }
}
