using ProjectLex.InventoryManagement.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLex.InventoryManagement.Desktop.DAL
{
    public interface IUnitOfWork
    {

        //public IRepository<Role> RoleRepository { get; }
        //public IRepository<Category> CategoryRepository { get; }
        //public IRepository<Warehouse> warehouseRepository { get; }
        //public IRepository<Supplier> SupplierRepository { get; }
        //public IRepository<Staff> StaffRepository { get; }
        //public IRepository<Product> ProductRepository { get; }
        //public IRepository<Order> OrderRepository { get; }
        //public IRepository<OrderDetail> OrderDetailRepository { get; }
        //public IRepository<Location> LocationRepository { get; }
        //public IRepository<Customer> CustomerRepository { get; }

        public GenericRepository<Role> RoleRepository { get; }
        public GenericRepository<Category> CategoryRepository { get; }
        public GenericRepository<Warehouse> WarehouseRepository { get; }
        public GenericRepository<Supplier> SupplierRepository { get; }
        public GenericRepository<Staff> StaffRepository { get; }
        public GenericRepository<Product> ProductRepository { get; }
        public GenericRepository<Order> OrderRepository { get; }
        public GenericRepository<OrderDetail> OrderDetailRepository { get; }
        public GenericRepository<Location> LocationRepository { get; }
        public GenericRepository<Customer> CustomerRepository { get; }

        public void Save();
    }
}
