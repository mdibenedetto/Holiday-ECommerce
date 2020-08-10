using System;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
 
using Serilog;
using dream_holiday.Models;
using dream_holiday.Models.EntityServices;
using dream_holiday.Data;

namespace dream_holiday
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

 
            services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(10);

                options.LoginPath = $"/Identity/Account/Login";
                options.SlidingExpiration = true;
            });

            services.AddHttpContextAccessor();

            var DefaultConnectionString = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlite(DefaultConnectionString));

            services
              .AddDefaultIdentity<ApplicationUser>(
                  options =>
                  {
                      // Password settings.
                      options.SignIn.RequireConfirmedAccount = false;
                      options.SignIn.RequireConfirmedEmail = false;
                      options.Password.RequireDigit = false;
                      options.Password.RequireLowercase = false;
                      options.Password.RequireNonAlphanumeric = false;
                      options.Password.RequireUppercase = false; 
                      options.Password.RequiredLength = 6; 

                      // Lockout settings.
                      options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                      options.Lockout.MaxFailedAccessAttempts = 25;
                      options.Lockout.AllowedForNewUsers = true;

                      // User settings.
                      options.User.AllowedUserNameCharacters =
                    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                      options.User.RequireUniqueEmail = false;
                  })
              .AddRoles<ApplicationRole>()
              .AddEntityFrameworkStores<ApplicationDbContext>();

            addAplicationEntityServices(services);

            services.AddControllersWithViews();
            services.AddRazorPages();

        }

        private void addAplicationEntityServices(IServiceCollection services)
        {
            services.AddTransient<UserResolverService>();
            services.AddTransient<UserAccountService>();
            services.AddTransient<TravelPackageService>();
            services.AddTransient<CartService>();
            services.AddTransient<CheckoutService>();
            services.AddTransient<OrderService>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(
                IApplicationBuilder app,
                IWebHostEnvironment env,
                UserManager<ApplicationUser> userManager,
                RoleManager<ApplicationRole> roleManager)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.Use(async (ctx, next) =>
            {
                await next();

                if (ctx.Response.StatusCode == 404 && !ctx.Response.HasStarted)
                {
                    //Re-execute the request so the user gets the error page
                    string originalPath = ctx.Request.Path.Value;
                    ctx.Items["originalPath"] = originalPath;
                    ctx.Request.Path = "/error/404";
                    await next();
                }
            });

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();


            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                context.Database.EnsureCreated();

                // create default UserRoles and Users
                Log.Information("Seed Admin user into DB");
                StartupDbUsers.SeedUsers(userManager, roleManager, context);

                Log.Information("Seed TravelPackage into DB");
                StartupDbData.SeedData(context);
            }


            app.UseEndpoints(endpoints =>
            {

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                // Example of how to add a new Route
                //endpoints.MapControllerRoute(name: "template",
                //   pattern: "template/one",
                //   defaults: new { controller = "Template", action = "Index" });

                endpoints.MapRazorPages();
            });

        }
    }
}
