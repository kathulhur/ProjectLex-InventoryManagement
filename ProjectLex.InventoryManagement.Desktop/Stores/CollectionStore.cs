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
        private readonly RoleCollection _roleCollection;
        private readonly UserCollection _userCollection;

        public CategoryCollection CategoryCollection => _categoryCollection;
        public BrandCollection BrandCollection => _brandCollection;
        public RoleCollection RoleCollection => _roleCollection;
        public UserCollection UserCollection => _userCollection;


        public CollectionStore(
            CategoryCollection categoryCollection,
            BrandCollection brandCollection,
            RoleCollection roleCollection,
            UserCollection userCollection
            )
        {
            _categoryCollection = categoryCollection;
            _brandCollection = brandCollection;
            _roleCollection = roleCollection;
            _userCollection = userCollection;

        }


    }
}
