using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Vaxometer.MongoOperations.DbSettings;
using Vaxometer.MongoOperations.DataModels;

namespace Vaxometer.MongoOperations
{
    public class MongoRepository<T> : IMongoRepository<T> where T : ICenter
    {
        private readonly IMongoCollection<T> _collection;

        public MongoRepository(IVexoDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _collection = database.GetCollection<T>(GetCollectionName(typeof(T)));
        }

        private protected string GetCollectionName(Type documentType)
        {
            return ((BsonCollectionAttribute)documentType.GetCustomAttributes(
                    typeof(BsonCollectionAttribute),
                    true)
                .FirstOrDefault())?.CollectionName;
        }

        public void CreateOrUpdate(List<T> collection)
        {
            foreach (var d in collection)
            {
                _collection.UpdateOne(x =>
              x.center_id == d.center_id,
              Builders<T>.Update
              .Set(p => p.block_name, d.block_name)
              .Set(p => p.center_id, d.center_id)
              .Set(p => p.district_name, d.district_name)
              .Set(p => p.fee_type, d.name)
              .Set(p => p.name, d.name)
              .Set(p => p.pincode, d.pincode)
              .Set(p => p.state_name, d.state_name),
              new UpdateOptions { IsUpsert = true });;
            }
        }

        public void Create(T item)
        {
            _collection.InsertOne(item);
        }

        public void CreateMany(List<T> items)
        {
            _collection.InsertMany(items);
        }

        public async Task<IEnumerable<T>> CentersByPinCode(int pincode)
        {
            var builder = Builders<T>.Filter;
            var query = builder.Eq("pincode", pincode); 
            return await _collection.Find(query).ToListAsync();
        }




        public virtual IEnumerable<Centers> GetBangaloreCenterFor18yrs()
        {
            ////var filter = Builders<BsonDocument>.Filter.Eq("min_age_limit", 18);
            ////var filter = Builders<T>.Filter.Eq(doc => doc.Id, objectId);
            //var filter = Builders<Sessions>.Filter.Where(new[]
            //{
            //    new ExpressionFilterDefinition<Sessions>(x => x.min_age_limit == 18),
            //});
            //return _collection.Find(filter).ToEnumerable();
            throw new NotImplementedException();
        }

        public virtual IEnumerable<T> FilterBy(Expression<Func<T, bool>> filterExpression)
        {
            return _collection.Find(filterExpression).ToEnumerable();
        }

        public virtual IEnumerable<TProjected> FilterBy<TProjected>(Expression<Func<T, bool>> filterExpression, Expression<Func<T, TProjected>> projectionExpression)
        {
            return _collection.Find(filterExpression).Project(projectionExpression).ToEnumerable();
        }

        

        public virtual Task<T> FindByIdAsync(string id)
        {
            return Task.Run(() =>
            {
                var objectId = new ObjectId(id);
                var filter = Builders<T>.Filter.Eq(doc => doc.Id, objectId);
                return _collection.Find(filter).SingleOrDefaultAsync();
            });
        }

        public virtual T FindOne(Expression<Func<T, bool>> filterExpression)
        {
            return _collection.Find(filterExpression).FirstOrDefault();
        }

        public virtual Task<T> FindOneAsync(Expression<Func<T, bool>> filterExpression)
        {
            return Task.Run(() => _collection.Find(filterExpression).FirstOrDefaultAsync());
        }

        public void InsertMany(ICollection<T> documents)
        {
            throw new NotImplementedException();
        }

        public Task InsertManyAsync(ICollection<T> documents)
        {
            throw new NotImplementedException();
        }

        public void InsertOne(T document)
        {
            throw new NotImplementedException();
        }

        public Task InsertOneAsync(T document)
        {
            throw new NotImplementedException();
        }

        public void ReplaceOne(T document)
        {
            throw new NotImplementedException();
        }

        public Task ReplaceOneAsync(T document)
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> AsQueryable()
        {
            throw new NotImplementedException();
        }
    }
}
