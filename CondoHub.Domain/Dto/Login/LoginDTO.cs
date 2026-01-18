using CondoHub.Domain.Enum;

namespace CondoHub.Domain.Dto.Login;

public class LoginDTO
{
    public long UserId { get; set; }
    public List<TypeUserEnum>? TypeUser { get; set; }
    public string Token { get; set; }
    public bool FirstAccess { get; set; }

    public bool ValidLogin()
    {
        return
        (
            TypeUser.Count != 1
            || string.IsNullOrEmpty(Token)
            || UserId <= 0
        );
    }


    public LoginDTO()
    {
    }

    public LoginDTO(long userId, string token, List<TypeUserEnum>? listTypeUser)
    {
        UserId = userId;
        Token = token;
        TypeUser = listTypeUser;
        FirstAccess = false;
    }
    public LoginDTO(long userId, string token, List<TypeUserEnum>? listTypeUser, bool firstAccess)
    {
        UserId = userId;
        Token = token;
        TypeUser = listTypeUser;
        FirstAccess = firstAccess;
    }
}