using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CondoHub.Domain.Entity.Ticket;

public class TicketAttachment : Util.Entity
{
    public long TicketId { get; set; }
    public long UploadedBy { get; set; }
    public string FileName { get; set; } = string.Empty;
    public string ContentType { get; set; } = string.Empty;
    public string StoragePath { get; set; } = string.Empty;
    public long FileSize { get; set; }

    [ForeignKey(nameof(TicketId))]
    [JsonIgnore]
    public Ticket? Ticket { get; set; }
}
