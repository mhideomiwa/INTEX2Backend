using Microsoft.EntityFrameworkCore;

namespace Intex2Backend.UserData;

public class UsersContext : DbContext
{
    public UsersContext(DbContextOptions<UsersContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
}