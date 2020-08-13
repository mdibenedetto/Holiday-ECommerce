
using System.Linq;
using dream_holiday.Models;
using Microsoft.AspNetCore.Identity;

namespace dream_holiday
{
    public static class StartupDbUsers
    {

        const string USER_NAME = "admin";
        const string USER_EMAIL = "admin@dreamholiday.com";
        const string DEFAULT_PASSWORD = "nci_admin_2020";

        public static void SeedUsers(
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager,
            Data.ApplicationDbContext context)
        {
            // CleanAdminUser(context);

            // CreateRoles(roleManager);
            // CreateDefaultUsers(userManager, context);
        }

        private static void CleanAdminUser(Data.ApplicationDbContext context)
        {
            // find all the users we want to removeß
            var users = context.Users
                            .Where(u => u.Email == USER_EMAIL
                                    || u.UserName == USER_EMAIL);
            var userIDS = users.Select(u => u.Id);
            // first remove the userAccunt in forey key
            var userAccounts = context.UserAccount
                                .Where(ua => userIDS.Contains(ua.User.Id));

            context.UserAccount.RemoveRange(userAccounts);
            // secont remove the users application
            context.Users.RemoveRange(users);
            // save and commit change
            context.SaveChanges();
        }

        // CREATE A ADMIN ROLE
        public static void CreateRoles(RoleManager<ApplicationRole> roleManager)
        {
            if (!roleManager.RoleExistsAsync(Roles.ADMIN).Result)
            {
                ApplicationRole role = new ApplicationRole { Name = Roles.ADMIN };
                IdentityResult roleResult = roleManager.CreateAsync(role).Result;
            }

            if (!roleManager.RoleExistsAsync(Roles.SUPER_USER).Result)
            {
                ApplicationRole role = new ApplicationRole { Name = Roles.SUPER_USER };
                IdentityResult roleResult = roleManager.CreateAsync(role).Result;
            }
        }

        // CREATE DEFAULT USER Admin
        public static void CreateDefaultUsers(
            UserManager<ApplicationUser> userManager, Data.ApplicationDbContext context)
        {


            var foundUser = userManager.FindByNameAsync(USER_NAME);
            //test: userManager.DeleteAsync(foundUser.Result);
            //test foundUser = userManager.FindByNameAsync(USER_NAME);

            if (foundUser.Result == null)
            {
                // set default user attributes
                var user = new ApplicationUser();
                user.UserName = USER_NAME;
                user.Email = USER_EMAIL;
                // create new Admin user and assign ADMIN role to.
                var result = userManager.CreateAsync(user, DEFAULT_PASSWORD).Result;
                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, Roles.ADMIN).Wait();
                    userManager.AddToRoleAsync(user, Roles.SUPER_USER).Wait();
                }
            }

        }

    }
}
