using System;
using Knowlead.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Knowlead.Scripts
{
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
        public static ApplicationDbContext InitializeContext(bool debug = false)
        {
            Console.Write("Debug: Initializing ApplicationDbContext ... ");
            Console.Out.Flush();
            DbContextOptions options = new DbContextOptionsBuilder().Options;
            IConfigurationRoot root = Startup.GetConfiguration(null);
            ApplicationDbContext ret = new ApplicationDbContext(root, options);
            if (debug)
            {
                var serviceProvider = ret.GetInfrastructure<IServiceProvider>();
                var loggerFactory = serviceProvider.GetService<ILoggerFactory>();
                loggerFactory.AddProvider(new EFLoggerProvider());
            }
            ret.Database.EnsureCreated();
            Console.WriteLine("OK");
            return ret;
        }
    }
}