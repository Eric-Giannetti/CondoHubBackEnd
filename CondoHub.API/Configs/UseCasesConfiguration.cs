using CondoHub.DataBase.MongoDb.RepositoryMongo;
using CondoHub.Domain.Interfaces.Repositorys;
using CondoHub.Domain.Interfaces.Services;
using CondoHub.Domain.Services;
using CondoHub.Services.Context;
using CondoHub.Services.Services;
using Microsoft.AspNetCore.Cors.Infrastructure;

namespace CondoHub.API.Configs;

public static class UseCasesConfiguration
{

    public static IServiceCollection AddUseCases(
        this IServiceCollection services)
    {
        services.AddHttpClient();
        services.AddRepositories();
        services.AddDomainEvents();
        return services;
    }

    private static IServiceCollection AddRepositories(
        this IServiceCollection services)
    {
        #region Services
        
        #endregion

        #region Repositories
        services.AddScoped<ICondominiumMongoRepository, CondominiumMongoRepository>();
        services.AddSingleton<ILogRepository, LogMongoRepository>();
        services.AddSingleton<ILogService, LogService>();
        #endregion

        return services;
    }

    private static IServiceCollection AddDomainEvents(
        this IServiceCollection services)
    {
        services.AddScoped<IUserContextService, UserContextService>();
        return services;
    }
}
