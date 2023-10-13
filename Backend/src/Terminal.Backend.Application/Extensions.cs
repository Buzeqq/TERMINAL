using Microsoft.Extensions.DependencyInjection;
using Terminal.Backend.Application.Services;

namespace Terminal.Backend.Application;

public static class Extensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(AssemblyReference.Assembly);
        });

        services.AddScoped<IConvertDtoService, ConvertDtoService>();
        
        return services;
    }
}