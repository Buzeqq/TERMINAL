using System.Reflection;
using Mapster;
using Microsoft.Extensions.DependencyInjection;
using Terminal.Backend.Application.Abstractions.Behaviors;
using Terminal.Backend.Application.Common;
using Terminal.Backend.Application.Common.Emails;
using Terminal.Backend.Application.Common.Services;
using Terminal.Backend.Application.DTO.ParameterValues;
using Terminal.Backend.Core.Abstractions;

namespace Terminal.Backend.Application;

public static class Extensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(AssemblyReference.Assembly);
            cfg.AddOpenBehavior(typeof(RequestLoggingPipelineBehavior<,>));
        });

        services.AddMapster();
        var typeAdapterConfig = TypeAdapterConfig.GlobalSettings;
        typeAdapterConfig.Scan(Assembly.GetExecutingAssembly());
        services.AddScoped<IConvertDtoService, ConvertDtoService>();
        services.AddScoped<IEmailConfirmationEmailSender, EmailConfirmationEmailSender>();
        services.AddTransient<IParameterValueVisitor<GetSampleBaseParameterValueDto>, ParameterValueToDtoVisitor>();

        return services;
    }
}
