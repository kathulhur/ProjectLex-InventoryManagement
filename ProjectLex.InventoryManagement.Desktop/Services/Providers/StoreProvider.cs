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
    public class StoreProvider : IProvider<Store>
    {
        private readonly ContextFactory _dbContextFactory;

        public StoreProvider(ContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task<IEnumerable<Store>> GetAll()
        {
            using InventoryManagementContext context = _dbContextFactory.GetDbContext();
            IEnumerable<StoreDTO> storeDTOs = await context.Stores.ToListAsync();

            return storeDTOs.Select(c => new Store(c));
        }

    }
}
