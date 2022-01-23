using Microsoft.Toolkit.Mvvm.Input;
using ProjectLex.InventoryManagement.Database.Models;
using ProjectLex.InventoryManagement.Desktop.DAL;
using ProjectLex.InventoryManagement.Desktop.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ProjectLex.InventoryManagement.Desktop.ViewModels
{
    public class OrderListViewModel : ViewModelBase
    {

        private bool _isDisposed = false;

        private readonly NavigationStore _navigationStore;
        private readonly UnitOfWork _unitOfWork;

        private readonly ObservableCollection<OrderViewModel> _orders;
        public IEnumerable<OrderViewModel> Orders => _orders;

        public RelayCommand<OrderViewModel> NavigateToCreateOrderCommand { get; }
        public RelayCommand LoadOrdersCommand { get; }
        public RelayCommand<OrderViewModel> RemoveOrderCommand { get; }
        public RelayCommand<OrderViewModel> NavigateToEditOrderCommand { get; }

        public OrderListViewModel(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
            _unitOfWork = new UnitOfWork();
            _orders = new ObservableCollection<OrderViewModel>();

            LoadOrdersCommand = new RelayCommand(LoadOrders);
            NavigateToCreateOrderCommand = new RelayCommand<OrderViewModel>(NavigateToCreateOrder);
            //RemoveOrderCommand = new RelayCommand<OrderViewModel>(RemoveOrder);
            //NavigateToEditOrderCommand = new RelayCommand<OrderViewModel>(NavigateToEditOrder);

        }

        private void RemoveOrder(OrderViewModel orderViewModel)
        {
            _unitOfWork.OrderRepository.Delete(orderViewModel.Order);
            _unitOfWork.Save();
            MessageBox.Show("Successful");
            _orders.Remove(orderViewModel);
        }

        //private void NavigateToEditOrder(OrderViewModel orderViewModel)
        //{
        //    _navigationStore.CurrentViewModel = EditOrderViewModel.LoadViewModel(_navigationStore, orderViewModel.Order);
        //}

        private void NavigateToCreateOrder(OrderViewModel orderViewModel)
        {
            _navigationStore.CurrentViewModel = CreateOrderViewModel.LoadViewModel(_navigationStore);
        }

        private void LoadOrders()
        {
            _orders.Clear();
            foreach (Order o in _unitOfWork.OrderRepository.Get(includeProperties: "Customer"))
            {
                _orders.Add(new OrderViewModel(o));
            }

        }
        

        public static OrderListViewModel LoadViewModel(NavigationStore navigationStore)
        {
            OrderListViewModel viewModel = new OrderListViewModel(navigationStore);
            viewModel.LoadOrdersCommand.Execute(null);

            return viewModel;
        }
        

        protected override void Dispose(bool disposing)
        {
            //Note: Implement finalizer only if the object have unmanaged resources

            if (!this._isDisposed)
            {
                if (disposing) // dispose all unamanage and managed resources
                {
                    // dispose resources here
                    _unitOfWork.Dispose();
                }

            }

            // call methods to cleanup the unamanaged resources

            _isDisposed = true;
            base.Dispose(disposing);
        }
    }
}
