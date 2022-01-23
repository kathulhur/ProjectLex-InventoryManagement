﻿using ProjectLex.InventoryManagement.Database.Models;
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

        public RoleViewModel(Role role)
        {
            _role = role;
        }
    }
}
