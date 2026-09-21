using CondoHub.DataBase.MongoDb.RepositoryMongo;
using CondoHub.Domain.Interfaces.Repositorys;
using CondoHub.Domain.Services;
using CondoHub.Services.Context;
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
