using ProjectLex.InventoryManagement.Desktop.Commands;
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

        public ICommand ToCategoryListViewModelCommand { get; }
        public ICommand ToCreateCategoryViewModelCommand { get; }
        public ICommand ToBrandListViewModelCommand { get; }
        public ICommand ToCreateBrandViewModelCommand { get; }

        public MainViewModel(ViewModelService viewModelService)
        {
            _viewModelService = viewModelService;
            _viewModelService.NavigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;
            viewModelService.NavigationStore.CurrentViewModel = viewModelService.MakeCategoryListViewModel();

            ToCategoryListViewModelCommand = viewModelService.ToCategoryListViewModelCommand;
            ToCreateCategoryViewModelCommand = viewModelService.ToCreateCategoryViewModelCommand;

            ToBrandListViewModelCommand = viewModelService.ToBrandListViewModelCommand;
            ToCreateBrandViewModelCommand = viewModelService.ToCreateBrandViewModelCommand;
        }

        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }


    }
}
