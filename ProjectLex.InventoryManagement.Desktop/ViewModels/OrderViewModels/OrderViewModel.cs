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
        public string UserID => _order.OrderID.ToString();
        public UserViewModel User
        {
            get
            {
                if (_order.User != null)
                {
                    return new UserViewModel(_order.User);
                }
                return null;
            }
        }

        public string CustomerName => _order.CustomerName;
        public string OrderTotal => _order.OrderTotal.ToString();


        public OrderViewModel(Order order)
        {
            _order = order;
        }

    }
}
