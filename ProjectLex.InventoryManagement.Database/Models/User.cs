using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLex.InventoryManagement.Database.Models
{
    public class User
    {
        [Key]
        public Guid UserID { get; set; }
        public Guid RoleID { get; set; }
        public Role Role { get; set; }
        public string UserUsername { get; set; }
        public string UserPassword { get; set; }

    }
}
