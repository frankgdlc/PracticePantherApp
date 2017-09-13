using Google.Apis.Util.Store;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using Newtonsoft.Json;

namespace CalendarApp.Web.OAuth
{
    /// <summary>
    /// Simple key-value pair model to store the user tokens.
    /// </summary>
    public class Item
    {
        [Key]
        [MaxLength(100)]
        public string Key { get; set; }

        [MaxLength(500)]
        public string Value { get; set; }
    }

    /// <summary>
    /// Entity Framework DbContext that serves as underlying support to the DataStore.
    /// </summary>
    public class OAuthContext : DbContext
    {
        public OAuthContext() : base("DefaultConnection") {}

        public DbSet<Item> Items { get; set; }
    }

    /// <summary>
    /// Custom DataStore implementation that uses the local database to store the auth and refresh tokens. Used by the <see cref="CalendarAppFlowMetadata"/>.
    /// </summary>
    public class LocalDbDataStore : IDataStore
    {
        private static string GenerateStoredKey(string key, Type t) => $"{t.FullName}-{key}";

        public async Task ClearAsync()
        {
            using (var context = new OAuthContext())
            {
                await context.Database.ExecuteSqlCommandAsync("TRUNCATE TABLE Items");
            }
        }

        public async Task DeleteAsync<T>(string key)
        {
            using (var context = new OAuthContext())
            {
                var generatedKey = GenerateStoredKey(key, typeof(T));
                var item = context.Items.FirstOrDefault(x => x.Key == generatedKey);
                if (item != null)
                {
                    context.Items.Remove(item);
                    await context.SaveChangesAsync();
                }
            }
        }

        public Task<T> GetAsync<T>(string key)
        {
            using (var context = new OAuthContext())
            {
                var generatedKey = GenerateStoredKey(key, typeof(T));
                var item = context.Items.FirstOrDefault(x => x.Key == generatedKey);
                T value = item == null ? default(T) : JsonConvert.DeserializeObject<T>(item.Value);
                return Task.FromResult(value);
            }
        }

        public async Task StoreAsync<T>(string key, T value)
        {
            using (var context = new OAuthContext())
            {
                var generatedKey = GenerateStoredKey(key, typeof(T));
                string json = JsonConvert.SerializeObject(value);

                var item = await context.Items.SingleOrDefaultAsync(x => x.Key == generatedKey);
                if (item == null)
                {
                    context.Items.Add(new Item { Key = generatedKey, Value = json });
                }
                else
                {
                    item.Value = json;
                }

                await context.SaveChangesAsync();
            }
        }
    }
}