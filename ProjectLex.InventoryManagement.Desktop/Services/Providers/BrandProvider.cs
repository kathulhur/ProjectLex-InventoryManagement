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
    public class BrandProvider : IProvider<Brand>
    {
        private readonly ContextFactory _dbContextFactory;

        public BrandProvider(ContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task<IEnumerable<Brand>> GetAll()
        {
            using InventoryManagementContext context = _dbContextFactory.GetDbContext();
            IEnumerable<BrandDTO> brandDTOs = await context.Brands.ToListAsync();

            return brandDTOs.Select(b => new Brand(b));
        }

    }
}
