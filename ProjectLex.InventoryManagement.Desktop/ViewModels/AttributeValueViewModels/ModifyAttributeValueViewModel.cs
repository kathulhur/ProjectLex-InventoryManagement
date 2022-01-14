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
    public class ModifyAttributeValueViewModel : ViewModelBase
    {
        private bool _isDisposed = false;

        private AttributeValueViewModel _AttributeValueViewModel;

        private string _AttributeValueID;
        public string AttributeValueID
        {
            get { return _AttributeValueID; }
            set
            {
                _AttributeValueID = value;
                OnPropertyChanged(nameof(AttributeValueID));
            }
        }

        private string _attributeID => _attribute.AttributeID;
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

        private string _AttributeValueAttributeValueName;
        public string AttributeValueName
        {
            get { return _AttributeValueAttributeValueName; }
            set
            {
                _AttributeValueAttributeValueName = value;
                OnPropertyChanged(nameof(AttributeValueName));
            }
        }

        private string _AttributeValueStatus;
        public string AttributeValueStatus
        {
            get { return _AttributeValueStatus; }
            set
            {
                _AttributeValueStatus = value;
                OnPropertyChanged(nameof(AttributeValueStatus));
            }
        }
       

        private readonly AttributeValueCollection _attributeValueCollection;
        private readonly NavigationStore _navigationStore;


        public ICommand SubmitCommand { get; }
        public ICommand CancelCommand { get; }

        public ModifyAttributeValueViewModel(NavigationStore navigationStore, AttributeValueCollection AttributeValueCollection, AttributeValueViewModel selectedAttributeValue, AttributeViewModel selectedAttribute)
        {
            _navigationStore = navigationStore;
            _attributeValueCollection = AttributeValueCollection;

            _AttributeValueViewModel = selectedAttributeValue;
            _attribute = selectedAttribute;

            _AttributeValueID = selectedAttributeValue.AttributeValueID;
            _AttributeValueAttributeValueName = selectedAttributeValue.AttributeValueName;
            _AttributeValueStatus = selectedAttributeValue.AttributeValueStatus;

            SubmitCommand = new ModifyDataCommand<AttributeValue>(AttributeValueCollection, CreateAttributeValue, CanModifyAttributeValue);
            CancelCommand = new NavigateCommand(NavigateToAttributeValueList);
        }

        public static ModifyAttributeValueViewModel LoadViewModel(NavigationStore navigationStore, AttributeValueCollection AttributeValueCollection, AttributeValueViewModel selectedAttributeValue, AttributeViewModel selectedAttribute)
        {
            ModifyAttributeValueViewModel viewModel = new ModifyAttributeValueViewModel(navigationStore, AttributeValueCollection, selectedAttributeValue, selectedAttribute);
            return viewModel;
        }

        public void NavigateToAttributeValueList(object obj)
        {
            _navigationStore.CurrentViewModel = AttributeValueListViewModel.LoadViewModel(_navigationStore, _attributeValueCollection, _attribute);
        }


        private AttributeValue CreateAttributeValue(object obj)
        {
            return new AttributeValue((ModifyAttributeValueViewModel)obj);
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


        public bool CanModifyAttributeValue(object obj)
        {
            return true;
        }
    }
}
