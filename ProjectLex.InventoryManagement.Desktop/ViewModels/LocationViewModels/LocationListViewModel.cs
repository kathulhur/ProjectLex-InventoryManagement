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

        private bool _isDialogOpen = false;
        public bool IsDialogOpen => _isDialogOpen;

        private ViewModelBase _dialogViewModel;
        public ViewModelBase DialogViewModel => _dialogViewModel;

        private UnitOfWork _unitOfWork;
        private readonly NavigationStore _navigationStore;

        private PaginationHelper<LocationViewModel> _paginationHelper;
        public PaginationHelper<LocationViewModel> PaginationHelper => _paginationHelper;

        private readonly ObservableCollection<LocationViewModel> _locations;
        public ObservableCollection<LocationViewModel> Locations { get; }

        public RelayCommand LoadLocationsCommand { get; }
        public RelayCommand CreateLocationCommand { get; }
        public RelayCommand<LocationViewModel> EditLocationCommand { get; }
        public RelayCommand<LocationViewModel> RemoveLocationCommand { get; }

        public LocationListViewModel(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
            _unitOfWork = new UnitOfWork();
            _locations = new ObservableCollection<LocationViewModel>();
            Locations = new ObservableCollection<LocationViewModel>();

            _paginationHelper = new PaginationHelper<LocationViewModel>(_locations, Locations);

            LoadLocationsCommand = new RelayCommand(LoadData);
            RemoveLocationCommand = new RelayCommand<LocationViewModel>(RemoveLocation);
            CreateLocationCommand = new RelayCommand(CreateLocation);
            EditLocationCommand = new RelayCommand<LocationViewModel>(EditLocation);
        }

        public void EditLocation(LocationViewModel locationViewModel)
        {
            _dialogViewModel?.Dispose();
            _dialogViewModel = EditLocationViewModel.LoadViewModel(_navigationStore, _unitOfWork, locationViewModel.Location, CloseDialogCallback);
            OnPropertyChanged(nameof(DialogViewModel));

            SetProperty(ref _isDialogOpen, true, nameof(IsDialogOpen));
        }


        public void CreateLocation()
        {
            _dialogViewModel?.Dispose();
            _dialogViewModel = CreateLocationViewModel.LoadViewModel(_navigationStore, _unitOfWork, CloseDialogCallback);
            OnPropertyChanged(nameof(DialogViewModel));

            SetProperty(ref _isDialogOpen, true, nameof(IsDialogOpen));
        }

        public void RemoveLocation(LocationViewModel locationViewModel)
        {
            _unitOfWork.LocationRepository.Delete(locationViewModel.Location);
            _unitOfWork.Save();
            _locations.Remove(locationViewModel);
            _paginationHelper.RefreshCollection();
            MessageBox.Show("Successful");
        }

        public void CloseDialogCallback()
        {
            LoadLocationsCommand.Execute(null);
            SetProperty(ref _isDialogOpen, false, nameof(IsDialogOpen));
        }

        public void LoadData()
        {
            _locations.Clear();
            foreach (Location r in _unitOfWork.LocationRepository.Get())
            {
                _locations.Add(new LocationViewModel(r));
            }
            _paginationHelper.RefreshCollection();
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
                    _paginationHelper?.Dispose();
                }

            }

            // call methods to cleanup the unamanaged resources

            _isDisposed = true;
            base.Dispose(disposing);
        }

    }
}
