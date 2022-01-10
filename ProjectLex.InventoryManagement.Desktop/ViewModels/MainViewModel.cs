using ProjectLex.InventoryManagement.Desktop.Commands;
using ProjectLex.InventoryManagement.Desktop.Models;
using ProjectLex.InventoryManagement.Desktop.Services;
using ProjectLex.InventoryManagement.Desktop.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ProjectLex.InventoryManagement.Desktop.ViewModels
{
    class MainViewModel : ViewModelBase
    {
        private readonly ViewModelService _viewModelService;
        public ViewModelBase CurrentViewModel => _viewModelService.NavigationStore.CurrentViewModel;

        private readonly User _user;
        public User CurrentUser => _user;

        public ICommand NavigateToCategoryListCommand { get; }
        public ICommand NavigateToBrandListCommand { get; }
        public ICommand NavigateToRoleListCommand { get; }
        public ICommand NavigateToStoreListCommand { get; }
        public ICommand NavigateToUserListCommand { get; }
        public ICommand NavigateToAttributeListCommand { get; }

        public MainViewModel(User user, ViewModelService viewModelService)
        {
            _user = user;

            _viewModelService = viewModelService;
            _viewModelService.NavigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;
            viewModelService.NavigationStore.CurrentViewModel = CreateCategoryViewModel.LoadViewModel(_viewModelService.NavigationStore, _viewModelService.CollectionStore.CategoryCollection);

            NavigateToCategoryListCommand = viewModelService.ToCategoryListViewModelCommand;
            NavigateToBrandListCommand = viewModelService.ToBrandListViewModelCommand;
            NavigateToRoleListCommand = viewModelService.ToRoleListViewModelCommand;
            NavigateToUserListCommand = viewModelService.ToUserListViewModelCommand;
            NavigateToStoreListCommand = viewModelService.ToStoreListViewModelCommand;
            NavigateToAttributeListCommand = viewModelService.ToAttributeListViewModelCommand;
        }

        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }


    }
}
