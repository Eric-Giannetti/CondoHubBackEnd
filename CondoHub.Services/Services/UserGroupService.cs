using CondoHub.Domain.Interfaces.Services;

namespace CondoHub.Services.Services;

public class UserGroupService : IUserGroupService
{
    public bool VerifyCanLogin(long userId)
    {
        return true;
    }
}