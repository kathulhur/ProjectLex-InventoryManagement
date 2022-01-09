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
        public ICommand ToModifyCategoryNavigateCommand { get; }

        public CategoryListViewModel(CategoryCollection categoryCollection, NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
            _categoryCollection = categoryCollection;
            _categoryCollection.CategoryRemoved += OnCategoryRemoved;
            _categories = new ObservableCollection<CategoryViewModel>();
            LoadCategoriesCommand = new LoadDataCommand<Category>(_categoryCollection, OnDataLoaded);
            RemoveCategoryCommand = new RemoveDataCommand<Category>(_categoryCollection, CanDelete);
        }

        public static CategoryListViewModel LoadViewModel(CategoryCollection collection, NavigationStore navigationStore)
        {
            CategoryListViewModel viewModel = new CategoryListViewModel(collection, navigationStore);
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
            CategoryViewModel categoryViewModel = new CategoryViewModel(category);
            categoryViewModel = _categories.Where(c => c.CategoryID == category.CategoryID).First();
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
                }

            }

            // call methods to cleanup the unamanaged resources

            _isDisposed = true;
            base.Dispose(disposing);
        }

        

    }
}
