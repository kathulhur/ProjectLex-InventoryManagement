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
        public CollectionStore CollectionStore => _collectionStore;

        private readonly ICommand _toCategoryListViewModelCommand;
        public ICommand ToCategoryListViewModelCommand => _toCategoryListViewModelCommand;


        private readonly ICommand _toBrandListViewModelCommand;
        public ICommand ToBrandListViewModelCommand => _toBrandListViewModelCommand;


        private readonly ICommand _toRoleListViewModelCommand;
        public ICommand ToRoleListViewModelCommand => _toRoleListViewModelCommand;


        private readonly ICommand _toUserListViewModelCommand;
        public ICommand ToUserListViewModelCommand => _toUserListViewModelCommand;


        private readonly ICommand _toStoreListViewModelCommand;
        public ICommand ToStoreListViewModelCommand => _toStoreListViewModelCommand;

        private readonly ICommand _toOrderListViewModelCommand;
        public ICommand ToOrderListViewModelCommand => _toOrderListViewModelCommand;

        private readonly ICommand _toAttributeListViewModelCommand;
        public ICommand ToAttributeListViewModelCommand => _toAttributeListViewModelCommand;


        public ViewModelService(NavigationStore navigationStore, CollectionStore collectionStore)
        {
            _navigationStore = navigationStore;
            _collectionStore = collectionStore;

            _toCategoryListViewModelCommand = new NavigateCommand(NavigateToCategoryList);
            _toBrandListViewModelCommand = new NavigateCommand(NavigateToBrandList);
            _toRoleListViewModelCommand = new NavigateCommand(NavigateToRoleList);
            _toUserListViewModelCommand = new NavigateCommand(NavigateToUserList);
            _toStoreListViewModelCommand = new NavigateCommand(NavigateToStoreList);
            _toAttributeListViewModelCommand = new NavigateCommand(NavigateToAttributeList);
        }

        public void NavigateToCategoryList(object obj)
        {
            _navigationStore.CurrentViewModel = CategoryListViewModel.LoadViewModel(NavigationStore, _collectionStore.CategoryCollection);
        }


        public void NavigateToBrandList(object ojb)
        {
            _navigationStore.CurrentViewModel = BrandListViewModel.LoadViewModel(_navigationStore, _collectionStore.BrandCollection);
        }

        public void NavigateToStoreList(object obj)
        {
            _navigationStore.CurrentViewModel = StoreListViewModel.LoadViewModel(_navigationStore, _collectionStore.StoreCollection);
        }

        public void NavigateToRoleList(object obj)
        {
            _navigationStore.CurrentViewModel = RoleListViewModel.LoadViewModel(_navigationStore, _collectionStore.RoleCollection);
        }

        public void NavigateToUserList(object obj)
        {
            _navigationStore.CurrentViewModel = UserListViewModel.LoadViewModel(_navigationStore, _collectionStore.UserCollection, _collectionStore.RoleCollection);
        }

        public void NavigateToAttributeList(object obj)
        {
            _navigationStore.CurrentViewModel = AttributeListViewModel.LoadViewModel(_navigationStore, _collectionStore.AttributeCollection);
        }


    }
}
