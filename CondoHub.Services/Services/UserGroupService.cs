using CondoHub.Domain.Interfaces.Services;
using CondoHub.Domain.Services;

namespace CondoHub.Services.Services;

public class UserGroupService : IUserGroupService
{
    #region Construtores
    private readonly IUserContextService _userContextService;
    
    public UserGroupService(
        IUserContextService userContextService)
    {
        _userContextService = userContextService;
    }
    

    #endregion
    public bool VerifyCanLogin(long userId)
    {
        return true;
    }
}