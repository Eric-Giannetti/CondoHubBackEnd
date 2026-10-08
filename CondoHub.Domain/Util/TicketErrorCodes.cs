namespace CondoHub.Domain.Util;

/// <summary>Well-known error codes returned inside <see cref="Result{T}"/> for ticket operations.</summary>
public static class TicketErrorCodes
{
    public const string NotFound = "TICKET_NOT_FOUND";
    public const string StatusConflict = "TICKET_STATUS_CONFLICT";
}
