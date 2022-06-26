using MongoDB.Driver;

namespace Study.Repository.Context
{
    public class MongoDbContext
    {
        public IMongoDatabase GetConnection()
        {
            var client = new MongoClient("mongodb+srv://instaclone-admin:admin@instaclone.d85gg.mongodb.net/myFirstDatabase?retryWrites=true&w=majority");
            var database = client.GetDatabase("study");

            return database;
        }
    }
}
