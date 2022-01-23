using ProjectLex.InventoryManagement.Database.Data;
using ProjectLex.InventoryManagement.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLex.InventoryManagement.Desktop.DAL
{
    class UnitOfWork : IDisposable
    {
        private bool disposed = false;

        private InventoryManagementContext context = new InventoryManagementContext();
        private GenericRepository<Role> _roleRepository;
        private GenericRepository<Category> _categoryRepository;
        private GenericRepository<Warehouse> _warehouseRepository;
        private GenericRepository<Supplier> _supplierRepository;
        private GenericRepository<Staff> _staffRepository;
        private GenericRepository<Product> _productRepository;
        private GenericRepository<Order> _orderRepository;
        private GenericRepository<OrderDetail> _orderDetailRepository;
        private GenericRepository<Location> _locationRepository;
        private GenericRepository<Customer> _customerRepository;

        public GenericRepository<Role> RoleRepository => _roleRepository;
        public GenericRepository<Category> CategoryRepository => _categoryRepository;
        public GenericRepository<Warehouse> warehouseRepository => _warehouseRepository;
        public GenericRepository<Supplier> SupplierRepository => _supplierRepository;
        public GenericRepository<Staff> StaffRepository => _staffRepository;
        public GenericRepository<Product> ProductRepository => _productRepository;
        public GenericRepository<Order> OrderRepository => _orderRepository;
        public GenericRepository<OrderDetail> OrderDetailRepository => _orderDetailRepository;
        public GenericRepository<Location> LocationRepository => _locationRepository;
        public GenericRepository<Customer> CustomerRepository => _customerRepository;
        public UnitOfWork()
        {
            _roleRepository = new GenericRepository<Role>(context);
            _categoryRepository = new GenericRepository<Category>(context);
            _warehouseRepository = new GenericRepository<Warehouse>(context);
            _supplierRepository = new GenericRepository<Supplier>(context);
            _staffRepository = new GenericRepository<Staff>(context);
            _productRepository = new GenericRepository<Product>(context);
            _orderRepository = new GenericRepository<Order>(context);
            _orderDetailRepository = new GenericRepository<OrderDetail>(context);
            _locationRepository = new GenericRepository<Location>(context);
            _customerRepository = new GenericRepository<Customer>(context);
        }


        public void Save()
        {
            context.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if(!this.disposed)
            {
                context.Dispose();
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
