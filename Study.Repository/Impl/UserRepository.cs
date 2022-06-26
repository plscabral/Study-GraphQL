using MongoDB.Bson;
using MongoDB.Driver;
using Study.Domain.Entities;
using Study.Domain.Interfaces;
using Study.Repository.Context;

namespace Study.Repository.Impl
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoCollection<User> _collection;

        public UserRepository()
        {
            var connection = new MongoDbContext().GetConnection();
            _collection = connection.GetCollection<User>("User");
        }

        public IList<User> GetUsers()
        {
            return _collection.Find("{}").ToList();
        }

        public User GetUserById(string id)
        {
            return _collection.Find(Builders<User>.Filter.Eq("_id", ObjectId.Parse(id))).SingleOrDefault();
        }

        public User GetUserByEmail(string email)
        {
            return _collection.Find(Builders<User>.Filter.Eq(x => x.Email, email)).SingleOrDefault();
        }

        public void Insert(User user)
        {
            _collection.InsertOne(user);
        }

        public void Refresh(User user)
        {
            var filter = Builders<User>.Filter.Eq("_id", user.Id);
            var update = Builders<User>.Update
                .Set(x => x.Name, user.Name)
                .Set(x => x.Surname, user.Surname)
                .Set(x => x.BirthDate, user.BirthDate)
                .Set(x => x.Email, user.Email)
                .Set(x => x.Password, user.Password)
                ;

            var result = _collection.UpdateOneAsync(filter, update).Result;
        }

        public void Delete(string id)
        {
            var filter = Builders<User>.Filter.Eq("_id", ObjectId.Parse(id));

            var result = _collection.DeleteOneAsync(filter).Result;
        }
    }
}
