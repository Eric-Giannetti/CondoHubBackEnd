using CondoHub.Domain.Dto.Login;
using CondoHub.Domain.Util;

namespace CondoHub.Domain.Interfaces.Services;

public interface ILoginService
{
     Result<LoginDTO> LoginUser(LoginCredentials credentials);
     Result<LoginDTO> RefreshToken(LoginDTO user);
}