using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using System;

namespace DobblePOC
{
    public class Program
    {
        public static void Main(string[] args)
        {
#if DEBUG
            var loggingLevel = new LoggingLevelSwitch { MinimumLevel = LogEventLevel.Debug };
#else
            var loggingLevel = new LoggingLevelSwitch { MinimumLevel = LogEventLevel.Information };
#endif
            Log.Logger = new LoggerConfiguration()
            .Enrich.FromLogContext()
            .MinimumLevel.ControlledBy(loggingLevel)
            .WriteTo.Console()
            .WriteTo.File("logs/log.log")
            .WriteTo.Seq("http://localhost:5341")
            .CreateLogger();

            try
            {
                Log.Information("Starting up");
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Application start-up failed");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
