using CondoHub.Domain.Dto.Condominium;
using CondoHub.Domain.Entity.Condominium;
using CondoHub.Domain.Interfaces.Repositorys;
using CondoHub.Domain.Services;
using CondoHub.Domain.Util;

namespace CondoHub.Services.Services;

public class CondominiumService
{
    
    
    #region Constructors
    private readonly ICondominiumMySqlRepository _mysqlRepository; 
    private readonly ICondominiumMongoRepository _mongoRepository;
    private readonly IUserContextService _userContextService;
    
    public CondominiumService(
        ICondominiumMySqlRepository mysqlRepository,
        ICondominiumMongoRepository mongoRepository,
        IUserContextService userContextService)
    {
        _mongoRepository = mongoRepository;
        _mysqlRepository = mysqlRepository;
        _userContextService = userContextService;
    }
    #endregion
    
    public Result CreatePlace(CreatePlaceDto dto)
    {
        Place place = new Place(dto);
        place.CreatedBy = _userContextService.UserId;
        place.UpdatedBy = _userContextService.UserId;
        
        _mongoRepository.AddPlace(place);
        
        return Result.Success();
    }
}