using AndrewK.Logger.Example;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    //.Enrich.FromLogContext()
    .WriteTo.Console(new Serilog.Formatting.Json.JsonFormatter())
    .CreateLogger();

using var host = Host.CreateDefaultBuilder(args)
    .UseSerilog()
    // .ConfigureLogging(logging =>
    // {
    //     logging.ClearProviders();
    //     logging.AddJsonConsole();
    // })
    .ConfigureServices((_, services) =>
    {
        // services.AddLogging();
        // services.Configure<JsonConsoleFormatterOptions>(options =>
        // {
        //     options.IncludeScopes = true;
        // });
        services.AddTransient<App>();
    })
    .Build();

var app = host.Services.GetRequiredService<App>();
await app.Run();