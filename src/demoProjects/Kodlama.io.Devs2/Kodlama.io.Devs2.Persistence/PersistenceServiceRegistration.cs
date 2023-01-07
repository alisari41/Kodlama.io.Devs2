using Kodlama.io.Devs2.Application.Services.Repositories;
using Kodlama.io.Devs2.Persistence.Contexts;
using Kodlama.io.Devs2.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Kodlama.io.Devs2.Persistence;

public static class PersistenceServiceRegistration
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<BaseDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("ProgrammingLanguageConnectionString")));// Projenin Adı Sonrasında ConnectionString

        #region Repository'lerin Bağlanması
        services.AddScoped<IProgrammingLanguageRepository, ProgrammingLanguageRepository>();// Eğer Biri IProgrammingLanguageRepository isterse ona ProgrammingLanguageRepository ver 
        services.AddScoped<ITechnologyRepository, TechnologyRepository>();

        #region JWT ( Json Web Token ) ve UseAuthentication ların Bağlanması 
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IOperationClaimRepository, OperationClaimRepository>();
        services.AddScoped<IUserOperationClaimRepository, UserOperationClaimRepository>();
        services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
        #endregion

        #endregion

        return services;
    }
}
