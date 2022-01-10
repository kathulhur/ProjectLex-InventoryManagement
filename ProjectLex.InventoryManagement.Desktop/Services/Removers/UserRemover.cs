using ProjectLex.InventoryManagement.Database.Data;
using ProjectLex.InventoryManagement.Database.DTOs;
using ProjectLex.InventoryManagement.Database.Services;
using ProjectLex.InventoryManagement.Desktop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLex.InventoryManagement.Desktop.Services.Removers
{
    class UserRemover : DatabaseServiceBase, IRemover<User>
    {
        public UserRemover(ContextFactory contextFactory)
            : base(contextFactory)
        {
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
