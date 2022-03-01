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
using ProjectLex.InventoryManagement.Desktop.ViewModels.ListViewHelpers;

namespace ProjectLex.InventoryManagement.Desktop.ViewModels
{
    public class CategoryListViewModel : ViewModelBase
    {
        private bool _isDisposed = false;

        private bool _isDialogOpen = false;

        public bool IsDialogOpen
        {
            get { return _isDialogOpen; }
        }

        private ViewModelBase _dialogViewModel;
        public ViewModelBase DialogViewModel => _dialogViewModel;

        private readonly ObservableCollection<CategoryViewModel> _categories;
        public ObservableCollection<CategoryViewModel> Categories { get; }

        private readonly UnitOfWork _unitOfWork;
        private readonly NavigationStore _navigationStore;


        public CategoryListViewHelper CategoryListViewHelper { get; }

        public RelayCommand LoadCategoriesCommand { get; }
        public RelayCommand<CategoryViewModel> RemoveCategoryCommand { get; }
        public RelayCommand<CategoryViewModel> EditCategoryCommand { get; }
        public RelayCommand CreateCategoryCommand { get; }


        public CategoryListViewModel(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
            _unitOfWork = new UnitOfWork();
            _categories = new ObservableCollection<CategoryViewModel>();
            Categories = new ObservableCollection<CategoryViewModel>();
            CategoryListViewHelper = new CategoryListViewHelper(_categories, Categories);
            LoadCategoriesCommand = new RelayCommand(LoadCategories);
            RemoveCategoryCommand = new RelayCommand<CategoryViewModel>(RemoveCategory);
            EditCategoryCommand = new RelayCommand<CategoryViewModel>(EditCategory);
            CreateCategoryCommand = new RelayCommand(CreateCategory);
        }

        public void RemoveCategory(CategoryViewModel categoryViewModel)
        {
            var result = MessageBox.Show("Do you really want to remove this item?", "Warning", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                _unitOfWork.CategoryRepository.Delete(categoryViewModel.Category);
                _unitOfWork.Save();
                _categories.Remove(categoryViewModel);
                CategoryListViewHelper.RefreshCollection();
                MessageBox.Show("Category Removed Successfully");
            }
        }


        public void EditCategory(CategoryViewModel categoryViewModel)
        {
            _dialogViewModel?.Dispose();
            _dialogViewModel = EditCategoryViewModel.LoadViewModel(_navigationStore, _unitOfWork, categoryViewModel.Category, CloseDialogCallback);
            OnPropertyChanged(nameof(DialogViewModel));

            _isDialogOpen = true;
            OnPropertyChanged(nameof(IsDialogOpen));
        }

        public void CreateCategory()
        {
            _dialogViewModel?.Dispose();
            _dialogViewModel = CreateCategoryViewModel.LoadViewModel(_navigationStore, _unitOfWork, CloseDialogCallback);
            OnPropertyChanged(nameof(DialogViewModel));

            _isDialogOpen = true;
            OnPropertyChanged(nameof(IsDialogOpen));

        }

        public void LoadCategories()
        {
            _categories.Clear();
            foreach(Category c in _unitOfWork.CategoryRepository.Get())
            {
                _categories.Add(new CategoryViewModel(c));
            }
            CategoryListViewHelper.RefreshCollection();
        }

        public static CategoryListViewModel LoadViewModel(NavigationStore navigationStore)
        {
            CategoryListViewModel viewModel = new CategoryListViewModel(navigationStore);
            viewModel.LoadCategoriesCommand.Execute(null);
            return viewModel;
        }

        public void CloseDialogCallback()
        {
            LoadCategoriesCommand.Execute(null);

            _isDialogOpen = false;
            OnPropertyChanged(nameof(IsDialogOpen));
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
                    _dialogViewModel?.Dispose();
                    CategoryListViewHelper?.Dispose();
                }

            }

            // call methods to cleanup the unamanaged resources

            _isDisposed = true;
            base.Dispose(disposing);
        }

        

    }
}
