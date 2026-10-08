using System.ComponentModel.DataAnnotations;

namespace CondoHub.Domain.Dto.User;

/// <summary>Fields that an authenticated user can change in their own profile.</summary>
public sealed record UpdateUserProfileRequestDTO
{
    [Required, StringLength(120)]
    public required string Name { get; init; }

    [Required, EmailAddress, StringLength(254)]
    public required string Email { get; init; }

    [Required, Phone, StringLength(30)]
    public required string Phone { get; init; }
}

/// <summary>Fields required to register or invite a standard user.</summary>
public sealed record CreateUserRequestDTO
{
    [Required, StringLength(100)]
    public required string Username { get; init; }

    [Required, StringLength(120)]
    public required string Name { get; init; }

    [Required, EmailAddress, StringLength(254)]
    public required string Email { get; init; }

    [Required, Phone, StringLength(30)]
    public required string Phone { get; init; }

    [Required, StringLength(14, MinimumLength = 11)]
    public required string Cpf { get; init; }
}

/// <summary>Safe user representation; excludes password hashes, CPF, and access-control fields.</summary>
public sealed record UserResponseDTO(
    long Id,
    string Name,
    string Email,
    string Phone);