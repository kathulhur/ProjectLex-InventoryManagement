using Microsoft.Toolkit.Mvvm.Input;
using ProjectLex.InventoryManagement.Database.Models;
using ProjectLex.InventoryManagement.Desktop.DAL;
using ProjectLex.InventoryManagement.Desktop.Stores;
using ProjectLex.InventoryManagement.Desktop.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using static ProjectLex.InventoryManagement.Desktop.Utilities.Constants;

namespace ProjectLex.InventoryManagement.Desktop.ViewModels
{
    public class CreateCategoryViewModel : ViewModelBase
    {

        private bool _isDisposed = false;

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

        
        private readonly UnitOfWork _unitOfWork;
        private readonly NavigationStore _navigationStore;

        private readonly Action _closeDialogCallback;
        public ICommand SubmitCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        public CreateCategoryViewModel(NavigationStore navigationStore, UnitOfWork unitOfWork, Action closeDialogCallback)
        {
            _unitOfWork = unitOfWork;
            _navigationStore = navigationStore;
            _closeDialogCallback = closeDialogCallback;
            SubmitCommand = new RelayCommand(Submit);
            CancelCommand = new RelayCommand(Cancel);
        }


        public void Submit()
        {
            ValidateAllProperties();

            if(HasErrors)
            {
                return;
            }

            Category newCategory = new Category()
            {
                CategoryID = Guid.NewGuid(),
                CategoryName = CategoryName,
                CategoryDescription = CategoryDescription,
                CategoryStatus = CategoryStatus
            };

            _unitOfWork.CategoryRepository.Insert(newCategory);
            _unitOfWork.LogRepository.Insert(LogUtil.CreateLog(LogCategory.CATEGORIES, ActionType.CREATE, $"New category created; CategoryID:{newCategory.CategoryID};"));
            _unitOfWork.Save();
            _closeDialogCallback();
        }

        private void Cancel()
        {
            _closeDialogCallback();
        }


        public static CreateCategoryViewModel LoadViewModel(NavigationStore navigationStore, UnitOfWork unitOfWork, Action closeDialogCallback)
        {
            return new CreateCategoryViewModel(navigationStore, unitOfWork, closeDialogCallback);
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
