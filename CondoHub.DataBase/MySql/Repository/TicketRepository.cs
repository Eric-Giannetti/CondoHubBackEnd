using CondoHub.DataBase.MySql.EntityFramework;
using CondoHub.Domain.Entity.Ticket;
using CondoHub.Domain.Enum;
using CondoHub.Domain.Interfaces.Repositorys;
using Microsoft.EntityFrameworkCore;

namespace CondoHub.DataBase.MySql.Repository;

public class TicketRepository : ITicketRepository
{
    private readonly CondoHubContext _context;

    public TicketRepository(CondoHubContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Ticket ticket, CancellationToken cancellationToken)
    {
        await _context.Ticket.AddAsync(ticket, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public Task<Ticket?> GetByIdAsync(long id, long condominiumId, CancellationToken cancellationToken)
    {
        return _context.Ticket
            .Include(t => t.Type)
            .Include(t => t.Category)
            .Where(t => !t.IsDeleted)
            .FirstOrDefaultAsync(t => t.Id == id && t.CondominiumId == condominiumId, cancellationToken);
    }

    public async Task<(List<Ticket> Items, int TotalCount)> GetPagedAsync(
        long condominiumId,
        TicketStatusEnum? status,
        long? typeId,
        int page,
        int pageSize,
        CancellationToken cancellationToken)
    {
        var query = _context.Ticket
            .Where(t => !t.IsDeleted && t.CondominiumId == condominiumId);

        if (status.HasValue)
            query = query.Where(t => t.Status == status.Value);

        if (typeId.HasValue)
            query = query.Where(t => t.TypeId == typeId.Value);

        var totalCount = await query.CountAsync(cancellationToken);

        var items = await query
            .OrderByDescending(t => t.CreatedAt)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return (items, totalCount);
    }

    public Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        return _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<TicketComment> AddCommentAsync(TicketComment comment, CancellationToken cancellationToken)
    {
        await _context.TicketComment.AddAsync(comment, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return comment;
    }

    public Task<List<TicketComment>> GetCommentsAsync(long ticketId, CancellationToken cancellationToken)
    {
        return _context.TicketComment
            .Where(c => c.TicketId == ticketId && !c.IsDeleted)
            .OrderBy(c => c.CreatedAt)
            .ToListAsync(cancellationToken);
    }

    public async Task<TicketAttachment> AddAttachmentAsync(TicketAttachment attachment, CancellationToken cancellationToken)
    {
        await _context.TicketAttachment.AddAsync(attachment, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return attachment;
    }

    public Task<List<TicketType>> GetTypesAsync(CancellationToken cancellationToken)
    {
        return _context.TicketType
            .Where(t => !t.IsDeleted)
            .OrderBy(t => t.Name)
            .ToListAsync(cancellationToken);
    }

    public Task<List<TicketCategory>> GetCategoriesAsync(long? typeId, CancellationToken cancellationToken)
    {
        var query = _context.TicketCategory.Where(c => !c.IsDeleted);

        if (typeId.HasValue)
            query = query.Where(c => c.TypeId == typeId.Value);

        return query.OrderBy(c => c.Name).ToListAsync(cancellationToken);
    }
}
