using System;
using System.IO;
using System.Linq;
using Knowlead.Common.Configurations.AppSettings;
using Knowlead.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

public class EFLoggerProvider : ILoggerProvider
{
    public ILogger CreateLogger(string categoryName)
    {
        return new EFLogger();
    }
    public void Dispose()
    {
        // N/A
    }

    private class EFLogger : Microsoft.Extensions.Logging.ILogger
    {
        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }
        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }
        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            Console.WriteLine(formatter(state, exception));
        }
    }
}
public static class ScriptUtils
{
    public static ApplicationDbContext InitializeContext(string[] args, bool debug = false)
    {
        Console.Write("Debug: Initializing ApplicationDbContext ... ");
        Console.Out.Flush();

        DbContextOptions dbOptions = new DbContextOptionsBuilder().Options;

        var env = args.Where(x => x.StartsWith("--env-")).FirstOrDefault();
        string envAppSettings;

        switch(env)
        {
            case "--env-production":
                envAppSettings = "Production";
            break;
            
            case "--env-development":
                envAppSettings = "Development";
            break;

            default:
                envAppSettings = "Development";
            break;
        }

        var _configPath = Directory.GetCurrentDirectory() + "/../" + AppSettings.Path;

        IConfigurationRoot root = new ConfigurationBuilder()
                .AddJsonFile($"{_configPath}/appsettings.json", optional: false, reloadOnChange: false)
                .AddJsonFile($"{_configPath}/appsettings.{envAppSettings}.json", optional: false, reloadOnChange: false)
                .Build();

        var appSettings = new AppSettings();
        root.GetSection("AppSettings").Bind(appSettings);

        var IOptions = new OptionsWrapper<AppSettings>(appSettings);
        
        ApplicationDbContext ret = new ApplicationDbContext(IOptions, dbOptions);
        if (debug)
        {
            var serviceProvider = ret.GetInfrastructure<IServiceProvider>();
            var loggerFactory = serviceProvider.GetService<ILoggerFactory>();
        }
        ret.Database.EnsureCreated();
        Console.WriteLine("OK");
        return ret;
    }
}