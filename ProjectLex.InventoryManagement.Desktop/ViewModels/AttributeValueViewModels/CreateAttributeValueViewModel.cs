using ProjectLex.InventoryManagement.Desktop.Collections;
using ProjectLex.InventoryManagement.Desktop.Commands;
using ProjectLex.InventoryManagement.Desktop;
using ProjectLex.InventoryManagement.Desktop.Services;
using ProjectLex.InventoryManagement.Desktop.Stores;
using ProjectLex.InventoryManagement.Desktop.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ProjectLex.InventoryManagement.Desktop.Models;

namespace ProjectLex.InventoryManagement.Desktop.ViewModels
{
    public class CreateAttributeValueViewModel : ViewModelBase
    {
        private bool _isDisposed = false;


        private string _attributeValueID;
        public string AttributeValueID
        {
            get { return _attributeValueID; }
            set
            {
                _attributeValueID = value;
                OnPropertyChanged(nameof(AttributeValueID));
            }
        }

        private string _attributeID => Attribute.AttributeID;
        public string AttributeID => _attributeID;


        private AttributeViewModel _attribute;

        public AttributeViewModel Attribute
        {
            get { return _attribute; }
            set
            {
                _attribute = value;
                OnPropertyChanged(nameof(Attribute));
            }
        }


        private string _attributeValueAttributeValueName;
        public string AttributeValueName
        {
            get { return _attributeValueAttributeValueName; }
            set
            {
                _attributeValueAttributeValueName = value;
                OnPropertyChanged(nameof(AttributeValueName));
            }
        }

        private string _attributeValueStatus;
        public string AttributeValueStatus
        {
            get { return _attributeValueStatus; }
            set
            {
                _attributeValueStatus = value;
                OnPropertyChanged(nameof(AttributeValueStatus));
            }
        }
       
        private readonly AttributeValueCollection _attributeValueCollection;
        private readonly NavigationStore _navigationStore;

        


        public ICommand SubmitCommand { get; }
        public ICommand CancelCommand { get; }

        public CreateAttributeValueViewModel(NavigationStore navigationStore, AttributeValueCollection attributeValueCollection, AttributeViewModel attribute)
        {
            _navigationStore = navigationStore;
            _attribute = attribute;
            _attributeValueCollection = attributeValueCollection;
            _attribute = attribute;
            SubmitCommand = new CreateDataCommand<AttributeValue>(attributeValueCollection, CreateAttributeValue, CanCreateAttributeValue);
            CancelCommand = new NavigateCommand(NavigateToAttributeValueList);
        }

        public static CreateAttributeValueViewModel LoadViewModel(NavigationStore navigationStore, AttributeValueCollection attributeValueCollection, AttributeViewModel attribute)
        {
            CreateAttributeValueViewModel viewModel = new CreateAttributeValueViewModel(navigationStore, attributeValueCollection, attribute);
            return viewModel;
        }

        public void NavigateToAttributeValueList(object obj)
        {
            _navigationStore.CurrentViewModel = AttributeValueListViewModel.LoadViewModel(_navigationStore, _attributeValueCollection, _attribute);
        }

        public AttributeValue CreateAttributeValue(object obj)
        {
            return new AttributeValue((CreateAttributeValueViewModel)obj);
        }


        protected override void Dispose(bool disposing)
        {
            if(!this._isDisposed)
            {
                if(disposing)
                {
                    // dispose managed resources
                }
                // dispose unmanaged resources
            }
            this._isDisposed = true;

            base.Dispose(disposing);
        }

        public bool CanCreateAttributeValue(object obj)
        {
            return true;
        }
    }
}
