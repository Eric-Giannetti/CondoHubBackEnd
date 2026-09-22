using CondoHub.DataBase.MySql.EntityFramework;
using CondoHub.DataBase.MySql.Dapper;
using CondoHub.DataBase.MongoDb.Context;
using Microsoft.EntityFrameworkCore;

namespace CondoHub.API.Configs;

public static class ConnectionsConfiguration
{
    public static IServiceCollection AddAppConnections(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbConnection(configuration);

        return services;
    }

    private static IServiceCollection AddDbConnection(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("CondoHubConnectionString");
        var mongoConnectionString = configuration.GetConnectionString("MongoDbConnectionString");

        services.AddDbContext<CondoHubContext>(options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
        services.AddSingleton<IDbConnectionFactory, DbConnectionFactory>();
        services.AddSingleton<MongoDbContext>();

        services.AddHealthChecks()
            .AddMySql(connectionString!)
            .AddMongoDb(mongoConnectionString!);

        return services;
    }

    public static WebApplication MigrateDatabase(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var services = scope.ServiceProvider;
        var context = services.GetRequiredService<CondoHubContext>();
        context.Database.Migrate();
        return app;
    }
}
