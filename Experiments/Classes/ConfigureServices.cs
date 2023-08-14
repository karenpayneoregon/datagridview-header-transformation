using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog.Events;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;
using SqlServerLibrary.Classes;
using static System.DateTime;


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
        SeriLogDevelopmentSetup();

        return services;

    }

    /// <summary>
    /// For setting up SeriLog if the reader desires to use SeriLog rather than AnsiConsole or Console
    /// </summary>
    public static void SeriLogDevelopmentSetup()
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Verbose()

            .WriteTo.File(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "LogFiles", $"{Now.Year}-{Now.Month}-{Now.Day}", "Log.txt"),
                rollingInterval: RollingInterval.Infinite,
                outputTemplate: "[{Timestamp:yyyy-MM-dd HH:mm:ss.fff} [{Level}] {Message}{NewLine}{Exception}")
            .CreateLogger();

    }
}