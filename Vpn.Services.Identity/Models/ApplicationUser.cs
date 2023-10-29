using Microsoft.AspNetCore.Identity;

namespace Vpn.Services.Identity.Models;

public class ApplicationUser : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
}