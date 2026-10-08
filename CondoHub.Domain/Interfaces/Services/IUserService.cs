using CondoHub.Domain.Dto.User;

namespace CondoHub.Domain.Interfaces.Services;

/// <summary>Defines user registration and self-service profile operations.</summary>
public interface IUserService
{
    /// <summary>Gets a profile by the ID resolved from the authenticated principal.</summary>
    Task<UserResponseDTO?> GetProfileAsync(long userId, CancellationToken cancellationToken);

    /// <summary>Updates the profile belonging to the supplied authenticated user ID.</summary>
    Task<UserResponseDTO?> UpdateProfileAsync(
        long userId,
        UpdateUserProfileRequestDTO request,
        CancellationToken cancellationToken);

    /// <summary>Creates or invites a standard user without accepting credentials or access level from the request.</summary>
    Task<UserResponseDTO> CreateUserAsync(
        CreateUserRequestDTO request,
        CancellationToken cancellationToken);
}