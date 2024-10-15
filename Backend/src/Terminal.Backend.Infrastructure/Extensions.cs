using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;
using Terminal.Backend.Application.Common;
using Terminal.Backend.Infrastructure.Authorization;
using Terminal.Backend.Infrastructure.DAL;
using Terminal.Backend.Infrastructure.DAL.Behaviours;
using Terminal.Backend.Infrastructure.Identity;
using Terminal.Backend.Infrastructure.Identity.Mails;
using Terminal.Backend.Infrastructure.Middleware;

namespace Terminal.Backend.Infrastructure;

public static class Extensions
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddExceptionHandler<DefaultExceptionHandler>();
        services.AddControllers();
        services.AddSingleton<RequestLogContextMiddleware>();
        services.AddSingleton(TimeProvider.System);
        services.AddHttpContextAccessor();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(c =>
        {
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "Authorization header using the Bearer scheme. Use 'Bearer {token}' format.",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        },
                        Scheme = "oauth2",
                        Name = "Bearer",
                        In = ParameterLocation.Header,
                    },
                    new List<string>()
                }
            });
        });
        services.AddCors();
        services.AddPostgres(configuration);
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(AssemblyReference.Assembly);
            cfg.AddOpenBehavior(typeof(UnitOfWorkBehaviour<,>));
        });

        services.AddAuthorizationBuilder();
        services.AddAntiforgery();

        services.AddIdentity();
        services.AddTerminalAuthorization();

        services.AddHttpClient(nameof(EmailSender), (serviceProvider, httpClient) =>
        {
            var options = serviceProvider.GetRequiredService<IOptions<EmailSenderOptions>>().Value;
            httpClient.BaseAddress = new Uri(options.BaseAddress);
            httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {options.Token}");
        });
        services.AddScoped<IEmailSender<ApplicationUser>, EmailSender>();

        services.AddOptions<EmailSenderOptions>()
            .BindConfiguration(nameof(EmailSender))
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services.AddOptions<CorsOptions>()
            .BindConfiguration(nameof(CorsOptions))
            .ValidateDataAnnotations()
            .ValidateOnStart();
    }

    public static void UseInfrastructure(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.SeedData();
        }

        app.UseExceptionHandler();
        app.UseMiddleware<RequestLogContextMiddleware>();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.DocExpansion(DocExpansion.None);
                c.EnableFilter();
                c.EnableDeepLinking();
            });
        }

        var allowedOrigins = app.Configuration.GetOptions<CorsOptions>(nameof(CorsOptions)).AllowedOrigins;
        app.UseCors(x => x
            .AllowCredentials()
            .WithOrigins(allowedOrigins)
            .AllowAnyHeader()
            .AllowAnyMethod());

        app.UseAuthentication();
        app.UseAuthorization();
        app.UseAntiforgery();

        app.MapControllers();
    }

    private static void SeedData(this WebApplication app)
    {
        try
        {
            using var scope = app.Services.CreateScope();
            using var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            if (userManager.Users.Count() <= 1)
            {
                var identityDbSeeder = new IdentityDbSeeder(userManager);
                identityDbSeeder.Seed();
            }

            var terminalDbContext = scope.ServiceProvider.GetRequiredService<TerminalDbContext>();
            if (terminalDbContext.Samples.Count() > 1)
            {
                return;
            }

            var terminalDbSeeder = new TerminalDbSeeder(terminalDbContext);
            terminalDbSeeder.Seed();
        }
        catch (Exception)
        {
            // ignored
        }
    }

    public static T GetOptions<T>(this IConfiguration configuration, string sectionName) where T : class, new()
    {
        var options = new T();
        var section = configuration.GetRequiredSection(sectionName);
        section.Bind(options);

        return options;
    }
}
