using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace ProgettoSettimanale7BackEnd.Models
{
    public class ApplicationUser : IdentityUser
    {

        [Required]
        public required string FirstName { get; set; }
        [Required]
        public required string LastName { get; set; }


        public ICollection<ApplicationUserRole>? UserRoles { get; set; }
        public ICollection<Biglietto>? Biglietti { get; set; }
    }
}
