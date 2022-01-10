using ProjectLex.InventoryManagement.Database.DTOs;
using ProjectLex.InventoryManagement.Desktop.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLex.InventoryManagement.Desktop.Models
{
    public class Store
    {
        public string StoreID { get; set; }
        public string StoreName { get; set; }
        public string StoreStatus { get; set; }

        public Store(StoreDTO storeDTO)
        {
            StoreID = storeDTO.StoreID.ToString();
            StoreName = storeDTO.StoreName;
            StoreStatus = storeDTO.StoreStatus;
        }

        public Store(CreateStoreViewModel createStoreViewModel)
        {
            StoreID = Guid.NewGuid().ToString();
            StoreName = createStoreViewModel.StoreName;
            StoreStatus = createStoreViewModel.StoreStatus;
        }

        public Store(ModifyStoreViewModel modifyStoreViewModel)
        {
            StoreID = modifyStoreViewModel.StoreID;
            StoreName = modifyStoreViewModel.StoreName;
            StoreStatus = modifyStoreViewModel.StoreStatus;
        }

        public Store(StoreViewModel storeViewModel)
        {
            StoreID = storeViewModel.StoreID;
            StoreName = storeViewModel.StoreName;
            StoreStatus = storeViewModel.StoreStatus;
        }
    }
}
