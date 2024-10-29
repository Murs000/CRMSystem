using System.Reflection;
using CRMSystem.Application.Common.Behaviour;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace CRMSystem.Application;

public static class ServiceRegistration
{
    public static void AddApplicationRegistration(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
    }
}