﻿using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Vaxometer.MongoOperations.DbSettings;
using Vaxometer.MongoOperations.DataModels;
using System.Runtime.CompilerServices;

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

        public async Task CreateOrUpdate(List<T> collection)
        {
            foreach (var d in collection)
            {
                await _collection.UpdateOneAsync<T>(x => x.center_id == d.center_id, 
                    Builders<T>.Update
                    .Set(p => p.block_name, d.block_name)
                    .Set(p => p.center_id, d.center_id)
                    .Set(p => p.district_name, d.district_name)
                    .Set(p => p.fee_type, d.fee_type)
                    .Set(p => p.name, d.name)
                    .Set(p => p.pincode, d.pincode)
                    .Set(p => p.state_name, d.state_name)
                    .Set(p => p.from, d.from)
                      .Set(p => p.to, d.to),
                     new UpdateOptions { IsUpsert = true });

                foreach(var s in d.sessions)
                {
                    
                    //var filter = Builders<T>.Filter.Eq(x=>x.center_id, d.center_id) & Builders<T>.Filter.ElemMatch(doc => doc.sessions, el => el.session_id == s.session_id);
                    //var secondFilter = filter.And(
                    //    filter.Eq(x => x.center_id, d.center_id),
                    //    filter.ElemMatch(x => x.sessions, c => c.session_id == s.session_id)
                    //    );

                    var filter = Builders<T>.Filter.And(
                        Builders<T>.Filter.Eq(x => x.center_id, d.center_id),
                        Builders<T>.Filter.ElemMatch(x => x.sessions, x => x.session_id == s.session_id));

                    var update = Builders<T>.Update
                         .Set(p => p.sessions[-1].available_capacity, s.available_capacity)
                         .Set(p => p.sessions[-1].date, s.date)
                         .Set(p => p.sessions[-1].min_age_limit, s.min_age_limit)
                         //.Set(p => p.sessions[-1].slots, s.slots)
                         .Set(p => p.sessions[-1].vaccine, s.vaccine);

                    var session = _collection.Find(filter).SingleOrDefault();
                    await _collection.UpdateOneAsync(filter, update, new UpdateOptions { IsUpsert = true });
                    
                }

                foreach (var v in d.vaccine_fees)
                {
                    var filter = Builders<T>.Filter;
                    var secondFilter = filter.And(
                        filter.Eq(x => x.center_id, d.center_id),
                        filter.ElemMatch(x => x.vaccine_fees, c => c.vaccine == v.vaccine)
                        );

                    var update = Builders<T>.Update
                         .Set(p => p.vaccine_fees[-1].fee, v.fee);
                         
                    var vacc = _collection.Find(secondFilter).SingleOrDefault();
                    await _collection.UpdateOneAsync(secondFilter, update, new UpdateOptions { IsUpsert = true });
                }
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

        public async Task<IEnumerable<T>> GetBangaloreCenterFor18yrs()
        {
            var builder = Builders<T>.Filter;
            var filter = builder.ElemMatch(x => x.sessions, Builders<Sessions>.Filter.Eq("min_age_limit", 18));
            return await _collection.Find(filter).ToListAsync();
        }

        public async Task<IEnumerable<T>> GetBangaloreCenterFor45yrs()
        {
            var filter = Builders<T>.Filter.ElemMatch(x => x.sessions, c => c.min_age_limit == 45);
            return await _collection.Find(filter).ToListAsync();
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
