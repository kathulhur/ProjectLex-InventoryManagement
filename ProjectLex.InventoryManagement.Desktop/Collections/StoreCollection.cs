using ProjectLex.InventoryManagement.Desktop.Commands;
using ProjectLex.InventoryManagement.Desktop.Controllers;
using ProjectLex.InventoryManagement.Desktop.Models;
using ProjectLex.InventoryManagement.Desktop.Services.Creators;
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
        private readonly IController<Store> _controller;

        private List<Store> _dataList;

        private Lazy<Task> _initializeLazy;
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

        public StoreCollection(IController<Store> controller)
        {
            _controller = controller;
            _dataList = new List<Store>();
            _initializeLazy = new Lazy<Task>(Initialize);
        }

        

        private async Task Initialize()
        {
            IEnumerable<Store> data = await _controller.Provider.GetAll();
            _dataList.Clear();
            _dataList.AddRange(data);
        }

        public async Task Load()
        {
            try
            {
                await _initializeLazy.Value;
            }
            catch (Exception e)
            {
                Debug.WriteLine("StoreCollection.Load : ");
                Debug.WriteLine(e.Message);
                _initializeLazy = new Lazy<Task>(Initialize);
                throw;
            }
        }

        public async Task<IEnumerable<Store>> GetAll()
        {
            return await _controller.Provider.GetAll();
        }

        public async Task Create(Store newStore)
        {
            await _controller.Creator.Create(newStore);
            _dataList.Add(newStore);
            OnStoreCreated(newStore);
        }

        public async Task Remove(Store store)
        {
            await _controller.Remover.Remove(store);
            Store removedStore = _dataList.Where(c => c.StoreID == store.StoreID).First();
            _dataList.Remove(removedStore);
            OnStoreRemoved(removedStore);
        }

        public async Task Modify(Store modifiedStore)
        {
            await _controller.Modifier.Modify(modifiedStore);
            int index = _dataList.FindIndex(c => c.StoreID == modifiedStore.StoreID);
            _dataList[index] = modifiedStore;
            OnStoreModified(modifiedStore);
        }

        
    }
}
