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
            _logger.LogInformation($"Creating Release '{options.ReleaseTitle}' @ {options.Tag} on {options.RepositoryOwner}/{options.RepositoryName}");
            var release = await _gitHub.Repository.Release.Create(options.RepositoryOwner, options.RepositoryName, new NewRelease(options.Tag)
            {
                Name = options.ReleaseTitle,
                Body = $"{DateTime.UtcNow.ToLongDateString()} @ {DateTime.UtcNow.ToShortTimeString()}",
                TargetCommitish = options.Tag
            });
            _logger.LogInformation("Release Created.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return -1;
        }
        return 0;
    }
}