using ProjectLex.InventoryManagement.Database.DTOs;
using ProjectLex.InventoryManagement.Desktop.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLex.InventoryManagement.Desktop.Models
{
    public class Role
    {
        public string RoleID { get; set; }
        public string RoleName { get; set; }
        public string RoleStatus { get; set; }

        public IEnumerable<User> Users { get; set; }

        public Role(RoleDTO roleDTO)
        {
            RoleID = roleDTO.RoleID.ToString();
            RoleName = roleDTO.RoleName;
            RoleStatus = roleDTO.RoleStatus;
        }

        public Role(CreateRoleViewModel createRoleViewModel)
        {
            RoleID = Guid.NewGuid().ToString();
            RoleName = createRoleViewModel.RoleName;
            RoleStatus = createRoleViewModel.RoleStatus;
        }
        public Role(ModifyRoleViewModel modifyRoleViewModel)
        {
            RoleID = modifyRoleViewModel.RoleID;
            RoleName = modifyRoleViewModel.RoleName;
            RoleStatus = modifyRoleViewModel.RoleStatus;
        }

        public Role(RoleViewModel roleViewModel)
        {
            RoleID = roleViewModel.RoleID;
            RoleName = roleViewModel.RoleName;
            RoleStatus = roleViewModel.RoleStatus;
        }

    }
}
