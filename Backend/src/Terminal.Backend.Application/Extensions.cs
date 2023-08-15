using Microsoft.Extensions.DependencyInjection;

namespace Terminal.Backend.Application;

public static class Extensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        return services;
    }
}