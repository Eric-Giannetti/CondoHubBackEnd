using CondoHub.DataBase.MongoDb.Context;
using CondoHub.Domain.Entity.Log;
using CondoHub.Domain.Interfaces.Repositorys;

namespace CondoHub.DataBase.MongoDb.RepositoryMongo;

public class LogMongoRepository : ILogRepository
{
    private readonly MongoDbContext _context;

    public LogMongoRepository(MongoDbContext context)
    {
        _context = context;
    }

    public void AddLog(LogEntry log)
    {
        var collection = _context.GetCollection<LogEntry>("Logs");
        collection.InsertOne(log);
    }
}
