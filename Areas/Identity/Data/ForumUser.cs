using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace StudentsUnite_II.Areas.Identity.Data;

// Add profile data for application users by adding properties to the ForumUser class
public class ForumUser : IdentityUser
{
    
    public ICollection<ForumUser> Friends { get; set; }
    public ICollection<ForumUser> Users { get; set; }
}

