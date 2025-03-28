using Microsoft.AspNetCore.Identity;

namespace ProgettoSettimanale7BackEnd.Models
{
    public class ApplicationRole : IdentityRole
    {
        public ICollection<ApplicationUserRole> UserRoles { get; set; }
    }
}
