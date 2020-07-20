using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Formatting.Compact;

namespace dream_holiday
{
    public class Program
    {
        public static void Main(string[] args)
        {
            const string LOG_FILE = "Logs/log_.txt";
            //const string LOG_FILE_JSON = "Logs/log_json.txt";
            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .WriteTo.File(
                         path: LOG_FILE
                        , rollingInterval: RollingInterval.Day
                        , rollOnFileSizeLimit: true
                        , fileSizeLimitBytes: 10 ^ 6
                )
                //.WriteTo.File(new CompactJsonFormatter(), LOG_FILE_JSON)
                .WriteTo.Seq(
                        Environment.GetEnvironmentVariable("SEQ_URL")
                        ?? "http://localhost:5001")
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
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                }).UseSerilog();
    }
}
