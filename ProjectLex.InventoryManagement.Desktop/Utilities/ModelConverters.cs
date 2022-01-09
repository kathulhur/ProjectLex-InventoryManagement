using ProjectLex.InventoryManagement.Database.DTOs;
using ProjectLex.InventoryManagement.Desktop.Models;
using ProjectLex.InventoryManagement.Desktop.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLex.InventoryManagement.Desktop.Utilities
{
    public static class ModelConverters
    {
        public static CategoryDTO CategoryToCategoryDTO(Category category)
        {
            return new CategoryDTO()
            {
                CategoryID = new Guid(category.CategoryID),
                CategoryName = category.CategoryName,
                CategoryStatus = category.CategoryStatus
            };

        }
        public static BrandDTO BrandToBrandDTO(Brand brand)
        {
            return new BrandDTO()
            {
                BrandID = new Guid(),
                BrandName = brand.BrandName,
                BrandStatus = brand.BrandStatus
            };

        }


        //public static Supplier CreateSupplierViewModelToSupplier(CreateSupplierViewModel supplierViewModel)
        //{
        //    return new Supplier(
        //        Convert.ToInt32(supplierViewModel.Id),
        //        supplierViewModel.Name,
        //        supplierViewModel.Address,
        //        supplierViewModel.Phone,
        //        supplierViewModel.Fax,
        //        supplierViewModel.Email,
        //        supplierViewModel.OtherDetails
        //        );
        //}

        //public static SupplierDTO SuppliertoSupplierDTO(Supplier supplier)
        //{
        //    return new SupplierDTO()
        //    {
        //        Name = supplier.Name,
        //        Address = supplier.Address,
        //        Phone = supplier.Phone,
        //        Fax = supplier.Fax,
        //        Email = supplier.Email,
        //        OtherDetails = supplier.OtherDetails
        //    };
        //}

        //public static Supplier SupplierDTOToSupplier(SupplierDTO supplierDTO)
        //{
        //    return new Supplier(
        //        supplierDTO.SupplierId,
        //        supplierDTO.Name,
        //        supplierDTO.Address,
        //        supplierDTO.Phone,
        //        supplierDTO.Fax,
        //        supplierDTO.Email,
        //        supplierDTO.OtherDetails
        //        );
        //}

        //public static Product ProductDTOToProduct(ProductDTO productDTO)
        //{
        //    return new Product(
        //        productDTO.ProductId,
        //        productDTO.ProductName,
        //        productDTO.ProductDescription,
        //        productDTO.ProductUnit,
        //        productDTO.ProductPrice,
        //        productDTO.ProductQuantity,
        //        productDTO.ProductStatus,
        //        productDTO.OtherDetails,
        //        productDTO.SupplierId,
        //        productDTO.CategoryId
        //    );
        //}

        //public static ProductDTO ProductToProductDTO(Product product)
        //{
        //    return new ProductDTO()
        //    {
        //        ProductId = product.ProductId,
        //        ProductName = product.ProductName,
        //        ProductDescription = product.ProductDescription,
        //        ProductUnit = product.ProductUnit,
        //        ProductPrice = product.ProductPrice,
        //        ProductQuantity = product.ProductQuantity,
        //        ProductStatus = product.ProductStatus,
        //        OtherDetails = product.OtherDetails,
        //        SupplierId = product.SupplierId,
        //        CategoryId = product.CategoryId
        //    };
        //}

        //public static Product CreateProductViewModelToProduct(CreateProductViewModel createProductViewModel)
        //{
        //    return new Product(
        //        createProductViewModel.Id,
        //        createProductViewModel.Name,
        //        createProductViewModel.Description,
        //        createProductViewModel.Unit,
        //        Convert.ToDecimal(createProductViewModel.Price),
        //        Convert.ToInt32(createProductViewModel.Quantity),
        //        Convert.ToInt32(createProductViewModel.Status),
        //        createProductViewModel.OtherDetails,
        //        Convert.ToInt32(createProductViewModel.SupplierId),
        //        Convert.ToInt32(createProductViewModel.CategoryId)
        //        );
        //}

    }
}
