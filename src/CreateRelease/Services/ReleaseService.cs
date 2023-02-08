using CreateRelease.Options;
using Microsoft.Extensions.Logging;
using Octokit;

namespace CreateRelease.Services;

public class ReleaseService : IReleaseService
{
    private readonly IGitHubClient _gitHub;
    private readonly ILogger<ReleaseService> _logger;

    public ReleaseService(IGitHubClient client, ILogger<ReleaseService> logger)
    {
        _gitHub = client;
        _logger = logger;
    }

    public async Task<int> ExecuteAsync(ReleaseOptions options)
    {
        try
        {
            _logger.LogInformation("Creating Release");
            var repo = await _gitHub.Repository.Get(options.RepositoryOwner, options.RepositoryName);
            var release = await _gitHub.Repository.Release.Create(options.RepositoryOwner, options.RepositoryName, new NewRelease(options.Tag)
            {
                Name = options.ReleaseTitle,
                Body = $"{DateTime.UtcNow.ToLongDateString()} @ {DateTime.UtcNow.ToShortTimeString()} UTC\r\n"
            });
            _logger.LogInformation($"Release '{options.ReleaseTitle}' @ {options.Tag} on {options.RepositoryOwner}/{options.RepositoryName}\" created.");
        }
        catch (Exception ex)
        {
            _logger.LogWarning($"Creation of release '{options.ReleaseTitle}' @ {options.Tag} on {options.RepositoryOwner}/{options.RepositoryName} failed.");
            _logger.LogError(ex, ex.Message);
            return -1;
        }
        return 0;
    }
}