using CondoHub.Domain.Dto.Login;
using CondoHub.Domain.Enum;
using CondoHub.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CondoHub.API.Controllers.V3;

[ApiController]
[Route("api/v3/[controller]")]
public class LoginController : Controller
{
    private readonly ILogger<LoginController> _logger;
    private readonly ILoginService _loginService;
    public LoginController(ILogger<LoginController> logger, ILoginService loginService)
    {
        _logger = logger;
        _loginService = loginService;
    }
    
    [HttpPost]
    public IActionResult LoginUser([FromBody]LoginCredentials credentials)
    {
        if(!credentials.IsValid()) return Unauthorized("Invalid login data.");
        
        
        var result = _loginService.LoginUser(credentials);
        
        if(result.IsSuccess) return Ok(result.Value);
        return Unauthorized(result.Error);
    }
    
    [HttpPost("refresh-token")]
    [Authorize]
    public IActionResult RefreshToken(TypeUserEnum loginDto)
    {
        var user = new LoginDTO()
        {
            UserId = Convert.ToInt64(User.FindFirst("sub")?.Value ?? User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value),
            TypeUser = new List<TypeUserEnum> { loginDto },
            FirstAccess = false
        };
        var result = _loginService.RefreshToken(user);
        
        
        return Ok(new LoginDTO());
    }
}