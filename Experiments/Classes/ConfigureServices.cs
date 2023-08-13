using Experiments.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog.Events;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;


namespace Experiments.Classes;

/// <summary>
/// Setup services
/// </summary>
public class Utilities
{
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

            services.Configure<ConnectionStrings>(ConfigurationRoot()
                .GetSection(nameof(ConnectionStrings)));


            services.AddScoped<ColumnInformation>();
            services.AddSingleton<ConstraintInformation>();
        }

        var services = new ServiceCollection();
        ConfigureService(services);

        return services;

    }

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