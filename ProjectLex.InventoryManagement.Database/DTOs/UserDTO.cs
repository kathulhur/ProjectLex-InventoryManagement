using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLex.InventoryManagement.Database.DTOs
{
    public class UserDTO
    {
        [Key]
        public Guid UserID { get; set; }
        public Guid RoleId { get; set; }
        public RoleDTO Role { get; set; }
        public string UserUsername { get; set; }
        public string UserPassword { get; set; }

    }
}
