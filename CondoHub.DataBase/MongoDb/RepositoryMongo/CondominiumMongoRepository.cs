using CondoHub.DataBase.MongoDb.Context;
using CondoHub.Domain.Entity.Condominium;
using CondoHub.Domain.Interfaces.Repositorys;
using CondoHub.Domain.Util;
using MongoDB.Driver;

namespace CondoHub.DataBase.MongoDb.RepositoryMongo;

public class CondominiumMongoRepository: ICondominiumMongoRepository
{
    private readonly MongoDbContext _context;

    public CondominiumMongoRepository(MongoDbContext context)
    {
        _context = context;
    }

    public Result AddPlace(Place place)
    {
        try
        {
            var collection = _context.GetCollection<Place>("Places");
            collection.InsertOne(place);
            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Failure(ex.Message);
        }
    }

    public Result<Place> GetPlaceById(long id)
    {
        try
        {
            var collection = _context.GetCollection<Place>("Places");
            var place = collection.Find(p => p.Id == id).FirstOrDefault();
            if (place == null)
            {
                return Result<Place>.Failure("Place not found");
            }
            return Result<Place>.Success(place);
        }
        catch (Exception ex)
        {
            return Result<Place>.Failure(ex.Message);
        }
    }
}