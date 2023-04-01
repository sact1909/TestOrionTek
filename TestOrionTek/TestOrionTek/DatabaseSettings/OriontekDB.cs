using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestOrionTek.DatabaseSettings
{
    public class OriontekDB
    {
        static readonly Lazy<SQLiteAsyncConnection> lazyInitializer = new Lazy<SQLiteAsyncConnection>(() =>
        {
            return new SQLiteAsyncConnection(SQLConstants.DatabasePath, SQLConstants.Flags);
        });

        static SQLiteAsyncConnection Entities => lazyInitializer.Value;

        protected static async Task<SQLiteAsyncConnection> InitializeAsync<T>()
        {


            if (!Entities.TableMappings.Any(m => m.MappedType.Name == typeof(T).Name))
            {
                await Entities.EnableWriteAheadLoggingAsync().ConfigureAwait(false);
                await Entities.CreateTablesAsync(CreateFlags.None, typeof(T)).ConfigureAwait(false);
            }


            return Entities;
        }
    }
}
