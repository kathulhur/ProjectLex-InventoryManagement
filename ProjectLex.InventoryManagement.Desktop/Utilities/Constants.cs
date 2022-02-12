using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLex.InventoryManagement.Desktop.Utilities
{
    public static class Constants
    {
        private static IEnumerable<string> _statuses = new List<string>()
        {
            "Active",
            "Inactive"
        };

        public static IEnumerable<string> Statuses = _statuses;



        private static IEnumerable<string> _availabilities = new List<string>()
        {
            "Unavailable",
            "Available",
            "Out Of Stock"
        };

        public static IEnumerable<string> Availabilities = _availabilities;



        private static IEnumerable<string> _deliveryStatuses = new List<string>()
        {
            "Processing",
            "Shipped",
            "In Transit",
            "Delivered"
        };

        public static IEnumerable<string> DeliveryStatuses = _deliveryStatuses;

        public enum LogCategory
        {
            ORDERS,
            CUSTOMERS,
            PRODUCTS,
            STORAGES,
            DEFECTIVES,
            CATEGORIES,
            LOCATIONS,
            SUPPLIERS,
            ROLES,
            STAFFS,
        }

        public enum ActionType
        {
            CREATE,
            UPDATE,
            DELETE,
            DISPOSE,
            GET,
            MOVE,
            DELARE_AS_DEFECTIVE,
            ADD_STOCK,
            DELIVERY_STATUS_CHANGE
        }

    }
}
