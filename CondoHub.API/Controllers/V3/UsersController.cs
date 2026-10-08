using System.Security.Claims;
using CondoHub.API.Attributes;
using CondoHub.Domain.Dto.User;
using CondoHub.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CondoHub.API.Controllers.V3;

[ApiController]
[Route("api/v3/[controller]")]
[Authorize]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("me")]
    [ProducesResponseType(typeof(UserResponseDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<UserResponseDTO>> GetMyProfile(CancellationToken cancellationToken)
    {
        if (!TryGetAuthenticatedUserId(out var userId))
            return Unauthorized();

        var profile = await _userService.GetProfileAsync(userId, cancellationToken);
        return profile is null ? NotFound() : Ok(profile);
    }

    [HttpPut("me")]
    [ProducesResponseType(typeof(UserResponseDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<UserResponseDTO>> UpdateMyProfile(
        [FromBody] UpdateUserProfileRequestDTO request,
        CancellationToken cancellationToken)
    {
        if (!TryGetAuthenticatedUserId(out var userId))
            return Unauthorized();

        var profile = await _userService.UpdateProfileAsync(userId, request, cancellationToken);
        return profile is null ? NotFound() : Ok(profile);
    }

    /// <summary>Creates or invites a standard user. Only admins and condominium managers are allowed.</summary>
    [HttpPost]
    [IsCondominiumManagerOrAdmin]
    [ProducesResponseType(typeof(UserResponseDTO), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<UserResponseDTO>> CreateUser(
        [FromBody] CreateUserRequestDTO request,
        CancellationToken cancellationToken)
    {
        var user = await _userService.CreateUserAsync(request, cancellationToken);
        return Created($"/api/v3/users/{user.Id}", user);
    }

    [HttpGet]
    [IsCondominiumManagerOrAdmin]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status501NotImplemented)]
    public IActionResult GetUsers()
    {
        return StatusCode(StatusCodes.Status501NotImplemented);
    }

    [HttpGet("{id:long}")]
    [IsCondominiumManagerOrAdmin]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status501NotImplemented)]
    public IActionResult GetUserById([FromRoute] long id)
    {
        return StatusCode(StatusCodes.Status501NotImplemented);
    }

    [HttpPut("{id:long}")]
    [IsCondominiumManagerOrAdmin]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status501NotImplemented)]
    public IActionResult UpdateUser([FromRoute] long id)
    {
        return StatusCode(StatusCodes.Status501NotImplemented);
    }

    [HttpPatch("{id:long}/status")]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status501NotImplemented)]
    [IsCondominiumManagerOrAdmin]
    public IActionResult UpdateUserStatus([FromRoute] long id)
    {
        return StatusCode(StatusCodes.Status501NotImplemented);
    }

    private bool TryGetAuthenticatedUserId(out long userId)
    {
        var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier)
            ?? User.FindFirstValue("sub");

        return long.TryParse(userIdClaim, out userId) && userId > 0;
    }

}