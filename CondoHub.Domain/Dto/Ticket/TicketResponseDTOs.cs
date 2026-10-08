using CondoHub.Domain.Enum;

namespace CondoHub.Domain.Dto.Ticket;

/// <summary>Full ticket details, returned by the create and get-by-id endpoints.</summary>
public sealed record TicketResponse(
    long Id,
    string Title,
    string Description,
    TicketStatusEnum Status,
    long TypeId,
    string? TypeName,
    long CategoryId,
    string? CategoryName,
    long UnitId,
    long CondominiumId,
    DateTime CreatedAt,
    DateTime? UpdatedAt);

/// <summary>Lightweight ticket representation used by the paginated listing endpoint.</summary>
public sealed record TicketListItemResponse(
    long Id,
    string Title,
    TicketStatusEnum Status,
    long TypeId,
    long CategoryId,
    long UnitId,
    DateTime CreatedAt);

public sealed record TicketCommentResponse(
    long Id,
    long TicketId,
    long UserId,
    string Message,
    DateTime CreatedAt);

public sealed record TicketAttachmentResponse(
    long Id,
    long TicketId,
    long UploadedBy,
    string FileName,
    string ContentType,
    long FileSize,
    DateTime CreatedAt);

public sealed record TicketTypeResponse(long Id, string Name);

public sealed record TicketCategoryResponse(long Id, string Name, long TypeId);
