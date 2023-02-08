using Octokit;

namespace CreateRelease.Options;

public interface ICredentials
{
    Credentials Credentials { get; }
}