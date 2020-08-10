using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
 
using Serilog;
using Serilog.Events;

namespace dream_holiday
{
    /// <summary>
    /// This class contains the entry point of the program.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// the main method is in charge to start the web application
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            SetLogger();

            try
            {
                Log.Information("Starting up");
                var host = CreateHostBuilder(args).Build();

                host.Run();
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

        /// <summary>
        /// This method set up the application logging.
        /// This web application is using Serilog to handle logs
        /// </summary>
        private static void SetLogger()
        {
            const string LOG_FILE = "Logs";

            // todo: add config to delete file after 1 months
            Func<LogEventLevel, string, Action<LoggerConfiguration>>
           SetLogger = (level, filePath) =>
           {
               Action<LoggerConfiguration> configureLogger =
                        l => l.Filter.ByIncludingOnly(e => e.Level == level)
                           .WriteTo.File(path: LOG_FILE + filePath
                                       , rollingInterval: RollingInterval.Month
                                        , rollOnFileSizeLimit: true
                                        , fileSizeLimitBytes: 1000000
                                    );

               return configureLogger;
           };

            /* We have different log files for different level of logs
             * If you want to be more specific with file logging             
             * uncomment those ones which are desired */
            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .WriteTo.File(
                         path: LOG_FILE + "/log_.txt"
                        , rollingInterval: RollingInterval.Day
                        , rollOnFileSizeLimit: true
                        , fileSizeLimitBytes: 1000000
                )
                .MinimumLevel.Debug().WriteTo.File(LOG_FILE + "/Verbose-.log")
                .WriteTo.Logger(SetLogger(LogEventLevel.Error, "/Error-.log"))
                .WriteTo.Logger(SetLogger(LogEventLevel.Fatal, "/Fatal-.log"))
                //.WriteTo.Logger(SetLogger(LogEventLevel.Information, "/Info-.log"))
                //.WriteTo.Logger(SetLogger(LogEventLevel.Debug, "/Debug-.log"))
                //.WriteTo.Logger(SetLogger(LogEventLevel.Warning, "/Warning-.log"))
                //.WriteTo.File(new CompactJsonFormatter(), "Logs/log_json.txt")              
                .CreateLogger();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                }).UseSerilog();
    }
}
