using ProjectLex.InventoryManagement.Desktop.Collections;
using ProjectLex.InventoryManagement.Desktop.Commands;
using ProjectLex.InventoryManagement.Desktop.Models;
using ProjectLex.InventoryManagement.Desktop.Services;
using ProjectLex.InventoryManagement.Desktop.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ProjectLex.InventoryManagement.Desktop.ViewModels
{
    public class CreateCategoryViewModel : ViewModelBase
    {
        private bool _isDisposed = false;

        private readonly CategoryCollection _categoryCollection;
        private readonly NavigationStore _navigationStore;

        public ICommand SubmitCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        public CreateCategoryViewModel(NavigationStore navigationStore, CategoryCollection categoryCollection)
        {
            _navigationStore = navigationStore;
            _categoryCollection = categoryCollection;
            SubmitCommand = new CreateDataCommand<Category>(categoryCollection, CreateCategory, CanCreateCategory);
            CancelCommand = new NavigateCommand(NavigateToCategoryList);
        }

        public static CreateCategoryViewModel LoadViewModel(NavigationStore navigationStore, CategoryCollection collection)
        {
            return new CreateCategoryViewModel(navigationStore, collection);
        }

        public void NavigateToCategoryList(object obj)
        {
            _navigationStore.CurrentViewModel = CategoryListViewModel.LoadViewModel(_navigationStore, _categoryCollection);
        }
        
        public Category CreateCategory(object obj)
        {
            return new Category((CreateCategoryViewModel)obj);
        }
        private string _categoryId;

        public string CategoryId
        {
            get { return _categoryId; }
            set
            {
                _categoryId = value;
                OnPropertyChanged(nameof(CategoryId));
            }
        }

        private string _categoryName;

        public string CategoryName
        {
            get { return _categoryName; }
            set
            {
                _categoryName = value;
                OnPropertyChanged(nameof(CategoryName));
            }
        }

        private string _categoryStatus;

        public string CategoryStatus
        {
            get { return _categoryStatus; }
            set
            {
                _categoryStatus = value;
                OnPropertyChanged(nameof(CategoryStatus));
            }
        }

        private bool CanCreateCategory(object obj)
        {
            return true;
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
