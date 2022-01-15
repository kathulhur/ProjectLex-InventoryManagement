using Microsoft.Toolkit.Mvvm.Input;
using ProjectLex.InventoryManagement.Database.Models;
using ProjectLex.InventoryManagement.Desktop.DAL;
using ProjectLex.InventoryManagement.Desktop.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ProjectLex.InventoryManagement.Desktop.ViewModels
{
    public class CreateOrderViewModel : ViewModelBase
    {
        private bool _isDisposed = false;


        private Order _order;


        public string UserID
        {
            get { return _order.UserID.ToString(); }
            set
            {
                _order.UserID = new Guid(value);
                OnPropertyChanged(nameof(UserID));
            }
        }


        public string CustomerName
        {
            get { return _order.CustomerName; }
            set
            {
                _order.CustomerName = value;
                OnPropertyChanged(nameof(CustomerName));
            }
        }


        public string OrderTotal
        {
            get { return _order.OrderTotal.ToString(); }
            set
            {
                _order.OrderTotal = Convert.ToDecimal(value);
                OnPropertyChanged(nameof(OrderTotal));
            }
        }


        private readonly NavigationStore _navigationStore;
        private readonly UnitOfWork _unitOfWork;


        private readonly OrderDetailListViewModel _orderDetailListViewModel;
        public OrderDetailListViewModel OrderDetailListViewModel => _orderDetailListViewModel;

        private readonly ObservableCollection<UserViewModel> _users;
        public IEnumerable<UserViewModel> Users => _users;
        


        public RelayCommand SubmitCommand { get; }
        public RelayCommand CancelCommand { get; }
        private RelayCommand LoadUsersCommand { get; }

        public CreateOrderViewModel(NavigationStore navigationStore, Order order)
        {
            _order = order;
            _navigationStore = navigationStore;
            _unitOfWork = new UnitOfWork();
            _users = new ObservableCollection<UserViewModel>();
            _orderDetailListViewModel = OrderDetailListViewModel.LoadViewModel(_navigationStore, _order);
            _order = new Order
            {
                OrderID = Guid.NewGuid()
            };

            SubmitCommand = new RelayCommand(CreateOrder);
            CancelCommand = new RelayCommand(NavigateToOrderList);
            LoadUsersCommand = new RelayCommand(LoadUsers);
        }

        private void CreateOrder()
        {
            _unitOfWork.OrderRepository.Insert(_order);
            _unitOfWork.Save();
            MessageBox.Show("Successful");
        }

        private void NavigateToOrderList()
        {
            _navigationStore.CurrentViewModel = OrderListViewModel.LoadViewModel(_navigationStore);
        }

        private void LoadUsers()
        {
            _users.Clear();
            foreach(User u in _unitOfWork.UserRepository.Get())
            {
                _users.Add(new UserViewModel(u));
            }
        }


        public static CreateOrderViewModel LoadViewModel(NavigationStore navigationStore, Order order)
        {
            CreateOrderViewModel viewModel = new CreateOrderViewModel(navigationStore, order);
            viewModel.LoadUsersCommand.Execute(null);
            return viewModel;
        }


        protected override void Dispose(bool disposing)
        {
            if(!this._isDisposed)
            {
                if(disposing)
                {
                    // dispose managed resources
                    _unitOfWork.Dispose();
                }
                // dispose unmanaged resources
            }
            this._isDisposed = true;

            base.Dispose(disposing);
        }

        public bool CanCreateOrder(object obj)
        {
            return true;
        }
    }
}
