using ProjectLex.InventoryManagement.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLex.InventoryManagement.Desktop.ViewModels
{
    public class LocationViewModel
    {
        private readonly Location _location;
        public Location Location => _location;

        public string LocationID => _location.LocationID.ToString();
        public string LocationZone => _location.LocationZone;
        public string LocationAisle => _location.LocationAisle;
        public string LocationBay => _location.LocationBay;
        public string LocationRow => _location.LocationRow;
        public string SubLocation => _location.SubLocation;
        public string LocationString => LocationZone + LocationAisle + "-" + LocationBay + "-" + LocationRow + SubLocation;

        public LocationViewModel(Location location)
        {
            _location = location;
        }
    }
}
