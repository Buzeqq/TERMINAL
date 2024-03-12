using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Terminal.Backend.Application.Invitations;

internal sealed class InvitationOptionsSetup : IConfigureOptions<InvitationOptions>
{
    private const string SectionName = "Invitations";
    private readonly IConfiguration _configuration;

    public InvitationOptionsSetup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void Configure(InvitationOptions options)
    {
        _configuration.GetRequiredSection(SectionName)
            .Bind(options);
    }
}