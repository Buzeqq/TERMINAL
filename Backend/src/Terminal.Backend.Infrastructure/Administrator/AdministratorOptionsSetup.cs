using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Terminal.Backend.Infrastructure.Administrator;

internal sealed class AdministratorOptionsSetup : IConfigureOptions<AdministratorOptions>
{
    public const string SectionName = "Administrator";
    private readonly IConfiguration _configuration;

    public AdministratorOptionsSetup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void Configure(AdministratorOptions options)
    {
        _configuration.GetSection(SectionName).Bind(options);
    }
}