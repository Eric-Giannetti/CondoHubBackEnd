using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CondoHub.Domain.Entity.Ticket;

public class TicketComment : Util.Entity
{
    public long TicketId { get; set; }
    public long UserId { get; set; }
    public string Message { get; set; } = string.Empty;

    [ForeignKey(nameof(TicketId))]
    [JsonIgnore]
    public Ticket? Ticket { get; set; }
}
