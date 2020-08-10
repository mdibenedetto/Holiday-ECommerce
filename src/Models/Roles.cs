using System;
namespace dream_holiday.Models
{
    /// <summary>
    /// This class contains all the list of roles used in the application.
    /// All roles are store in constant to make esier chance or refacore the code.
    /// In this application we have create one role "Administrator" but the code
    /// is made to be extendable in a future where we want to add more role.
    /// </summary>
    public static class Roles
    {
        public const string ADMIN = "Administrator";
    }
}
