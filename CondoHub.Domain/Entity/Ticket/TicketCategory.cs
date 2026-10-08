using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CondoHub.Domain.Entity.Ticket;

public class TicketCategory : Util.Entity
{
    public string Name { get; set; } = string.Empty;
    public long TypeId { get; set; }

    [ForeignKey(nameof(TypeId))]
    [JsonIgnore]
    public TicketType? Type { get; set; }
}
