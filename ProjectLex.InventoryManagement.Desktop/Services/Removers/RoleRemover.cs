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
    class RoleRemover : DatabaseServiceBase, IRemover<Role>
    {
        public RoleRemover(ContextFactory contextFactory)
            : base(contextFactory)
        {
        }

        public async Task Remove(Role role)
        {
            using InventoryManagementContext context = ContextFactory.GetDbContext();
            RoleDTO roleDTO = context.Roles
                .Where(r => r.RoleID == new Guid(role.RoleID)).First();

            context.Roles.Remove(roleDTO);
            await context.SaveChangesAsync();
        }
    }
}
