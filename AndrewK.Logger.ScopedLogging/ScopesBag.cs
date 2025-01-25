using System.Collections.Concurrent;
using AndrewK.Logger.ScopedLogging.Extensions;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace AndrewK.Logger.ScopedLogging;

/// <summary>
///     Makes it possible to write scoped parameters
///     to all <see cref="ILogger"/> calls in the scope area
/// </summary>
/// <remarks>
///     It is recommended to use via extension methods such as:
///     <see cref="Extensions.InitScopes{T}(Microsoft.Extensions.Logging.ILogger{T})"/>
/// </remarks>
/// <example>
/// <code>
///     using (var scopesBag = logger.InitScopes())
///     {
///         scopesBag.AppendScope("First", first);
///         logger.LogInformation(...); // First will be appended
///
///         scopesBag.AppendScope("Second", second);
///         logger.LogInformation(...) // First and Second will be appended
///     }
/// </code>
/// </example>
/// <example>
/// <code>
///     using (var scopesBag = logger.InitScopes("First", first))
///     {
///         // previous example valid...
///     }
/// </code>
/// </example>
public sealed class ScopesBag<TCategoryName> : IDisposable
{
    private readonly ILogger<TCategoryName> _scopeCreator;
    private readonly ConcurrentBag<IDisposable?> _scopes = [];

    internal ScopesBag(ILogger<TCategoryName> logger)
    {
        _scopeCreator = logger;
    }

    public ScopesBag<TCategoryName> AppendScope(string propertyName, object? propertyValue)
    {
        return AppendScope([(propertyName, propertyValue)]);
    }

    public ScopesBag<TCategoryName> AppendScope(params (string PropertyName, object? PropertyValue)[] args)
    {
        // all items should be considered as objects to separate scope parameters between each other
        IDictionary<string, object> parameters = args.ToDictionary(
            pair => pair.PropertyName,
            // to prevent double serialization we must check if a property have been already serialized
            pair => pair.PropertyValue as string ??
                    (object?)JsonConvert.SerializeObject(pair.PropertyValue) ??
                    string.Empty);

        var scope = _scopeCreator.BeginScope(parameters);

        AppendScope(scope);

        return this;
    }

    private void AppendScope(IDisposable? scope)
    {
        // Adds the item to the beginning of the collection
        _scopes.Add(scope);
    }

    public void Dispose()
    {
        // Disposable objects used here have isDisposed check inside the Dispose method implementation
        foreach (var scope in _scopes)
        {
            scope?.Dispose();
        }
    }
}