using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLex.InventoryManagement.Database.Models
{
    public class Role
    {
        [Key]
        public Guid RoleID { get; set; }
        public string RoleName { get; set; }
        public string RoleStatus { get; set; }
        public string RoleDescription { get; set; }

        public bool OrdersView { get; set; }
        public bool OrdersAdd { get; set; }
        public bool OrdersEdit { get; set; }
        public bool OrdersDelete { get; set; }


        public bool CustomersView { get; set; }
        public bool CustomersAdd { get; set; }
        public bool CustomersEdit { get; set; }
        public bool CustomersDelete { get; set; }


        public bool ProductsView { get; set; }
        public bool ProductsAdd { get; set; }
        public bool ProductsEdit { get; set; }
        public bool ProductsDelete { get; set; }


        public bool StoragesView { get; set; }
        public bool StoragesAdd { get; set; }
        public bool StoragesEdit { get; set; }
        public bool StoragesDelete { get; set; }


        public bool DefectivesView { get; set; }
        public bool DefectivesAdd { get; set; }
        public bool DefectivesEdit { get; set; }
        public bool DefectivesDelete { get; set; }


        public bool CategoriesView { get; set; }
        public bool CategoriesAdd { get; set; }
        public bool CategoriesEdit { get; set; }
        public bool CategoriesDelete { get; set; }


        public bool LocationsView { get; set; }
        public bool LocationsAdd { get; set; }
        public bool LocationsEdit { get; set; }
        public bool LocationsDelete { get; set; }


        public bool SuppliersView { get; set; }
        public bool SuppliersAdd { get; set; }
        public bool SuppliersEdit { get; set; }
        public bool SuppliersDelete { get; set; }


        public bool RolesView { get; set; }
        public bool RolesAdd { get; set; }
        public bool RolesEdit { get; set; }
        public bool RolesDelete { get; set; }


        public bool StaffsView { get; set; }
        public bool StaffsAdd { get; set; }
        public bool StaffsEdit { get; set; }
        public bool StaffsDelete { get; set; }


        public bool LogsView { get; set; }
        public bool LogsAdd { get; set; }
        public bool LogsEdit { get; set; }
        public bool LogsDelete { get; set; }


        public ICollection<Staff> Staffs { get; set; }
    }
}
