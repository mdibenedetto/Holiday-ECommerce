using System;
using Microsoft.EntityFrameworkCore;

namespace dream_holiday.Models
{
    /// <summary>
    /// This class is used just as utily functionality to clear a table
    /// </summary>
    public static class EntityExtensions
    {
        public static void Clear<T>(this DbSet<T> dbSet) where T : class
        {
            dbSet.RemoveRange(dbSet);
        }
    }
}
