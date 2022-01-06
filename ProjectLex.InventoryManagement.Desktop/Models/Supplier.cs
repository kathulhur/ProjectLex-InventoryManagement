using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLex.InventoryManagement.Desktop.Models
{
    public class Supplier
    {
        public string Id { get; }
        public string Name { get; }
        public string Address { get; }
        public string Phone { get; }
        public string Fax { get; }
        public string Email { get; }
        public string OtherDetails { get; }

        public Supplier(int id, string name, string address, string phone, string fax, string email, string otherDetails)
        {
            Id = Convert.ToString(id);
            Name = name;
            Address = address;
            Phone = phone;
            Fax = fax;
            Email = email;
            OtherDetails = otherDetails;
        }

    }
}
