using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Intex2Backend.UserData
{
    // Define your custom user class inheriting from IdentityUser
    public class ApplicationUser : IdentityUser
    {
        // Add your custom properties here
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string AddressLine1 { get; set; }
        public string? AddressLine2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string Country { get; set; }
    }

    public class UsersContext : IdentityDbContext<ApplicationUser> // Use your custom user class
    {
        public UsersContext(DbContextOptions<UsersContext> options) : base(options)
        {
        }

        // Optionally, you can add DbSet for custom entities related to users here
        // public DbSet<CustomEntity> CustomEntities { get; set; }
    }
}