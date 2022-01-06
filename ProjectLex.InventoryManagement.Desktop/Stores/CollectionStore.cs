using ProjectLex.InventoryManagement.Desktop.Collections;
using ProjectLex.InventoryManagement.Desktop.Controllers;
using ProjectLex.InventoryManagement.Desktop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLex.InventoryManagement.Desktop.Stores
{
    public class CollectionStore
    {
        private readonly CategoryCollection _categoryCollection;
        private readonly ProductCollection _productCollection;
        private readonly SupplierCollection _supplierCollection;

        public CategoryCollection CategoryCollection => _categoryCollection;
        public SupplierCollection SupplierCollection => _supplierCollection;
        public ProductCollection ProductCollection => _productCollection;


        public CollectionStore(
            CategoryCollection categoryCollection,
            ProductCollection productCollection,
            SupplierCollection supplierCollection
            )
        {
            _categoryCollection = categoryCollection;
            _supplierCollection = supplierCollection;
            _productCollection = productCollection;

        }


    }
}
