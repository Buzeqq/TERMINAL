using Microsoft.Extensions.Configuration;

namespace Terminal.Backend.Integration;

public sealed class OptionsProvider
{
    private readonly IConfigurationRoot _configuration = GetConfigurationRoot();

    public T Get<T>(string sectionName) where T : class, new() => _configuration.GetSection(sectionName).Get<T>()!;

    private static IConfigurationRoot GetConfigurationRoot()
        => new ConfigurationBuilder()
            .AddJsonFile("appsettings.test.json", true)
            .AddEnvironmentVariables()
            .Build();
}