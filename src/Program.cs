using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Compact;

namespace dream_holiday
{
    public class Program
    {
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
                                       , rollingInterval: RollingInterval.Day
                                        , rollOnFileSizeLimit: true
                                        , fileSizeLimitBytes: 1000000
                                    );

               return configureLogger;
           };

            //const string LOG_FILE_JSON = "Logs/log_json.txt";
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
                .WriteTo.Logger(SetLogger(LogEventLevel.Information, "/Info-.log"))
                .WriteTo.Logger(SetLogger(LogEventLevel.Debug, "/Debug-.log"))
                .WriteTo.Logger(SetLogger(LogEventLevel.Warning, "/Warning-.log"))
                .WriteTo.Logger(SetLogger(LogEventLevel.Error, "/Error-.log"))
                .WriteTo.Logger(SetLogger(LogEventLevel.Fatal, "/Fatal-.log"))
                //.WriteTo.File(new CompactJsonFormatter(), LOG_FILE_JSON)
                //.WriteTo.Seq(
                //        Environment.GetEnvironmentVariable("SEQ_URL")
                //        ?? "http://localhost:5001")
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
