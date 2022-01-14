using Microsoft.Toolkit.Mvvm.Input;
using ProjectLex.InventoryManagement.Database.Models;
using ProjectLex.InventoryManagement.Desktop.DAL;
using ProjectLex.InventoryManagement.Desktop.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ProjectLex.InventoryManagement.Desktop.ViewModels
{
    public class EditCategoryViewModel : ViewModelBase
    {
        private bool _isDisposed = false;

        private Category _category;


        public string CategoryName
        {
            get { return _category.CategoryName; }
            set
            {
                _category.CategoryName = value;
                OnPropertyChanged(nameof(CategoryName));
            }
        }

        public string CategoryStatus
        {
            get { return _category.CategoryStatus; }
            set
            {
                _category.CategoryStatus = value;
                OnPropertyChanged(nameof(CategoryStatus));
            }
        }
        private readonly UnitOfWork _unitOfWork;
        private readonly NavigationStore _navigationStore;

        public RelayCommand SubmitCommand { get; set; }
        public RelayCommand CancelCommand { get; set; }

        public EditCategoryViewModel(NavigationStore navigationStore, Category category)
        {
            _navigationStore = navigationStore;
            _category = category;
            SubmitCommand = new RelayCommand(EditCategory);
            CancelCommand = new RelayCommand(NavigateToCategoryList);
            _unitOfWork = new UnitOfWork();
        }


        private void NavigateToCategoryList()
        {
            _navigationStore.CurrentViewModel = CategoryListViewModel.LoadViewModel(_navigationStore);
        }


        private void EditCategory()
        {
            _unitOfWork.CategoryRepository.Update(_category);
            _unitOfWork.Save();
        }


        public static EditCategoryViewModel LoadViewModel(NavigationStore navigationStore, Category category)
        {
            EditCategoryViewModel viewModel = new EditCategoryViewModel(navigationStore, category);
            return viewModel;
        }
        
        
        protected override void Dispose(bool disposing)
        {
            if(!this._isDisposed)
            {
                if(disposing)
                {
                    // dispose managed resources
                    _unitOfWork.Dispose();
                }
                // dispose unmanaged resources
            }
            this._isDisposed = true;

            base.Dispose(disposing);
        }
    }
}
