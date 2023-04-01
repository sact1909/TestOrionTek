using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TestOrionTek.DatabaseSettings.DbServices.Abstract
{
    public interface IRepository<TEntity> where TEntity : class, new()
    {

        Task<List<TEntity>> GetAllAsync();

        Task<TEntity> GetSingleAsync(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "");

        Task<int> SaveItemAsync(TEntity item);

        Task<int> UpdateItemAsync(TEntity item);

        Task<int> DeleteItemAsync(TEntity item);

        Task<int> DeleteAllItemAsync();
    }
}
