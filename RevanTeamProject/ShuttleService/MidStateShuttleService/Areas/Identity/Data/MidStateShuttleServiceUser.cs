﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace MidStateShuttleService.Areas.Identity.Data;

// Add profile data for application users by adding properties to the MidStateShuttleServiceUser class
public class MidStateShuttleServiceUser : IdentityUser
{
    public MidStateShuttleServiceUser()
    {
        
    }
}
