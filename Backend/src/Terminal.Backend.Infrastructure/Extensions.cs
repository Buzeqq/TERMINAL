using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Terminal.Backend.Infrastructure.DAL;
using Terminal.Backend.Infrastructure.DAL.Behaviours;
using Terminal.Backend.Infrastructure.Middleware;

namespace Terminal.Backend.Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers();
        services.AddSingleton<ExceptionMiddleware>();
        services.AddHttpContextAccessor();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddCors();
        services.AddPostgres(configuration);
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(AssemblyReference.Assembly);
            cfg.AddOpenBehavior(typeof(UnitOfWorkBehaviour<,>));
        });

        return services;
    }

    public static WebApplication UseInfrastructure(this WebApplication app)
    {
        app.UseMiddleware<ExceptionMiddleware>();
        if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseCors(x => x
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowAnyOrigin());
        }

        // app.UseAuthentication();
        // app.UseAuthorization();
        app.MapControllers();
        
        if (!app.Configuration.GetOptions<PostgresOptions>("Postgres").Seed) return app;
        using var scope = app.Services.CreateScope();
        using var dbContext = scope.ServiceProvider.GetRequiredService<TerminalDbContext>();
        var seeder = new TerminalDbSeeder(dbContext);
        try
        {
            seeder.Seed();
        }
        catch (Exception) { } // todo

        // var measurement = dbContext.Measurements
        //     .Include(m => m.Steps)
        //     .Include(m => m.Tags)
        //     .SingleOrDefault(m => m.Id == Guid.Parse("125bd034-e27c-4b89-97ff-8a701b1c5316"));
        
        if (!app.Environment.IsProduction()) return app;
        dbContext.Database.Migrate();

        return app;
    }

    public static T GetOptions<T>(this IConfiguration configuration, string sectionName) where T : class, new()
    {
        var options = new T();
        var section = configuration.GetRequiredSection(sectionName);
        section.Bind(options);

        return options;
    }
}