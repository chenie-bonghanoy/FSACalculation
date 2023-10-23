using Microsoft.AspNetCore.Identity;

namespace FSACalculation.Data.Entities
{
    public class UserLogin : IdentityUser
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public int isAdmin { get; set; }

        public int empId { get; set; }
    }
}
