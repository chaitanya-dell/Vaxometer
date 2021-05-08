using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vaxometer.Models;

namespace Vaxometer.Repository
{
    public interface IDataRepository
    {
        bool Save(CentersData centersData);
        bool CreateOne(MongoOperations.DataModels.Centers request);
        bool CreateMany(MongoOperations.DataModels.Centers request);

        public Task<List<MongoOperations.DataModels.Centers>> GetBangaloreCenterFor18yrs();
    }
}
