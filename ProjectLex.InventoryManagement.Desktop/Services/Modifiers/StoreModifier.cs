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
    class StoreModifier : DatabaseServiceBase, IModifier<Store>
    {
        public StoreModifier(ContextFactory dbContextFactory) : base(dbContextFactory)
        {
        }

        public async Task Modify(Store store)
        {
            using InventoryManagementContext context = ContextFactory.GetDbContext();
            StoreDTO storeDTO = context.Stores.Where(s => s.StoreID == new Guid(store.StoreID)).First();
            UpdateStore(storeDTO, store);
            await context.SaveChangesAsync();
        }

        private void UpdateStore(StoreDTO storeDTO, Store store)
        {
            if(!(storeDTO.StoreName.Equals(store.StoreName)))
            {
                storeDTO.StoreName = store.StoreName;
            }

            if (!(storeDTO.StoreStatus.Equals(store.StoreStatus)))
            {
                storeDTO.StoreStatus = store.StoreStatus;
            }

        }
    }
}
