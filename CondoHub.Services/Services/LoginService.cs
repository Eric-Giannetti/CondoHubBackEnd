using CondoHub.Domain.Dto.Login;
using CondoHub.Domain.Entity;
using CondoHub.Domain.Enum;
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
    #region Construtores

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

    #endregion



    public Result<LoginDTO> LoginUser(LoginCredentials credentials)
    {
        credentials.password = SecurityHelper.HashPassword(credentials.password);
        var user = _loginRepository.GetUserByCredentials(credentials);
        

        var permissionResult = PermissionToLogin(user);
        if(!permissionResult.IsSuccess) return Result<LoginDTO>.Failure(permissionResult.Error);
        

        var typesUser = _userRepository.GetAutorizationTypesByUserId(user.Id);
        if (typesUser.Count == 0)
            return Result<LoginDTO>.Failure("User does not have an authorization profile.");

        SetUserContext(user.Id, typesUser[0]);
        var token = SecurityHelper.GenerateJwtToken(_userContextService, _configuration);
        return Result<LoginDTO>.Success(new LoginDTO(user.Id, token, typesUser));
    }

    public Result<LoginDTO> RefreshToken(LoginDTO user)
    {
        var permissionResult = PermissionToLogin(user.UserId);
        if(!permissionResult.IsSuccess) return Result<LoginDTO>.Failure(permissionResult.Error);
        
        if (user.TypeUser == null || user.TypeUser.Count != 1)
            return Result<LoginDTO>.Failure("Exactly one user type must be selected for token refresh.");

        var userType = user.TypeUser[0];
        if (!_userRepository.GetAutorizationTypesByUserId(user.UserId).Contains(userType))
            return Result<LoginDTO>.Failure("Invalid user type for token refresh.");

        SetUserContext(user.UserId, userType);
        var token = SecurityHelper.GenerateJwtToken(_userContextService, _configuration);
        
        return Result<LoginDTO>.Success(new LoginDTO(user.UserId, token, user.TypeUser));
    }

    private void SetUserContext(long userId, TypeUserEnum userType)
    {
        _userContextService.UserId = userId;
        _userContextService.userEnum = userType;
        _userContextService.IsAdmin = userType == TypeUserEnum.Admin;
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