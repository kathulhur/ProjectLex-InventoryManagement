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

namespace ProjectLex.InventoryManagement.Desktop.ViewModels
{
    class StorageListViewModel : ViewModelBase
    {
        private bool _isDisposed = false;

        private UnitOfWork _unitOfWork;
        private readonly NavigationStore _navigationStore;

        private PaginationHelper<LocationViewModel> _paginationHelper;
        public PaginationHelper<LocationViewModel> PaginationHelper => _paginationHelper;


        private readonly ObservableCollection<LocationViewModel> _locations;
        public ObservableCollection<LocationViewModel> Locations { get; }

        public RelayCommand LoadLocationsCommand { get; }
        public RelayCommand CreateLocationCommand { get; }
        public RelayCommand<LocationViewModel> LocationDetailsCommand { get; }

        public StorageListViewModel(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
            _unitOfWork = new UnitOfWork();
            _locations = new ObservableCollection<LocationViewModel>();
            Locations = new ObservableCollection<LocationViewModel>();

            _paginationHelper = new PaginationHelper<LocationViewModel>(_locations, Locations);

            LoadLocationsCommand = new RelayCommand(LoadLocations);
            LocationDetailsCommand = new RelayCommand<LocationViewModel>(LocationDetails);
        }

        public void LocationDetails(LocationViewModel locationViewModel)
        {
            _navigationStore.CurrentViewModel = StorageDetailViewModel.LoadViewModel(_navigationStore, locationViewModel.Location.LocationID);
        }



        public void LoadLocations()
        {
            _locations.Clear();
            foreach (Location r in _unitOfWork.LocationRepository.Get())
            {
                _locations.Add(new LocationViewModel(r));
            }
            _paginationHelper.RefreshCollection();
        }


        public static StorageListViewModel LoadViewModel(NavigationStore navigationStore)
        {
            StorageListViewModel viewModel = new StorageListViewModel(navigationStore);
            viewModel.LoadLocationsCommand.Execute(null);
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
                    _unitOfWork.Dispose();
                    _paginationHelper.Dispose();
                }

            }

            // call methods to cleanup the unamanaged resources

            _isDisposed = true;
            base.Dispose(disposing);
        }

    }
}
