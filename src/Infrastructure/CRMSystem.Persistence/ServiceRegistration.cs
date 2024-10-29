using CRMSystem.Application.Interfaces;
using CRMSystem.Persistence.Concrete;
using CRMSystem.Persistence.DataAccess;
using CRMSystem.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace CRMSystem.Persistence;

public static class ServiceRegistration
{
    public static void AddPersistenceRegistration(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddDbContext<CRMDB>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

        services.Scan(scan => scan
             .FromAssembliesOf(typeof(IRepository<>))
             .AddClasses(classes => classes.AssignableTo(typeof(IRepository<>)))
             .AsImplementedInterfaces()
             .WithScopedLifetime());

        services.Scan(scan => scan
            .FromAssembliesOf(typeof(Repository<>))
            .AddClasses(classes => classes.AssignableTo(typeof(Repository<>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        services.AddScoped<IClaimManager, ClaimManager>();
        services.AddScoped<IUserManager, UserManager>();
        services.AddScoped<IEmailManager, EmailManager>();
    }
}
