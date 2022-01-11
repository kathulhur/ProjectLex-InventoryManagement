using Microsoft.EntityFrameworkCore;
using ProjectLex.InventoryManagement.Database.Data;
using ProjectLex.InventoryManagement.Database.DTOs;
using ProjectLex.InventoryManagement.Database.Services;
using ProjectLex.InventoryManagement.Desktop.Models;
using ProjectLex.InventoryManagement.Desktop.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLex.InventoryManagement.Desktop.Services.Providers
{
    public class OrderProvider : DatabaseServiceBase, IProvider<Order>
    {

        public OrderProvider(ContextFactory dbContextFactory) : base(dbContextFactory)
        {
        }

        public async Task<IEnumerable<Order>> GetAll()
        {
            using InventoryManagementContext context = ContextFactory.GetDbContext();
            IEnumerable<OrderDTO> orderDTOs = await context.Orders.ToListAsync();

            return orderDTOs.Select(u => new Order(u));
        }


        public async Task Create(Order order)
        {
            using InventoryManagementContext context = ContextFactory.GetDbContext();
            OrderDTO OrderDTO = ModelConverters.OrderToOrderDTO(order);
            context.Orders.Add(OrderDTO);
            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateException dbE)
            {
                Debug.WriteLine("Order Creator: ");
                Debug.WriteLine(dbE.InnerException);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);

            }
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


        public async Task Remove(Order order)
        {
            using InventoryManagementContext context = ContextFactory.GetDbContext();
            OrderDTO orderDTO = context.Orders
                .Where(r => r.OrderID == new Guid(order.OrderID)).First();

            context.Orders.Remove(orderDTO);
            await context.SaveChangesAsync();
        }
    }
}
