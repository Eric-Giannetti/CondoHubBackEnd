using MongoDB.Driver;
using Microsoft.Extensions.Configuration;

namespace CondoHub.DataBase.MongoDb.Context
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;

        public MongoDbContext(IConfiguration configuration)
        {
            var connectionString = configuration.GetValue<string>("MongoDbSettings:ConnectionString");
            var databaseName = configuration.GetValue<string>("MongoDbSettings:DatabaseName");
            var client = new MongoClient(connectionString);
            _database = client.GetDatabase(databaseName);
        }

        public IMongoCollection<T> GetCollection<T>(string name)
        {
            return _database.GetCollection<T>(name);
        }
    }
}
