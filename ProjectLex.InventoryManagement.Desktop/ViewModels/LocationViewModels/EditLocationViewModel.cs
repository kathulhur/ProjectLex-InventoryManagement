using Microsoft.Toolkit.Mvvm.Input;
using ProjectLex.InventoryManagement.Database.Models;
using ProjectLex.InventoryManagement.Desktop.DAL;
using ProjectLex.InventoryManagement.Desktop.Stores;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ProjectLex.InventoryManagement.Desktop.ViewModels
{
    class EditLocationViewModel : ViewModelBase
    {
        private bool _isDisposed = false;

        private Location _location;

        public string _locationName;
        [Required(ErrorMessage = "Location name is Required")]
        public string LocationName
        {
            get => _locationName;
            set
            {
                SetProperty(ref _locationName, value);
            }

        }




        private readonly UnitOfWork _unitOfWork;
        private readonly NavigationStore _navigationStore;


        public event Action<Location> Submitted;

        protected virtual void OnSubmit(Location location)
        {
            Submitted?.Invoke(location);
        }

        public event Action Cancelled;

        protected virtual void OnCancel()
        {
            Cancelled?.Invoke();
        }

        public ICommand SubmitCommand { get; set; }
        public ICommand CancelCommand { get; set; }
        private readonly Action _closeDialogCallback;

        public EditLocationViewModel(NavigationStore navigationStore, UnitOfWork unitOfWork, Location location, Action closeDialogCallback)
        {
            _unitOfWork = new UnitOfWork();
            _navigationStore = navigationStore;
            _location = location;
            _closeDialogCallback = closeDialogCallback;

            SetInitialValues(_location);

            SubmitCommand = new RelayCommand(Submit);
            CancelCommand = new RelayCommand(Cancel);
        }

        private void SetInitialValues(Location location)
        {
            _locationName = location.LocationName;
        }


        public void Submit()
        {
            ValidateAllProperties();

            if (HasErrors)
            {
                return;
            }

            _location.LocationName = _locationName;

            _unitOfWork.LocationRepository.Update(_location);
            _unitOfWork.Save();

            MessageBox.Show("Success");
            _closeDialogCallback();
        }

        private void Cancel()
        {
            _closeDialogCallback();
        }

        public static EditLocationViewModel LoadViewModel(NavigationStore navigationStore, UnitOfWork unitOfWork, Location location, Action closeDialogCallback)
        {
            return new EditLocationViewModel(navigationStore, unitOfWork, location, closeDialogCallback);
        }


        protected override void Dispose(bool disposing)
        {
            if (!this._isDisposed)
            {
                if (disposing)
                {
                    // dispose managed resources
                }
                // dispose unmanaged resources
            }
            this._isDisposed = true;

            base.Dispose(disposing);
        }
    }
}
