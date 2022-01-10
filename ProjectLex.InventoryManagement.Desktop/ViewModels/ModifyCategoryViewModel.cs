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
    public class ModifyCategoryViewModel : ViewModelBase
    {
        private bool _isDisposed = false;
        private string _categoryId;

        private readonly IDataCollection<Category> _dataCollection;

        public ICommand SubmitCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        public ModifyCategoryViewModel(IDataCollection<Category> categoryCollection)
        {
            _dataCollection = categoryCollection;
            SubmitCommand = new ModifyDataCommand<Category>(_dataCollection, CreateCategory, CanModifyCategory);
            //CancelCommand = new NavigateCommand<ViewModelBase>(CanCreateCategory);
        }



        private Category CreateCategory(object obj)
        {
            return new Category((ModifyCategoryViewModel)obj);
        }

        private bool CanModifyCategory(object obj)
        {
            return true;
        }

        public static ModifyCategoryViewModel LoadViewModel(IDataCollection<Category> collection)
        {
            ModifyCategoryViewModel viewModel = new ModifyCategoryViewModel(collection);
            return viewModel;
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
