using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using SenseHome.Common.Exceptions;
using SenseHome.DomainModels.Base;

namespace SenseHome.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        protected IMongoCollection<T> collection;

        public async Task<T> CreateAsync(T entity)
        {
            await collection.InsertOneAsync(entity);
            return entity;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var filter = Builders<T>.Filter.Eq(p => p.Id, id);
            var result = await collection.DeleteOneAsync(filter);
            if (!result.IsAcknowledged)
            {
                throw new BadRequestException("No entity found with this id");
            }
            return result.DeletedCount > 0;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            var cursor = await collection.FindAsync(e => true);
            return cursor.ToList();
        }

        public IQueryable<T> GetAsQueryable()
        {
            return collection.AsQueryable();
        }

        public async Task<T> GetAsync(string id)
        {
            var cursor = await collection.FindAsync(e => e.Id == id);
            var entity = cursor.FirstOrDefault();
            if (entity == null)
            {
                throw new NotFoundException($"No entity exists with ID {id}");
            }
            return entity;
        }

        public async Task<T> GetOrDefaultAsync(string id)
        {
            var cursor = await collection.FindAsync(e => e.Id == id);
            return cursor.FirstOrDefault();
        }

        public async Task<T> UpdateAsync(T entity)
        {
            var filter = Builders<T>.Filter.Eq(p => p.Id, entity.Id);
            var result = await collection.ReplaceOneAsync(filter, entity);
            if (!result.IsAcknowledged)
            {
                throw new Exception($"Failed to update entity {entity.Id}");
            }
            if (result.MatchedCount == 0)
            {
                throw new BadRequestException("Entity is not available in database");
            }
            if (result.ModifiedCount == 0)
            {
                throw new BadRequestException($"Failed to update profile {entity.Id}");
            }
            return entity;
        }
    }
}
