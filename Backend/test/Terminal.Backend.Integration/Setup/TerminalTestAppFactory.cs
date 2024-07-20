using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Terminal.Backend.Api;
using Terminal.Backend.Application.Common;
using Terminal.Backend.Infrastructure.DAL;
using Testcontainers.PostgreSql;

namespace Terminal.Backend.Integration.Setup;

// ReSharper disable once ClassNeverInstantiated.Global
public sealed class TerminalTestAppFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    private readonly PostgreSqlContainer _dbContainer = new PostgreSqlBuilder()
        .WithImage("postgres:latest")
        .WithDatabase("terminal")
        .WithUsername("root")
        .WithPassword("root")
        .WithPortBinding(5433, 5432)
        .Build();


    protected override void ConfigureWebHost(IWebHostBuilder builder) =>
        builder
            .UseEnvironment("Test")
            .ConfigureTestServices(services =>
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

            var connectionString = string.Join(';', _dbContainer.GetConnectionString(), "Pooling=false");
            services.AddDbContext<TerminalDbContext>(options =>
            {
                options
                    .UseNpgsql(connectionString)
                    .UseSnakeCaseNamingConvention();
            });
            services.AddDbContext<UserDbContext>(options =>
            {
                options
                    .UseNpgsql(connectionString);
            });
        });

    public async Task InitializeAsync()
    {
        await _dbContainer.StartAsync();
        using var scope = Services.CreateScope();

        var terminalDbContext = scope.ServiceProvider.GetRequiredService<TerminalDbContext>();
        await terminalDbContext.Database.MigrateAsync();

        var userDbContext = scope.ServiceProvider.GetRequiredService<UserDbContext>();
        await userDbContext.Database.MigrateAsync();
        await InitUsers(scope.ServiceProvider);
    }

    Task IAsyncLifetime.DisposeAsync() => _dbContainer.StopAsync();

    private static async Task InitUsers(IServiceProvider serviceProvider)
    {
        var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

        await userManager.CreateAsync(Users.Admin, Users.Password);
        await userManager.AddToRoleAsync(Users.Admin, ApplicationRole.Administrator.Name!);

        await userManager.CreateAsync(Users.Moderator, Users.Password);
        await userManager.AddToRoleAsync(Users.Moderator, ApplicationRole.Moderator.Name!);

        await userManager.CreateAsync(Users.User, Users.Password);
        await userManager.AddToRoleAsync(Users.User, ApplicationRole.User.Name!);

        await userManager.CreateAsync(Users.Guest, Users.Password);
        await userManager.AddToRoleAsync(Users.Guest, ApplicationRole.Guest.Name!);
    }
}
