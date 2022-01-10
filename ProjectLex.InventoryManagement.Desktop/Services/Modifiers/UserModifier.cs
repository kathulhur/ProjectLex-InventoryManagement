using ProjectLex.InventoryManagement.Database.Data;
using ProjectLex.InventoryManagement.Database.DTOs;
using ProjectLex.InventoryManagement.Database.Services;
using ProjectLex.InventoryManagement.Desktop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLex.InventoryManagement.Desktop.Services.Modifiers
{
    class UserModifier : DatabaseServiceBase, IModifier<User>
    {
        public UserModifier(ContextFactory dbContextFactory) : base(dbContextFactory)
        {
        }

        public async Task Modify(User user)
        {
            using InventoryManagementContext context = ContextFactory.GetDbContext();
            UserDTO userDTO = context.Users.Where(u => u.UserID == new Guid(user.UserID)).First();
            UpdateUser(userDTO, user);
            await context.SaveChangesAsync();
        }

        private void UpdateUser(UserDTO userDTO, User user)
        {
            if (!userDTO.RoleId.Equals(new Guid(user.RoleID)))
            {
                userDTO.RoleId = new Guid(user.RoleID);
            }

            if (!userDTO.UserUsername.Equals(user.UserUsername))
            {
                userDTO.UserUsername = user.UserUsername;
            }

            if (!userDTO.UserPassword.Equals(user.UserPassword))
            {
                userDTO.UserPassword = user.UserPassword;
            }

        }
    }
}
