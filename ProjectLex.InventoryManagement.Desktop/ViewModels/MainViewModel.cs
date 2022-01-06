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
        public ICommand ToSupplierListViewModelCommand { get; }

        public MainViewModel(ViewModelService viewModelService)
        {
            _viewModelService = viewModelService;
            _viewModelService.NavigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;
            viewModelService.NavigationStore.CurrentViewModel = viewModelService.MakeCreateProductViewModel();

            ToCategoryListViewModelCommand = new NavigateCommand<CategoryListViewModel>
                (
                    new NavigationService<CategoryListViewModel>
                    (
                        _viewModelService.NavigationStore,
                        _viewModelService.MakeCategoryListViewModel
                    )
                );

            ToSupplierListViewModelCommand = new NavigateCommand<SupplierListViewModel>
                (
                    new NavigationService<SupplierListViewModel>
                    (
                        _viewModelService.NavigationStore,
                        _viewModelService.MakeSupplierListViewModel
                    )
                );
        }

        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }


    }
}
