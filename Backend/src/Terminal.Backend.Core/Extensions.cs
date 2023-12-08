using Microsoft.Extensions.DependencyInjection;

namespace Terminal.Backend.Core;

public static class Extensions
{
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        return services;
    }
}