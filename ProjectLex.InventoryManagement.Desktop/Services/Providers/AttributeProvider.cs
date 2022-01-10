using Microsoft.EntityFrameworkCore;
using ProjectLex.InventoryManagement.Database.Data;
using ProjectLex.InventoryManagement.Database.DTOs;
using ProjectLex.InventoryManagement.Database.Services;
using ProjectLex.InventoryManagement.Desktop.Models;
using ProjectLex.InventoryManagement.Desktop.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLex.InventoryManagement.Desktop.Services.Providers
{
    public class AttributeProvider : IProvider<Models.Attribute>
    {
        private readonly ContextFactory _dbContextFactory;

        public AttributeProvider(ContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task<IEnumerable<Models.Attribute>> GetAll()
        {
            using InventoryManagementContext context = _dbContextFactory.GetDbContext();
            IEnumerable<AttributeDTO> attributeDTOs = await context.Attributes.ToListAsync();

            return attributeDTOs.Select(a => new Models.Attribute(a));
        }

    }
}
