using System.Data;

namespace CondoHub.DataBase.MySql.Dapper
{
    public interface IDbConnectionFactory
    {
        IDbConnection CreateConnection();
    }
}
