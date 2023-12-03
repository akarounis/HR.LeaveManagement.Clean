
namespace HR.LeaveManagement.Identity.Models;

public class ApplicationUser : Microsoft.AspNetCore.Identity.IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }   
}
