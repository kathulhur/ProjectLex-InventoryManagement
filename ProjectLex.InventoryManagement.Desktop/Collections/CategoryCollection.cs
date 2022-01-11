using ProjectLex.InventoryManagement.Desktop.Commands;
using ProjectLex.InventoryManagement.Desktop.Models;
using ProjectLex.InventoryManagement.Desktop.Services.Providers;
using ProjectLex.InventoryManagement.Desktop.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLex.InventoryManagement.Desktop.Collections
{
    public class CategoryCollection : IDataCollection<Category>, ILoadable<Category>
    {
        private readonly IProvider<Category> _provider;

        private List<Category> _dataList;

        public IEnumerable<Category> DataList => _dataList;

        public event Action<Category> CategoryCreated;
        public event Action<Category> CategoryRemoved;
        public event Action<Category> CategoryModified;
        public CategoryCollection(IProvider<Category> provider)
        {
            _provider = provider;
            _dataList = new List<Category>();
        }

        private void OnCategoryCreated(Category category)
        {
            CategoryCreated?.Invoke(category);
        }

        private void OnCategoryRemoved(Category category)
        {
            CategoryRemoved?.Invoke(category);
        }

        private void OnCategoryModified(Category modifiedCategory)
        {
            CategoryRemoved?.Invoke(modifiedCategory);
        }


        


        public async Task Load()
        {
            try
            {
                IEnumerable<Category> data = await _provider.GetAll();
                _dataList.Clear();
                _dataList.AddRange(data);
            }
            catch (Exception e)
            {
                Debug.WriteLine("CategoryCollection.Load : ");
                Debug.WriteLine(e.Message);
                Debug.WriteLine(e.InnerException);
            }
        }

        public async Task<IEnumerable<Category>> GetAll()
        {
            return await _provider.GetAll();
        }

        public async Task Create(Category newCategory)
        {
            await _provider.Create(newCategory);
            _dataList.Add(newCategory);
            OnCategoryCreated(newCategory);
        }

        public async Task Remove(Category category)
        {
            await _provider.Remove(category);
            Category removedCategory = _dataList.Where(c => c.CategoryID == category.CategoryID).First();
            _dataList.Remove(removedCategory);
            OnCategoryRemoved(removedCategory);
        }

        public async Task Modify(Category modifiedCategory)
        {
            await _provider.Modify(modifiedCategory);
            int index = _dataList.FindIndex(c => c.CategoryID == modifiedCategory.CategoryID);
            _dataList[index] = modifiedCategory;
            OnCategoryModified(modifiedCategory);
        }

        
    }
}
