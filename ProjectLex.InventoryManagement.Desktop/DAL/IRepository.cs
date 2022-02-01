using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLex.InventoryManagement.Desktop.DAL
{
    public interface IRepository<TEntity> where TEntity : class
    {
        public IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "");

        public TEntity GetByID(object id);

        public void Insert(TEntity entity);

        public void Delete(object id);

        public void Delete(TEntity entityToDelete);

        public void Update(TEntity entityToUpdate);
    }
}
