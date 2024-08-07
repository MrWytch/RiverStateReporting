using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace RiverStateReporting.Data.Model
{
    /// <summary>
    /// IdentityUser extended by another property
    /// </summary>
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string FullName { get; set; } // Full and actual name of a user
    }
}
