using ProjectLex.InventoryManagement.Desktop.Commands;
using ProjectLex.InventoryManagement.Desktop.Collections;
using ProjectLex.InventoryManagement.Desktop.Models;
using ProjectLex.InventoryManagement.Desktop.Services;
using ProjectLex.InventoryManagement.Desktop.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ProjectLex.InventoryManagement.Desktop.ViewModels
{
    public class CategoryListViewModel : ViewModelBase
    {
        private bool _isDisposed = false;

        private readonly ObservableCollection<CategoryViewModel> _categories;
        public IEnumerable<CategoryViewModel> Categories => _categories;

        private readonly CategoryCollection _categoryCollection;
        private readonly NavigationStore _navigationStore;
        public ICommand LoadCategoriesCommand { get; }
        public ICommand RemoveCategoryCommand { get; }
        public ICommand NavigateToModifyCategoryCommand { get; }
        public ICommand NavigateToCreateCategoryCommand { get; }

        public CategoryListViewModel
            (
                NavigationStore navigationStore,
                CategoryCollection categoryCollection
            )
        {
            _navigationStore = navigationStore;
            _categoryCollection = categoryCollection;
            _categoryCollection.CategoryRemoved += OnCategoryRemoved;
            _categories = new ObservableCollection<CategoryViewModel>();
            LoadCategoriesCommand = new LoadDataCommand<Category>(_categoryCollection, OnDataLoaded);
            RemoveCategoryCommand = new RemoveDataCommand<Category>(_categoryCollection, CreateCategory, CanDelete);
            NavigateToModifyCategoryCommand = new NavigateCommand(NavigateToModifyCategory);
            NavigateToCreateCategoryCommand = new NavigateCommand(NavigateToCreateCategory);
        }

        public void NavigateToModifyCategory(object obj)
        {
            CategoryViewModel categoryViewModel = (CategoryViewModel)obj;
            _navigationStore.CurrentViewModel = ModifyCategoryViewModel.LoadViewModel(_navigationStore, _categoryCollection, categoryViewModel);
        }

        public void NavigateToCreateCategory(object obj)
        {
            _navigationStore.CurrentViewModel = CreateCategoryViewModel.LoadViewModel(_navigationStore, _categoryCollection);

        }

        public Category CreateCategory(object obj)
        {
            return new Category((CategoryViewModel)obj);
        }

        public static CategoryListViewModel LoadViewModel(NavigationStore navigationStore, CategoryCollection categoryCollection)
        {
            CategoryListViewModel viewModel = new CategoryListViewModel(navigationStore, categoryCollection);
            viewModel.LoadCategoriesCommand.Execute(null);

            return viewModel;
        }

        private void OnDataLoaded()
        {
            _categories.Clear();

            foreach (Category c in _categoryCollection.DataList)
            {
                CategoryViewModel newCategoryViewModel = new CategoryViewModel(c);
                _categories.Add(newCategoryViewModel);
            }

        }

        private void OnCategoryRemoved(Category category)
        {
            CategoryViewModel categoryViewModel = _categories.Where(c => c.CategoryID == category.CategoryID).First();
            _categories.Remove(categoryViewModel);
        }

        private bool CanDelete(object obj)
        {
            return true;
        }

        protected override void Dispose(bool disposing)
        {
            //Note: Implement finalizer only if the object have unmanaged resources

            if (!this._isDisposed)
            {
                if (disposing) // dispose all unamanage and managed resources
                {
                    // dispose resources here
                    _categoryCollection.CategoryRemoved -= OnCategoryRemoved;
                }

            }

            // call methods to cleanup the unamanaged resources

            _isDisposed = true;
            base.Dispose(disposing);
        }

        

    }
}
