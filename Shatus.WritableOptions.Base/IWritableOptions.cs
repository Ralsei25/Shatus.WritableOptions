using Microsoft.Extensions.Options;

namespace Shatus.WritableOptions.Base;

public interface IWritableOptions<TOptions> : IOptionsSnapshot<TOptions> where TOptions : class
{
    Task Update(TOptions changedOptions);
}