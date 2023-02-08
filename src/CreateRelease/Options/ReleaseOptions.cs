using CommandLine;
using Octokit;
using SemVersion;

namespace CreateRelease.Options;

public class ReleaseOptions: ICredentials
{
    [Option('v', "version", Required = true, HelpText = "Semantic Version of the Release (SemVer 2.0)")]
    public string Version { get; set; } = "0.1.0";

    [Option('t', "tag", Required = true, HelpText = "The git tag to create the release from.")]
    public string Tag { get; set; } = string.Empty;

    [Option('d', "description", Required = false, Default = "Release {0}", HelpText = "A release description/title template with {0} placeholder for the version.")]
    public string DescriptionTemplate { get; set; } = string.Empty;

    [Option('o', "owner", Required = true, HelpText="The owner of the repository.")]
    public string RepositoryOwner { get; set; } = string.Empty;

    [Option('n', "name",Required = true, HelpText = "The name of the repository,")]
    public string RepositoryName { get; set; } = string.Empty;

    [Option('a', "artifacts", Required = false, Default = "", HelpText = "Path to the build artifacts to attach to the release.")]
    public string ArtifactsPath { get; set; } = string.Empty;

    [Option('p', "AccessToken", Required = true, HelpText = "GitHub access token (Personal Access Token) providing authorization to create the release.")]
    public string AccessToken { get; set; } = string.Empty;


    public string ReleaseTitle => string.Format(DescriptionTemplate, Version);

    public Credentials Credentials => new (AccessToken);
}