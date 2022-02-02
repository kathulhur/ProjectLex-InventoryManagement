﻿using System;
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


    }
}
