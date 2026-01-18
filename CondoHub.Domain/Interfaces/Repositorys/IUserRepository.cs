using CondoHub.Domain.Enum;

namespace CondoHub.Domain.Interfaces.Repositorys;

public interface IUserRepository
{
    List<TypeUserEnum> GetAutorizationTypesByUserId(long userId);
}