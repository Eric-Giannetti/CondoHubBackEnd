using CondoHub.Domain.Entity.Ticket;

namespace CondoHub.Domain.Interfaces.Repositorys;

public interface ITicketRepository
{
    Task AddAsync(Ticket ticket, CancellationToken cancellationToken);

    Task<Ticket?> GetByIdAsync(long id, long condominiumId, CancellationToken cancellationToken);

    Task<(List<Ticket> Items, int TotalCount)> GetPagedAsync(
        long condominiumId,
        Enum.TicketStatusEnum? status,
        long? typeId,
        int page,
        int pageSize,
        CancellationToken cancellationToken);

    Task SaveChangesAsync(CancellationToken cancellationToken);

    Task<TicketComment> AddCommentAsync(TicketComment comment, CancellationToken cancellationToken);

    Task<List<TicketComment>> GetCommentsAsync(long ticketId, CancellationToken cancellationToken);

    Task<TicketAttachment> AddAttachmentAsync(TicketAttachment attachment, CancellationToken cancellationToken);

    Task<List<TicketType>> GetTypesAsync(CancellationToken cancellationToken);

    Task<List<TicketCategory>> GetCategoriesAsync(long? typeId, CancellationToken cancellationToken);
}
