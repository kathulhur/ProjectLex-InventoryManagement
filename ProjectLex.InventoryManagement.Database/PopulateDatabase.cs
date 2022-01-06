using ProjectLex.InventoryManagement.Database.Data;
using ProjectLex.InventoryManagement.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLex.InventoryManagement.Database
{
    public static class PopulateDatabase
    {
        public static void Populate()
        {
            using InventoryContext context = new InventoryContext();

            CategoryDTO NewCategory1 = new CategoryDTO()
            {
                CategoryName = "Category1",
                Description = "This is Category1 description"
            };
            context.Add(NewCategory1);
            context.SaveChanges();

            CategoryDTO NewCategory2 = new CategoryDTO()
            {
                CategoryName = "Category2",
                Description = "This is Category2 description"
            };
            context.Add(NewCategory2);
            context.SaveChanges();

            SupplierDTO NewSupplier1 = new SupplierDTO()
            {
                Name = "Supplier1",
                Address = "Address1",
                Phone = "12345",
                Fax = "98765",
                Email = "supplier1@email.com",
                OtherDetails = "Other details of supplier1"
            };
            context.Add(NewSupplier1);
            context.SaveChanges();

            SupplierDTO NewSupplier2 = new SupplierDTO()
            {
                Name = "Supplier2",
                Address = "Address2",
                Phone = "12345",
                Fax = "98765",
                Email = "supplier1@email.com",
                OtherDetails = "Other details of supplier2"
            };
            context.Add(NewSupplier2);
            context.SaveChanges();

            ProductDTO NewProduct1 = new ProductDTO()
            {
                ProductId = "1",
                ProductName = "Product1",
                ProductDescription = "Description of Product 1",
                ProductUnit = "Unit1",
                ProductPrice = 1111.11M,
                ProductQuantity = 11,
                ProductStatus = 1,
                OtherDetails = "Other details of Product1",
                SupplierId = 1,
                CategoryId = 1
            };
            context.Add(NewProduct1);
            context.SaveChanges();


            ProductDTO NewProduct2 = new ProductDTO()
            {
                ProductId = "2",
                ProductName = "Product2",
                ProductDescription = "Description of Product 2",
                ProductUnit = "Unit2",
                ProductPrice = 2222.22M,
                ProductQuantity = 22,
                ProductStatus = 2,
                OtherDetails = "Other details of Product1",
                SupplierId = 2,
                CategoryId = 2
            };
            context.Add(NewProduct2);
            context.SaveChanges();


            RoleDTO NewRole1 = new RoleDTO()
            {
                RoleName = "Role1",
                Description = "Description of Role1"
            };
            context.Add(NewRole1);
            context.SaveChanges();

            RoleDTO NewRole2 = new RoleDTO()
            {
                RoleName = "Role2",
                Description = "Description of Role2"
            };
            context.Add(NewRole2);
            context.SaveChanges();

            StaffDTO NewStaff1 = new StaffDTO()
            {
                FirstName = "Staff1",
                LastName = "Staff1",
                Address = "Address of staff1",
                Phone = "12345",
                Email = "staff1@email.com",
                Username = "staff1user",
                Password = "staf1pass",
                RoleId = 1
            };
            context.Add(NewStaff1);
            context.SaveChanges();

            StaffDTO NewStaff2 = new StaffDTO()
            {
                FirstName = "Staff2",
                LastName = "Staff2",
                Address = "Address of staff2",
                Phone = "12345",
                Email = "staff2@email.com",
                Username = "staff2user",
                Password = "staf2pass",
                RoleId = 2
            };
            context.Add(NewStaff2);
            context.SaveChanges();

            PaymentDTO NewPayment1 = new PaymentDTO()
            {
                PaymentType = "Type1",
                OtherDetails = "Other details of payment type 1"
            };
            context.Add(NewPayment1);
            context.SaveChanges();

            PaymentDTO NewPayment2 = new PaymentDTO()
            {
                PaymentType = "Type2",
                OtherDetails = "Other details of payment type 2"
            };
            context.Add(NewPayment2);
            context.SaveChanges();

            CustomerDTO NewCustomer1 = new CustomerDTO()
            {
                FirstName = "Customer1",
                LastName = "Customer1",
                Address = "Address of Customer1",
                Phone = "12345",
                Email = "customer1@email.com",
                StaffId = 1
            };
            context.Add(NewCustomer1);
            context.SaveChanges();

            CustomerDTO NewCustomer2 = new CustomerDTO()
            {
                FirstName = "Customer2",
                LastName = "Customer2",
                Address = "Address of Customer2",
                Phone = "12345",
                Email = "customer2@email.com",
                StaffId = 2
            };
            context.Add(NewCustomer2);
            context.SaveChanges();

            OrderDTO NewOrder1 = new OrderDTO()
            {
                Date = new DateTime(),
                Details = "Detail of NewOrder1",
                CustomerId = 1
            };
            context.Add(NewOrder1);
            context.SaveChanges();

            OrderDTO NewOrder2 = new OrderDTO()
            {
                Date = new DateTime(),
                Details = "Detail of NewOrder2",
                CustomerId = 2
            };
            context.Add(NewOrder2);
            context.SaveChanges();

            OrderDetailDTO NewOrderDetail1 = new OrderDetailDTO()
            {
                OrderDetailId = "1",
                UnitPrice = 1111111.11M,
                Size = 111,
                Quantity = 11,
                Discount = 1,
                Total = 111111111.11M,
                Date = new DateTime(),
                ProductId = "1",
                OrderId = 1,
                PaymentId = 1
            };
            context.Add(NewOrderDetail1);
            context.SaveChanges();

            OrderDetailDTO NewOrderDetail2 = new OrderDetailDTO()
            {
                OrderDetailId = "2",
                UnitPrice = 222222.22M,
                Size = 222,
                Quantity = 22,
                Discount = 2,
                Total = 222222222.22M,
                Date = new DateTime(),
                ProductId = "2",
                OrderId = 2,
                PaymentId = 2
            };
            context.Add(NewOrderDetail2);
            context.SaveChanges();

        }
    }
}
