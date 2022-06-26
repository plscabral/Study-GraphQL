using Study.Domain.Entities;

namespace Study.Domain.Interfaces
{
    public interface IUserRepository
    {
        IList<User> GetUsers();
        User GetUserById(string id);
        User GetUserByEmail(string email);
        void Insert(User user);
        void Refresh(User user);
        void Delete(string id);
    }
}
