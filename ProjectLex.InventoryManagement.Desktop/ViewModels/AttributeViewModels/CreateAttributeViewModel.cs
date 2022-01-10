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
    public class CreateAttributeViewModel : ViewModelBase
    {
        private bool _isDisposed = false;

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

        public CreateAttributeViewModel(NavigationStore navigationStore, AttributeCollection attributeCollection)
        {
            _navigationStore = navigationStore;
            _attributeCollection = attributeCollection;
            SubmitCommand = new CreateDataCommand<Models.Attribute>(attributeCollection, CreateAttribute, CanCreateAttribute);
            CancelCommand = new NavigateCommand(NavigateToAttributeList);
        }

        public static CreateAttributeViewModel LoadViewModel(NavigationStore navigationStore, AttributeCollection attributeCollection)
        {
            return new CreateAttributeViewModel(navigationStore, attributeCollection);
        }

        public void NavigateToAttributeList(object obj)
        {
            _navigationStore.CurrentViewModel = AttributeListViewModel.LoadViewModel(_navigationStore, _attributeCollection);
        }

        public Models.Attribute CreateAttribute(object obj)
        {
            return new Models.Attribute((CreateAttributeViewModel)obj);
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

        public bool CanCreateAttribute(object obj)
        {
            return true;
        }
    }
}
