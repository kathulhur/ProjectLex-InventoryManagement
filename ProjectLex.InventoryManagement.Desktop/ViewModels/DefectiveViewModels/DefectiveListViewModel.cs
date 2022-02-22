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
    public class DefectiveListViewModel : ViewModelBase
    {

        private bool _isDisposed = false;

        private bool _isDialogOpen = false;
        public bool IsDialogOpen => _isDialogOpen;

        private ViewModelBase _dialogViewModel;
        public ViewModelBase DialogViewModel => _dialogViewModel;

        private readonly NavigationStore _navigationStore;
        private readonly UnitOfWork _unitOfWork;

        private PaginationHelper<DefectiveViewModel> _paginationHelper;
        public PaginationHelper<DefectiveViewModel> PaginationHelper => _paginationHelper;

        private readonly ObservableCollection<DefectiveViewModel> _defectives;
        public ObservableCollection<DefectiveViewModel> Defectives { get; }

        public RelayCommand CreateDefectiveCommand { get; }
        public RelayCommand LoadDefectivesCommand { get; }
        public RelayCommand<DefectiveViewModel> RemoveDefectiveCommand { get; }
        public RelayCommand<DefectiveViewModel> EditDefectiveCommand { get; }

        public DefectiveListViewModel(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
            _unitOfWork = new UnitOfWork();

            _defectives = new ObservableCollection<DefectiveViewModel>();
            Defectives = new ObservableCollection<DefectiveViewModel>();
            _paginationHelper = new PaginationHelper<DefectiveViewModel>(_defectives, Defectives);

            LoadDefectivesCommand = new RelayCommand(LoadDefectives);
            RemoveDefectiveCommand = new RelayCommand<DefectiveViewModel>(RemoveDefective);
            EditDefectiveCommand = new RelayCommand<DefectiveViewModel>(EditDefective);
            CreateDefectiveCommand = new RelayCommand(CreateDefective);

        }


        private void RemoveDefective(DefectiveViewModel defectiveViewModel)
        {
            _unitOfWork.DefectiveRepository.Delete(defectiveViewModel.Defective);
            _unitOfWork.Save();
            _defectives.Remove(defectiveViewModel);
            _paginationHelper.RefreshCollection();
            MessageBox.Show("Successful");
        }


        private void EditDefective(DefectiveViewModel defectiveViewModel)
        {
            _dialogViewModel?.Dispose();
            _dialogViewModel = EditDefectiveViewModel.LoadViewModel(_navigationStore, _unitOfWork, defectiveViewModel.Defective, CloseDialogCallback);
            OnPropertyChanged(nameof(DialogViewModel));

            _isDialogOpen = true;
            OnPropertyChanged(nameof(IsDialogOpen));
        }

        private void CreateDefective()
        {
            _dialogViewModel?.Dispose();
            _dialogViewModel = CreateDefectiveViewModel.LoadViewModel(_navigationStore, _unitOfWork, CloseDialogCallback);
            OnPropertyChanged(nameof(DialogViewModel));

            _isDialogOpen = true;
            OnPropertyChanged(nameof(IsDialogOpen));
        }

        private void CloseDialogCallback()
        {
            LoadDefectives();

            _isDialogOpen = false;
            OnPropertyChanged(nameof(IsDialogOpen));
        }

        private void LoadDefectives()
        {
            _defectives.Clear();
            foreach (Defective u in _unitOfWork.DefectiveRepository.Get(includeProperties: "Product"))
            {
                _defectives.Add(new DefectiveViewModel(u));
            }
            _paginationHelper.RefreshCollection();
        }

        public static DefectiveListViewModel LoadViewModel(NavigationStore navigationStore)
        {
            DefectiveListViewModel viewModel = new DefectiveListViewModel(navigationStore);
            viewModel.LoadDefectivesCommand.Execute(null);

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
                    _paginationHelper?.Dispose();
                }

            }

            // call methods to cleanup the unamanaged resources

            _isDisposed = true;
            base.Dispose(disposing);
        }
    }
}
