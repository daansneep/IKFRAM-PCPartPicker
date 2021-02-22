using Microsoft.AspNetCore.Identity;

namespace Domain
{
    public class Admin : IdentityUser
    {
        public string DisplayName { get; set; }
    }
}