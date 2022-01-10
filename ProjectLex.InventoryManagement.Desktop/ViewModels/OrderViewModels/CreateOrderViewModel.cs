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
    public class CreateOrderViewModel : ViewModelBase
    {
        private bool _isDisposed = false;


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

        private UserViewModel _user;
        private string _userID => _user.UserID;
        public string UserID => _userID;


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
        private ICommand LoadUsersCommand { get; }

        public CreateOrderViewModel(User user, IDataCollection<Order> orderCollection)
        {
            _orderCollection = orderCollection;
            _user = new UserViewModel(user);
            _users = new ObservableCollection<UserViewModel>();
            SubmitCommand = new CreateDataCommand<Order>(orderCollection, CreateOrder, CanCreateOrder);
        }

        public Order CreateOrder(object obj)
        {
            return new Order((CreateOrderViewModel)obj);
        }



        public static CreateOrderViewModel LoadViewModel(User user, IDataCollection<Order> orderCollection)
        {
            CreateOrderViewModel viewModel = new CreateOrderViewModel(user, orderCollection);
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

        public bool CanCreateOrder(object obj)
        {
            return true;
        }
    }
}
