using MongoDB.Bson;
using Study.Api.Models;
using Study.Domain.Entities;
using Study.Domain.Interfaces;

namespace Study.Api.Model
{
    public class Mutation
    {
        private readonly IUserRepository _userRepository;

        public Mutation(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        #region @User

        public User InsertUser(UserModel user)
        {
            var newUser = new User
            {
                Id = ObjectId.GenerateNewId().ToString(),
                Name = user.Name,
                Surname = user.Surname,
                Email = user.Email,
                Password = user.Password,
                BirthDate = user.BirthDate,
                CreatedAt = DateTime.Now,
            };

            _userRepository.Insert(newUser);

            return newUser;
        }
         
        public string DeleteUser(string id)
        {
            _userRepository.Delete(id);

            return "Usuário deletado com sucesso!";
        }

        #endregion
    }
}
