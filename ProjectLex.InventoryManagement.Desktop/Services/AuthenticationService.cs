using ProjectLex.InventoryManagement.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLex.InventoryManagement.Desktop.Services
{
    class AuthenticationService
    {
        private Staff _staff;
        public Staff Staff => _staff;

        public bool IsAuthenticated { get; private set; }


        public AuthenticationService()
        {

        }

        public static void Authenticate(string staffname, string password)
        {

        }


    }
}
