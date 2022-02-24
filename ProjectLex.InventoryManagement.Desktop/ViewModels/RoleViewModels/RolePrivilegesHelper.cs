using ProjectLex.InventoryManagement.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLex.InventoryManagement.Desktop.ViewModels
{
    public class RolePrivilegesHelper : ViewModelBase
    {
        private bool _isDisposed = false;

        private bool _ordersView = false;
        public bool OrdersView
        {
            get { return _ordersView; }
            set { SetProperty(ref _ordersView, value); }
        }

        private bool _ordersAdd = false;
        public bool OrdersAdd
        {
            get { return _ordersAdd; }
            set { SetProperty(ref _ordersAdd, value); }
        }

        private bool _ordersEdit = false;
        public bool OrdersEdit
        {
            get { return _ordersEdit; }
            set { SetProperty(ref _ordersEdit, value); }
        }

        private bool _ordersDelete = false;
        public bool OrdersDelete
        {
            get { return _ordersDelete; }
            set { SetProperty(ref _ordersDelete, value); }
        }





        private bool _customersView = false;
        public bool CustomersView
        {
            get { return _customersView; }
            set { SetProperty(ref _customersView, value); }
        }

        private bool _customersAdd = false;
        public bool CustomersAdd
        {
            get { return _customersAdd; }
            set { SetProperty(ref _customersAdd, value); }
        }

        private bool _customersEdit = false;
        public bool CustomersEdit
        {
            get { return _customersEdit; }
            set { SetProperty(ref _customersEdit, value); }
        }

        private bool _customersDelete = false;
        public bool CustomersDelete
        {
            get { return _customersDelete; }
            set { SetProperty(ref _customersDelete, value); }
        }




        private bool _productsView = false;
        public bool ProductsView
        {
            get { return _productsView; }
            set { SetProperty(ref _productsView, value); }
        }

        private bool _productsAdd = false;
        public bool ProductsAdd
        {
            get { return _productsAdd; }
            set { SetProperty(ref _productsAdd, value); }
        }

        private bool _productsEdit = false;
        public bool ProductsEdit
        {
            get { return _productsEdit; }
            set { SetProperty(ref _productsEdit, value); }
        }

        private bool _productsDelete = false;
        public bool ProductsDelete
        {
            get { return _productsDelete; }
            set { SetProperty(ref _productsDelete, value); }
        }




        private bool _storagesView = false;
        public bool StoragesView
        {
            get { return _storagesView; }
            set { SetProperty(ref _storagesView, value); }
        }

        private bool _storagesAdd = false;
        public bool StoragesAdd
        {
            get { return _storagesAdd; }
            set { SetProperty(ref _storagesAdd, value); }
        }

        private bool _storagesEdit = false;
        public bool StoragesEdit
        {
            get { return _storagesEdit; }
            set { SetProperty(ref _storagesEdit, value); }
        }

        private bool _storagesDelete = false;
        public bool StoragesDelete
        {
            get { return _storagesDelete; }
            set { SetProperty(ref _storagesDelete, value); }
        }





        private bool _defectivesView = false;
        public bool DefectivesView
        {
            get { return _defectivesView; }
            set { SetProperty(ref _defectivesView, value); }
        }

        private bool _defectivesAdd = false;
        public bool DefectivesAdd
        {
            get { return _defectivesAdd; }
            set { SetProperty(ref _defectivesAdd, value); }
        }

        private bool _defectivesEdit = false;
        public bool DefectivesEdit
        {
            get { return _defectivesEdit; }
            set { SetProperty(ref _defectivesEdit, value); }
        }

        private bool _defectivesDelete = false;
        public bool DefectivesDelete
        {
            get { return _defectivesDelete; }
            set { SetProperty(ref _defectivesDelete, value); }
        }





        private bool _categoriesView = false;
        public bool CategoriesView
        {
            get { return _categoriesView; }
            set { SetProperty(ref _categoriesView, value); }
        }

        private bool _categoriesAdd = false;
        public bool CategoriesAdd
        {
            get { return _categoriesAdd; }
            set { SetProperty(ref _categoriesAdd, value); }
        }

        private bool _categoriesEdit = false;
        public bool CategoriesEdit
        {
            get { return _categoriesEdit; }
            set { SetProperty(ref _categoriesEdit, value); }
        }

        private bool _categoriesDelete = false;
        public bool CategoriesDelete
        {
            get { return _categoriesDelete; }
            set { SetProperty(ref _categoriesDelete, value); }
        }





        private bool _locationsView = false;
        public bool LocationsView
        {
            get { return _locationsView; }
            set { SetProperty(ref _locationsView, value); }
        }

        private bool _locationsAdd = false;
        public bool LocationsAdd
        {
            get { return _locationsAdd; }
            set { SetProperty(ref _locationsAdd, value); }
        }

        private bool _locationsEdit = false;
        public bool LocationsEdit
        {
            get { return _locationsEdit; }
            set { SetProperty(ref _locationsEdit, value); }
        }

        private bool _locationsDelete = false;
        public bool LocationsDelete
        {
            get { return _locationsDelete; }
            set { SetProperty(ref _locationsDelete, value); }
        }





        private bool _suppliersView = false;
        public bool SuppliersView
        {
            get { return _suppliersView; }
            set { SetProperty(ref _suppliersView, value); }
        }

        private bool _suppliersAdd = false;
        public bool SuppliersAdd
        {
            get { return _suppliersAdd; }
            set { SetProperty(ref _suppliersAdd, value); }
        }

        private bool _suppliersEdit = false;
        public bool SuppliersEdit
        {
            get { return _suppliersEdit; }
            set { SetProperty(ref _suppliersEdit, value); }
        }

        private bool _suppliersDelete = false;
        public bool SuppliersDelete
        {
            get { return _suppliersDelete; }
            set { SetProperty(ref _suppliersDelete, value); }
        }



        private bool _rolesView = false;
        public bool RolesView
        {
            get { return _rolesView; }
            set { SetProperty(ref _rolesView, value); }
        }

        private bool _rolesAdd = false;
        public bool RolesAdd
        {
            get { return _rolesAdd; }
            set { SetProperty(ref _rolesAdd, value); }
        }

        private bool _rolesEdit = false;
        public bool RolesEdit
        {
            get { return _rolesEdit; }
            set { SetProperty(ref _rolesEdit, value); }
        }

        private bool _rolesDelete = false;
        public bool RolesDelete
        {
            get { return _rolesDelete; }
            set { SetProperty(ref _rolesDelete, value); }
        }




        private bool _staffsView = false;
        public bool StaffsView
        {
            get { return _staffsView; }
            set { SetProperty(ref _staffsView, value); }
        }

        private bool _staffsAdd = false;
        public bool StaffsAdd
        {
            get { return _staffsAdd; }
            set { SetProperty(ref _staffsAdd, value); }
        }

        private bool _staffsEdit = false;
        public bool StaffsEdit
        {
            get { return _staffsEdit; }
            set { SetProperty(ref _staffsEdit, value); }
        }

        private bool _staffsDelete = false;
        public bool StaffsDelete
        {
            get { return _staffsDelete; }
            set { SetProperty(ref _staffsDelete, value); }
        }



        private bool _logsView = false;
        public bool LogsView
        {
            get { return _logsView; }
            set { SetProperty(ref _logsView, value); }
        }

        private bool _logsAdd = false;
        public bool LogsAdd
        {
            get { return _logsAdd; }
            set { SetProperty(ref _logsAdd, value); }
        }

        private bool _logsEdit = false;
        public bool LogsEdit
        {
            get { return _logsEdit; }
            set { SetProperty(ref _logsEdit, value); }
        }

        private bool _logsDelete = false;
        public bool LogsDelete
        {
            get { return _logsDelete; }
            set { SetProperty(ref _logsDelete, value); }
        }


        public RolePrivilegesHelper()
        {
        }


        public RolePrivilegesHelper(Role role)
        {
            OrdersView      = role.OrdersView;
            OrdersAdd       = role.OrdersAdd;
            OrdersEdit      = role.OrdersEdit;
            OrdersDelete    = role.OrdersDelete;


            CustomersView      = role.CustomersView;
            CustomersAdd       = role.CustomersAdd;
            CustomersEdit      = role.CustomersEdit;
            CustomersDelete    = role.CustomersDelete;


            ProductsView      = role.ProductsView;
            ProductsAdd       = role.ProductsAdd;
            ProductsEdit      = role.ProductsEdit;
            ProductsDelete    = role.ProductsDelete;


            StoragesView      = role.StoragesView;
            StoragesAdd       = role.StoragesAdd;
            StoragesEdit      = role.StoragesEdit;
            StoragesDelete    = role.StoragesDelete;


            DefectivesView      = role.DefectivesView;
            DefectivesAdd       = role.DefectivesAdd;
            DefectivesEdit      = role.DefectivesEdit;
            DefectivesDelete    = role.DefectivesDelete;


            CategoriesView      = role.CategoriesView;
            CategoriesAdd       = role.CategoriesAdd;
            CategoriesEdit      = role.CategoriesEdit;
            CategoriesDelete    = role.CategoriesDelete;


            LocationsView      = role.LocationsView;
            LocationsAdd       = role.LocationsAdd;
            LocationsEdit      = role.LocationsEdit;
            LocationsDelete    = role.LocationsDelete;


            SuppliersView      = role.SuppliersView;
            SuppliersAdd       = role.SuppliersAdd;
            SuppliersEdit      = role.SuppliersEdit;
            SuppliersDelete    = role.SuppliersDelete;


            RolesView      = role.RolesView;
            RolesAdd       = role.RolesAdd;
            RolesEdit      = role.RolesEdit;
            RolesDelete    = role.RolesDelete;


            StaffsView      = role.StaffsView;
            StaffsAdd       = role.StaffsAdd;
            StaffsEdit      = role.StaffsEdit;
            StaffsDelete    = role.StaffsDelete;


            LogsView      = role.LogsView;
            LogsAdd       = role.LogsAdd;
            LogsEdit      = role.LogsEdit;
            LogsDelete    = role.LogsDelete;
        }



        protected override void Dispose(bool disposing)
        {
            if (!this._isDisposed)
            {
                if (disposing)
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
