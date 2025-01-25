using Microsoft.Extensions.Logging;

namespace AndrewK.Logger.ScopedLogging.Extensions;

public static class Extensions
{
    /// <summary>
    /// Initializes a new <see cref="ScopesBag{T}"/> 
    /// allowing scoped parameters to be appended to log messages.
    /// </summary>
    /// <typeparam name="T">The category type for the logger.</typeparam>
    /// <param name="logger">The logger instance to create the scope for.</param>
    /// <returns>A new instance of <see cref="ScopesBag{T}"/> for managing log scopes.</returns>
    public static ScopesBag<T> InitScopes<T>(this ILogger<T> logger)
    {
        return new ScopesBag<T>(logger);
    }

    /// <summary>
    /// Initializes a new <see cref="ScopesBag{T}"/> and adds an initial scope parameter.
    /// </summary>
    /// <inheritdoc cref="InitScopes{T}(ILogger{T})"/>
    public static ScopesBag<T> InitScopes<T>(this ILogger<T> logger, string propertyName, object? propertyValue)
    {
        var bag = new ScopesBag<T>(logger);
        
        return bag.AppendScope(propertyName, propertyValue);
    }

    /// <summary>
    /// Initializes a new <see cref="ScopesBag{T}"/> and adds multiple initial scope parameters.
    /// </summary>
    /// <inheritdoc cref="InitScopes{T}(ILogger{T})"/>
    public static ScopesBag<T> InitScopes<T>(this ILogger<T> logger,
        params (string PropertyName, object? PropertyValue)[] args)
    {
        var bag = new ScopesBag<T>(logger);
        
        return bag.AppendScope(args);
    }
}