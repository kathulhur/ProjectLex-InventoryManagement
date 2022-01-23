using Microsoft.Toolkit.Mvvm.Input;
using ProjectLex.InventoryManagement.Database.Models;
using ProjectLex.InventoryManagement.Desktop.DAL;
using ProjectLex.InventoryManagement.Desktop.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ProjectLex.InventoryManagement.Desktop.ViewModels
{
    public class StaffListViewModel : ViewModelBase
    {

        private bool _isDisposed = false;

        private bool _isDialogOpen = false;
        public bool IsDialogOpen => _isDialogOpen;

        private ViewModelBase _dialogViewModel;
        public ViewModelBase DialogViewModel => _dialogViewModel;

        private readonly NavigationStore _navigationStore;
        private readonly UnitOfWork _unitOfWork;

        private readonly ObservableCollection<StaffViewModel> _staffs;
        public IEnumerable<StaffViewModel> Staffs => _staffs;
        
        public RelayCommand CreateStaffCommand { get; }
        public RelayCommand LoadStaffsCommand { get; }
        public RelayCommand<StaffViewModel> RemoveStaffCommand { get; }
        public RelayCommand<StaffViewModel> EditStaffCommand { get; }

        public StaffListViewModel(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
            _unitOfWork = new UnitOfWork();

            _staffs = new ObservableCollection<StaffViewModel>();

            LoadStaffsCommand = new RelayCommand(LoadStaffs);
            RemoveStaffCommand = new RelayCommand<StaffViewModel>(RemoveStaff);
            EditStaffCommand = new RelayCommand<StaffViewModel>(EditStaff);
            CreateStaffCommand = new RelayCommand(CreateStaff);

        }


        private void RemoveStaff(StaffViewModel staffViewModel)
        {
            _unitOfWork.StaffRepository.Delete(staffViewModel.Staff);
            _unitOfWork.Save();
            _staffs.Remove(staffViewModel);
            MessageBox.Show("Successful");
        }


        private void EditStaff(StaffViewModel staffViewModel)
        {
            _dialogViewModel?.Dispose();
            _dialogViewModel = EditStaffViewModel.LoadViewModel(_navigationStore, staffViewModel.Staff, CloseDialogCallback);
            OnPropertyChanged(nameof(DialogViewModel));

            _isDialogOpen = true;
            OnPropertyChanged(nameof(IsDialogOpen));
        }

        private void CreateStaff()
        {
            _dialogViewModel?.Dispose();
            _dialogViewModel = CreateStaffViewModel.LoadViewModel(_navigationStore, CloseDialogCallback);
            OnPropertyChanged(nameof(DialogViewModel));

            _isDialogOpen = true;
            OnPropertyChanged(nameof(IsDialogOpen));
        }

        private void CloseDialogCallback()
        {
            LoadStaffsCommand.Execute(null);

            _isDialogOpen = false;
            OnPropertyChanged(nameof(IsDialogOpen));
        }

        private void LoadStaffs()
        {
            _staffs.Clear();
            foreach(Staff u in _unitOfWork.StaffRepository.Get(includeProperties: "Role"))
            {
                _staffs.Add(new StaffViewModel(u));
            }
        }

        public static StaffListViewModel LoadViewModel(NavigationStore navigationStore)
        {
            StaffListViewModel viewModel = new StaffListViewModel(navigationStore);
            viewModel.LoadStaffsCommand.Execute(null);

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
                    _dialogViewModel?.Dispose();
                }

            }

            // call methods to cleanup the unamanaged resources

            _isDisposed = true;
            base.Dispose(disposing);
        }
    }
}
