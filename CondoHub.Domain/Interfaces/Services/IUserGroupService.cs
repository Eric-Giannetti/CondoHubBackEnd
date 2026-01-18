namespace CondoHub.Domain.Interfaces.Services;

public interface IUserGroupService
{
    bool VerifyCanLogin(long userId);
}