using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Logging.Console;

namespace AndrewK.Logger.ScopedLogging.Formatters;

public class ScopedJsonConsoleFormatter : ConsoleFormatter
{
    public ScopedJsonConsoleFormatter(string name) : base(name)
    {
    }

    public override void Write<TState>(in LogEntry<TState> logEntry, IExternalScopeProvider? scopeProvider, TextWriter textWriter)
    {
        
    }
}