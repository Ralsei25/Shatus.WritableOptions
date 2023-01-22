using Shatus.WritableOptions.Base;

namespace Shatus.WritableOptions.Extensions;

public static class WritableOptionsExtensions
{
    public static async Task Update<TOptions>(this IWritableOptions<TOptions> options, Action<TOptions> change)
        where TOptions : class
    {
        change(options.Value);
        await options.Update(options.Value);
    }
}