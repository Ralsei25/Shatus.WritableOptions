using Microsoft.Extensions.Options;

namespace Shatus.WritableOptions.Base;

public abstract class WritableOptionsBase<TOptions> : IWritableOptions<TOptions>
    where TOptions : class
{
    private readonly IOptionsMonitor<TOptions> _options;

    protected WritableOptionsBase(IOptionsMonitor<TOptions> options)
    {
        _options = options;
    }

    public TOptions Value => _options.CurrentValue;

    public TOptions Get(string? name)
        => _options.Get(name);

    public abstract Task Update(TOptions changedOptions);
}