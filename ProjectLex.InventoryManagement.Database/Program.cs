using ProjectLex.InventoryManagement.Database.Data;
using ProjectLex.InventoryManagement.Database.DTOs;
using ProjectLex.InventoryManagement.Database.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLex.InventoryManagement.Database
{
    class Program
    {
        public static void Main()
        {
            using InventoryManagementContext context = new InventoryManagementContext();
            UserDTO user = new UserDTO
            {
                UserID = new Guid("17D4F9AD-ABF7-458E-8663-2F19229E8A8F"),
                RoleID = new Guid("B91797B6-6433-45FE-AB3E-FD59D888CD14"),
                UserUsername = "username",
                UserPassword = "password"
            };

            context.Users.Update(user);
            context.SaveChanges();
            
        }

    }
}
