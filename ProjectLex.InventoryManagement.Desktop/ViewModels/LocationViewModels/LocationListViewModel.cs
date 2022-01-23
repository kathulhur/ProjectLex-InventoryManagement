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
    class LocationListViewModel : ViewModelBase
    {
        private bool _isDisposed = false;

        private UnitOfWork _unitOfWork;

        private readonly NavigationStore _navigationStore;
        private readonly ObservableCollection<LocationViewModel> _locations;
        public IEnumerable<LocationViewModel> Locations => _locations;

        public RelayCommand LoadLocationsCommand { get; }
        public RelayCommand NavigateToCreateLocationCommand { get; }
        public RelayCommand<LocationViewModel> NavigateToEditLocationCommand { get; }
        public RelayCommand<LocationViewModel> RemoveLocationCommand { get; }

        public LocationListViewModel(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
            _unitOfWork = new UnitOfWork();
            _locations = new ObservableCollection<LocationViewModel>();

            LoadLocationsCommand = new RelayCommand(LoadData);
            RemoveLocationCommand = new RelayCommand<LocationViewModel>(RemoveLocation);
            NavigateToCreateLocationCommand = new RelayCommand(NavigateToCreateLocation);
            NavigateToEditLocationCommand = new RelayCommand<LocationViewModel>(NavigateToEditLocation);
        }

        public void NavigateToEditLocation(LocationViewModel locationViewModel)
        {
            _navigationStore.CurrentViewModel = EditLocationViewModel.LoadViewModel(_navigationStore, locationViewModel.Location);
        }


        public void NavigateToCreateLocation()
        {
            _navigationStore.CurrentViewModel = CreateLocationViewModel.LoadViewModel(_navigationStore);
        }

        public void RemoveLocation(LocationViewModel locationViewModel)
        {
            _unitOfWork.LocationRepository.Delete(locationViewModel.Location);
            _unitOfWork.Save();
            _locations.Remove(locationViewModel);
            MessageBox.Show("Successful");
        }

        public void LoadData()
        {
            foreach (Location r in _unitOfWork.LocationRepository.Get())
            {
                _locations.Add(new LocationViewModel(r));
            }
        }


        public static LocationListViewModel LoadViewModel(NavigationStore navigationStore)
        {
            LocationListViewModel viewModel = new LocationListViewModel(navigationStore);
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
                }

            }

            // call methods to cleanup the unamanaged resources

            _isDisposed = true;
            base.Dispose(disposing);
        }

    }
}
