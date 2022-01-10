using ProjectLex.InventoryManagement.Desktop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLex.InventoryManagement.Desktop.ViewModels
{
    public class OrderViewModel : ViewModelBase
    {
        public readonly string OrderID;
        public readonly string UserID;

        public readonly UserViewModel User;

        public readonly string CustomerName;
        public readonly string OrderTotal;


        public OrderViewModel(Order order, User user)
        {
            OrderID = order.OrderID;
            UserID = order.UserID;
            User = new UserViewModel(user);
            CustomerName = order.CustomerName;
            OrderTotal = order.OrderTotal;
        }

        public OrderViewModel(Order order)
        {
            OrderID = order.OrderID;
            UserID = order.UserID;
            CustomerName = order.CustomerName;
            OrderTotal = order.OrderTotal;
        }

    }
}
