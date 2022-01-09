using ProjectLex.InventoryManagement.Desktop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLex.InventoryManagement.Desktop.ViewModels
{
    public class BrandViewModel : ViewModelBase
    {
        private readonly Brand _brand;
        public string BrandID => _brand.BrandID;
        public string BrandName => _brand.BrandName;
        public string BrandStatus => _brand.BrandStatus;

        public BrandViewModel(Brand brand)
        {
            _brand = brand;
        }
    }
}
