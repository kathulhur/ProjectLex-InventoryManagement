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
    public class CreateRoleViewModel : ViewModelBase
    {
        private bool _isDisposed = false;

        private string _roleID;
        public string RoleID
        {
            get { return _roleID; }
            set
            {
                _roleID = value;
                OnPropertyChanged(nameof(RoleID));
            }
        }

        private string _roleName;
        public string RoleName
        {
            get { return _roleName; }
            set
            {
                _roleName = value;
                OnPropertyChanged(nameof(RoleName));
            }
        }

        private string _roleStatus;
        public string RoleStatus
        {
            get { return _roleStatus; }
            set
            {
                _roleStatus = value;
                OnPropertyChanged(nameof(RoleStatus));
            }
        }
       

        private readonly IDataCollection<Role> _roleCollection;

        public ICommand SubmitCommand { get; }
        public ICommand CancelCommand { get; }

        public CreateRoleViewModel(IDataCollection<Role> roleCollection)
        {
            _roleCollection = roleCollection;
            SubmitCommand = new CreateDataCommand<Role>(roleCollection, CreateRole, CanCreateRole);
        }
        public Role CreateRole(object obj)
        {
            return new Role((CreateRoleViewModel)obj);
        }


        public static CreateRoleViewModel LoadViewModel(IDataCollection<Role> roleCollection)
        {
            return new CreateRoleViewModel(roleCollection);
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

        public bool CanCreateRole(object obj)
        {
            return true;
        }
    }
}
