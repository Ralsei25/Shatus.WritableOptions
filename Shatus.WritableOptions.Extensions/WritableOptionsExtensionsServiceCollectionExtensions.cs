using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Shatus.WritableOptions.Base;
using Shatus.WritableOptions.Files;

namespace Shatus.WritableOptions.Extensions;

public static class WritableOptionsExtensionsServiceCollectionExtensions
{
    public static void ConfigureWritable<TOptions>(this IServiceCollection services, IConfigurationSection section,
        string fileName = "appsettings.json", string? configsDirectoryPath = null) where TOptions : class, new()
    {
        if (string.IsNullOrWhiteSpace(configsDirectoryPath))
            configsDirectoryPath = AppDomain.CurrentDomain.BaseDirectory;

        services.Configure<TOptions>(section);
        services.AddTransient<IWritableOptions<TOptions>>(provider =>
        {
            var options = provider.GetRequiredService<IOptionsMonitor<TOptions>>();
            return new JsonFileSectionWritableOptions<TOptions>(options, Path.Combine(configsDirectoryPath, fileName),
                section.Key);
        });
    }
}