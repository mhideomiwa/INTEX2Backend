namespace Intex2Backend.UserData;

public class EfUserRepository : IUserRepository
{
    private readonly UsersContext _context;

    public EfUserRepository(UsersContext context)
    {
        _context = context;
    }

    public User GetUserByCredentials(string username, string password)
    {
        // Query the database to find the user with the provided username and password
        return _context.Users.FirstOrDefault(u => u.UserName == username && u.Password == password);
    }

    public void CreateUser(User user)
    {
        // Add the new user to the database
        _context.Users.Add(user);
        _context.SaveChanges();
    }
}