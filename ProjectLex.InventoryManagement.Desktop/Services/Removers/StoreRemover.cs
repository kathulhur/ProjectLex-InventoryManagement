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
    public class StoreRemover : DatabaseServiceBase, IRemover<Store>
    {
        public StoreRemover(ContextFactory contextFactory)
            : base(contextFactory)
        {
        }

        public async Task Remove(Store store)
        {
            using InventoryManagementContext context = ContextFactory.GetDbContext();
            StoreDTO storeDTO = context.Stores
                .Where(c => c.StoreID == new Guid(store.StoreID)).First();

            context.Stores.Remove(storeDTO);
            await context.SaveChangesAsync();
        }
    }
}
