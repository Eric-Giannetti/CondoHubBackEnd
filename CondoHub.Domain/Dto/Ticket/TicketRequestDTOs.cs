using System.ComponentModel.DataAnnotations;
using CondoHub.Domain.Enum;

namespace CondoHub.Domain.Dto.Ticket;

/// <summary>Fields required to open a new ticket. Status always starts as Open.</summary>
public sealed record CreateTicketRequest
{
    [Required, StringLength(150, MinimumLength = 3)]
    public required string Title { get; init; }

    [Required, StringLength(2000, MinimumLength = 3)]
    public required string Description { get; init; }

    [Required, Range(1, long.MaxValue)]
    public required long TypeId { get; init; }

    [Required, Range(1, long.MaxValue)]
    public required long CategoryId { get; init; }

    [Required, Range(1, long.MaxValue)]
    public required long UnitId { get; init; }
}

/// <summary>Fields that can be edited while a ticket is still Open.</summary>
public sealed record UpdateTicketRequest
{
    [Required, StringLength(150, MinimumLength = 3)]
    public required string Title { get; init; }

    [Required, StringLength(2000, MinimumLength = 3)]
    public required string Description { get; init; }
}

/// <summary>Moves a ticket to a new status.</summary>
public sealed record UpdateTicketStatusRequest
{
    [Required, EnumDataType(typeof(TicketStatusEnum))]
    public required TicketStatusEnum Status { get; init; }
}

/// <summary>Adds a message to the ticket's communication thread.</summary>
public sealed record CreateTicketCommentRequest
{
    [Required, StringLength(2000, MinimumLength = 1)]
    public required string Message { get; init; }
}

/// <summary>Pagination and filters accepted by the ticket listing endpoint.</summary>
public sealed record TicketQueryParameters
{
    public TicketStatusEnum? Status { get; init; }
    public long? TypeId { get; init; }

    [Range(1, int.MaxValue)]
    public int Page { get; init; } = 1;

    [Range(1, 100)]
    public int PageSize { get; init; } = 20;
}
