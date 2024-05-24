using Microsoft.AspNetCore.Identity;

namespace StudentsUnite_II.Areas.Identity.Data;

// Add profile data for application users by adding properties to the ForumUser class
public class ForumUser : IdentityUser
{
    public string? departament { get; set; }
    public string? specialization { get; set; }
    public int? yearOfStudy { get; set; }
    public string? description { get; set; }
    public string? personalPageLink { get; set; }

}

