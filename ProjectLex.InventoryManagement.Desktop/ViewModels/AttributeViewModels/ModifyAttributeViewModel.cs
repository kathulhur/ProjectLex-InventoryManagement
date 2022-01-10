using ProjectLex.InventoryManagement.Desktop.Collections;
using ProjectLex.InventoryManagement.Desktop.Commands;
using ProjectLex.InventoryManagement.Desktop.Models;
using ProjectLex.InventoryManagement.Desktop.Services;
using ProjectLex.InventoryManagement.Desktop.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ProjectLex.InventoryManagement.Desktop.ViewModels
{
    public class ModifyAttributeViewModel : ViewModelBase
    {
        private bool _isDisposed = false;

        private AttributeViewModel _attributeViewModel;

        private string _attributeID;
        public string AttributeID
        {
            get { return _attributeID; }
            set
            {
                _attributeID = value;
                OnPropertyChanged(nameof(AttributeID));
            }
        }

        private string _attributeName;
        public string AttributeName
        {
            get { return _attributeName; }
            set
            {
                _attributeName = value;
                OnPropertyChanged(nameof(AttributeName));
            }
        }


        private readonly AttributeCollection _attributeCollection;
        private readonly NavigationStore _navigationStore;

        public ICommand SubmitCommand { get; }
        public ICommand CancelCommand { get; }

        public ModifyAttributeViewModel(NavigationStore navigationStore, AttributeCollection attributeCollection, AttributeViewModel attributeViewModel)
        {
            _navigationStore = navigationStore;
            _attributeViewModel = attributeViewModel;
            _attributeID = attributeViewModel.AttributeID;
            _attributeName = attributeViewModel.AttributeName;
            _attributeCollection = attributeCollection;
            SubmitCommand = new ModifyDataCommand<Models.Attribute>(attributeCollection, CreateAttribute, CanModifyAttribute);
            CancelCommand = new NavigateCommand(NavigateToAttributeList);
        }

        public static ModifyAttributeViewModel LoadViewModel(NavigationStore navigationStore, AttributeCollection attributeCollection, AttributeViewModel attributeViewModel)
        {
            ModifyAttributeViewModel viewModel = new ModifyAttributeViewModel(navigationStore, attributeCollection, attributeViewModel);
            return viewModel;
        }

        public void NavigateToAttributeList(object obj)
        {
            _navigationStore.CurrentViewModel = AttributeListViewModel.LoadViewModel(_navigationStore, _attributeCollection);
        }
        private Models.Attribute CreateAttribute(object obj)
        {
            return new Models.Attribute((ModifyAttributeViewModel)obj);
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


        public bool CanModifyAttribute(object obj)
        {
            return true;
        }
    }
}
