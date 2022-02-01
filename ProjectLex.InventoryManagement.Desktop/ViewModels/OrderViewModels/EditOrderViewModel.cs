using Microsoft.Toolkit.Mvvm.Input;
using ProjectLex.InventoryManagement.Database.Models;
using ProjectLex.InventoryManagement.Desktop.DAL;
using ProjectLex.InventoryManagement.Desktop.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ProjectLex.InventoryManagement.Desktop.ViewModels
{
    public class EditOrderViewModel : ViewModelBase
    {
        private bool _isDisposed = false;

        private Order _order;


        private string _customerID;

        [Required(ErrorMessage = "Customer is Required")]
        public string CustomerID
        {
            get => _customerID;
            set
            {
                SetProperty(ref _customerID, value);
                _customer = _customers.Where(c => c.CustomerID == _customerID).SingleOrDefault();
            }
        }

        private CustomerViewModel _customer;
        public CustomerViewModel Customer
        {
            set
            {
                SetProperty(ref _customer, value);
            }
        }


        private string _orderTotal;


        [Required(ErrorMessage = "OrderTotal is Required")]
        public string OrderTotal
        {
            get { return _orderTotal; }
        }

        private ViewModelBase _dialogViewModel;
        public ViewModelBase DialogViewModel => _dialogViewModel;

        private bool _isDialogOpen = false;
        public bool IsDialogOpen => _isDialogOpen;


        private readonly NavigationStore _navigationStore;
        private readonly UnitOfWork _unitOfWork;


        private readonly ObservableCollection<OrderDetailViewModel> _orderDetails;
        public IEnumerable<OrderDetailViewModel> OrderDetails => _orderDetails;


        private readonly ObservableCollection<CustomerViewModel> _customers;
        public IEnumerable<CustomerViewModel> Customers => _customers;



        public RelayCommand SubmitCommand { get; }
        public RelayCommand CancelCommand { get; }
        public RelayCommand NavigateToCreateOrderDetailCommand { get; }

        public RelayCommand LoadOrderDetailsCommand { get; }
        public RelayCommand<OrderDetailViewModel> RemoveOrderDetailCommand { get; }
        public RelayCommand<OrderDetailViewModel> EditOrderDetailCommand { get; }
        public RelayCommand CreateOrderDetailCommand { get; }

        public EditOrderViewModel(NavigationStore navigationStore, Guid orderID)
        {
            _navigationStore = navigationStore;
            _unitOfWork = new UnitOfWork();

            _order = _unitOfWork.OrderRepository.Get(o => o.OrderID == orderID, includeProperties: "Customer,OrderDetails,OrderDetails.Product").Single();


            _customers = new ObservableCollection<CustomerViewModel>();
            LoadCustomers(_customers);

            _orderDetails = new ObservableCollection<OrderDetailViewModel>();
            SetInitialValues(_order);

            
            SubmitCommand = new RelayCommand(Submit);
            CancelCommand = new RelayCommand(Cancel);

            RemoveOrderDetailCommand = new RelayCommand<OrderDetailViewModel>(RemoveOrderDetail);
            EditOrderDetailCommand = new RelayCommand<OrderDetailViewModel>(EditOrderDetail);
            CreateOrderDetailCommand = new RelayCommand(CreateOrderDetail);
        }

        private void SetInitialValues(Order order)
        {
            _customerID = order.CustomerID.ToString();
            _customer = new CustomerViewModel(order.Customer);
            _orderTotal = order.OrderTotal.ToString();
            _orderDetails.Clear();
            foreach (OrderDetail od in _order.OrderDetails)
            {
                _orderDetails.Add(new OrderDetailViewModel(od));
            }
        }


        private void Submit()
        {
            ValidateAllProperties();

            if (HasErrors)
            {
                return;
            }
            else if (_orderDetails.Count == 0)
            {
                MessageBox.Show("Product order list cannot be empty");
            }

            _order.CustomerID = new Guid(_customerID);
            _order.Customer = _customer.Customer;
            _order.OrderTotal = _orderDetails.Sum(od => od.OrderDetail.OrderDetailAmount);

            _unitOfWork.Save();

            MessageBox.Show("Successful");
            CancelCommand.Execute(null);
        }

        private void Cancel()
        {
            _navigationStore.CurrentViewModel = OrderListViewModel.LoadViewModel(_navigationStore);
        }

        private void RemoveOrderDetail(OrderDetailViewModel orderDetailViewModel)
        {
            _orderDetails.Remove(orderDetailViewModel);
            _order.OrderDetails.Remove(orderDetailViewModel.OrderDetail);

            _order.OrderTotal = _orderDetails.Sum(od => od.OrderDetail.OrderDetailAmount);
        }

        private void EditOrderDetail(OrderDetailViewModel orderDetailViewModel)
        {
            _dialogViewModel?.Dispose();
            _dialogViewModel = EditOrderDetailViewModel.LoadViewModel(_navigationStore, orderDetailViewModel.OrderDetail, CloseDialogCallback);
            OnPropertyChanged(nameof(DialogViewModel));

            _isDialogOpen = true;
            OnPropertyChanged(nameof(IsDialogOpen));
        }

        private void CloseDialogCallback()
        {
            _orderDetails.Clear();
            foreach(OrderDetail od in _order.OrderDetails)
            {
                _orderDetails.Add(new OrderDetailViewModel(od));
            }

            _orderTotal = _order.OrderDetails.Sum(od => od.OrderDetailAmount).ToString();
            OnPropertyChanged(nameof(OrderTotal));


            _isDialogOpen = false;
            OnPropertyChanged(nameof(IsDialogOpen));


        }

        private void CreateOrderDetail()
        {
            _dialogViewModel?.Dispose();
            _dialogViewModel = CreateOrderDetailViewModel.LoadViewModel(_navigationStore, _order, CloseDialogCallback);
            OnPropertyChanged(nameof(DialogViewModel));

            _isDialogOpen = true;
            OnPropertyChanged(nameof(IsDialogOpen));
        }

        private void LoadCustomers(ObservableCollection<CustomerViewModel> customer)
        {
            customer.Clear();
            foreach (Customer u in _unitOfWork.CustomerRepository.Get())
            {
                customer.Add(new CustomerViewModel(u));
            }
        }


        public static EditOrderViewModel LoadViewModel(NavigationStore navigationStore, Order order)
        {
            EditOrderViewModel viewModel = new EditOrderViewModel(navigationStore, order.OrderID);
            return viewModel;
        }


        protected override void Dispose(bool disposing)
        {
            if (!this._isDisposed)
            {
                if (disposing)
                {
                    // dispose managed resources
                    _unitOfWork.Dispose();
                    _dialogViewModel?.Dispose();
                }
                // dispose unmanaged resources
            }
            this._isDisposed = true;

            base.Dispose(disposing);
        }
    }
}
