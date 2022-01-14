using ProjectLex.InventoryManagement.Desktop.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ProjectLex.InventoryManagement.Desktop.DAL;
using Microsoft.Toolkit.Mvvm.Input;
using ProjectLex.InventoryManagement.Database.Models;
using System.Windows;

namespace ProjectLex.InventoryManagement.Desktop.ViewModels
{
    public class CategoryListViewModel : ViewModelBase
    {
        private bool _isDisposed = false;

        private readonly ObservableCollection<CategoryViewModel> _categories;
        public IEnumerable<CategoryViewModel> Categories => _categories;

        private readonly UnitOfWork _unitOfWork;
        private readonly NavigationStore _navigationStore;
        public RelayCommand LoadCategoriesCommand { get; }
        public RelayCommand<CategoryViewModel> RemoveCategoryCommand { get; }
        public RelayCommand<CategoryViewModel> NavigateToModifyCategoryCommand { get; }
        public RelayCommand NavigateToCreateCategoryCommand { get; }

        public CategoryListViewModel(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
            _unitOfWork = new UnitOfWork();
            _categories = new ObservableCollection<CategoryViewModel>();
            LoadCategoriesCommand = new RelayCommand(LoadCategories);
            RemoveCategoryCommand = new RelayCommand<CategoryViewModel>(RemoveCategory);
            NavigateToModifyCategoryCommand = new RelayCommand<CategoryViewModel>(NavigateToModifyCategory);
            NavigateToCreateCategoryCommand = new RelayCommand(NavigateToCreateCategory);
        }

        public void RemoveCategory(CategoryViewModel categoryViewModel)
        {
            _unitOfWork.CategoryRepository.Delete(categoryViewModel.Category);
            _unitOfWork.Save();
            _categories.Remove(categoryViewModel);
            MessageBox.Show("Category Removed Successfully");
        }

        public void NavigateToModifyCategory(CategoryViewModel categoryViewModel)
        {
            _navigationStore.CurrentViewModel = EditCategoryViewModel.LoadViewModel(_navigationStore, categoryViewModel.Category);
        }

        public void NavigateToCreateCategory()
        {
            _navigationStore.CurrentViewModel = CreateCategoryViewModel.LoadViewModel(_navigationStore);

        }

        public void LoadCategories()
        {
            foreach(Category c in _unitOfWork.CategoryRepository.Get())
            {
                _categories.Add(new CategoryViewModel(c));
            }

        }

        public static CategoryListViewModel LoadViewModel(NavigationStore navigationStore)
        {
            CategoryListViewModel viewModel = new CategoryListViewModel(navigationStore);
            viewModel.LoadCategoriesCommand.Execute(null);

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
                }

            }

            // call methods to cleanup the unamanaged resources

            _isDisposed = true;
            base.Dispose(disposing);
        }

        

    }
}
