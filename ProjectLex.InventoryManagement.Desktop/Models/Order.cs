using ProjectLex.InventoryManagement.Database.DTOs;
using ProjectLex.InventoryManagement.Desktop.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLex.InventoryManagement.Desktop.Models
{
    public class Order
    {
        public string OrderID { get; set; }
        public string UserID { get; set; }
        public string CustomerName { get; set; }
        public string OrderTotal { get; set; }

        public Order(OrderDTO orderDTO)
        {
            OrderID = orderDTO.OrderID.ToString();
            UserID = orderDTO.UserID.ToString();
            CustomerName = orderDTO.CustomerName;
            OrderTotal = orderDTO.OrderTotal.ToString();
        }

        public Order(CreateOrderViewModel createOrderViewModel)
        {
            OrderID = Guid.NewGuid().ToString();
            UserID = createOrderViewModel.UserID;
            CustomerName = createOrderViewModel.CustomerName;
            OrderTotal = createOrderViewModel.OrderTotal;
        }

        public Order(ModifyOrderViewModel modifyOrderViewModel)
        {
            OrderID = modifyOrderViewModel.OrderID;
            UserID = modifyOrderViewModel.UserID;
            CustomerName = modifyOrderViewModel.CustomerName;
            OrderTotal = modifyOrderViewModel.OrderTotal;
        }

        public Order(OrderViewModel orderViewModel)
        {
            OrderID = orderViewModel.OrderID;
            UserID = orderViewModel.UserID;
            CustomerName = orderViewModel.CustomerName;
            OrderTotal = orderViewModel.OrderTotal;
        }

    }
}
