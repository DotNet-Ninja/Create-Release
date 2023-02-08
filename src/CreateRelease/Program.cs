using CommandLine;
using CreateRelease.Options;
using CreateRelease.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CreateRelease;

public static class Program
{
    public static async Task<int> Main(string[] args)
    {
        return await Parser.Default.ParseArguments<ReleaseOptions>(args)
            .MapResult(
                async (options) =>
                {
                    var services = ServiceProviderBuilder.Build(options);
                    return await services.GetRequiredService<IReleaseService>().ExecuteAsync(options);
                },
                errors =>
                {
                    Console.WriteLine("Create Release aborted.");
                    return Task.FromResult(-2);
                });
    }
}
