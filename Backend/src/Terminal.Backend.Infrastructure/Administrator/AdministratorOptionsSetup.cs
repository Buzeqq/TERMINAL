using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Terminal.Backend.Infrastructure.Administrator;

internal sealed class AdministratorOptionsSetup(IConfiguration configuration) : IConfigureOptions<AdministratorOptions>
{
    public const string SectionName = "Administrator";

    public void Configure(AdministratorOptions options) => configuration.GetSection(SectionName).Bind(options);
}
