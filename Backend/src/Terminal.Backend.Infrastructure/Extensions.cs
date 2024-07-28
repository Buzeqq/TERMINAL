using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;
using Terminal.Backend.Application.Common;
using Terminal.Backend.Core.Entities;
using Terminal.Backend.Core.Entities.Parameters;
using Terminal.Backend.Core.Entities.ParameterValues;
using Terminal.Backend.Core.ValueObjects;
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

            var admin = new ApplicationUser
            {
                Email = "admin@terminal.com", UserName = "admin@terminal.com", EmailConfirmed = true
            };
            userManager.CreateAsync(admin, "1qaz@WSX").Wait();
            userManager.AddToRoleAsync(admin, nameof(ApplicationRole.Administrator)).Wait();

            var terminalDbContext = scope.ServiceProvider.GetRequiredService<TerminalDbContext>();

            #region tags

            var tag1 = new Tag(TagId.Create(), "new-sample");
            var tag2 = new Tag(TagId.Create(), "methane-rich");
            var tag3 = new Tag(TagId.Create(), "popular-sample");
            var tag4 = new Tag(TagId.Create(), "hot");
            var tag5 = new Tag(TagId.Create(), "high-pressure");

            terminalDbContext.Tags.AddRange(tag1, tag2, tag3, tag4, tag5);

            #endregion

            #region projects

            var projectUpturn = new Project(ProjectId.Create(), "Upturn");
            var projectBessy2 = new Project(ProjectId.Create(), "Bessy 2");
            var projectNitro = new Project(ProjectId.Create(), "Nitro");
            var projectNobelium = new Project(ProjectId.Create(), "Nobelium");

            terminalDbContext.Projects.AddRange(projectUpturn, projectBessy2, projectNitro, projectNobelium);

            #endregion

            #region parameters

            var bcParameter = new IntegerParameter(ParameterId.Create(), "B/C", "ppm", 1);
            var hydrogenParameter = new IntegerParameter(ParameterId.Create(), "H\u2082", "sccm", 1);
            var methaneParameter = new IntegerParameter(ParameterId.Create(), "CH\u2084", "sccm", 1);
            var diboranParameter = new IntegerParameter(ParameterId.Create(), "B\u2082H\u2086", "sccm", 1);
            var nucleationParameter = new TextParameter(ParameterId.Create(), "Nucleation Method",
                new List<string>
                {
                    "spin-coating",
                    "nucleation",
                    "dip-coating",
                    "without nucleation"
                });
            var temperatureParameter = new IntegerParameter(ParameterId.Create(), "Temperature", "C\u2070", 1);
            var pressureParameter = new IntegerParameter(ParameterId.Create(), "Pressure", "Torr", 1);
            var powerParameter = new IntegerParameter(ParameterId.Create(), "Pmw", "W", 1);
            var timeParameter = new DecimalParameter(ParameterId.Create(), "Time", "h", 0.1m);
            var substrateParameter = new TextParameter(ParameterId.Create(), "Substrate",
                new List<string>
                {
                    "silicon",
                    "silicon dioxide",
                    "glass",
                    "tantalum"
                });
            var bufferParameter = new DecimalParameter(ParameterId.Create(), "Buffer", "h", 0.1m);
            var additionalGasesParameter = new TextParameter(ParameterId.Create(), "Additional gases",
                new List<string> { "none", "nitrogen", "oxygen" }, order: 12);
            var additionalGassesAmountParameter =
                new IntegerParameter(ParameterId.Create(), "Additional gases amount", "sccm", 1);
            additionalGassesAmountParameter.SetParent(additionalGasesParameter);
            terminalDbContext.Parameters.AddRange(bufferParameter,
                substrateParameter,
                timeParameter,
                powerParameter,
                pressureParameter,
                temperatureParameter,
                nucleationParameter,
                diboranParameter,
                methaneParameter,
                bcParameter,
                hydrogenParameter,
                additionalGasesParameter,
                additionalGassesAmountParameter);

            terminalDbContext.Parameters.AddRange(bufferParameter,
                substrateParameter,
                timeParameter,
                powerParameter,
                pressureParameter,
                temperatureParameter,
                nucleationParameter,
                diboranParameter,
                methaneParameter,
                bcParameter,
                hydrogenParameter,
                additionalGasesParameter);

            #endregion

            #region steps

            var step1 = new SampleStep(StepId.Create(), new Comment("First step!"));
            step1.Parameters.Add(new IntegerParameterValue(ParameterValueId.Create(), bcParameter, 2000));
            step1.Parameters.Add(new IntegerParameterValue(ParameterValueId.Create(), hydrogenParameter, 300));
            step1.Parameters.Add(new IntegerParameterValue(ParameterValueId.Create(), methaneParameter, 100));
            step1.Parameters.Add(new IntegerParameterValue(ParameterValueId.Create(), diboranParameter, 240));
            step1.Parameters.Add(new TextParameterValue(ParameterValueId.Create(), nucleationParameter,
                nucleationParameter.AllowedValues.First()));
            step1.Parameters.Add(new IntegerParameterValue(ParameterValueId.Create(), temperatureParameter, 800));
            step1.Parameters.Add(new IntegerParameterValue(ParameterValueId.Create(), pressureParameter, 20));
            step1.Parameters.Add(new IntegerParameterValue(ParameterValueId.Create(), powerParameter, 1300));
            step1.Parameters.Add(new DecimalParameterValue(ParameterValueId.Create(), timeParameter, 2.3m));
            step1.Parameters.Add(new TextParameterValue(ParameterValueId.Create(), substrateParameter,
                substrateParameter.AllowedValues.First()));
            step1.Parameters.Add(new DecimalParameterValue(ParameterValueId.Create(), bufferParameter, 0.0m));
            step1.Parameters.Add(new TextParameterValue(ParameterValueId.Create(), additionalGasesParameter,
                additionalGasesParameter.AllowedValues.First()));
            step1.Parameters.Add(new IntegerParameterValue(ParameterValueId.Create(), bcParameter, 2000));

            var step2 = new SampleStep(StepId.Create(), new Comment("First step!"));
            step2.Parameters.Add(new IntegerParameterValue(ParameterValueId.Create(), bcParameter, 2000));
            step2.Parameters.Add(new IntegerParameterValue(ParameterValueId.Create(), hydrogenParameter, 300));
            step2.Parameters.Add(new IntegerParameterValue(ParameterValueId.Create(), methaneParameter, 100));
            step2.Parameters.Add(new IntegerParameterValue(ParameterValueId.Create(), diboranParameter, 240));
            step2.Parameters.Add(new TextParameterValue(ParameterValueId.Create(), nucleationParameter,
                nucleationParameter.AllowedValues.First()));
            step2.Parameters.Add(new IntegerParameterValue(ParameterValueId.Create(), temperatureParameter, 800));
            step2.Parameters.Add(new IntegerParameterValue(ParameterValueId.Create(), pressureParameter, 20));
            step2.Parameters.Add(new IntegerParameterValue(ParameterValueId.Create(), powerParameter, 1300));
            step2.Parameters.Add(new DecimalParameterValue(ParameterValueId.Create(), timeParameter, 2.3m));
            step2.Parameters.Add(new TextParameterValue(ParameterValueId.Create(), substrateParameter,
                substrateParameter.AllowedValues.First()));
            step2.Parameters.Add(new DecimalParameterValue(ParameterValueId.Create(), bufferParameter, 0.0m));
            step2.Parameters.Add(new TextParameterValue(ParameterValueId.Create(), additionalGasesParameter,
                additionalGasesParameter.AllowedValues.First()));

            var step3 = new SampleStep(StepId.Create(), new Comment("First step!"));
            step3.Parameters.Add(new IntegerParameterValue(ParameterValueId.Create(), bcParameter, 2000));
            step3.Parameters.Add(new IntegerParameterValue(ParameterValueId.Create(), hydrogenParameter, 300));
            step3.Parameters.Add(new IntegerParameterValue(ParameterValueId.Create(), methaneParameter, 100));
            step3.Parameters.Add(new IntegerParameterValue(ParameterValueId.Create(), diboranParameter, 240));
            step3.Parameters.Add(new TextParameterValue(ParameterValueId.Create(), nucleationParameter,
                nucleationParameter.AllowedValues.First()));
            step3.Parameters.Add(new IntegerParameterValue(ParameterValueId.Create(), temperatureParameter, 800));
            step3.Parameters.Add(new IntegerParameterValue(ParameterValueId.Create(), pressureParameter, 20));
            step3.Parameters.Add(new IntegerParameterValue(ParameterValueId.Create(), powerParameter, 1300));
            step3.Parameters.Add(new DecimalParameterValue(ParameterValueId.Create(), timeParameter, 2.3m));
            step3.Parameters.Add(new TextParameterValue(ParameterValueId.Create(), substrateParameter,
                substrateParameter.AllowedValues.First()));
            step3.Parameters.Add(new DecimalParameterValue(ParameterValueId.Create(), bufferParameter, 0.0m));
            step3.Parameters.Add(new TextParameterValue(ParameterValueId.Create(), additionalGasesParameter,
                additionalGasesParameter.AllowedValues.First()));

            var step4 = new SampleStep(StepId.Create(), new Comment("First step!"));
            step4.Parameters.Add(new IntegerParameterValue(ParameterValueId.Create(), bcParameter, 2000));
            step4.Parameters.Add(new IntegerParameterValue(ParameterValueId.Create(), hydrogenParameter, 300));
            step4.Parameters.Add(new IntegerParameterValue(ParameterValueId.Create(), methaneParameter, 100));
            step4.Parameters.Add(new IntegerParameterValue(ParameterValueId.Create(), diboranParameter, 240));
            step4.Parameters.Add(new TextParameterValue(ParameterValueId.Create(), nucleationParameter,
                nucleationParameter.AllowedValues.First()));
            step4.Parameters.Add(new IntegerParameterValue(ParameterValueId.Create(), temperatureParameter, 800));
            step4.Parameters.Add(new IntegerParameterValue(ParameterValueId.Create(), pressureParameter, 20));
            step4.Parameters.Add(new IntegerParameterValue(ParameterValueId.Create(), powerParameter, 1300));
            step4.Parameters.Add(new DecimalParameterValue(ParameterValueId.Create(), timeParameter, 2.3m));
            step4.Parameters.Add(new TextParameterValue(ParameterValueId.Create(), substrateParameter,
                substrateParameter.AllowedValues.First()));
            step4.Parameters.Add(new DecimalParameterValue(ParameterValueId.Create(), bufferParameter, 0.0m));
            step4.Parameters.Add(new TextParameterValue(ParameterValueId.Create(), additionalGasesParameter,
                additionalGasesParameter.AllowedValues.First()));


            var step5 = new SampleStep(StepId.Create(), new Comment("First step!"));
            step5.Parameters.Add(new IntegerParameterValue(ParameterValueId.Create(), bcParameter, 2000));
            step5.Parameters.Add(new IntegerParameterValue(ParameterValueId.Create(), hydrogenParameter, 300));
            step5.Parameters.Add(new IntegerParameterValue(ParameterValueId.Create(), methaneParameter, 100));
            step5.Parameters.Add(new IntegerParameterValue(ParameterValueId.Create(), diboranParameter, 240));
            step5.Parameters.Add(new TextParameterValue(ParameterValueId.Create(), nucleationParameter,
                nucleationParameter.AllowedValues.First()));
            step5.Parameters.Add(new IntegerParameterValue(ParameterValueId.Create(), temperatureParameter, 800));
            step5.Parameters.Add(new IntegerParameterValue(ParameterValueId.Create(), pressureParameter, 20));
            step5.Parameters.Add(new IntegerParameterValue(ParameterValueId.Create(), powerParameter, 1300));
            step5.Parameters.Add(new DecimalParameterValue(ParameterValueId.Create(), timeParameter, 2.3m));
            step5.Parameters.Add(new TextParameterValue(ParameterValueId.Create(), substrateParameter,
                substrateParameter.AllowedValues.First()));
            step5.Parameters.Add(new DecimalParameterValue(ParameterValueId.Create(), bufferParameter, 0.0m));
            step5.Parameters.Add(new TextParameterValue(ParameterValueId.Create(), additionalGasesParameter,
                additionalGasesParameter.AllowedValues.First()));

            var step6 = new SampleStep(StepId.Create(), new Comment("First step!"));
            step6.Parameters.Add(new IntegerParameterValue(ParameterValueId.Create(), bcParameter, 2000));
            step6.Parameters.Add(new IntegerParameterValue(ParameterValueId.Create(), hydrogenParameter, 300));
            step6.Parameters.Add(new IntegerParameterValue(ParameterValueId.Create(), methaneParameter, 100));
            step6.Parameters.Add(new IntegerParameterValue(ParameterValueId.Create(), diboranParameter, 240));
            step6.Parameters.Add(new TextParameterValue(ParameterValueId.Create(), nucleationParameter,
                nucleationParameter.AllowedValues.First()));
            step6.Parameters.Add(new IntegerParameterValue(ParameterValueId.Create(), temperatureParameter, 800));
            step6.Parameters.Add(new IntegerParameterValue(ParameterValueId.Create(), pressureParameter, 20));
            step6.Parameters.Add(new IntegerParameterValue(ParameterValueId.Create(), powerParameter, 1300));
            step6.Parameters.Add(new DecimalParameterValue(ParameterValueId.Create(), timeParameter, 2.3m));
            step6.Parameters.Add(new TextParameterValue(ParameterValueId.Create(), substrateParameter,
                substrateParameter.AllowedValues.First()));
            step6.Parameters.Add(new DecimalParameterValue(ParameterValueId.Create(), bufferParameter, 0.0m));
            step6.Parameters.Add(new TextParameterValue(ParameterValueId.Create(), additionalGasesParameter,
                additionalGasesParameter.AllowedValues.First()));

            var step7 = new SampleStep(StepId.Create(), new Comment("First step!"));
            step7.Parameters.Add(new IntegerParameterValue(ParameterValueId.Create(), bcParameter, 2000));
            step7.Parameters.Add(new IntegerParameterValue(ParameterValueId.Create(), hydrogenParameter, 300));
            step7.Parameters.Add(new IntegerParameterValue(ParameterValueId.Create(), methaneParameter, 100));
            step7.Parameters.Add(new IntegerParameterValue(ParameterValueId.Create(), diboranParameter, 240));
            step7.Parameters.Add(new TextParameterValue(ParameterValueId.Create(), nucleationParameter,
                nucleationParameter.AllowedValues.First()));
            step7.Parameters.Add(new IntegerParameterValue(ParameterValueId.Create(), temperatureParameter, 800));
            step7.Parameters.Add(new IntegerParameterValue(ParameterValueId.Create(), pressureParameter, 20));
            step7.Parameters.Add(new IntegerParameterValue(ParameterValueId.Create(), powerParameter, 1300));
            step7.Parameters.Add(new DecimalParameterValue(ParameterValueId.Create(), timeParameter, 2.3m));
            step7.Parameters.Add(new TextParameterValue(ParameterValueId.Create(), substrateParameter,
                substrateParameter.AllowedValues.First()));
            step7.Parameters.Add(new DecimalParameterValue(ParameterValueId.Create(), bufferParameter, 0.0m));
            step7.Parameters.Add(new TextParameterValue(ParameterValueId.Create(), additionalGasesParameter,
                additionalGasesParameter.AllowedValues.First()));

            var step8 = new SampleStep(StepId.Create(), new Comment("First step!"));
            step8.Parameters.Add(new IntegerParameterValue(ParameterValueId.Create(), bcParameter, 2000));
            step8.Parameters.Add(new IntegerParameterValue(ParameterValueId.Create(), hydrogenParameter, 300));
            step8.Parameters.Add(new IntegerParameterValue(ParameterValueId.Create(), methaneParameter, 100));
            step8.Parameters.Add(new IntegerParameterValue(ParameterValueId.Create(), diboranParameter, 240));
            step8.Parameters.Add(new TextParameterValue(ParameterValueId.Create(), nucleationParameter,
                nucleationParameter.AllowedValues.First()));
            step8.Parameters.Add(new IntegerParameterValue(ParameterValueId.Create(), temperatureParameter, 800));
            step8.Parameters.Add(new IntegerParameterValue(ParameterValueId.Create(), pressureParameter, 20));
            step8.Parameters.Add(new IntegerParameterValue(ParameterValueId.Create(), powerParameter, 1300));
            step8.Parameters.Add(new DecimalParameterValue(ParameterValueId.Create(), timeParameter, 2.3m));
            step8.Parameters.Add(new TextParameterValue(ParameterValueId.Create(), substrateParameter,
                substrateParameter.AllowedValues.First()));
            step8.Parameters.Add(new DecimalParameterValue(ParameterValueId.Create(), bufferParameter, 0.0m));
            step8.Parameters.Add(new TextParameterValue(ParameterValueId.Create(), additionalGasesParameter,
                additionalGasesParameter.AllowedValues.First()));

            var step9 = new SampleStep(StepId.Create(), new Comment("First step!"));
            step9.Parameters.Add(new IntegerParameterValue(ParameterValueId.Create(), bcParameter, 2000));
            step9.Parameters.Add(new IntegerParameterValue(ParameterValueId.Create(), hydrogenParameter, 300));
            step9.Parameters.Add(new IntegerParameterValue(ParameterValueId.Create(), methaneParameter, 100));
            step9.Parameters.Add(new IntegerParameterValue(ParameterValueId.Create(), diboranParameter, 240));
            step9.Parameters.Add(new TextParameterValue(ParameterValueId.Create(), nucleationParameter,
                nucleationParameter.AllowedValues.First()));
            step9.Parameters.Add(new IntegerParameterValue(ParameterValueId.Create(), temperatureParameter, 800));
            step9.Parameters.Add(new IntegerParameterValue(ParameterValueId.Create(), pressureParameter, 20));
            step9.Parameters.Add(new IntegerParameterValue(ParameterValueId.Create(), powerParameter, 1300));
            step9.Parameters.Add(new DecimalParameterValue(ParameterValueId.Create(), timeParameter, 2.3m));
            step9.Parameters.Add(new TextParameterValue(ParameterValueId.Create(), substrateParameter,
                substrateParameter.AllowedValues.First()));
            step9.Parameters.Add(new DecimalParameterValue(ParameterValueId.Create(), bufferParameter, 0.0m));
            step9.Parameters.Add(new TextParameterValue(ParameterValueId.Create(), additionalGasesParameter,
                additionalGasesParameter.AllowedValues.First()));

            var step10 = new SampleStep(StepId.Create(), new Comment("First step!"));
            step10.Parameters.Add(new IntegerParameterValue(ParameterValueId.Create(), bcParameter, 2000));
            step10.Parameters.Add(new IntegerParameterValue(ParameterValueId.Create(), hydrogenParameter, 300));
            step10.Parameters.Add(new IntegerParameterValue(ParameterValueId.Create(), methaneParameter, 100));
            step10.Parameters.Add(new IntegerParameterValue(ParameterValueId.Create(), diboranParameter, 240));
            step10.Parameters.Add(new TextParameterValue(ParameterValueId.Create(), nucleationParameter,
                nucleationParameter.AllowedValues.First()));
            step10.Parameters.Add(new IntegerParameterValue(ParameterValueId.Create(), temperatureParameter, 800));
            step10.Parameters.Add(new IntegerParameterValue(ParameterValueId.Create(), pressureParameter, 20));
            step10.Parameters.Add(new IntegerParameterValue(ParameterValueId.Create(), powerParameter, 1300));
            step10.Parameters.Add(new DecimalParameterValue(ParameterValueId.Create(), timeParameter, 2.3m));
            step10.Parameters.Add(new TextParameterValue(ParameterValueId.Create(), substrateParameter,
                substrateParameter.AllowedValues.First()));
            step10.Parameters.Add(new DecimalParameterValue(ParameterValueId.Create(), bufferParameter, 0.0m));
            step10.Parameters.Add(new TextParameterValue(ParameterValueId.Create(), additionalGasesParameter,
                additionalGasesParameter.AllowedValues.First()));

            #endregion

            #region samples

            var sample1 = new Sample(
                SampleId.Create(),
                projectUpturn,
                null,
                new Comment("First sample!"),
                new List<SampleStep> { step1 },
                new List<Tag> { tag1, tag3, tag5 });
            terminalDbContext.Samples.Add(sample1);
            terminalDbContext.SaveChanges();

            var sample2 = new Sample(
                SampleId.Create(),
                projectUpturn,
                null,
                new Comment("First sample!"),
                new List<SampleStep> { step2 },
                new List<Tag> { tag1, tag3, tag5 });
            terminalDbContext.Samples.Add(sample2);
            terminalDbContext.SaveChanges();

            var sample3 = new Sample(
                SampleId.Create(),
                projectBessy2,
                null,
                new Comment("First sample!"),
                new List<SampleStep> { step3 },
                new List<Tag> { tag1, tag3, tag5 });
            terminalDbContext.Samples.Add(sample3);
            terminalDbContext.SaveChanges();

            var sample4 = new Sample(
                SampleId.Create(),
                projectBessy2,
                null,
                new Comment("First sample!"),
                new List<SampleStep> { step4 },
                new List<Tag> { tag1, tag3, tag5 });
            terminalDbContext.Samples.Add(sample4);
            terminalDbContext.SaveChanges();

            var sample5 = new Sample(
                SampleId.Create(),
                projectNitro,
                null,
                new Comment("First sample!"),
                new List<SampleStep> { step5 },
                new List<Tag> { tag1, tag3, tag5 });
            terminalDbContext.Samples.Add(sample5);
            terminalDbContext.SaveChanges();

            var sample6 = new Sample(
                SampleId.Create(),
                projectNitro,
                null,
                new Comment("First sample!"),
                new List<SampleStep> { step6 },
                new List<Tag> { tag1, tag3, tag5 });
            terminalDbContext.Samples.Add(sample6);
            terminalDbContext.SaveChanges();

            var sample7 = new Sample(
                SampleId.Create(),
                projectNobelium,
                null,
                new Comment("First sample!"),
                new List<SampleStep> { step7 },
                new List<Tag> { tag1, tag3, tag5 });
            terminalDbContext.Samples.Add(sample7);
            terminalDbContext.SaveChanges();

            var sample8 = new Sample(
                SampleId.Create(),
                projectNobelium,
                null,
                new Comment("First sample!"),
                new List<SampleStep> { step8 },
                new List<Tag> { tag1, tag3, tag5 });
            terminalDbContext.Samples.Add(sample8);
            terminalDbContext.SaveChanges();

            var sample9 = new Sample(
                SampleId.Create(),
                projectNobelium,
                null,
                new Comment("First sample!"),
                new List<SampleStep> { step9 },
                new List<Tag> { tag1, tag3, tag5 });
            terminalDbContext.Samples.Add(sample9);
            terminalDbContext.SaveChanges();

            var sample10 = new Sample(
                SampleId.Create(),
                projectNitro,
                null,
                new Comment("First sample!"),
                new List<SampleStep> { step10 },
                new List<Tag> { tag1, tag3, tag5 });
            terminalDbContext.Samples.Add(sample10);
            terminalDbContext.SaveChanges();

            var sample11 = new Sample(
                SampleId.Create(),
                projectNitro,
                null,
                new Comment("First sample!"),
                new List<SampleStep> { step10 },
                new List<Tag> { tag1, tag3, tag5 });
            terminalDbContext.Samples.Add(sample11);
            terminalDbContext.SaveChanges();

            var sample12 = new Sample(
                SampleId.Create(),
                projectNitro,
                null,
                new Comment("First sample!"),
                new List<SampleStep> { step10 },
                new List<Tag> { tag1, tag3, tag5 });
            terminalDbContext.Samples.Add(sample12);
            terminalDbContext.SaveChanges();

            var sample13 = new Sample(
                SampleId.Create(),
                projectNitro,
                null,
                new Comment("First sample!"),
                new List<SampleStep> { step10 },
                new List<Tag> { tag1, tag3, tag5 });
            terminalDbContext.Samples.Add(sample13);
            terminalDbContext.SaveChanges();

            var sample14 = new Sample(
                SampleId.Create(),
                projectNitro,
                null,
                new Comment("First sample!"),
                new List<SampleStep> { step10 },
                new List<Tag> { tag1, tag3, tag5 });
            terminalDbContext.Samples.Add(sample14);
            terminalDbContext.SaveChanges();

            var sample15 = new Sample(
                SampleId.Create(),
                projectNitro,
                null,
                new Comment("First sample!"),
                new List<SampleStep> { step10 },
                new List<Tag> { tag1, tag3, tag5 });
            terminalDbContext.Samples.Add(sample15);
            terminalDbContext.SaveChanges();

            var sample16 = new Sample(
                SampleId.Create(),
                projectNitro,
                null,
                new Comment("First sample!"),
                new List<SampleStep> { step10 },
                new List<Tag> { tag1, tag3, tag5 });
            terminalDbContext.Samples.Add(sample16);
            terminalDbContext.SaveChanges();

            var sample17 = new Sample(
                SampleId.Create(),
                projectNitro,
                null,
                new Comment("First sample!"),
                new List<SampleStep> { step10 },
                new List<Tag> { tag1, tag3, tag5 });
            terminalDbContext.Samples.Add(sample17);
            terminalDbContext.SaveChanges();

            var sample18 = new Sample(
                SampleId.Create(),
                projectNitro,
                null,
                new Comment("First sample!"),
                new List<SampleStep> { step10 },
                new List<Tag> { tag1, tag3, tag5 });
            terminalDbContext.Samples.Add(sample18);
            terminalDbContext.SaveChanges();

            var sample19 = new Sample(
                SampleId.Create(),
                projectNitro,
                null,
                new Comment("First sample!"),
                new List<SampleStep> { step10 },
                new List<Tag> { tag1, tag3, tag5 });
            terminalDbContext.Samples.Add(sample19);
            terminalDbContext.SaveChanges();

            var sample20 = new Sample(
                SampleId.Create(),
                projectNitro,
                null,
                new Comment("First sample!"),
                new List<SampleStep> { step10 },
                new List<Tag> { tag1, tag3, tag5 });
            terminalDbContext.Samples.Add(sample20);
            terminalDbContext.SaveChanges();

            var sample21 = new Sample(
                SampleId.Create(),
                projectNitro,
                null,
                new Comment("First sample!"),
                new List<SampleStep> { step10 },
                new List<Tag> { tag1, tag3, tag5 });
            terminalDbContext.Samples.Add(sample21);
            terminalDbContext.SaveChanges();

            var sample22 = new Sample(
                SampleId.Create(),
                projectNitro,
                null,
                new Comment("First sample!"),
                new List<SampleStep> { step10 },
                new List<Tag> { tag1, tag3, tag5 });
            terminalDbContext.Samples.Add(sample22);
            terminalDbContext.SaveChanges();

            var sample23 = new Sample(
                SampleId.Create(),
                projectNitro,
                null,
                new Comment("First sample!"),
                new List<SampleStep> { step10 },
                new List<Tag> { tag1, tag3, tag5 });
            terminalDbContext.Samples.Add(sample23);
            terminalDbContext.SaveChanges();

            var sample24 = new Sample(
                SampleId.Create(),
                projectNitro,
                null,
                new Comment("First sample!"),
                new List<SampleStep> { step10 },
                new List<Tag> { tag1, tag3, tag5 });
            terminalDbContext.Samples.Add(sample24);
            terminalDbContext.SaveChanges();

            var sample25 = new Sample(
                SampleId.Create(),
                projectNitro,
                null,
                new Comment("First sample!"),
                new List<SampleStep> { step10 },
                new List<Tag> { tag1, tag3, tag5 });
            terminalDbContext.Samples.Add(sample25);
            terminalDbContext.SaveChanges();

            var sample26 = new Sample(
                SampleId.Create(),
                projectNitro,
                null,
                new Comment("First sample!"),
                new List<SampleStep> { step10 },
                new List<Tag> { tag1, tag3, tag5 });
            terminalDbContext.Samples.Add(sample26);
            terminalDbContext.SaveChanges();

            var sample27 = new Sample(
                SampleId.Create(),
                projectNitro,
                null,
                new Comment("First sample!"),
                new List<SampleStep> { step10 },
                new List<Tag> { tag1, tag3, tag5 });
            terminalDbContext.Samples.Add(sample27);
            terminalDbContext.SaveChanges();

            var sample28 = new Sample(
                SampleId.Create(),
                projectNitro,
                null,
                new Comment("First sample!"),
                new List<SampleStep> { step10 },
                new List<Tag> { tag1, tag3, tag5 });
            terminalDbContext.Samples.Add(sample28);
            terminalDbContext.SaveChanges();

            var sample29 = new Sample(
                SampleId.Create(),
                projectNitro,
                null,
                new Comment("First sample!"),
                new List<SampleStep> { step10 },
                new List<Tag> { tag1, tag3, tag5 });
            terminalDbContext.Samples.Add(sample29);
            terminalDbContext.SaveChanges();

            var sample30 = new Sample(
                SampleId.Create(),
                projectNitro,
                null,
                new Comment("First sample!"),
                new List<SampleStep> { step10 },
                new List<Tag> { tag1, tag3, tag5 });
            terminalDbContext.Samples.Add(sample30);
            terminalDbContext.SaveChanges();

            var sample31 = new Sample(
                SampleId.Create(),
                projectNitro,
                null,
                new Comment("First sample!"),
                new List<SampleStep> { step10 },
                new List<Tag> { tag1, tag3, tag5 });
            terminalDbContext.Samples.Add(sample31);
            terminalDbContext.SaveChanges();

            var sample32 = new Sample(
                SampleId.Create(),
                projectNitro,
                null,
                new Comment("First sample!"),
                new List<SampleStep> { step10 },
                new List<Tag> { tag1, tag3, tag5 });
            terminalDbContext.Samples.Add(sample32);
            terminalDbContext.SaveChanges();

            var sample33 = new Sample(
                SampleId.Create(),
                projectNitro,
                null,
                new Comment("First sample!"),
                new List<SampleStep> { step10 },
                new List<Tag> { tag1, tag3, tag5 });
            terminalDbContext.Samples.Add(sample33);
            terminalDbContext.SaveChanges();

            var sample34 = new Sample(
                SampleId.Create(),
                projectNitro,
                null,
                new Comment("First sample!"),
                new List<SampleStep> { step10 },
                new List<Tag> { tag1, tag3, tag5 });
            terminalDbContext.Samples.Add(sample34);
            terminalDbContext.SaveChanges();

            var sample35 = new Sample(
                SampleId.Create(),
                projectNitro,
                null,
                new Comment("First sample!"),
                new List<SampleStep> { step10 },
                new List<Tag> { tag1, tag3, tag5 });
            terminalDbContext.Samples.Add(sample35);
            terminalDbContext.SaveChanges();

            var sample36 = new Sample(
                SampleId.Create(),
                projectNitro,
                null,
                new Comment("First sample!"),
                new List<SampleStep> { step10 },
                new List<Tag> { tag1, tag3, tag5 });
            terminalDbContext.Samples.Add(sample36);
            terminalDbContext.SaveChanges();

            var sample37 = new Sample(
                SampleId.Create(),
                projectNitro,
                null,
                new Comment("First sample!"),
                new List<SampleStep> { step10 },
                new List<Tag> { tag1, tag3, tag5 });
            terminalDbContext.Samples.Add(sample37);
            terminalDbContext.SaveChanges();

            var sample38 = new Sample(
                SampleId.Create(),
                projectNitro,
                null,
                new Comment("First sample!"),
                new List<SampleStep> { step10 },
                new List<Tag> { tag1, tag3, tag5 });
            terminalDbContext.Samples.Add(sample38);
            terminalDbContext.SaveChanges();

            var sample39 = new Sample(
                SampleId.Create(),
                projectNitro,
                null,
                new Comment("First sample!"),
                new List<SampleStep> { step10 },
                new List<Tag> { tag1, tag3, tag5 });
            terminalDbContext.Samples.Add(sample39);
            terminalDbContext.SaveChanges();

            var sample40 = new Sample(
                SampleId.Create(),
                projectNitro,
                null,
                new Comment("First sample!"),
                new List<SampleStep> { step10 },
                new List<Tag> { tag1, tag3, tag5 });
            terminalDbContext.Samples.Add(sample40);
            terminalDbContext.SaveChanges();

            var sample41 = new Sample(
                SampleId.Create(),
                projectNitro,
                null,
                new Comment("First sample!"),
                new List<SampleStep> { step10 },
                new List<Tag> { tag1, tag3, tag5 });
            terminalDbContext.Samples.Add(sample41);
            terminalDbContext.SaveChanges();

            var sample42 = new Sample(
                SampleId.Create(),
                projectNitro,
                null,
                new Comment("First sample!"),
                new List<SampleStep> { step10 },
                new List<Tag> { tag1, tag3, tag5 });
            terminalDbContext.Samples.Add(sample42);
            terminalDbContext.SaveChanges();

            var sample43 = new Sample(
                SampleId.Create(),
                projectNitro,
                null,
                new Comment("First sample!"),
                new List<SampleStep> { step10 },
                new List<Tag> { tag1, tag3, tag5 });
            terminalDbContext.Samples.Add(sample43);
            terminalDbContext.SaveChanges();

            var sample44 = new Sample(
                SampleId.Create(),
                projectNitro,
                null,
                new Comment("First sample!"),
                new List<SampleStep> { step10 },
                new List<Tag> { tag1, tag3, tag5 });
            terminalDbContext.Samples.Add(sample44);
            terminalDbContext.SaveChanges();

            var sample45 = new Sample(
                SampleId.Create(),
                projectNitro,
                null,
                new Comment("First sample!"),
                new List<SampleStep> { step10 },
                new List<Tag> { tag1, tag3, tag5 });
            terminalDbContext.Samples.Add(sample45);

            terminalDbContext.SaveChanges();

            #endregion
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
