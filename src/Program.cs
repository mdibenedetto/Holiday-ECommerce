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
        const string LOG_FILE = "/Logs";

        public static void Main(string[] args)
        {
            //const string LOG_FILE = "Logs/log_.txt";
            //const string LOG_FILE_JSON = "Logs/log_json.txt";
            //Log.Logger = new LoggerConfiguration()
            //    .Enrich.FromLogContext()
            //    .WriteTo.Console()
            //    .WriteTo.File(
            //             path: LOG_FILE
            //            , rollingInterval: RollingInterval.Day
            //            , rollOnFileSizeLimit: true
            //            , fileSizeLimitBytes: 1000000
            //    )
            //    //.WriteTo.File(new CompactJsonFormatter(), LOG_FILE_JSON)
            //    //.WriteTo.Seq(
            //    //        Environment.GetEnvironmentVariable("SEQ_URL")
            //    //        ?? "http://localhost:5001")
            //    .CreateLogger();


            var loggerConfig = new LoggerConfiguration();
            loggerConfig.MinimumLevel.Debug().WriteTo.File(LOG_FILE + "/Verbose-.log"); 

            SetLogger(loggerConfig, LogEventLevel.Information, "/Info-.log");
            SetLogger(loggerConfig, LogEventLevel.Debug, "/Debug-.log");
            SetLogger(loggerConfig, LogEventLevel.Warning, "/Warning-.log");
            SetLogger(loggerConfig, LogEventLevel.Error, "/Error-.log");
            SetLogger(loggerConfig, LogEventLevel.Fatal, "/Fatal-.log");

            Log.Logger = loggerConfig.CreateLogger();

            try
            {
                Log.Information("Starting up"); 
                var host = CreateHostBuilder(args).Build();

                Log.Information("Add test data");
                //StartupTestData.Startup(host);

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

        private static void SetLogger(LoggerConfiguration loggerConfig, LogEventLevel level, string filePath)
        {
            loggerConfig.WriteTo.
                   Logger(l => l.Filter.ByIncludingOnly(e => e.Level == level)
                   .WriteTo.File(path: LOG_FILE + filePath
                               , rollingInterval: RollingInterval.Day
                                , rollOnFileSizeLimit: true
                                , fileSizeLimitBytes: 1000000
                            ));
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                }).UseSerilog();
    }
}
