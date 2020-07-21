using System;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using dream_holiday.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using dream_holiday.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using dream_holiday.Models.EntityServices;

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
            services.AddTransient<UserResolverService>();

            //services
            //.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            //.AddCookie(options =>
            //{
            //    options.ExpireTimeSpan = TimeSpan.FromMinutes(1);
            //    options.Cookie.Expiration = TimeSpan.FromHours(5);
            //    options.LoginPath = $"/Identity/Account/Login";
            //    options.AccessDeniedPath = $"/Identity/Account/AccessDenied";
            //    options.SlidingExpiration = true;
            //});

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
                      //options.Password.RequireDigit = true;
                      //options.Password.RequireLowercase = true;
                      //options.Password.RequireNonAlphanumeric = true;
                      //options.Password.RequireUppercase = true;
                      options.Password.RequiredLength = 6;
                      //options.Password.RequiredUniqueChars = 1;

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

            services.AddControllersWithViews();
            services.AddRazorPages(); 

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

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            

            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                context.Database.EnsureCreated();
            }

            // create default UserRoles and Users
            StartupUsers.Startup(userManager, roleManager);

            app.UseEndpoints(endpoints =>
            {

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                //endpoints.MapControllerRoute(name: "template",
                //   pattern: "template/one",
                //   defaults: new { controller = "Template", action = "Index" });

                endpoints.MapRazorPages();
            });

        }
    }
}
