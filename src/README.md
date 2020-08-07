USERS:

1)
username:  nci_test@ncirl.ie
pwd:       nci_test@ncirl.ie

2)

username:  admin@dreamholiday.com
pwd:       nci_admin_2020
 



SQLLITE ONLINE VIEWER
https://inloop.github.io/sqlite-viewer/

- dotnet ef database drop


- dotnet ef migrations remove --context ApplicationDbContext
- dotnet ef migrations add INIT_DB --context ApplicationDbContext
- dotnet ef database update --context ApplicationDbContext  


TODO
SCAFFOLD PAGE
https://localhost:5001/Identity/Account/AccessDenied?ReturnUrl=%2FTravelPackage


https://docs.microsoft.com/en-us/aspnet/core/data/ef-mvc/intro?view=aspnetcore-3.1#initialize-db-with-test-data
  context.Database.EnsureCreated();


ef-mvc
https://docs.microsoft.com/en-us/aspnet/core/data/ef-mvc/intro?view=aspnetcore-3.1

ADD a new table
http://www.binaryintellect.net/articles/87446533-54b3-41ad-bea9-994091686a55.aspx


 For the different patterns supported at design time,
see https://go.microsoft.com/fwlink/?linkid=851728



https://docs.microsoft.com/en-us/aspnet/core/security/authentication/customize-identity-model?view=aspnetcore-3.1

https://www.c-sharpcorner.com/article/add-custom-user-data-to-identify-in-register-form-in-asp-net-core-3-0/

Support for ASP.NET Core Identity was added to your project.

For setup and configuration information, see https://go.microsoft.com/fwlink/?linkid=2116645.


# Authorization

https://docs.microsoft.com/en-us/aspnet/core/security/authorization/simple?view=aspnetcore-3.1
[Authorize]
public class AccountController : Controller
{
    [AllowAnonymous]
    public ActionResult Login()
    {
    }

    public ActionResult Logout()
    {
    }
}

https://docs.microsoft.com/en-us/aspnet/core/security/authorization/roles?view=aspnetcore-3.1
public void ConfigureServices(IServiceCollection services)
{
    services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(
            Configuration.GetConnectionString("DefaultConnection")));
    services.AddDefaultIdentity<IdentityUser>()
        .AddRoles<IdentityRole>()
        .AddEntityFrameworkStores<ApplicationDbContext>();

    services.AddControllersWithViews();
    services.AddRazorPages();



[Authorize(Roles = "Administrator")]
public class AdministrationController : Controller
{
}