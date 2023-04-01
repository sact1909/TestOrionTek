using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TestOrionTek.DatabaseSettings.DbServices.Abstract;

namespace TestOrionTek.DatabaseSettings.DbServices
{
    public class Repository<TEntity> : OriontekDB, IRepository<TEntity> where TEntity : class, new()
    {
        public Repository()
        {

        }

        public async Task<List<TEntity>> GetAllAsync()
        {
            var databaseConnection = await InitializeAsync<TEntity>().ConfigureAwait(false);
            return await databaseConnection.Table<TEntity>().ToListAsync();
        }

        public async Task<TEntity> GetSingleAsync(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "")
        {
            var databaseConnection = await InitializeAsync<TEntity>().ConfigureAwait(false);
            return await databaseConnection.Table<TEntity>().Where(filter).FirstOrDefaultAsync();
        }

        public async Task<int> SaveItemAsync(TEntity item)
        {
            var databaseConnection = await InitializeAsync<TEntity>().ConfigureAwait(false);
            return await databaseConnection.InsertAsync(item);
        }

        public async Task<int> UpdateItemAsync(TEntity item)
        {
            var databaseConnection = await InitializeAsync<TEntity>().ConfigureAwait(false);
            return await databaseConnection.UpdateAsync(item);
        }

        public async Task<int> DeleteItemAsync(TEntity item)
        {
            var databaseConnection = await InitializeAsync<TEntity>().ConfigureAwait(false);
            return await databaseConnection.DeleteAsync(item);
        }

        public async Task<int> DeleteAllItemAsync()
        {
            var databaseConnection = await InitializeAsync<TEntity>().ConfigureAwait(false);
            return await databaseConnection.DeleteAllAsync<TEntity>();
        }
    }
}
