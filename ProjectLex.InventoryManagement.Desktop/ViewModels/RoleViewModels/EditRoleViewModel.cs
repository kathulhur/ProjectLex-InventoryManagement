using Microsoft.Toolkit.Mvvm.Input;
using ProjectLex.InventoryManagement.Database.Models;
using ProjectLex.InventoryManagement.Desktop.DAL;
using ProjectLex.InventoryManagement.Desktop.Stores;
using ProjectLex.InventoryManagement.Desktop.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using static ProjectLex.InventoryManagement.Desktop.Utilities.Constants;

namespace ProjectLex.InventoryManagement.Desktop.ViewModels
{
    public class EditRoleViewModel : ViewModelBase
    {
        private bool _isDisposed = false;

        private Role _role;

        public string _roleName;

        [Required(ErrorMessage = "Name is Required")]
        [MinLength(2, ErrorMessage = "Name should be longer than 2 characters")]
        [MaxLength(50, ErrorMessage = "Name longer than 50 characters is Not Allowed")]
        public string RoleName
        {
            get => _roleName;
            set
            {
                SetProperty(ref _roleName, value, true);
            }

        }

        private string _roleDescription;

        [Required(ErrorMessage = "Description is Required")]
        [MinLength(10, ErrorMessage = "Description should be at least 10 characters long")]
        [MaxLength(50, ErrorMessage = "Description longer than 50 characters is Not Allowed")]
        public string RoleDescription
        {
            get => _roleDescription;
            set
            {
                SetProperty(ref _roleDescription, value, true);
            }
        }


        private string _roleStatus;

        [Required(ErrorMessage = "Status is Required")]
        public string RoleStatus
        {
            get { return _roleStatus; }
            set
            {
                SetProperty(ref _roleStatus, value, true);
            }
        }

        public RolePrivilegesHelper RolePrivilegesHelper { get; }

        private readonly UnitOfWork _unitOfWork;
        private readonly NavigationStore _navigationStore;
        private readonly Action _closeDialogCallback;

        public RelayCommand SubmitCommand { get; }
        public RelayCommand CancelCommand { get; }

        public EditRoleViewModel(NavigationStore navigationStore, UnitOfWork unitOfWork, Role role, Action closeDialogCallback)
        {
            _unitOfWork = unitOfWork;
            _navigationStore = navigationStore;
            _closeDialogCallback = closeDialogCallback;
            _role = role;

            RolePrivilegesHelper = new RolePrivilegesHelper(role);

            RoleName = _role.RoleName;
            RoleDescription = _role.RoleDescription;
            RoleStatus = _role.RoleStatus;


            SubmitCommand = new RelayCommand(Submit);
            CancelCommand = new RelayCommand(Cancel);
        }
        private void Submit()
        {
            ValidateAllProperties();

            if (HasErrors)
            {
                return;
            }

            _role.RoleName = RoleName;
            _role.RoleDescription = RoleDescription;
            _role.RoleStatus = RoleStatus;
            _role.OrdersView = RolePrivilegesHelper.OrdersView;
            _role.OrdersAdd = RolePrivilegesHelper.OrdersAdd;
            _role.OrdersEdit = RolePrivilegesHelper.OrdersEdit;
            _role.OrdersDelete = RolePrivilegesHelper.OrdersDelete;


            _role.CustomersView = RolePrivilegesHelper.CustomersView;
            _role.CustomersAdd = RolePrivilegesHelper.CustomersAdd;
            _role.CustomersEdit = RolePrivilegesHelper.CustomersEdit;
            _role.CustomersDelete = RolePrivilegesHelper.CustomersDelete;


            _role.ProductsView = RolePrivilegesHelper.ProductsView;
            _role.ProductsAdd = RolePrivilegesHelper.ProductsAdd;
            _role.ProductsEdit = RolePrivilegesHelper.ProductsEdit;
            _role.ProductsDelete = RolePrivilegesHelper.ProductsDelete;


            _role.StoragesView = RolePrivilegesHelper.StoragesView;
            _role.StoragesAdd = RolePrivilegesHelper.StoragesAdd;
            _role.StoragesEdit = RolePrivilegesHelper.StoragesEdit;
            _role.StoragesDelete = RolePrivilegesHelper.StoragesDelete;


            _role.DefectivesView = RolePrivilegesHelper.DefectivesView;
            _role.DefectivesAdd = RolePrivilegesHelper.DefectivesAdd;
            _role.DefectivesEdit = RolePrivilegesHelper.DefectivesEdit;
            _role.DefectivesDelete = RolePrivilegesHelper.DefectivesDelete;


            _role.CategoriesView = RolePrivilegesHelper.CategoriesView;
            _role.CategoriesAdd = RolePrivilegesHelper.CategoriesAdd;
            _role.CategoriesEdit = RolePrivilegesHelper.CategoriesEdit;
            _role.CategoriesDelete = RolePrivilegesHelper.CategoriesDelete;


            _role.LocationsView = RolePrivilegesHelper.LocationsView;
            _role.LocationsAdd = RolePrivilegesHelper.LocationsAdd;
            _role.LocationsEdit = RolePrivilegesHelper.LocationsEdit;
            _role.LocationsDelete = RolePrivilegesHelper.LocationsDelete;


            _role.SuppliersView = RolePrivilegesHelper.SuppliersView;
            _role.SuppliersAdd = RolePrivilegesHelper.SuppliersAdd;
            _role.SuppliersEdit = RolePrivilegesHelper.SuppliersEdit;
            _role.SuppliersDelete = RolePrivilegesHelper.SuppliersDelete;


            _role.RolesView = RolePrivilegesHelper.RolesView;
            _role.RolesAdd = RolePrivilegesHelper.RolesAdd;
            _role.RolesEdit = RolePrivilegesHelper.RolesEdit;
            _role.RolesDelete = RolePrivilegesHelper.RolesDelete;


            _role.StaffsView = RolePrivilegesHelper.StaffsView;
            _role.StaffsAdd = RolePrivilegesHelper.StaffsAdd;
            _role.StaffsEdit = RolePrivilegesHelper.StaffsEdit;
            _role.StaffsDelete = RolePrivilegesHelper.StaffsDelete;


            _role.LogsView = RolePrivilegesHelper.LogsView;
            _role.LogsAdd = RolePrivilegesHelper.LogsAdd;
            _role.LogsEdit = RolePrivilegesHelper.LogsEdit;
            _role.LogsDelete = RolePrivilegesHelper.LogsDelete;


            _unitOfWork.RoleRepository.Update(_role);
            _unitOfWork.LogRepository.Insert(LogUtil.CreateLog(LogCategory.ROLES, ActionType.CREATE, $"Role updated; RoleID:{_role.RoleID};"));
            _unitOfWork.Save();

            _closeDialogCallback();
        }

        public static EditRoleViewModel LoadViewModel(NavigationStore navigationStore, UnitOfWork unitOfWork, Role role, Action closeDialogCallback)
        {
            EditRoleViewModel viewModel = new EditRoleViewModel(navigationStore, unitOfWork, role, closeDialogCallback);
            return viewModel;
        }

        public void Cancel()
        {
            _closeDialogCallback();
        }



        protected override void Dispose(bool disposing)
        {
            if(!this._isDisposed)
            {
                if(disposing)
                {
                    // dispose managed resources
                    RolePrivilegesHelper.Dispose();
                }
                // dispose unmanaged resources
            }
            this._isDisposed = true;

            base.Dispose(disposing);
        }


        public bool CanModifyRole(object obj)
        {
            return true;
        }
    }
}
