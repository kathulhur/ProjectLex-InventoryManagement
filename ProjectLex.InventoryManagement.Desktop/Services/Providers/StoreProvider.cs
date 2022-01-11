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
    public class StoreProvider : DatabaseServiceBase, IProvider<Store>
    {
        public StoreProvider(ContextFactory dbContextFactory) : base(dbContextFactory)
        {
        }

        public async Task<IEnumerable<Store>> GetAll()
        {
            using InventoryManagementContext context = ContextFactory.GetDbContext();
            IEnumerable<StoreDTO> storeDTOs = await context.Stores.ToListAsync();

            return storeDTOs.Select(c => new Store(c));
        }


        public async Task Create(Store store)
        {
            using InventoryManagementContext context = ContextFactory.GetDbContext();
            StoreDTO StoreDTO = ModelConverters.StoreToStoreDTO(store);
            context.Stores.Add(StoreDTO);
            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateException dbE)
            {
                Debug.WriteLine("StoreCreator.Create: ");
                Debug.WriteLine(dbE.InnerException);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);

            }
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
            if (!(storeDTO.StoreName.Equals(store.StoreName)))
            {
                storeDTO.StoreName = store.StoreName;
            }

            if (!(storeDTO.StoreStatus.Equals(store.StoreStatus)))
            {
                storeDTO.StoreStatus = store.StoreStatus;
            }

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
