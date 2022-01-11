using Microsoft.EntityFrameworkCore;
using ProjectLex.InventoryManagement.Database.Data;
using ProjectLex.InventoryManagement.Database.DTOs;
using ProjectLex.InventoryManagement.Database.Services;
using ProjectLex.InventoryManagement.Desktop.Models;
using ProjectLex.InventoryManagement.Desktop.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLex.InventoryManagement.Desktop.Services.Providers
{
    public class UserProvider : DatabaseServiceBase, IProvider<User>
    {

        public UserProvider(ContextFactory dbContextFactory) : base(dbContextFactory)
        {
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            using InventoryManagementContext context = ContextFactory.GetDbContext();
            IEnumerable<UserDTO> userDTOs = await context.Users.Include(u => u.Role).ToListAsync();

            return userDTOs.Select(u => new User(u));
        }


        public async Task Create(User user)
        {
            using InventoryManagementContext context = ContextFactory.GetDbContext();
            UserDTO UserDTO = ModelConverters.UserToUserDTO(user);
            context.Users.Add(UserDTO);
            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateException dbE)
            {
                Debug.WriteLine("UserProvider.Create : ");
                Debug.WriteLine(dbE.InnerException);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);

            }
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


        public async Task Remove(User user)
        {
            using InventoryManagementContext context = ContextFactory.GetDbContext();
            UserDTO userDTO = context.Users
                .Where(r => r.UserID == new Guid(user.UserID)).First();

            context.Users.Remove(userDTO);
            await context.SaveChangesAsync();
        }

    }
}
