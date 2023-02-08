using CreateRelease.Options;
using CreateRelease.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Octokit;

namespace CreateRelease;

public static class ServiceProviderBuilder
{
    public static IServiceProvider Build(ICredentials credentials)
    {
        var gitHub = new GitHubClient(new ProductHeaderValue("DotNetNinja-Create-Release", "1.0.0"))
        {
            Credentials = credentials.Credentials
        };

        var services = new ServiceCollection();
        services.AddSingleton<IGitHubClient>(gitHub)
            .AddSingleton<IReleaseService, ReleaseService>()
            .AddLogging(c => c.AddConsole());

        return services.BuildServiceProvider();
    }
    
}