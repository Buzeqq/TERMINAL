using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Terminal.Backend.Application.Abstractions;
using Terminal.Backend.Core.Entities;
using Terminal.Backend.Core.ValueObjects;
using Terminal.Backend.Infrastructure.Administrator;
using Terminal.Backend.Infrastructure.Authentication;
using Terminal.Backend.Infrastructure.Authentication.OptionsSetup;
using Terminal.Backend.Infrastructure.Authentication.Requirements;
using Terminal.Backend.Infrastructure.DAL;
using Terminal.Backend.Infrastructure.DAL.Behaviours;
using Terminal.Backend.Infrastructure.Mails;
using Terminal.Backend.Infrastructure.Middleware;
using IAuthorizationService = Terminal.Backend.Infrastructure.Authentication.IAuthorizationService;

namespace Terminal.Backend.Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers();
        services.AddSingleton<ExceptionMiddleware>();
        services.AddHttpContextAccessor();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(c =>
        {
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "JWT Authorization header using the Bearer scheme. Use 'Bearer {token}' format.",
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
        
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer();
        services.AddAuthorization();
        services.AddAuthorizationBuilder()
            .AddPolicy(Role.Registered, policy =>
            {
                policy.AddRequirements(new RoleRequirement(Role.Registered));
            })
            .AddPolicy(Role.Guest, policy =>
            {
                policy.AddRequirements(new RoleRequirement(Role.Guest));
            })
            .AddPolicy(Role.Moderator, policy =>
            {
                policy.AddRequirements(new RoleRequirement(Role.Registered));
            })
            .AddPolicy(Role.Administrator, policy =>
            {
                policy.AddRequirements(new RoleRequirement(Role.Administrator));
            });
        services.AddScoped<IAuthorizationService, AuthorizationService>();
        services.AddSingleton<IAuthorizationPolicyProvider, PermissionAuthorizationPolicyProvider>();
        services.AddSingleton<IAuthorizationHandler, PermissionAuthorizationHandler>();
        services.AddSingleton<IAuthorizationHandler, RoleAuthorizationHandler>();
        services.ConfigureOptions<JwtOptionsSetup>();
        services.ConfigureOptions<JwtBearerOptionsSetup>();
        services.ConfigureOptions<AdministratorOptionsSetup>();
        services.AddScoped<IJwtProvider, JwtProvider>();
        services.AddScoped<IMailService, MailService>();

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

        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();
        
        using var scope = app.Services.CreateScope();
        using var dbContext = scope.ServiceProvider.GetRequiredService<TerminalDbContext>();

        if (app.Configuration.GetOptions<PostgresOptions>("Postgres").Seed && app.Environment.IsDevelopment())
        {
            var seeder = new TerminalDbSeeder(dbContext);
            try
            {
                seeder.Seed();
            }
            catch (Exception)
            {
            }
        }

        if (app.Environment.IsProduction())
        {
            dbContext.Database.Migrate();
        }
        
        var passwordHasher = scope.ServiceProvider.GetRequiredService<IPasswordHasher>();
        var adminOptions = app.Configuration.GetOptions<AdministratorOptions>(AdministratorOptionsSetup.SectionName);
        try
        {
            var administratorExists = dbContext.Users.Any(u => u.Role == Role.Administrator);
            if (!administratorExists)
            {
                var adminRole = dbContext.Attach(Role.Administrator).Entity;
                var admin = User.CreateActiveUser(UserId.Create(), new Email(adminOptions.Email),
                    passwordHasher.Hash(adminOptions.Password));
                admin.SetRole(adminRole);
                dbContext.Users.Add(admin);
                
                dbContext.SaveChanges();
            }
        }
        catch (Exception)
        {
            // log admin already exists, skipping...
        }
        
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