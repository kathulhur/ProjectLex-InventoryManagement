using Microsoft.Toolkit.Mvvm.Input;
using ProjectLex.InventoryManagement.Desktop.Stores;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ProjectLex.InventoryManagement.Desktop.ViewModels
{
    class MainViewModel : ViewModelBase
    {
        private readonly NavigationStore _navigationStore;
        public ViewModelBase CurrentViewModel => _navigationStore.CurrentViewModel;
        private readonly MainWindow _mainWindow;

        public RelayCommand NavigateToRoleListCommand { get; }
        public RelayCommand NavigateToCategoryListCommand { get; }
        public RelayCommand NavigateToSupplierListCommand { get; }
        public RelayCommand NavigateToStaffListCommand { get; }
        public RelayCommand NavigateToProductListCommand { get; }
        public RelayCommand NavigateToOrderListCommand { get; }
        public RelayCommand NavigateToLocationListCommand { get; }
        public RelayCommand NavigateToCustomerListCommand { get; }

        public RelayCommand LogOutCommand { get; }
        public MainViewModel(MainWindow mainWindow, NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
            _mainWindow = mainWindow;
            _navigationStore.CurrentViewModel = CategoryListViewModel.LoadViewModel(_navigationStore);
            _navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;

            NavigateToCategoryListCommand = new RelayCommand(NavigateToCategoryList);
            NavigateToRoleListCommand = new RelayCommand(NavigateToRoleList);
            NavigateToSupplierListCommand = new RelayCommand(NavigateToSupplierList);
            NavigateToStaffListCommand = new RelayCommand(NavigateToStaffList);
            NavigateToProductListCommand = new RelayCommand(NavigateToProductList);
            NavigateToOrderListCommand = new RelayCommand(NavigateToOrderList);
            NavigateToLocationListCommand = new RelayCommand(NavigateToLocationList);
            NavigateToCustomerListCommand = new RelayCommand(NavigateToCustomerList);
            LogOutCommand = new RelayCommand(LogOut);

            mainWindow.Closing += OnClosing;
        }

        private void LogOut()
        {
            _mainWindow.Close();
        }


        private void OnClosing(Object obj, EventArgs e)
        {
            Application.Current.MainWindow = new LoginWindow(_navigationStore);
            Application.Current.MainWindow.Show();
            this.Dispose();
        }



        public void NavigateToRoleList()
        {
            _navigationStore.CurrentViewModel = RoleListViewModel.LoadViewModel(_navigationStore);
        }

        public void NavigateToLocationList()
        {
            _navigationStore.CurrentViewModel =LocationListViewModel.LoadViewModel(_navigationStore);
        }

        public void NavigateToCategoryList()
        {
            _navigationStore.CurrentViewModel = CategoryListViewModel.LoadViewModel(_navigationStore);
        }


        public void NavigateToSupplierList()
        {
            _navigationStore.CurrentViewModel = SupplierListViewModel.LoadViewModel(_navigationStore);
        }

        public void NavigateToStaffList()
        {
            _navigationStore.CurrentViewModel = StaffListViewModel.LoadViewModel(_navigationStore);
        }

        public void NavigateToProductList()
        {
            _navigationStore.CurrentViewModel = ProductListViewModel.LoadViewModel(_navigationStore);
        }

        public void NavigateToCustomerList()
        {
            _navigationStore.CurrentViewModel = CustomerListViewModel.LoadViewModel(_navigationStore);
        }


        public void NavigateToOrderList()
        {
            _navigationStore.CurrentViewModel = OrderListViewModel.LoadViewModel(_navigationStore);
        }

        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }


    }
}
