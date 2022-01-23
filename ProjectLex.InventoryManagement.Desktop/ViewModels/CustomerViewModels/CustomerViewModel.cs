using ProjectLex.InventoryManagement.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLex.InventoryManagement.Desktop.ViewModels
{
    public class CustomerViewModel : ViewModelBase
    {
        private readonly Customer _customer;
        public Customer Customer => _customer;

        public string CustomerID => _customer.CustomerID.ToString();
        public string CustomerFirstname => _customer.CustomerFirstname;
        public string CustomerLastname => _customer.CustomerLastname;
        public string CustomerFullname => _customer.CustomerFirstname + " " + _customer.CustomerLastname;
        public string CustomerAddress => _customer.CustomerAddress;
        public string CustomerPhone => _customer.CustomerPhone;
        public string CustomerEmail => _customer.CustomerEmail;

        public string StaffID => _customer.StaffID.ToString();
        public StaffViewModel Staff
        {
            get
            {
                if(_customer.Staff != null)
                {
                    return new StaffViewModel(_customer.Staff);
                }
                return null;
            }
        }

        public CustomerViewModel(Customer customer)
        {
            _customer = customer;

        }
    }
}
