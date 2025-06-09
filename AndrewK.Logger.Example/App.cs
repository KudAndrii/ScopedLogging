using AndrewK.Logger.ScopedLogging.Extensions;
using Microsoft.Extensions.Logging;

namespace AndrewK.Logger.Example;

public class App(ILogger<App> logger)
{
    public Task Run()
    {
        using var bag = logger.InitScopes(("ScopeKey", "ScopeValue"), ("ScopeKey2", "ScopeValue2"));
        bag.AppendScope("ScopeKey3", "ScopeValue3");
        using var scope = logger.BeginScope("Test: {Num}", 222);
        logger.LogInformation("Log within a scoped context.");
        return Task.CompletedTask;
    }
}