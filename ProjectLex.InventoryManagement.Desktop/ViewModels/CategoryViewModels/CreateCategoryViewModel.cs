using Microsoft.Toolkit.Mvvm.Input;
using ProjectLex.InventoryManagement.Database.Models;
using ProjectLex.InventoryManagement.Desktop.DAL;
using ProjectLex.InventoryManagement.Desktop.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ProjectLex.InventoryManagement.Desktop.ViewModels
{
    public class CreateCategoryViewModel : ViewModelBase
    {

        private bool _isDisposed = false;

        public string _categoryName;

        [Required(ErrorMessage = "Name is Required")]
        [MinLength(2, ErrorMessage = "Name should be longer than 2 characters")]
        [MaxLength(50, ErrorMessage = "Name longer than 50 characters is Not Allowed")]
        public string CategoryName
        {
            get => _categoryName;
            set
            {
                SetProperty(ref _categoryName, value);
            }
        }



        private string _categoryDescription;

        [Required(ErrorMessage = "Description is Required")]
        [MinLength(10, ErrorMessage = "Description should be longer than 2 characters")]
        [MaxLength(50, ErrorMessage = "Description longer than 50 characters is Not Allowed")]
        public string CategoryDescription
        {
            get => _categoryDescription;
            set
            {
                SetProperty(ref _categoryDescription, value);
            }
        }


       

        private string _categoryStatus;

        [Required(ErrorMessage = "Status is Required")]
        public string CategoryStatus
        {
            get { return _categoryStatus; }
            set
            {
                SetProperty(ref _categoryStatus, value);
            }
        }

        
        private readonly UnitOfWork _unitOfWork;
        private readonly NavigationStore _navigationStore;

        private readonly Action _closeDialogCallback;
        public ICommand SubmitCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        public CreateCategoryViewModel(NavigationStore navigationStore, Action closeDialogCallback)
        {
            _unitOfWork = new UnitOfWork();
            _navigationStore = navigationStore;
            _closeDialogCallback = closeDialogCallback;
            SubmitCommand = new RelayCommand(CreateCategory);
            CancelCommand = new RelayCommand(CancelCreateCategory);
        }

        public static CreateCategoryViewModel LoadViewModel(NavigationStore navigationStore, Action closeDialogCallback)
        {
            return new CreateCategoryViewModel(navigationStore, closeDialogCallback);
        }

        public void NavigateToCategoryList()
        {
            _navigationStore.CurrentViewModel = CategoryListViewModel.LoadViewModel(_navigationStore);
        }

        public void CreateCategory()
        {
            ValidateAllProperties();

            if(HasErrors)
            {
                return;
            }

            Category category = new Category()
            {
                CategoryID = Guid.NewGuid(),
                CategoryName = CategoryName,
                CategoryDescription = CategoryDescription,
                CategoryStatus = CategoryStatus
            };

            _unitOfWork.CategoryRepository.Insert(category);
            _unitOfWork.Save();
            _closeDialogCallback();
        }

        private void CancelCreateCategory()
        {
            _closeDialogCallback();
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
