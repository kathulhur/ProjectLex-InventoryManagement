using ProjectLex.InventoryManagement.Desktop.Collections;
using ProjectLex.InventoryManagement.Desktop.Commands;
using ProjectLex.InventoryManagement.Desktop.Models;
using ProjectLex.InventoryManagement.Desktop.Services;
using ProjectLex.InventoryManagement.Desktop.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ProjectLex.InventoryManagement.Desktop.ViewModels
{
    public class AttributeValueListViewModel : ViewModelBase
    {

        private bool _isDisposed = false;
        private readonly NavigationStore _navigationStore;
        private readonly ObservableCollection<AttributeValueViewModel> _attributeValues;
        public IEnumerable<AttributeValueViewModel> AttributeValues => _attributeValues;

        private readonly AttributeValueCollection _attributeValueCollection;

        private readonly AttributeViewModel _selectedAttribute;

        public ICommand ToCreateAttributeValueCommand { get; }
        public ICommand LoadAttributeValuesCommand { get; }
        public ICommand RemoveAttributeValueCommand { get; }
        public ICommand NavigateToModifyAttributeValueCommand { get; }
        public ICommand NavigateToCreateAttributeValueCommand { get; }

        public AttributeValueListViewModel(NavigationStore navigationStore, AttributeValueCollection attributeValueCollection, AttributeViewModel selectedAttribute)
        {
            _navigationStore = navigationStore;
            _attributeValueCollection = attributeValueCollection;
            _selectedAttribute = selectedAttribute;

            _attributeValueCollection.AttributeValueRemoved += OnAttributeValueRemoved;

            _attributeValues = new ObservableCollection<AttributeValueViewModel>();

            LoadAttributeValuesCommand = new LoadDataCommand<AttributeValue>(_attributeValueCollection, OnAttributeValueLoaded);

            RemoveAttributeValueCommand = new RemoveDataCommand<AttributeValue>(_attributeValueCollection, CreateAttributeValue, CanRemoveAttributeValue);
            NavigateToModifyAttributeValueCommand = new NavigateCommand(NavigateToModifyAttributeValue);
            NavigateToCreateAttributeValueCommand = new NavigateCommand(NavigateToCreateAttributeValue);

        }

        public static AttributeValueListViewModel LoadViewModel(NavigationStore navigationStore, AttributeValueCollection attributeValueCollection, AttributeViewModel selectedAttribute)
        {
            AttributeValueListViewModel viewModel = new AttributeValueListViewModel(navigationStore, attributeValueCollection, selectedAttribute);
            viewModel.LoadAttributeValuesCommand.Execute(null);

            return viewModel;
        }

        public void NavigateToModifyAttributeValue(object obj)
        {
            AttributeValueViewModel selectedAttributeValueViewModel = (AttributeValueViewModel)obj;
            _navigationStore.CurrentViewModel = ModifyAttributeValueViewModel.LoadViewModel(_navigationStore, _attributeValueCollection, selectedAttributeValueViewModel, _selectedAttribute);

        }


        public void NavigateToCreateAttributeValue(object obj)
        {
            _navigationStore.CurrentViewModel = CreateAttributeValueViewModel.LoadViewModel(_navigationStore, _attributeValueCollection, _selectedAttribute);
        }

        public AttributeValue CreateAttributeValue(object obj)
        {
            return new AttributeValue((AttributeValueViewModel)obj);
        }



        public void OnAttributeValueRemoved(AttributeValue attributeValue)
        {
            AttributeValueViewModel removedAttributeValueViewModel = _attributeValues.First(r => r.AttributeValueID == attributeValue.AttributeValueID);
            _attributeValues.Remove(removedAttributeValueViewModel);

        }
        
        public bool CanRemoveAttributeValue(object obj)
        {
            return true;
        }

        private void OnAttributeValueLoaded()
        {
            _attributeValues.Clear();

            foreach (AttributeValue u in _attributeValueCollection.DataList)
            {
                if(u.AttributeID == _selectedAttribute.AttributeID)
                {
                    AttributeValueViewModel attributeValueViewModel = new AttributeValueViewModel(u, _selectedAttribute);
                    _attributeValues.Add(attributeValueViewModel);
                }
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
                    _attributeValueCollection.AttributeValueRemoved -= OnAttributeValueRemoved;
                }

            }

            // call methods to cleanup the unamanaged resources

            _isDisposed = true;
            base.Dispose(disposing);
        }
    }
}
