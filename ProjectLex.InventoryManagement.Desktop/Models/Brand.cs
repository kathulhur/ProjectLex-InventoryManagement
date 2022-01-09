using ProjectLex.InventoryManagement.Database.DTOs;
using ProjectLex.InventoryManagement.Desktop.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLex.InventoryManagement.Desktop.Models
{
    public class Brand
    {
        public string BrandID { get; }
        public string BrandName { get; }
        public string BrandStatus { get; }

        public Brand(BrandDTO brandDTO)
        {
            BrandID = brandDTO.BrandID.ToString();
            BrandName = brandDTO.BrandName;
            BrandStatus = brandDTO.BrandStatus;
        }

        public Brand(CreateBrandViewModel createBrandViewModel)
        {
            BrandID = Guid.NewGuid().ToString();
            BrandName = createBrandViewModel.BrandName;
            BrandStatus = createBrandViewModel.BrandStatus;
        }
        public Brand(ModifyBrandViewModel modifyBrandViewModel)
        {
            BrandID = modifyBrandViewModel.BrandID;
            BrandName = modifyBrandViewModel.BrandName;
            BrandStatus = modifyBrandViewModel.BrandStatus;
        }

        public Brand(BrandViewModel brandViewModel)
        {
            BrandID = brandViewModel.BrandID;
            BrandName = brandViewModel.BrandName;
            BrandStatus = brandViewModel.BrandStatus;
        }

    }
}
