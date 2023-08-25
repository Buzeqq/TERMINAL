using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Terminal.Backend.Application.Abstractions;
using Terminal.Backend.Core.Repositories;
using Terminal.Backend.Infrastructure.DAL.Decorators;
using Terminal.Backend.Infrastructure.DAL.Repositories;

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
        services.AddScoped<IProjectRepository, ProjectRepository>();
        services.AddScoped<ITagRepository, TagRepository>();
        services.Decorate(typeof(ICommandHandler<>), typeof(UnitOfWorkCommandHandlerDecorator<>));
    
        return services;
    }
}