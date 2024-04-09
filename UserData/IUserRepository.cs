namespace Intex2Backend.UserData;

public interface IUserRepository
{
    User GetUserByCredentials(string username, string password);
    void CreateUser(User user);
}