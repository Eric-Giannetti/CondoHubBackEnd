namespace CondoHub.Domain.Entity.Ticket;

public class TicketType : Util.Entity
{
    public string Name { get; set; } = string.Empty;

    public List<TicketCategory> Categories { get; set; } = new();
}
