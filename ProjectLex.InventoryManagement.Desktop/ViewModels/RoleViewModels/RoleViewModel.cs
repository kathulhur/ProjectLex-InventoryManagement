using ProjectLex.InventoryManagement.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLex.InventoryManagement.Desktop.ViewModels
{
    public class RoleViewModel : ViewModelBase
    {
        private readonly Role _role;
        public Role Role => _role;
        public string RoleID => _role.RoleID.ToString();
        public string RoleName => _role.RoleName;
        public string RoleDescription => _role.RoleDescription;
        public string RoleStatus => _role.RoleStatus;

        public bool OrdersView => _role.OrdersView;
        public bool OrdersAdd => _role.OrdersAdd;
        public bool OrdersEdit => _role.OrdersEdit;
        public bool OrdersDelete => _role.OrdersDelete;


        public bool CustomersView => _role.CustomersView;
        public bool CustomersAdd => _role.CustomersAdd;
        public bool CustomersEdit => _role.CustomersEdit;
        public bool CustomersDelete => _role.CustomersDelete;


        public bool ProductsView => _role.ProductsView;
        public bool ProductsAdd => _role.ProductsAdd;
        public bool ProductsEdit => _role.ProductsEdit;
        public bool ProductsDelete => _role.ProductsDelete;


        public bool StoragesView => _role.StoragesView;
        public bool StoragesAdd => _role.StoragesAdd;
        public bool StoragesEdit => _role.StoragesEdit;
        public bool StoragesDelete => _role.StoragesDelete;


        public bool DefectivesView => _role.DefectivesView;
        public bool DefectivesAdd => _role.DefectivesAdd;
        public bool DefectivesEdit => _role.DefectivesEdit;
        public bool DefectivesDelete => _role.DefectivesDelete;


        public bool CategoriesView => _role.CategoriesView;
        public bool CategoriesAdd => _role.CategoriesAdd;
        public bool CategoriesEdit => _role.CategoriesEdit;
        public bool CategoriesDelete => _role.CategoriesDelete;


        public bool LocationsView => _role.LocationsView;
        public bool LocationsAdd => _role.LocationsAdd;
        public bool LocationsEdit => _role.LocationsEdit;
        public bool LocationsDelete => _role.LocationsDelete;


        public bool SuppliersView => _role.SuppliersView;
        public bool SuppliersAdd => _role.SuppliersAdd;
        public bool SuppliersEdit => _role.SuppliersEdit;
        public bool SuppliersDelete => _role.SuppliersDelete;


        public bool RolesView => _role.RolesView;
        public bool RolesAdd => _role.RolesAdd;
        public bool RolesEdit => _role.RolesEdit;
        public bool RolesDelete => _role.RolesDelete;


        public bool StaffsView => _role.StaffsView;
        public bool StaffsAdd => _role.StaffsAdd;
        public bool StaffsEdit => _role.StaffsEdit;
        public bool StaffsDelete => _role.StaffsDelete;


        public bool LogsView => _role.LogsView;
        public bool LogsAdd => _role.LogsAdd;
        public bool LogsEdit => _role.LogsEdit;
        public bool LogsDelete => _role.LogsDelete;


        public RoleViewModel(Role role)
        {
            _role = role;
        }
    }
}
