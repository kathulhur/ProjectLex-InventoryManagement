using ProjectLex.InventoryManagement.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLex.InventoryManagement.Desktop.ViewModels
{
    public class OrderDetailViewModel
    {
        private readonly OrderDetail _orderDetail;
        public OrderDetail OrderDetail => _orderDetail;
        public string ProductID => _orderDetail.ProductID.ToString();
        public string StoreID => _orderDetail.StoreID.ToString();
        public string OrderID => _orderDetail.OrderID.ToString();
        public string OrderDetailQuantity => _orderDetail.OrderDetailQuantity.ToString();
        public string OrderDetailAmount => _orderDetail.OrderDetailAmount.ToString();
        public ProductViewModel Product 
        {
            get
            {
                if(_orderDetail.Product != null)
                {
                    return new ProductViewModel(_orderDetail.Product);
                }
                return null;
            }
        }

        public OrderViewModel Order
        {
            get
            {
                if (_orderDetail.Product != null)
                {
                    return new OrderViewModel(_orderDetail.Order);
                }
                return null;
            }
        }

        public OrderDetailViewModel(OrderDetail orderDetail)
        {
            _orderDetail = orderDetail;
        }
    }
}
