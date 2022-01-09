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
    public class CreateCategoryViewModel : ViewModelBase
    {
        private bool _isDisposed = false;

        private readonly IDataCollection<Category> _dataCollection;

        public ICommand SubmitCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        public CreateCategoryViewModel(IDataCollection<Category> categoryCollection)
        {
            SubmitCommand = new CreateDataCommand<Category>(categoryCollection, CanCreateCategory);
            _dataCollection = categoryCollection;
        }

        private string _categoryId;

        public static CreateCategoryViewModel LoadViewModel(IDataCollection<Category> collection)
        {
            return new CreateCategoryViewModel(collection);
        }

        public string CategoryId
        {
            get { return _categoryId; }
            set
            {
                _categoryId = value;
                OnPropertyChanged(nameof(CategoryId));
            }
        }

        private string _categoryName;

        public string CategoryName
        {
            get { return _categoryName; }
            set
            {
                _categoryName = value;
                OnPropertyChanged(nameof(CategoryName));
            }
        }

        private string _categoryStatus;

        public string CategoryStatus
        {
            get { return _categoryStatus; }
            set
            {
                _categoryStatus = value;
                OnPropertyChanged(nameof(CategoryStatus));
            }
        }

        private bool CanCreateCategory(object obj)
        {
            return true;
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

    }
}
