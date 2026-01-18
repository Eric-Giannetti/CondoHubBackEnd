using CondoHub.Domain.Dto.Login;
using CondoHub.Domain.Entity;

namespace CondoHub.Domain.Interfaces.Repositorys;

public interface ILoginRepository
{
     User? GetUserByCredentials(LoginCredentials credentials);
}