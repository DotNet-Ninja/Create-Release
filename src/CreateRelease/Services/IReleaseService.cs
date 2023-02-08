using CreateRelease.Options;

namespace CreateRelease.Services;

public interface IReleaseService
{
    Task<int> ExecuteAsync(ReleaseOptions options);
}