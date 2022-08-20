using System;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using SenseHome.Common.Exceptions;
using SenseHome.DB.Mongo;

namespace SenseHome.Repositories.User
{
    public class UserRepository : BaseRepository<DomainModels.User>, IUserRepository
    {
        public UserRepository(MongoDBContext mongoDbContext)
        {
            collection = mongoDbContext.Database.GetCollection<DomainModels.User>("users");

        }

        public async Task<bool> AddLog(string id, DateTime dateTime)
        {
            var filter = Builders<DomainModels.User>.Filter.Eq(u => u.Id, id);
            var update = Builders<DomainModels.User>.Update.Push(u => u.Logs, dateTime);
            var result = await collection.UpdateOneAsync(filter, update);
            return result.IsAcknowledged && result.ModifiedCount > 0;
        }

        public async Task<DomainModels.User> GetByNameAsync(string name)
        {
            var cursor = await collection.FindAsync(doc => doc.Name == name);
            var user = cursor.FirstOrDefault();
            if (user == null)
            {
                throw new NotFoundException($"No user exists with name {name}");
            }
            return user;
        }

        public async Task<DomainModels.User> GetByNameOrDefaultAsync(string name)
        {
            var cursor = await collection.FindAsync(doc => doc.Name == name);
            return cursor.FirstOrDefault();
        }
    }
}
