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
        private readonly AuthenticationStore _authenticationStore;
        public AuthenticationStore AuthenticationStore => _authenticationStore;

        public ViewModelBase CurrentViewModel => _navigationStore.CurrentViewModel;
        public bool IsLoggedIn => _authenticationStore.IsLoggedIn;

        public RelayCommand NavigateToRoleListCommand { get; }
        public RelayCommand NavigateToCategoryListCommand { get; }
        public RelayCommand NavigateToSupplierListCommand { get; }
        public RelayCommand NavigateToStaffListCommand { get; }
        public RelayCommand NavigateToProductListCommand { get; }
        public RelayCommand NavigateToOrderListCommand { get; }
        public RelayCommand NavigateToLocationListCommand { get; }
        public RelayCommand NavigateToCustomerListCommand { get; }
        public RelayCommand NavigateToDefectiveListCommand { get; }
        public RelayCommand NavigateToStorageListCommand { get; }
        public RelayCommand NavigateToDashboardCommand { get; }
        public RelayCommand NavigateToLogListCommand { get; }

        public RelayCommand LogOutCommand { get; }
        public MainViewModel(NavigationStore navigationStore, AuthenticationStore authenticationStore)
        {
            _navigationStore = navigationStore;
            _authenticationStore = authenticationStore;

            _navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;
            _navigationStore.CurrentViewModel = new LoginViewModel(_navigationStore, _authenticationStore);

            _authenticationStore.IsLoggedIn = false;
            _authenticationStore.IsLoggedInChanged += OnIsLoggedInChanged;

            NavigateToCategoryListCommand = new RelayCommand(NavigateToCategoryList);
            NavigateToRoleListCommand = new RelayCommand(NavigateToRoleList);
            NavigateToSupplierListCommand = new RelayCommand(NavigateToSupplierList);
            NavigateToStaffListCommand = new RelayCommand(NavigateToStaffList);
            NavigateToProductListCommand = new RelayCommand(NavigateToProductList);
            NavigateToOrderListCommand = new RelayCommand(NavigateToOrderList);
            NavigateToLocationListCommand = new RelayCommand(NavigateToLocationList);
            NavigateToCustomerListCommand = new RelayCommand(NavigateToCustomerList);
            NavigateToDefectiveListCommand = new RelayCommand(NavigateToDefectiveList);
            NavigateToStorageListCommand = new RelayCommand(NavigateToStorageList);
            NavigateToDashboardCommand = new RelayCommand(NavigateToDashboard);
            NavigateToLogListCommand = new RelayCommand(NavigateToLogList);
            LogOutCommand = new RelayCommand(LogOut);

        }

        private void LogOut()
        {
            _authenticationStore.CurrentStaff = null;
            _authenticationStore.IsLoggedIn = false;
            _navigationStore.CurrentViewModel = new LoginViewModel(_navigationStore, _authenticationStore);
        }

        private void NavigateToDashboard()
        {
            
            _navigationStore.CurrentViewModel = new DashboardViewModel(_navigationStore);
        }

        private void NavigateToLogList()
        {

            _navigationStore.CurrentViewModel = LogListViewModel.LoadViewModel(_navigationStore);
        }

        public void NavigateToStorageList()
        {
            _navigationStore.CurrentViewModel = StorageListViewModel.LoadViewModel(_navigationStore);
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


        public void NavigateToDefectiveList()
        {
            _navigationStore.CurrentViewModel = DefectiveListViewModel.LoadViewModel(_navigationStore);
        }



        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }

        private void OnIsLoggedInChanged()
        {
            OnPropertyChanged(nameof(IsLoggedIn));
        }


    }
}
