using System.Text.Json;
using Microsoft.Extensions.Options;
using Shatus.WritableOptions.Base;

namespace Shatus.WritableOptions.Files;

public class JsonFileWritableOptions<TOptions> : WritableOptionsBase<TOptions> where TOptions : class
{
    private readonly string _filePath;

    public JsonFileWritableOptions(IOptionsMonitor<TOptions> options, string filePath) : base(options)
    {
        _filePath = filePath;
    }

    public override async Task Update(TOptions changedOptions)
    {
        if (!File.Exists(_filePath))
            await File.Create(_filePath).DisposeAsync();

        await File.WriteAllTextAsync(_filePath,
            JsonSerializer.Serialize(changedOptions, new JsonSerializerOptions { WriteIndented = true }));
    }
}