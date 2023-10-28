using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Terminal.Backend.Core.Repositories;
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
            x.UseNpgsql(postgresOptions.ConnectionString)
                .UseLoggerFactory(LoggerFactory.Create(b => b
                    .AddConsole()
                    .SetMinimumLevel(LogLevel.Debug)))
                .EnableSensitiveDataLogging());
        services.AddScoped(typeof(IUnitOfWork<>), typeof(PostgresUnitOfWork<>));
        services.AddScoped<IProjectRepository, ProjectRepository>();
        services.AddScoped<ITagRepository, TagRepository>();
        services.AddScoped<IParameterRepository, ParameterRepository>();
        services.AddScoped<IParameterValueRepository, ParameterValueRepository>();
        services.AddScoped<IRecipeRepository, RecipeRepository>();
        services.AddScoped<IStepsRepository, StepsRepository>();
        services.AddScoped<IMeasurementRepository, MeasurementRepository>();
        
        return services;
    }
}