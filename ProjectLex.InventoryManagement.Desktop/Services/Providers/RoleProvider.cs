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
    public class RoleProvider : DatabaseServiceBase, IProvider<Role>
    {

        public RoleProvider(ContextFactory dbContextFactory) : base(dbContextFactory)
        {
        }

        public async Task<IEnumerable<Role>> GetAll()
        {
            using InventoryManagementContext context = ContextFactory.GetDbContext();
            IEnumerable<RoleDTO> roleDTOs = await context.Roles.ToListAsync();

            return roleDTOs.Select(b => new Role(b));
        }

        public async Task Create(Role role)
        {
            using InventoryManagementContext context = ContextFactory.GetDbContext();
            RoleDTO RoleDTO = ModelConverters.RoleToRoleDTO(role);
            context.Roles.Add(RoleDTO);
            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateException dbE)
            {
                Debug.WriteLine("RoleProvider.Create : ");
                Debug.WriteLine(dbE.InnerException);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);

            }
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
