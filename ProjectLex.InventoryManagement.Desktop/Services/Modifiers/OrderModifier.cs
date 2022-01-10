using ProjectLex.InventoryManagement.Database.Data;
using ProjectLex.InventoryManagement.Database.DTOs;
using ProjectLex.InventoryManagement.Database.Services;
using ProjectLex.InventoryManagement.Desktop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLex.InventoryManagement.Desktop.Services.Modifiers
{
    class OrderModifier : DatabaseServiceBase, IModifier<Order>
    {
        public OrderModifier(ContextFactory dbContextFactory) : base(dbContextFactory)
        {
        }

        public async Task Modify(Order order)
        {
            using InventoryManagementContext context = ContextFactory.GetDbContext();
            OrderDTO orderDTO = context.Orders.Where(u => u.OrderID == new Guid(order.OrderID)).First();
            UpdateOrder(orderDTO, order);
            await context.SaveChangesAsync();
        }

        private void UpdateOrder(OrderDTO orderDTO, Order order)
        {
            if (!orderDTO.UserID.Equals(new Guid(order.UserID)))
            {
                orderDTO.UserID = new Guid(order.UserID);
            }

            if (!orderDTO.CustomerName.Equals(order.CustomerName))
            {
                orderDTO.CustomerName = order.CustomerName;
            }

            if (!orderDTO.OrderTotal.Equals(order.OrderTotal))
            {
                orderDTO.OrderTotal = Convert.ToDecimal(order.OrderTotal);
            }

        }
    }
}
