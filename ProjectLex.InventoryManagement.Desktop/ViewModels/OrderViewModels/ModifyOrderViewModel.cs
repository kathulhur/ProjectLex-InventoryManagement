using ProjectLex.InventoryManagement.Desktop.Collections;
using ProjectLex.InventoryManagement.Desktop.Commands;
using ProjectLex.InventoryManagement.Desktop.Models;
using ProjectLex.InventoryManagement.Desktop.Services;
using ProjectLex.InventoryManagement.Desktop.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ProjectLex.InventoryManagement.Desktop.ViewModels
{
    public class ModifyOrderViewModel : ViewModelBase
    {
        private bool _isDisposed = false;

        private OrderViewModel _orderViewModel;

        private string _orderID;
        public string OrderID
        {
            get { return _orderID; }
            set
            {
                _orderID = value;
                OnPropertyChanged(nameof(OrderID));
            }
        }

        private string _userID => _user.UserID;
        public string UserID => _userID;


        private UserViewModel _user;
        public UserViewModel User
        {
            get { return _user; }
            set
            {
                _user = value;
                OnPropertyChanged(nameof(User));
            }
        }

        private string _customerName;
        public string CustomerName
        {
            get { return _customerName; }
            set
            {
                _customerName = value;
                OnPropertyChanged(nameof(CustomerName));
            }
        }

        private string _orderTotal;
        public string OrderTotal
        {
            get { return _orderTotal; }
            set
            {
                _orderTotal = value;
                OnPropertyChanged(nameof(OrderTotal));
            }
        }
       

        private readonly IDataCollection<Order> _orderCollection;
        private readonly UserCollection _userCollection;

        private readonly ObservableCollection<UserViewModel> _users;
        public IEnumerable<UserViewModel> Users => _users;

        public ICommand SubmitCommand { get; }
        public ICommand CancelCommand { get; }
        public ICommand LoadUsersCommand { get; }

        public ModifyOrderViewModel
            (
                IDataCollection<Order> orderCollection, 
                UserCollection userCollection, 
                OrderViewModel orderViewModel
            )
        {
            _orderCollection = orderCollection;
            _userCollection = userCollection;
            _users = new ObservableCollection<UserViewModel>();

            _orderViewModel = orderViewModel;
            _user = orderViewModel.User;
            _orderID = orderViewModel.OrderID;
            _customerName = orderViewModel.CustomerName;
            _orderTotal = orderViewModel.OrderTotal;

            _orderCollection = orderCollection;
            SubmitCommand = new ModifyDataCommand<Order>(orderCollection, CreateOrder, CanModifyOrder);
            LoadUsersCommand = new LoadDataCommand<User>(_userCollection, OnDataLoaded);
        }

        public void OnDataLoaded()
        {
            _users.Clear();

            foreach (User r in _userCollection.DataList)
            {
                UserViewModel userViewModel = new UserViewModel(r);
                _users.Add(userViewModel);
                if(_orderViewModel.UserID == userViewModel.UserID)
                {
                    User = userViewModel;
                }
            }
        }

        private Order CreateOrder(object obj)
        {
            return new Order((ModifyOrderViewModel)obj);
        }

        public static ModifyOrderViewModel LoadViewModel(IDataCollection<Order> orderCollection, UserCollection userCollection, OrderViewModel orderViewModel)
        {
            ModifyOrderViewModel viewModel = new ModifyOrderViewModel(orderCollection, userCollection, orderViewModel);
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
                }
                // dispose unmanaged resources
            }
            this._isDisposed = true;

            base.Dispose(disposing);
        }


        public bool CanModifyOrder(object obj)
        {
            return true;
        }
    }
}
