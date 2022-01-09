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

namespace ProjectLex.InventoryManagement.Desktop.Services.Creators
{
    public class RoleCreator : ICreator<Role>
    {

        private readonly ContextFactory _dbContextFactory;

        public RoleCreator(ContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task Create(Role role)
        {
            using InventoryManagementContext context = _dbContextFactory.GetDbContext();
            RoleDTO RoleDTO = ModelConverters.RoleToRoleDTO(role);
            context.Roles.Add(RoleDTO);
            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateException dbE)
            {
                Debug.WriteLine(dbE.InnerException);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);

            }
        }
    }
}
