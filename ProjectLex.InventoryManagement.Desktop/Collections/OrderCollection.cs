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
    public class OrderCollection : IDataCollection<Order>, ILoadable<Order>
    {
        private readonly IProvider<Order> _provider;

        private List<Order> _dataList;

        private Lazy<Task> _initializeLazy;

        public IEnumerable<Order> DataList => _dataList;


        public OrderCollection(IProvider<Order> provider)
        {
            _provider = provider;
            _dataList = new List<Order>();
            _initializeLazy = new Lazy<Task>(Initialize);
        }

        public event Action<Order> OrderCreated;
        public event Action<Order> OrderRemoved;
        public event Action<Order> OrderModified;
        private async Task Initialize()
        {
            IEnumerable<Order> data = await _provider.GetAll();
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
                Debug.WriteLine(e.Message);
                _initializeLazy = new Lazy<Task>(Initialize);
                throw;
            }
        }

        public async Task<IEnumerable<Order>> GetAll()
        {
            return await _provider.GetAll();
        }

        public async Task Create(Order newOrder)
        {
            await _provider.Create(newOrder);
            _dataList.Add(newOrder);
            OnOrderCreated(newOrder);
        }

        public async Task Remove(Order order)
        {
            await _provider.Remove(order);
            Order removedOrder = _dataList.Where(r => r.OrderID == order.OrderID).First();
            _dataList.Remove(removedOrder);
            OnOrderRemoved(removedOrder);
        }

        public async Task Modify(Order modifiedOrder)
        {
            await _provider.Modify(modifiedOrder);
            int index = _dataList.FindIndex(b => b.OrderID == modifiedOrder.OrderID);
            _dataList[index] = modifiedOrder;
            OnOrderModified(modifiedOrder);
    }


        private void OnOrderCreated(Order order)
        {
            OrderCreated?.Invoke(order);
        }

        private void OnOrderRemoved(Order order)
        {
            OrderRemoved?.Invoke(order);
        }

        private void OnOrderModified(Order order)
        {
            OrderModified?.Invoke(order);
        }

}
}
