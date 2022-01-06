using ProjectLex.InventoryManagement.Database.Models;
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
    public class CategoryListViewModel : ViewModelBase, IUpdatable<Category>
    {
        private bool _isDisposed = false;

        private readonly ObservableCollection<CategoryViewModel> _categories;
        public IEnumerable<CategoryViewModel> Categories => _categories;

        private readonly CategoryCollection _categoryCollection;

        public ICommand CreateCategoryCommand { get; }

        public ICommand LoadCategoriesCommand { get; }

        public CategoryListViewModel(CategoryCollection categoryCollection, NavigationService<CreateCategoryViewModel> navigationService)
        {
            _categoryCollection = categoryCollection;
            _categories = new ObservableCollection<CategoryViewModel>();
            CreateCategoryCommand = new NavigateCommand<CreateCategoryViewModel>(navigationService);
            LoadCategoriesCommand = new LoadCategoriesCommand(this, _categoryCollection);

            _categoryCollection.DataCreated += OnCategoryCreated;

        }

        private void OnCategoryCreated(Category category)
        {
            CategoryViewModel newCategoryViewModel = new CategoryViewModel(category);
            _categories.Add(newCategoryViewModel);

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

        public static CategoryListViewModel LoadViewModel(CategoryCollection collection, NavigationService<CreateCategoryViewModel> navigationService)
        {
            CategoryListViewModel viewModel = new CategoryListViewModel(collection, navigationService);
            viewModel.LoadCategoriesCommand.Execute(null);

            return viewModel;
        }

        public void Update(IEnumerable<Category> categories)
        {
            _categories.Clear();

            foreach (Category c in categories)
            {
                CategoryViewModel newCategoryViewModel = new CategoryViewModel(c);
                _categories.Add(newCategoryViewModel);
            }
        }
    }
}
