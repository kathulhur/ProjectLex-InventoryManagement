using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLex.InventoryManagement.Desktop.Utilities
{
    public static class Constants
    {
        public static IEnumerable<string> _statuses = new List<string>()
        {
            "",
            "Active",
            "Inactive"
        };

        public static IEnumerable<string> Statuses = _statuses;

        public static IEnumerable<string> _availabilities = new List<string>()
        {
            "Unavailable",
            "Available",
            "Out Of Stock"
        };

        public static IEnumerable<string> Availabilities = _availabilities;


    }
}
