using CondoHub.Domain.Entity.Condominium;
using CondoHub.Domain.Util;

namespace CondoHub.Domain.Interfaces.Repositorys;

public interface ICondominiumMongoRepository
{
    public Result AddPlace(Place place);
}