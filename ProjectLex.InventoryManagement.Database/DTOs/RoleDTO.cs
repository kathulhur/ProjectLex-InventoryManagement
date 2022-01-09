using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLex.InventoryManagement.Database.DTOs
{
    public class RoleDTO
    {
        [Key]
        public Guid RoleID { get; set; }
        public string RoleName { get; set; }
        public string RoleStatus { get; set; }

        public ICollection<UserDTO> Users { get; set; }
    }
}
