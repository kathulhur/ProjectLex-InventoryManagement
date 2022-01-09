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
        private readonly BrandCollection _brandCollection;

        public CategoryCollection CategoryCollection => _categoryCollection;
        public BrandCollection BrandCollection => _brandCollection;


        public CollectionStore(
            CategoryCollection categoryCollection,
            BrandCollection brandCollection
            )
        {
            _categoryCollection = categoryCollection;
            _brandCollection = brandCollection;

        }


    }
}
