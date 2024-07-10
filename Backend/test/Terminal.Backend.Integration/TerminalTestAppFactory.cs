using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Terminal.Backend.Api;
using Terminal.Backend.Infrastructure.DAL;
using Testcontainers.PostgreSql;

namespace Terminal.Backend.Integration;

// ReSharper disable once ClassNeverInstantiated.Global
public sealed class TerminalTestAppFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    private readonly PostgreSqlContainer _dbContainer = new PostgreSqlBuilder()
        .WithImage("postgres:latest")
        .WithDatabase("terminal")
        .WithUsername("root")
        .WithPassword("root")
        .Build();


    protected override void ConfigureWebHost(IWebHostBuilder builder) =>
        builder.ConfigureTestServices(services =>
        {
            var descriptors = services
                .Where(d => d.ServiceType == typeof(DbContextOptions<TerminalDbContext>) ||
                            d.ServiceType == typeof(DbContextOptions<UserDbContext>));

            IEnumerable<ServiceDescriptor> serviceDescriptors = descriptors.ToList();
            if (serviceDescriptors.Any())
            {
                foreach (var descriptor in serviceDescriptors)
                {
                    services.Remove(descriptor);
                }
            }

            services.AddDbContext<TerminalDbContext>(options =>
            {
                options
                    .UseNpgsql(_dbContainer.GetConnectionString())
                    .UseSnakeCaseNamingConvention();
            });
            services.AddDbContext<UserDbContext>(options =>
            {
                options
                    .UseNpgsql(_dbContainer.GetConnectionString());
            });
        });

    public async Task InitializeAsync()
    {
        await _dbContainer.StartAsync();
        using var scope = Services.CreateScope();

        var terminalDbContext = scope.ServiceProvider.GetRequiredService<TerminalDbContext>();
        await terminalDbContext.Database.MigrateAsync();
        await terminalDbContext.DisposeAsync();

        var userDbContext = scope.ServiceProvider.GetRequiredService<UserDbContext>();
        await userDbContext.Database.MigrateAsync();
        await userDbContext.DisposeAsync();
    }

    Task IAsyncLifetime.DisposeAsync() => _dbContainer.StopAsync();
}
