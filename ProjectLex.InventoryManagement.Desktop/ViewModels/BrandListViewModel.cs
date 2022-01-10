using ProjectLex.InventoryManagement.Desktop.Collections;
using ProjectLex.InventoryManagement.Desktop.Commands;
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
    public class BrandListViewModel : ViewModelBase
    {

        private bool _isDisposed = false;
        private readonly NavigationStore _navigationStore;
        private readonly ObservableCollection<BrandViewModel> _brands;
        public IEnumerable<BrandViewModel> Brands => _brands;

        private readonly BrandCollection _brandCollection;

        public ICommand ToCreateBrandCommand { get; }
        public ICommand LoadBrandsCommand { get; }
        public ICommand RemoveBrandCommand { get; }
        public ICommand NavigateToModifyBrandCommand { get; }

        public BrandListViewModel(BrandCollection brandCollection, NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
            _brandCollection = brandCollection;
            _brandCollection.BrandRemoved += OnBrandRemoved;
            _brands = new ObservableCollection<BrandViewModel>();
            LoadBrandsCommand = new LoadDataCommand<Brand>(_brandCollection, OnBrandLoaded);
            RemoveBrandCommand = new RemoveDataCommand<Brand>(_brandCollection, CreateBrand, CanRemoveBrand);
            NavigateToModifyBrandCommand = new ModifyDataNavigateCommand(NavigateToModifyBrand);

        }

        public Brand CreateBrand(object obj)
        {
            return new Brand((BrandViewModel)obj);
        }

        public void NavigateToModifyBrand(object obj)
        {
            BrandViewModel brandViewModel = (BrandViewModel)obj;
            _navigationStore.CurrentViewModel = ModifyBrandViewModel.LoadViewModel(_brandCollection, brandViewModel);

        }

        public static BrandListViewModel LoadViewModel(BrandCollection brandCollection, NavigationStore navigationStore)
        {
            BrandListViewModel viewModel = new BrandListViewModel(brandCollection, navigationStore);
            viewModel.LoadBrandsCommand.Execute(null);
            return viewModel;

            
        }

        public void OnBrandRemoved(Brand brand)
        {
            BrandViewModel brandViewModel = new BrandViewModel(brand);
            BrandViewModel removedBrandViewModel = _brands.Where(b => b.BrandID == brand.BrandID).First();
            _brands.Remove(removedBrandViewModel);
        }
        
        public bool CanRemoveBrand(object obj)
        {
            return true;
        }

        private void OnBrandLoaded()
        {
            _brands.Clear();

            foreach (Brand b in _brandCollection.DataList)
            {
                BrandViewModel brandViewModel = new BrandViewModel(b);
                _brands.Add(brandViewModel);
            }

        }

        protected override void Dispose(bool disposing)
        {
            //Note: Implement finalizer only if the object have unmanaged resources

            if (!this._isDisposed)
            {
                if (disposing) // dispose all unamanage and managed resources
                {
                    // dispose resources here
                    _brandCollection.BrandRemoved -= OnBrandRemoved;
                }

            }

            // call methods to cleanup the unamanaged resources

            _isDisposed = true;
            base.Dispose(disposing);
        }
    }
}
