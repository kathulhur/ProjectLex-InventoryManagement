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

        private bool _isDialogOpen = false;
        public bool IsDialogOpen => _isDialogOpen;

        private ViewModelBase _dialogViewModel;
        public ViewModelBase DialogViewModel => _dialogViewModel;

        private readonly NavigationStore _navigationStore;
        private readonly UnitOfWork _unitOfWork;

        private PaginationHelper<OrderViewModel> _paginationHelper;
        public PaginationHelper<OrderViewModel> PaginationHelper => _paginationHelper;

        private readonly ObservableCollection<OrderViewModel> _orders;
        public ObservableCollection<OrderViewModel> Orders { get; }

        public RelayCommand<OrderViewModel> CreateOrderCommand { get; }
        public RelayCommand LoadOrdersCommand { get; }
        public RelayCommand<OrderViewModel> RemoveOrderCommand { get; }
        public RelayCommand<OrderViewModel> EditOrderCommand { get; }
        public RelayCommand<OrderViewModel> PrintInvoiceCommand { get; }

        public OrderListViewModel(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
            _unitOfWork = new UnitOfWork();
            _orders = new ObservableCollection<OrderViewModel>();
            Orders = new ObservableCollection<OrderViewModel>();

            _paginationHelper = new PaginationHelper<OrderViewModel>(_orders, Orders);

            LoadOrdersCommand = new RelayCommand(LoadOrders);
            CreateOrderCommand = new RelayCommand<OrderViewModel>(CreateOrder);
            RemoveOrderCommand = new RelayCommand<OrderViewModel>(RemoveOrder);
            EditOrderCommand = new RelayCommand<OrderViewModel>(EditOrder);
            PrintInvoiceCommand = new RelayCommand<OrderViewModel>(PrintInvoice);

        }

        private void PrintInvoice(OrderViewModel orderViewModel)
        {
            _dialogViewModel?.Dispose();
            _dialogViewModel = PrintInvoiceViewModel.LoadViewModel(_navigationStore, orderViewModel.Order.OrderID);
            OnPropertyChanged(nameof(DialogViewModel));

            _isDialogOpen = true;
            OnPropertyChanged(nameof(IsDialogOpen));
        }

        private void RemoveOrder(OrderViewModel orderViewModel)
        {
            _unitOfWork.OrderRepository.Delete(orderViewModel.Order);
            _unitOfWork.Save();
            _orders.Remove(orderViewModel);
            _paginationHelper.RefreshCollection();
            MessageBox.Show("Successful");
        }

        private void EditOrder(OrderViewModel orderViewModel)
        {
            _navigationStore.CurrentViewModel = EditOrderViewModel.LoadViewModel(_navigationStore, orderViewModel.Order);
        }

        private void CreateOrder(OrderViewModel orderViewModel)
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
            _paginationHelper.RefreshCollection();

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
                    _unitOfWork?.Dispose();
                    _dialogViewModel?.Dispose();
                    _paginationHelper?.Dispose();
                }

            }

            // call methods to cleanup the unamanaged resources

            _isDisposed = true;
            base.Dispose(disposing);
        }
    }
}
