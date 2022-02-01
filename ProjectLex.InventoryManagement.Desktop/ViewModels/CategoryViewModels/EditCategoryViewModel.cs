using Microsoft.Toolkit.Mvvm.Input;
using ProjectLex.InventoryManagement.Database.Models;
using ProjectLex.InventoryManagement.Desktop.DAL;
using ProjectLex.InventoryManagement.Desktop.Stores;
using ProjectLex.InventoryManagement.Desktop.Utilities;
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
    public class EditCategoryViewModel : ViewModelBase
    {
        private bool _isDisposed = false;

        private Category _category;

        public string _categoryName;

        [Required(ErrorMessage = "Name is Required")]
        [MinLength(2, ErrorMessage = "Name should be at least 2 characters long")]
        [MaxLength(50, ErrorMessage = "Name longer than 50 characters is Not Allowed")]
        public string CategoryName
        {
            get => _categoryName;
            set
            {
                SetProperty(ref _categoryName, value, true);
            }

        }

        private string _categoryDescription;

        [Required(ErrorMessage = "Description is Required")]
        [MinLength(10, ErrorMessage = "Description should be at least 10 characters long")]
        [MaxLength(50, ErrorMessage = "Description longer than 50 characters is Not Allowed")]
        public string CategoryDescription
        {
            get => _categoryDescription;
            set
            {
                SetProperty(ref _categoryDescription, value, true);
            }
        }



        private string _categoryStatus;

        [Required(ErrorMessage = "Status is Required")]
        public string CategoryStatus
        {
            get { return _categoryStatus; }
            set
            {
                SetProperty(ref _categoryStatus, value, true);
            }
        }


        public IEnumerable<string> _statuses;
        public IEnumerable<string> Statuses => _statuses;

        private readonly UnitOfWork _unitOfWork;
        private readonly NavigationStore _navigationStore;

        private readonly Action _closeDialogCallback;
        public RelayCommand SubmitCommand { get; set; }
        public RelayCommand CancelCommand { get; set; }

        public EditCategoryViewModel(NavigationStore navigationStore, UnitOfWork unitOfWork, Category category, Action closeDialogCallback)
        {
            _navigationStore = navigationStore;
            _category = category;
            _unitOfWork = unitOfWork;
            _closeDialogCallback = closeDialogCallback;
            SetInitialValues(_category);

            SubmitCommand = new RelayCommand(Submit);
            CancelCommand = new RelayCommand(Cancel);

            _categoryName = _category.CategoryName;
            _categoryDescription = _category.CategoryDescription;

            CategoryStatus = Constants.Statuses.Where(s => s.Equals(_category.CategoryStatus)).FirstOrDefault();
        }

        private void SetInitialValues(Category category)
        {
            _categoryName = category.CategoryName;
            _categoryDescription = category.CategoryDescription;
        }


        private void Cancel()
        {
            _closeDialogCallback();
        }


        private void Submit()
        {
            ValidateAllProperties();

            if (HasErrors)
            {
                return;
            }
            

            _category.CategoryName = CategoryName;
            _category.CategoryDescription = CategoryDescription;
            _category.CategoryStatus = CategoryStatus;


            _unitOfWork.CategoryRepository.Update(_category);
            _unitOfWork.Save();

            _closeDialogCallback();
        }


        public static EditCategoryViewModel LoadViewModel(NavigationStore navigationStore, UnitOfWork unitOfWork, Category category, Action closeDialogCallback)
        {
            EditCategoryViewModel viewModel = new EditCategoryViewModel(navigationStore, unitOfWork, category, closeDialogCallback);
            return viewModel;
        }
        
        
        protected override void Dispose(bool disposing)
        {
            if(!this._isDisposed)
            {
                if(disposing)
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
