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
    class RoleModifier : DatabaseServiceBase, IModifier<Role>
    {
        public RoleModifier(ContextFactory dbContextFactory) : base(dbContextFactory)
        {
        }

        public async Task Modify(Role role)
        {
            using InventoryManagementContext context = ContextFactory.GetDbContext();
            RoleDTO roleDTO = context.Roles.Where(b => b.RoleID == new Guid(role.RoleID)).First();
            UpdateRole(roleDTO, role);
            await context.SaveChangesAsync();
        }

        private void UpdateRole(RoleDTO roleDTO, Role role)
        {
            if (!roleDTO.RoleName.Equals(role.RoleName))
            {
                roleDTO.RoleName = role.RoleName;
            }

            if (!roleDTO.RoleStatus.Equals(role.RoleStatus))
            {
                roleDTO.RoleStatus = role.RoleStatus;
            }

        }
    }
}
