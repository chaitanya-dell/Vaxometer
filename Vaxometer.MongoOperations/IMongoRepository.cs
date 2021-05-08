using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Vaxometer.MongoOperations
{ 
    public interface IMongoRepository<T> where T : ICenter
    {
        void CreateOrUpdate(List<T> collection);
        void Create(T item);
        void CreateMany(List<T> items);


        IQueryable<T> AsQueryable();

        IEnumerable<T> FilterBy(
            Expression<Func<T, bool>> filterExpression);

        IEnumerable<TProjected> FilterBy<TProjected>(
            Expression<Func<T, bool>> filterExpression,
            Expression<Func<T, TProjected>> projectionExpression);

        T FindOne(Expression<Func<T, bool>> filterExpression);

        Task<T> FindOneAsync(Expression<Func<T, bool>> filterExpression);

     

        Task<T> FindByIdAsync(string id);

        void InsertOne(T document);

        Task InsertOneAsync(T document);

        void InsertMany(ICollection<T> documents);

        Task InsertManyAsync(ICollection<T> documents);

        void ReplaceOne(T document);

        Task ReplaceOneAsync(T document);

    }
}
