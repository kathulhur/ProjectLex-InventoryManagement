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
    public class AttributeListViewModel : ViewModelBase
    {

        private bool _isDisposed = false;
        private readonly NavigationStore _navigationStore;
        private readonly ObservableCollection<AttributeViewModel> _attributes;
        public IEnumerable<AttributeViewModel> Attributes => _attributes;

        private readonly AttributeCollection _attributeCollection;

        public ICommand ToCreateAttributeCommand { get; }
        public ICommand LoadAttributesCommand { get; }
        public ICommand RemoveAttributeCommand { get; }
        public ICommand NavigateToModifyAttributeCommand { get; }
        public ICommand NavigateToCreateAttributeCommand { get; }

        public AttributeListViewModel(NavigationStore navigationStore, AttributeCollection attributeCollection)
        {
            _navigationStore = navigationStore;
            _attributeCollection = attributeCollection;
            _attributeCollection.AttributeRemoved += OnAttributeRemoved;
            _attributes = new ObservableCollection<AttributeViewModel>();
            LoadAttributesCommand = new LoadDataCommand<Models.Attribute>(_attributeCollection, OnAttributeLoaded);
            RemoveAttributeCommand = new RemoveDataCommand<Models.Attribute>(_attributeCollection, CreateAttribute, CanRemoveAttribute);
            NavigateToModifyAttributeCommand = new NavigateCommand(NavigateToModifyAttribute);
            NavigateToCreateAttributeCommand = new NavigateCommand(NavigateToCreateAttribute);
        }

        public static AttributeListViewModel LoadViewModel(NavigationStore navigationStore, AttributeCollection attributeCollection)
        {
            AttributeListViewModel viewModel = new AttributeListViewModel(navigationStore, attributeCollection);
            viewModel.LoadAttributesCommand.Execute(null);
            return viewModel;
        }
        public void NavigateToModifyAttribute(object obj)
        {
            AttributeViewModel attributeViewModel = (AttributeViewModel)obj;
            _navigationStore.CurrentViewModel = ModifyAttributeViewModel.LoadViewModel(_navigationStore, _attributeCollection, attributeViewModel);
        }

        public void NavigateToCreateAttribute(object obj)
        {
            _navigationStore.CurrentViewModel = CreateAttributeViewModel.LoadViewModel(_navigationStore, _attributeCollection);
        }

        public Models.Attribute CreateAttribute(object obj)
        {
            return new Models.Attribute((AttributeViewModel)obj);
        }



        public void OnAttributeRemoved(Models.Attribute attribute)
        {
            AttributeViewModel removedAttributeViewModel = _attributes.Where(b => b.AttributeID == attribute.AttributeID).First();
            _attributes.Remove(removedAttributeViewModel);
        }
        
        public bool CanRemoveAttribute(object obj)
        {
            return true;
        }

        private void OnAttributeLoaded()
        {
            _attributes.Clear();

            foreach (Models.Attribute b in _attributeCollection.DataList)
            {
                AttributeViewModel attributeViewModel = new AttributeViewModel(b);
                _attributes.Add(attributeViewModel);
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
                    _attributeCollection.AttributeRemoved -= OnAttributeRemoved;
                }

            }

            // call methods to cleanup the unamanaged resources

            _isDisposed = true;
            base.Dispose(disposing);
        }
    }
}
