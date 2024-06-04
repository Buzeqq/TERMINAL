using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Terminal.Backend.Api;
using Terminal.Backend.Infrastructure.DAL;
using Testcontainers.PostgreSql;
using Xunit;

namespace Terminal.Backend.Integration;

public sealed class TerminalTestApp : WebApplicationFactory<Program>, IAsyncLifetime
{
    public HttpClient Client { get; private set; }

    private readonly PostgreSqlContainer _dbContainer = new PostgreSqlBuilder()
        .WithImage("postgres:latest")
        .WithDatabase("terminal")
        .WithUsername("root")
        .WithPassword("root")
        .Build();

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureAppConfiguration(configurationBuilder =>
        {
            configurationBuilder
                .AddJsonFile("appsettings.test.json", true)
                .AddEnvironmentVariables();
        });

        builder.ConfigureTestServices(services =>
        {
            var descriptorTypeTerminalDbContext =
                typeof(DbContextOptions<TerminalDbContext>);
            var descriptorTypeUserDbContext =
                typeof(DbContextOptions<UserDbContext>);

            var descriptorTerminal = services
                .SingleOrDefault(s => s.ServiceType == descriptorTypeTerminalDbContext);

            if (descriptorTerminal is not null)
            {
                services.Remove(descriptorTerminal);
            }

            var descriptorUser = services
                .SingleOrDefault(s => s.ServiceType == descriptorTypeUserDbContext);

            if (descriptorUser is not null)
            {
                services.Remove(descriptorUser);
            }

            services.AddDbContext<TerminalDbContext>(options =>
                options.UseNpgsql(this._dbContainer.GetConnectionString()));
            services.AddDbContext<UserDbContext>(options =>
                options.UseNpgsql(this._dbContainer.GetConnectionString()));
        });
    }

    public Task InitializeAsync()
    {
        this.Client = this.WithWebHostBuilder(builder =>
        {
            builder.UseEnvironment("Test");
        }).CreateClient();
        return this._dbContainer.StartAsync();
    }

    public new Task DisposeAsync() => this._dbContainer.StopAsync();
}
