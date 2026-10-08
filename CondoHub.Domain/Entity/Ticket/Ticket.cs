using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using CondoHub.Domain.Enum;

namespace CondoHub.Domain.Entity.Ticket;

public class Ticket : Util.Entity
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public TicketStatusEnum Status { get; set; } = TicketStatusEnum.Open;
    public long TypeId { get; set; }
    public long CategoryId { get; set; }
    public long UnitId { get; set; }
    public long CondominiumId { get; set; }

    [ForeignKey(nameof(TypeId))]
    [JsonIgnore]
    public TicketType? Type { get; set; }

    [ForeignKey(nameof(CategoryId))]
    [JsonIgnore]
    public TicketCategory? Category { get; set; }

    public List<TicketComment> Comments { get; set; } = new();
    public List<TicketAttachment> Attachments { get; set; } = new();
}
