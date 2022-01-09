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

        public Role(RoleDTO brandDTO)
        {
            RoleID = brandDTO.RoleID.ToString();
            RoleName = brandDTO.RoleName;
            RoleStatus = brandDTO.RoleStatus;
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

        public Role(RoleViewModel brandViewModel)
        {
            RoleID = brandViewModel.RoleID;
            RoleName = brandViewModel.RoleName;
            RoleStatus = brandViewModel.RoleStatus;
        }

    }
}
