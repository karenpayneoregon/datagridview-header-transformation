using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog.Events;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;
using SqlServerLibrary.Classes;


namespace Experiments.Classes;

/// <summary>
/// Setup services
/// </summary>
public class Utilities
{

    public static string NorthWindConnectionString 
        => ConfigurationRoot().GetConnectionString(nameof(ConnectionStrings.NorthWindConnection));
    public static string BooksConnection 
        => ConfigurationRoot().GetConnectionString(nameof(ConnectionStrings.BooksConnection));
    public static string ComputedConnection 
        => ConfigurationRoot().GetConnectionString(nameof(ConnectionStrings.ComputedConnection));
    
    /// <summary>
    /// Read sections from appsettings.json
    /// </summary>
    public static IConfigurationRoot ConfigurationRoot() =>
        new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false)
            .AddEnvironmentVariables()
            .Build();

    public static ServiceCollection ConfigureServices()
    {
        static void ConfigureService(IServiceCollection services)
        {
            services.AddLogging(builder =>
            {
                builder.AddConsole();
                builder.AddDebug();
                builder.AddConfiguration(ConfigurationRoot().GetSection("Logging"));
            });

            services.AddScoped<ConstraintsService>();
            services.AddScoped<ColumnsService>();
        }

        var services = new ServiceCollection();
        ConfigureService(services);

        return services;

    }

    /// <summary>
    /// For setting up SeriLog if the reader desires to use SeriLog rather than AnsiConsole or Console
    /// </summary>
    public static void SeriLogDevelopmentSetup()
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Information()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
            .MinimumLevel.Override("Microsoft.Hosting.Lifetime", LogEventLevel.Information)
            .WriteTo.Console(theme: AnsiConsoleTheme.Literate)
            .CreateLogger();

    }
}