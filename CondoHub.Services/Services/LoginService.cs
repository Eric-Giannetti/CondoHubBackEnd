using CondoHub.Domain.Dto.Login;
using CondoHub.Domain.Entity;
using CondoHub.Domain.Interfaces.Repositorys;
using CondoHub.Domain.Interfaces.Services;
using CondoHub.Domain.Services;
using CondoHub.Domain.Util;
using CondoHub.Security.Services;
using CondoHub.Services.Context;
using Microsoft.Extensions.Configuration;

namespace CondoHub.Services.Services;

public class LoginService : ILoginService
{
    private readonly ILoginRepository _loginRepository;
    private readonly IUserContextService _userContextService;
    private readonly IConfiguration _configuration;
    private readonly IUserRepository _userRepository;
    private readonly IUserGroupService _userGroupService;

    public LoginService(
        ILoginRepository loginRepository,
        IUserRepository userRepository,
        IUserGroupService userGroupService,
        IUserContextService userContextService, 
        IConfiguration configuration)
    {
        _loginRepository = loginRepository;
        _userContextService = userContextService;
        _userGroupService = userGroupService;
        _configuration = configuration;
        _userRepository = userRepository;
    }


    public Result<LoginDTO> LoginUser(LoginCredentials credentials)
    {
        credentials.password = SecurityHelper.HashPassword(credentials.password);
        var user = _loginRepository.GetUserByCredentials(credentials);
        

        var permissionResult = PermissionToLogin(user);
        if(!permissionResult.IsSuccess) return Result<LoginDTO>.Failure(permissionResult.Error);
        

        var token = SecurityHelper.GenerateJwtToken(_userContextService, _configuration);
        var typesUser = _userRepository.GetAutorizationTypesByUserId(user.Id);

        
        return Result<LoginDTO>.Success(new LoginDTO(user.Id, token, typesUser));
    }

    public Result<LoginDTO> RefreshToken(LoginDTO user)
    {
        var permissionResult = PermissionToLogin(user.UserId);
        if(!permissionResult.IsSuccess) return Result<LoginDTO>.Failure(permissionResult.Error);
        
        if(!_userRepository.GetAutorizationTypesByUserId(user.UserId).Where(c => c == user.TypeUser.FirstOrDefault()).Any())
            return Result<LoginDTO>.Failure("Invalid user type for token refresh.");
        
        var token = SecurityHelper.GenerateJwtToken(_userContextService, _configuration);
        
        return Result<LoginDTO>.Success(new LoginDTO(user.UserId, token, user.TypeUser));
    }

    private Result PermissionToLogin(User? user)
    {
        if(user == null) return Result.Failure("Invalid username or password.");
        if(!_userGroupService.VerifyCanLogin(user.Id)) return Result.Failure("User does not have permission to log in.");
        
        return Result.Success();
    }
    private Result PermissionToLogin(long? id)
    {
        if(id == null || id == 0) return Result.Failure("Invalid username or password.");
        if(!_userGroupService.VerifyCanLogin(id.Value)) return Result.Failure("User does not have permission to log in.");
        
        return Result.Success();
    }
}