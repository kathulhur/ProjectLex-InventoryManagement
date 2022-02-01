using ProjectLex.InventoryManagement.Database.Models;
using ProjectLex.InventoryManagement.Desktop.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLex.InventoryManagement.Desktop.Stores
{
    public class AuthenticationStore
    {
        public Staff CurrentStaff { get; set; } 


        private bool _isLoggedIn = false;

        public bool IsLoggedIn
        {
            get => _isLoggedIn;
            set
            {
                _isLoggedIn = value;
                OnIsLoggedInChanged();
            }
        }

        public event Action IsLoggedInChanged;

        private void OnIsLoggedInChanged()
        {
            IsLoggedInChanged?.Invoke();
        }

    }
}
