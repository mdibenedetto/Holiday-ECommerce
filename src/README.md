- dotnet ef database drop


- dotnet ef migrations remove --context ApplicationDbContext
- dotnet ef migrations add refresh_TABLE_APPLICATION_USER_2 --context ApplicationDbContext
- dotnet ef database update --context ApplicationDbContext  


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
