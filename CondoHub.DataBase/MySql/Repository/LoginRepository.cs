using CondoHub.DataBase.MySql.EntityFramework;
using CondoHub.Domain.Dto.Login;
using CondoHub.Domain.Entity;
using CondoHub.Domain.Interfaces.Repositorys;

namespace CondoHub.DataBase.MySql.Repository;

public class LoginRepository : ILoginRepository
{
    private readonly CondoHubContext _context;
    LoginRepository(CondoHubContext context)
    {
        context = _context;
    }
    public User? GetUserByCredentials(LoginCredentials credentials)
    {
        return _context.User.FirstOrDefault(u => 
                                                        u.Username == credentials.login && 
                                                        u.PasswordHash == credentials.password);
    }
    
    public UserData? GetUserDataById(long userId)
    {
        return _context.UserData.FirstOrDefault(u => u.UserId == userId);
    }
}