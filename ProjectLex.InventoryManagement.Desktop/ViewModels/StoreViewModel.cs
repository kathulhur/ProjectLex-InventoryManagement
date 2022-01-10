using ProjectLex.InventoryManagement.Desktop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLex.InventoryManagement.Desktop.ViewModels
{
    public class StoreViewModel : ViewModelBase
    {
        private readonly Store _store;
        public string StoreID => _store.StoreID;
        public string StoreName => _store.StoreName;
        public string StoreStatus => _store.StoreStatus;

        public StoreViewModel(Store store)
        {
            _store = store;
        }
    }
}
