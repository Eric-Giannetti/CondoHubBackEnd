namespace CondoHub.Domain.Dto.Login;

public class LoginCredentials
{
    public string login { get; set; }
    public string password { get; set; }
    
    public bool IsValid()
    {
        return !string.IsNullOrEmpty(login) && !string.IsNullOrEmpty(password);
    }
}