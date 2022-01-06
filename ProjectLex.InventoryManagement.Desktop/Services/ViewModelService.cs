using ProjectLex.InventoryManagement.Desktop.Collections;
using ProjectLex.InventoryManagement.Desktop.Stores;
using ProjectLex.InventoryManagement.Desktop.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLex.InventoryManagement.Desktop.Services
{
    public class ViewModelService
    {
        private readonly NavigationStore _navigationStore;
        private readonly CollectionStore _collectionStore;

        public NavigationStore NavigationStore => _navigationStore;
        public ViewModelService
            (
                NavigationStore navigationStore,
                CollectionStore collectionStore
            )
        {
            _navigationStore = navigationStore;
            _collectionStore = collectionStore;
        }

        public CreateCategoryViewModel MakeCreateCategoryViewModel()
        {
            return CreateCategoryViewModel.LoadViewModel(
                _collectionStore.CategoryCollection, 
                new NavigationService<CategoryListViewModel>(_navigationStore, MakeCategoryListViewModel));
        }

        public CategoryListViewModel MakeCategoryListViewModel()
        {
            return CategoryListViewModel.LoadViewModel(
                _collectionStore.CategoryCollection, 
                new NavigationService<CreateCategoryViewModel>(_navigationStore, MakeCreateCategoryViewModel));
        }

        public CreateSupplierViewModel MakeCreateSupplierViewModel()
        {
            return CreateSupplierViewModel.LoadViewModel(
                _collectionStore.SupplierCollection, 
                new NavigationService<SupplierListViewModel>(_navigationStore, MakeSupplierListViewModel));
        }

        public SupplierListViewModel MakeSupplierListViewModel()
        {
            return SupplierListViewModel.LoadViewModel(
                _collectionStore.SupplierCollection, 
                new NavigationService<CreateSupplierViewModel>(_navigationStore, MakeCreateSupplierViewModel));
        }

        public CreateProductViewModel MakeCreateProductViewModel()
        {
            return CreateProductViewModel.LoadViewModel(
                _collectionStore.ProductCollection,
                _collectionStore.CategoryCollection,
                _collectionStore.SupplierCollection,
                new NavigationService<ProductListViewModel>(_navigationStore, MakeProductListViewModel));
        }

        public ProductListViewModel MakeProductListViewModel()
        {
            return ProductListViewModel.LoadViewModel(
                _collectionStore.ProductCollection,
                _collectionStore.CategoryCollection,
                _collectionStore.SupplierCollection, 
                new NavigationService<CreateProductViewModel>(_navigationStore, MakeCreateProductViewModel));
        }

    }
}
