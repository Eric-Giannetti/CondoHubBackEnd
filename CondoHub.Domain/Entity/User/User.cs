namespace CondoHub.Domain.Entity;

public class User
{
    public long Id { get; set; }
    public string Username { get; set; }
    public string PasswordHash { get; set; }
    public bool IsAdm { get; set; }
}