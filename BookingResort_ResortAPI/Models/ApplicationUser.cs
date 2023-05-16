using Microsoft.AspNetCore.Identity;

namespace BookingResort_ResortAPI.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
    }
}
