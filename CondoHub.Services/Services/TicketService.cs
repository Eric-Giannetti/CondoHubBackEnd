using CondoHub.Domain.Dto.Ticket;
using CondoHub.Domain.Entity.Ticket;
using CondoHub.Domain.Enum;
using CondoHub.Domain.Interfaces.Repositorys;
using CondoHub.Domain.Interfaces.Services;
using CondoHub.Domain.Util;

namespace CondoHub.Services.Services;

public class TicketService : ITicketService
{
    private readonly ITicketRepository _repository;

    public TicketService(ITicketRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<TicketResponse>> CreateAsync(
        CreateTicketRequest request,
        long condominiumId,
        long userId,
        CancellationToken cancellationToken)
    {
        var ticket = new Ticket
        {
            Title = request.Title,
            Description = request.Description,
            TypeId = request.TypeId,
            CategoryId = request.CategoryId,
            UnitId = request.UnitId,
            CondominiumId = condominiumId,
            Status = TicketStatusEnum.Open,
            CreatedAt = DateTime.UtcNow,
            CreatedBy = userId,
            UpdatedBy = userId,
        };

        await _repository.AddAsync(ticket, cancellationToken);

        return Result<TicketResponse>.Success(ToResponse(ticket));
    }

    public async Task<Result<PagedResult<TicketListItemResponse>>> GetTicketsAsync(
        TicketQueryParameters query,
        long condominiumId,
        CancellationToken cancellationToken)
    {
        var (items, totalCount) = await _repository.GetPagedAsync(
            condominiumId,
            query.Status,
            query.TypeId,
            query.Page,
            query.PageSize,
            cancellationToken);

        var paged = new PagedResult<TicketListItemResponse>
        {
            Items = items.Select(ToListItemResponse).ToList(),
            CurrentPage = query.Page,
            PageSize = query.PageSize,
            TotalCount = totalCount,
            TotalPages = (int)Math.Ceiling(totalCount / (double)query.PageSize),
        };

        return Result<PagedResult<TicketListItemResponse>>.Success(paged);
    }

    public async Task<Result<TicketResponse>> GetByIdAsync(long id, long condominiumId, CancellationToken cancellationToken)
    {
        var ticket = await _repository.GetByIdAsync(id, condominiumId, cancellationToken);
        if (ticket is null)
            return Result<TicketResponse>.Failure(TicketErrorCodes.NotFound);

        return Result<TicketResponse>.Success(ToResponse(ticket));
    }

    public async Task<Result<TicketResponse>> UpdateStatusAsync(
        long id,
        long condominiumId,
        UpdateTicketStatusRequest request,
        long userId,
        CancellationToken cancellationToken)
    {
        var ticket = await _repository.GetByIdAsync(id, condominiumId, cancellationToken);
        if (ticket is null)
            return Result<TicketResponse>.Failure(TicketErrorCodes.NotFound);

        ticket.Status = request.Status;
        ticket.UpdatedAt = DateTime.UtcNow;
        ticket.UpdatedBy = userId;

        await _repository.SaveChangesAsync(cancellationToken);
        return Result<TicketResponse>.Success(ToResponse(ticket));
    }

    public async Task<Result<TicketResponse>> UpdateAsync(
        long id,
        long condominiumId,
        UpdateTicketRequest request,
        long userId,
        CancellationToken cancellationToken)
    {
        var ticket = await _repository.GetByIdAsync(id, condominiumId, cancellationToken);
        if (ticket is null)
            return Result<TicketResponse>.Failure(TicketErrorCodes.NotFound);

        if (ticket.Status != TicketStatusEnum.Open)
            return Result<TicketResponse>.Failure(TicketErrorCodes.StatusConflict);

        ticket.Title = request.Title;
        ticket.Description = request.Description;
        ticket.UpdatedAt = DateTime.UtcNow;
        ticket.UpdatedBy = userId;

        await _repository.SaveChangesAsync(cancellationToken);
        return Result<TicketResponse>.Success(ToResponse(ticket));
    }

    public async Task<Result<TicketCommentResponse>> AddCommentAsync(
        long ticketId,
        long condominiumId,
        long userId,
        CreateTicketCommentRequest request,
        CancellationToken cancellationToken)
    {
        var ticket = await _repository.GetByIdAsync(ticketId, condominiumId, cancellationToken);
        if (ticket is null)
            return Result<TicketCommentResponse>.Failure(TicketErrorCodes.NotFound);

        var comment = new TicketComment
        {
            TicketId = ticketId,
            UserId = userId,
            Message = request.Message,
            CreatedAt = DateTime.UtcNow,
            CreatedBy = userId,
            UpdatedBy = userId,
        };

        await _repository.AddCommentAsync(comment, cancellationToken);

        return Result<TicketCommentResponse>.Success(ToCommentResponse(comment));
    }

    public async Task<Result<List<TicketCommentResponse>>> GetCommentsAsync(
        long ticketId,
        long condominiumId,
        CancellationToken cancellationToken)
    {
        var ticket = await _repository.GetByIdAsync(ticketId, condominiumId, cancellationToken);
        if (ticket is null)
            return Result<List<TicketCommentResponse>>.Failure(TicketErrorCodes.NotFound);

        var comments = await _repository.GetCommentsAsync(ticketId, cancellationToken);
        return Result<List<TicketCommentResponse>>.Success(comments.Select(ToCommentResponse).ToList());
    }

    public async Task<Result<TicketAttachmentResponse>> AddAttachmentAsync(
        long ticketId,
        long condominiumId,
        long userId,
        string fileName,
        string contentType,
        long fileSize,
        string storagePath,
        CancellationToken cancellationToken)
    {
        var ticket = await _repository.GetByIdAsync(ticketId, condominiumId, cancellationToken);
        if (ticket is null)
            return Result<TicketAttachmentResponse>.Failure(TicketErrorCodes.NotFound);

        var attachment = new TicketAttachment
        {
            TicketId = ticketId,
            UploadedBy = userId,
            FileName = fileName,
            ContentType = contentType,
            FileSize = fileSize,
            StoragePath = storagePath,
            CreatedAt = DateTime.UtcNow,
            CreatedBy = userId,
            UpdatedBy = userId,
        };

        await _repository.AddAttachmentAsync(attachment, cancellationToken);

        return Result<TicketAttachmentResponse>.Success(ToAttachmentResponse(attachment));
    }

    public async Task<Result<List<TicketTypeResponse>>> GetTypesAsync(CancellationToken cancellationToken)
    {
        var types = await _repository.GetTypesAsync(cancellationToken);
        return Result<List<TicketTypeResponse>>.Success(types.Select(t => new TicketTypeResponse(t.Id, t.Name)).ToList());
    }

    public async Task<Result<List<TicketCategoryResponse>>> GetCategoriesAsync(long? typeId, CancellationToken cancellationToken)
    {
        var categories = await _repository.GetCategoriesAsync(typeId, cancellationToken);
        return Result<List<TicketCategoryResponse>>.Success(categories.Select(c => new TicketCategoryResponse(c.Id, c.Name, c.TypeId)).ToList());
    }

    private static TicketResponse ToResponse(Ticket ticket) => new(
        ticket.Id,
        ticket.Title,
        ticket.Description,
        ticket.Status,
        ticket.TypeId,
        ticket.Type?.Name,
        ticket.CategoryId,
        ticket.Category?.Name,
        ticket.UnitId,
        ticket.CondominiumId,
        ticket.CreatedAt,
        ticket.UpdatedAt);

    private static TicketListItemResponse ToListItemResponse(Ticket ticket) => new(
        ticket.Id,
        ticket.Title,
        ticket.Status,
        ticket.TypeId,
        ticket.CategoryId,
        ticket.UnitId,
        ticket.CreatedAt);

    private static TicketCommentResponse ToCommentResponse(TicketComment comment) => new(
        comment.Id,
        comment.TicketId,
        comment.UserId,
        comment.Message,
        comment.CreatedAt);

    private static TicketAttachmentResponse ToAttachmentResponse(TicketAttachment attachment) => new(
        attachment.Id,
        attachment.TicketId,
        attachment.UploadedBy,
        attachment.FileName,
        attachment.ContentType,
        attachment.FileSize,
        attachment.CreatedAt);
}
