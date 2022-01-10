using ProjectLex.InventoryManagement.Desktop.Collections;
using ProjectLex.InventoryManagement.Desktop.Commands;
using ProjectLex.InventoryManagement.Desktop.Stores;
using ProjectLex.InventoryManagement.Desktop.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ProjectLex.InventoryManagement.Desktop.Services
{
    public class ViewModelService
    {
        private readonly NavigationStore _navigationStore;
        private readonly CollectionStore _collectionStore;

        public NavigationStore NavigationStore => _navigationStore;

        private readonly ICommand _toCreateCategoryViewModelCommand;
        public ICommand ToCreateCategoryViewModelCommand => _toCreateCategoryViewModelCommand;


        private readonly ICommand _toCategoryListViewModelCommand;
        public ICommand ToCategoryListViewModelCommand => _toCategoryListViewModelCommand;



        private readonly ICommand _toCreateBrandViewModelCommand;
        public ICommand ToCreateBrandViewModelCommand => _toCreateBrandViewModelCommand;


        private readonly ICommand _toBrandListViewModelCommand;
        public ICommand ToBrandListViewModelCommand => _toBrandListViewModelCommand;



        private readonly ICommand _toCreateRoleViewModelCommand;
        public ICommand ToCreateRoleViewModelCommand => _toCreateRoleViewModelCommand;


        private readonly ICommand _toRoleListViewModelCommand;
        public ICommand ToRoleListViewModelCommand => _toRoleListViewModelCommand;



        private readonly ICommand _toCreateUserViewModelCommand;
        public ICommand ToCreateUserViewModelCommand => _toCreateUserViewModelCommand;

        private readonly ICommand _toUserListViewModelCommand;
        public ICommand ToUserListViewModelCommand => _toUserListViewModelCommand;



        private readonly ICommand _toCreateStoreViewModelCommand;
        public ICommand ToCreateStoreViewModelCommand => _toCreateStoreViewModelCommand;

        private readonly ICommand _toStoreListViewModelCommand;
        public ICommand ToStoreListViewModelCommand => _toStoreListViewModelCommand;

        public ViewModelService
            (
                NavigationStore navigationStore,
                CollectionStore collectionStore
            )
        {
            _navigationStore = navigationStore;
            _collectionStore = collectionStore;

            _toCreateCategoryViewModelCommand = new NavigateCommand<CreateCategoryViewModel>(new NavigationService<CreateCategoryViewModel>(navigationStore, MakeCreateCategoryViewModel));
            _toCategoryListViewModelCommand = new NavigateCommand<CategoryListViewModel>(new NavigationService<CategoryListViewModel>(navigationStore, MakeCategoryListViewModel));
            

            _toCreateBrandViewModelCommand = new NavigateCommand<CreateBrandViewModel>(new NavigationService<CreateBrandViewModel>(navigationStore, MakeCreateBrandViewModel));
            _toBrandListViewModelCommand = new NavigateCommand<BrandListViewModel>(new NavigationService<BrandListViewModel>(navigationStore, MakeBrandListViewModel));

            
            _toCreateRoleViewModelCommand = new NavigateCommand<CreateRoleViewModel>(new NavigationService<CreateRoleViewModel>(navigationStore, MakeCreateRoleViewModel));
            _toRoleListViewModelCommand = new NavigateCommand<RoleListViewModel>(new NavigationService<RoleListViewModel>(navigationStore, MakeRoleListViewModel));
            

            _toCreateUserViewModelCommand = new NavigateCommand<CreateUserViewModel>(new NavigationService<CreateUserViewModel>(navigationStore, MakeCreateUserViewModel));
            _toUserListViewModelCommand = new NavigateCommand<UserListViewModel>(new NavigationService<UserListViewModel>(navigationStore, MakeUserListViewModel));


            _toCreateStoreViewModelCommand = new NavigateCommand<CreateStoreViewModel>(new NavigationService<CreateStoreViewModel>(navigationStore, MakeCreateStoreViewModel));
            _toStoreListViewModelCommand = new NavigateCommand<StoreListViewModel>(new NavigationService<StoreListViewModel>(navigationStore, MakeStoreListViewModel));
        }


        public CreateCategoryViewModel MakeCreateCategoryViewModel()
        {
            return CreateCategoryViewModel.LoadViewModel(_collectionStore.CategoryCollection);
        }

        public CategoryListViewModel MakeCategoryListViewModel()
        {
            return CategoryListViewModel.LoadViewModel(_collectionStore.CategoryCollection, _navigationStore);
        }


        public CreateStoreViewModel MakeCreateStoreViewModel()
        {
            return CreateStoreViewModel.LoadViewModel(_collectionStore.StoreCollection);
        }

        public StoreListViewModel MakeStoreListViewModel()
        {
            return StoreListViewModel.LoadViewModel(_collectionStore.StoreCollection, _navigationStore);
        }


        public CreateBrandViewModel MakeCreateBrandViewModel()
        {
            return CreateBrandViewModel.LoadViewModel(_collectionStore.BrandCollection);
        }

        public BrandListViewModel MakeBrandListViewModel()
        {
            return BrandListViewModel.LoadViewModel(_collectionStore.BrandCollection, _navigationStore);
        }



        public CreateRoleViewModel MakeCreateRoleViewModel()
        {
            return CreateRoleViewModel.LoadViewModel(_collectionStore.RoleCollection);
        }

        public RoleListViewModel MakeRoleListViewModel()
        {
            return RoleListViewModel.LoadViewModel(_collectionStore.RoleCollection, _navigationStore);
        }



        public CreateUserViewModel MakeCreateUserViewModel()
        {
            return CreateUserViewModel.LoadViewModel(_collectionStore.UserCollection, _collectionStore.RoleCollection);
        }

        public UserListViewModel MakeUserListViewModel()
        {
            return UserListViewModel.LoadViewModel(_collectionStore.UserCollection, _collectionStore.RoleCollection, _navigationStore);
        }

        //public CreateProductViewModel MakeCreateProductViewModel()
        //{
        //    return CreateProductViewModel.LoadViewModel(
        //        _collectionStore.ProductCollection,
        //        _collectionStore.CategoryCollection,
        //        _collectionStore.SupplierCollection,
        //        new NavigationService<ProductListViewModel>(_navigationStore, MakeProductListViewModel));
        //}

        //public ProductListViewModel MakeProductListViewModel()
        //{
        //    return ProductListViewModel.LoadViewModel(
        //        _collectionStore.ProductCollection,
        //        _collectionStore.CategoryCollection,
        //        _collectionStore.SupplierCollection, 
        //        new NavigationService<CreateProductViewModel>(_navigationStore, MakeCreateProductViewModel));
        //}

    }
}
