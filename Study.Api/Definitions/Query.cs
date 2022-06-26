using Study.Domain.Entities;
using Study.Domain.Interfaces;

namespace Study.Api.Model
{
    public class Query
    {
        private readonly IUserRepository _userRepository;

        public Query(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        #region @User

        public IList<User> GetUsers() => _userRepository.GetUsers();

        public User GetUserById(string id) => _userRepository.GetUserById(id);

        public User GetUserByEmail(string email) => _userRepository.GetUserByEmail(email);

        #endregion
    }
}
