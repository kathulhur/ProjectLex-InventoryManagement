using ProjectLex.InventoryManagement.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLex.InventoryManagement.Desktop.ViewModels
{
    public class OrderViewModel : ViewModelBase
    {
        private Order _order;
        public Order Order => _order;
        public string OrderID => _order.OrderID.ToString();
        public string CustomerID => _order.OrderID.ToString();
        public string OrderDate => _order.OrderDate.ToString();
        public string OrderTotal => _order.OrderTotal.ToString();
        public string DeliveryStatus => _order.DeliveryStatus;

        public CustomerViewModel Customer
        {
            get
            {
                if (_order.Customer != null)
                {
                    return new CustomerViewModel(_order.Customer);
                }
                return null;
            }
        }

        public OrderViewModel(Order order)
        {
            _order = order;
        }

    }
}
