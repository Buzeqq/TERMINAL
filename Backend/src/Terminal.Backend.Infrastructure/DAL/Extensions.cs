using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Terminal.Backend.Infrastructure.DAL.Decorators;

namespace Terminal.Backend.Infrastructure.DAL;

internal static class Extensions
{
    private const string OptionsSectionName = "Postgres";
    
    public static IServiceCollection AddPostgres(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<PostgresOptions>(configuration.GetRequiredSection(OptionsSectionName));
        var postgresOptions = configuration.GetOptions<PostgresOptions>(OptionsSectionName);
        services.AddDbContext<TerminalDbContext>(x =>
            x.UseNpgsql(postgresOptions.ConnectionString));
        services.AddScoped<IUnitOfWork, PostgresUnitOfWork>();
        services.TryDecorate(typeof(IRequestHandler<>), typeof(UnitOfWorkCommandHandlerDecorator<>));
    
        return services;
    }
}