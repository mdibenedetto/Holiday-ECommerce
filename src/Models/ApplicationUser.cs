﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

/// <summary>
/// These classes is used to override the User primary key to be Guid rather the default type String
/// </summary>
namespace dream_holiday.Models
{
    public class ApplicationRole: IdentityRole<Guid> { }

    public class ApplicationUser : IdentityUser<Guid> {}
}
