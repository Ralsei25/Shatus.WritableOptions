using System.Text.Json;
using System.Text.Json.Nodes;
using Microsoft.Extensions.Options;
using Shatus.WritableOptions.Base;

namespace Shatus.WritableOptions.Files;

public class JsonFileSectionWritableOptions<TOptions> : WritableOptionsBase<TOptions> where TOptions : class
{
    private readonly string _filePath;
    private readonly string _section;

    public JsonFileSectionWritableOptions(IOptionsMonitor<TOptions> options, string filePath, string section) :
        base(options)
    {
        _filePath = filePath;
        _section = section;
    }

    public override async Task Update(TOptions changedOptions)
    {
        if (!File.Exists(_filePath))
            await File.Create(_filePath).DisposeAsync();

        var jsonObject = JsonSerializer.Deserialize<JsonObject>(await File.ReadAllTextAsync(_filePath))
                         ?? new JsonObject();

        jsonObject[_section] = JsonNode.Parse(JsonSerializer.Serialize(Value));
        await File.WriteAllTextAsync(_filePath,
            JsonSerializer.Serialize(jsonObject, new JsonSerializerOptions { WriteIndented = true }));
    }
}