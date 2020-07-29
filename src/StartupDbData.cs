//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using dream_holiday.Models;
//using Microsoft.AspNetCore.Hosting;
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.Hosting;
//using Microsoft.Extensions.Logging;
//using Serilog;
//using Serilog.Events;
//using Serilog.Formatting.Compact;

//namespace dream_holiday
//{
//    public class StartupTestData
//    {
//        static IHost host;
//        static ILogger logger;

//        public static void Startup(IHost _host, ILogger _logger)
//        {
//            host = _host;
//            logger = _logger;

//            StartupTravelPackages(host);
//        }


//        public static void StartupTravelPackages(IHost host)
//        {
//            using (var scope = host.Services.CreateScope())
//            {
//                var services = scope.ServiceProvider;
//                try
//                {
//                    var context = services.GetRequiredService<TravelPackage>();
//                    DbInitializer.Initialize(context);
//                }
//                catch (Exception ex)
//                {
//                    var logger = services.GetRequiredService<ILogger<Program>>();
//                    logger.LogError(ex, "An error occurred while seeding the database.");
//                }
//            }



//        }
//    }
//}
