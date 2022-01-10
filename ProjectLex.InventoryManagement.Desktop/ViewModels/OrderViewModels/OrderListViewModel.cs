using ProjectLex.InventoryManagement.Desktop.Collections;
using ProjectLex.InventoryManagement.Desktop.Commands;
using ProjectLex.InventoryManagement.Desktop.Models;
using ProjectLex.InventoryManagement.Desktop.Services;
using ProjectLex.InventoryManagement.Desktop.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ProjectLex.InventoryManagement.Desktop.ViewModels
{
    public class OrderListViewModel : ViewModelBase
    {

        private bool _isDisposed = false;
        private readonly NavigationStore _navigationStore;
        private readonly ObservableCollection<OrderViewModel> _orders;
        public IEnumerable<OrderViewModel> Orders => _orders;

        private readonly OrderCollection _orderCollection;
        private readonly UserCollection _userCollection;

        public ICommand ToCreateOrderCommand { get; }
        public ICommand LoadOrdersCommand { get; }
        public ICommand LoadUsersCommand { get; }
        public ICommand RemoveOrderCommand { get; }
        public ICommand NavigateToModifyOrderCommand { get; }

        public OrderListViewModel
            (
                OrderCollection orderCollection, 
                UserCollection userCollection, 
                NavigationStore navigationStore
            )
        {
            _navigationStore = navigationStore;
            _orderCollection = orderCollection;
            _userCollection = userCollection;
            _orderCollection.OrderRemoved += OnOrderRemoved;
            _orders = new ObservableCollection<OrderViewModel>();
            LoadOrdersCommand = new LoadDataCommand<Order>(_orderCollection, OnOrderLoaded);
            LoadUsersCommand = new LoadDataCommand<User>(_userCollection, OnOrderLoaded);
            RemoveOrderCommand = new RemoveDataCommand<Order>(_orderCollection, CreateOrder, CanRemoveOrder);
            NavigateToModifyOrderCommand = new ModifyDataNavigateCommand(NavigateToModifyOrder);

        }

        public Order CreateOrder(object obj)
        {
            return new Order((OrderViewModel)obj);
        }

        public void NavigateToModifyOrder(object obj)
        {
            OrderViewModel orderViewModel = (OrderViewModel)obj;
            _navigationStore.CurrentViewModel = ModifyOrderViewModel.LoadViewModel(_orderCollection, _userCollection, orderViewModel);

        }

        public static OrderListViewModel LoadViewModel
            (
                OrderCollection orderCollection, 
                UserCollection userCollection, 
                NavigationStore navigationStore
            )
        {
            OrderListViewModel viewModel = new OrderListViewModel(orderCollection, userCollection, navigationStore);
            viewModel.LoadOrdersCommand.Execute(null);
            viewModel.LoadUsersCommand.Execute(null);

            return viewModel;
        }

        public void OnOrderRemoved(Order order)
        {
            OrderViewModel removedOrderViewModel = _orders.First(r => r.OrderID == order.OrderID);
            _orders.Remove(removedOrderViewModel);

        }
        
        public bool CanRemoveOrder(object obj)
        {
            return true;
        }

        private void OnOrderLoaded()
        {
            _orders.Clear();

            foreach (Order u in _orderCollection.DataList)
            {
                User user = _userCollection.DataList.Where(r => r.UserID == u.UserID).FirstOrDefault();
                OrderViewModel orderViewModel = new OrderViewModel(u, user);
                _orders.Add(orderViewModel);
            }

        }

        protected override void Dispose(bool disposing)
        {
            //Note: Implement finalizer only if the object have unmanaged resources

            if (!this._isDisposed)
            {
                if (disposing) // dispose all unamanage and managed resources
                {
                    // dispose resources here
                    _orderCollection.OrderRemoved -= OnOrderRemoved;
                }

            }

            // call methods to cleanup the unamanaged resources

            _isDisposed = true;
            base.Dispose(disposing);
        }
    }
}
