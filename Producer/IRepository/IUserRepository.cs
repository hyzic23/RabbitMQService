using Producer.Model;

namespace Producer.IRepository
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAllUsers();
        User AddUser(User user);
    }
}
