using ProjectLex.InventoryManagement.Desktop.Commands;
using ProjectLex.InventoryManagement.Desktop.Models;
using ProjectLex.InventoryManagement.Desktop.Services.Providers;
using ProjectLex.InventoryManagement.Desktop.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLex.InventoryManagement.Desktop.Collections
{
    public class StoreCollection : IDataCollection<Store>, ILoadable<Store>
    {
        private readonly IProvider<Store> _provider;

        private List<Store> _dataList;

        public IEnumerable<Store> DataList => _dataList;

        public event Action<Store> StoreCreated;
        public event Action<Store> StoreRemoved;
        public event Action<Store> StoreModified;

        private void OnStoreCreated(Store store)
        {
            StoreCreated?.Invoke(store);
        }

        private void OnStoreRemoved(Store store)
        {
            StoreRemoved?.Invoke(store);
        }

        private void OnStoreModified(Store modifiedStore)
        {
            StoreRemoved?.Invoke(modifiedStore);
        }

        public StoreCollection(IProvider<Store> provider)
        {
            _provider = provider;
            _dataList = new List<Store>();
        }


        public async Task Load()
        {
            try
            {
                IEnumerable<Store> data = await _provider.GetAll();
                _dataList.Clear();
                _dataList.AddRange(data);
            }
            catch (Exception e)
            {
                Debug.WriteLine("StoreCollection.Load : ");
                Debug.WriteLine(e.Message);
                Debug.WriteLine(e.InnerException);
            }
        }

        public async Task<IEnumerable<Store>> GetAll()
        {
            return await _provider.GetAll();
        }

        public async Task Create(Store newStore)
        {
            await _provider.Create(newStore);
            _dataList.Add(newStore);
            OnStoreCreated(newStore);
        }

        public async Task Remove(Store store)
        {
            await _provider.Remove(store);
            Store removedStore = _dataList.Where(c => c.StoreID == store.StoreID).First();
            _dataList.Remove(removedStore);
            OnStoreRemoved(removedStore);
        }

        public async Task Modify(Store modifiedStore)
        {
            await _provider.Modify(modifiedStore);
            int index = _dataList.FindIndex(c => c.StoreID == modifiedStore.StoreID);
            _dataList[index] = modifiedStore;
            OnStoreModified(modifiedStore);
        }

        
    }
}
