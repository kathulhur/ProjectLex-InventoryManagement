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
        private GenericRepository<Store> _storeRepository;
        private GenericRepository<Supplier> _supplierRepository;
        private GenericRepository<User> _userRepository;
        private GenericRepository<Product> _productRepository;
        private GenericRepository<ProductCategory> _productCategoryRepository;
        private GenericRepository<Order> _orderRepository;
        private GenericRepository<OrderDetail> _orderDetailRepository;

        public GenericRepository<Role> RoleRepository => _roleRepository;
        public GenericRepository<Category> CategoryRepository => _categoryRepository;
        public GenericRepository<Store> StoreRepository => _storeRepository;
        public GenericRepository<Supplier> SupplierRepository => _supplierRepository;
        public GenericRepository<User> UserRepository => _userRepository;
        public GenericRepository<Product> ProductRepository => _productRepository;
        public GenericRepository<ProductCategory> ProductCategoryRepository => _productCategoryRepository;
        public GenericRepository<Order> OrderRepository => _orderRepository;
        public GenericRepository<OrderDetail> OrderDetailRepository => _orderDetailRepository;
        public UnitOfWork()
        {
            _roleRepository = new GenericRepository<Role>(context);
            _categoryRepository = new GenericRepository<Category>(context);
            _storeRepository = new GenericRepository<Store>(context);
            _supplierRepository = new GenericRepository<Supplier>(context);
            _userRepository = new GenericRepository<User>(context);
            _productRepository = new GenericRepository<Product>(context);
            _productCategoryRepository = new GenericRepository<ProductCategory>(context);
            _orderRepository = new GenericRepository<Order>(context);
            _orderDetailRepository = new GenericRepository<OrderDetail>(context);
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
