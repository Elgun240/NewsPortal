using Microsoft.AspNetCore.Identity;

namespace Practice_3.Models
{
    public class AppUser : IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public bool IsDeactive { get; set; }
    }
}
