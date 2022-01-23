using ProjectLex.InventoryManagement.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLex.InventoryManagement.Desktop.ViewModels
{
    public class StaffViewModel : ViewModelBase
    {
        private readonly Staff _staff;

        public Staff Staff => _staff;
        public string StaffID => _staff.StaffID.ToString();
        public string RoleID => _staff.RoleID.ToString();
        public string StaffFirstName => _staff.StaffFirstName;
        public string StaffLastName => _staff.StaffLastName;
        public string StaffFullname => _staff.StaffFirstName + " " + _staff.StaffLastName;
        public string StaffAddress => _staff.StaffAddress;
        public string StaffPhone => _staff.StaffPhone;
        public string StaffEmail => _staff.StaffEmail;
        public string StaffUsername => _staff.StaffUsername;
        public string StaffPassword => _staff.StaffPassword;

        public RoleViewModel Role
        {
            get
            {
                if(_staff.Role != null)
                {
                    return new RoleViewModel(_staff.Role);
                }
                return null;
            }
        }

        


        public StaffViewModel(Staff staff)
        {
            _staff = staff;
        }

        
    }
}
