using CondoHub.Domain.Dto.Ticket;
using CondoHub.Domain.Util;

namespace CondoHub.Domain.Interfaces.Services;

/// <summary>All methods return <see cref="Result{T}"/> so callers validate <c>IsSuccess</c> before reading <c>Value</c>.</summary>
public interface ITicketService
{
    Task<Result<TicketResponse>> CreateAsync(
        CreateTicketRequest request,
        long condominiumId,
        long userId,
        CancellationToken cancellationToken);

    Task<Result<PagedResult<TicketListItemResponse>>> GetTicketsAsync(
        TicketQueryParameters query,
        long condominiumId,
        CancellationToken cancellationToken);

    Task<Result<TicketResponse>> GetByIdAsync(long id, long condominiumId, CancellationToken cancellationToken);

    Task<Result<TicketResponse>> UpdateStatusAsync(
        long id,
        long condominiumId,
        UpdateTicketStatusRequest request,
        long userId,
        CancellationToken cancellationToken);

    Task<Result<TicketResponse>> UpdateAsync(
        long id,
        long condominiumId,
        UpdateTicketRequest request,
        long userId,
        CancellationToken cancellationToken);

    Task<Result<TicketCommentResponse>> AddCommentAsync(
        long ticketId,
        long condominiumId,
        long userId,
        CreateTicketCommentRequest request,
        CancellationToken cancellationToken);

    Task<Result<List<TicketCommentResponse>>> GetCommentsAsync(
        long ticketId,
        long condominiumId,
        CancellationToken cancellationToken);

    Task<Result<TicketAttachmentResponse>> AddAttachmentAsync(
        long ticketId,
        long condominiumId,
        long userId,
        string fileName,
        string contentType,
        long fileSize,
        string storagePath,
        CancellationToken cancellationToken);

    Task<Result<List<TicketTypeResponse>>> GetTypesAsync(CancellationToken cancellationToken);

    Task<Result<List<TicketCategoryResponse>>> GetCategoriesAsync(long? typeId, CancellationToken cancellationToken);
}
