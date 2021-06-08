using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using PickEmLeagueDatabase.Interfaces;

namespace PickEmLeagueDatabase.Databases
{
    public class DynamoDBDatabaseContext : IDatabaseContext
    {
        private bool _disposed;
        AmazonDynamoDBClient Client;
        private bool _useTestDb;

        public DynamoDBDatabaseContext(IConfiguration configuration)
        {
            Client = new AmazonDynamoDBClient();
            _useTestDb = configuration.GetValue<bool>("UseTestDatabase");
        }

        public async Task Create<T>(T entity) where T : class
        {
            Document document = Document.FromJson(JsonConvert.SerializeObject(entity));
            Table table = GetTable<T>();       
            await table.PutItemAsync(document);
        }

        public async Task Delete<T>(object key) where T : class
        {
            Primitive hash = new Primitive(key.ToString(), false);
            Table table = GetTable<T>();
            await table.DeleteItemAsync(hash);
        }

        public T Get<T>(object key) where T : class
        {
            throw new NotImplementedException();
        }

        public async Task<IQueryable<T>> GetQueryableAsync<T>() where T : class
        {
            ScanOperationConfig config = new ScanOperationConfig();
            Table table = GetTable<T>();
            Search search = table.Scan(config);
            List<Document> docList;// = new List<Document>();
            Task<List<Document>> getNextBatch;
            List<T> items = new List<T>();

            do
            {
                try
                {
                    getNextBatch = search.GetNextSetAsync();
                    docList = await getNextBatch;
                }
                catch (Exception ex)
                {
                    throw new Exception("        FAILED to get the next batch of movies from Search! Reason:\n          " +
                                       ex.Message);
                }

                foreach (Document doc in docList)
                {
                    items.Add(JsonConvert.DeserializeObject<T>(doc.ToJson()));
                }
            } while (!search.IsDone);

            return items.AsQueryable();
        }

        public Task<int> SaveChangesAsync()
        {
            //throw new NotImplementedException();
            return Task.FromResult(1);
        }

        public void Dispose()
        {
            //Dispose(true);
            //GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            //if (!_disposed)
            //{
            //    if (disposing)
            //    {
            //        this.Dispose();
            //    }
            //}

            //_disposed = true;
        }

        private Table GetTable<T>()
        {
            DynamoDBTableAttribute tableAttribute = typeof(T).GetCustomAttribute<DynamoDBTableAttribute>();
            return Table.LoadTable(Client, GetTableName(tableAttribute));
        }

        private string GetTableName(DynamoDBTableAttribute tableAttribute)
        {
            return tableAttribute.TableName += _useTestDb ? "-Test" : "";
        }
    }
}
