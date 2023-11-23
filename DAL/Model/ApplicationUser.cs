using Microsoft.AspNetCore.Identity;

namespace DAL.Model
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int ContactNo { get; set; }
        
    }
}
