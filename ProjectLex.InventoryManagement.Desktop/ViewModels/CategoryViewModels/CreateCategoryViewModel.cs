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
    public class CreateCategoryViewModel : ViewModelBase
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

        public ICommand SubmitCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        public CreateCategoryViewModel(NavigationStore navigationStore)
        {
            _unitOfWork = new UnitOfWork();
            _navigationStore = navigationStore;
            SubmitCommand = new RelayCommand(CreateCategory);
            CancelCommand = new RelayCommand(NavigateToCategoryList);

            _category = new Category()
            {
                CategoryID = Guid.NewGuid()
            };
        }

        public static CreateCategoryViewModel LoadViewModel(NavigationStore navigationStore)
        {
            return new CreateCategoryViewModel(navigationStore);
        }

        public void NavigateToCategoryList()
        {
            _navigationStore.CurrentViewModel = CategoryListViewModel.LoadViewModel(_navigationStore);
        }
        
        public void CreateCategory()
        {
            _unitOfWork.CategoryRepository.Insert(_category);
            _unitOfWork.Save();

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
                    _unitOfWork.Dispose();
                }
                // dispose unmanaged resources
            }
            this._isDisposed = true;

            base.Dispose(disposing);
        }

    }
}
