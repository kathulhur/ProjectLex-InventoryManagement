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

        public ICollection<Staff> Staffs { get; set; }
    }
}
