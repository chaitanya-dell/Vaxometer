using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vaxometer.MongoOperations;
using Vaxometer.MongoOperations.DbSettings;
using Vaxometer.MongoOperations.DataModels;
using Vaxometer.Models;

namespace Vaxometer.Repository
{
    public class DataRepository : IDataRepository
    {
        private IMongoRepository<MongoOperations.DataModels.Centers> _mongoCenters;
        private readonly IVexoDatabaseSettings _settings;
        private readonly ILogger<DataRepository> _logger;

        public DataRepository(
            IVexoDatabaseSettings settings, ILogger<DataRepository> logger,
            IMongoRepository<MongoOperations.DataModels.Centers> mongoRepository)
        {
            _settings = settings;
            _logger = logger;
            _mongoCenters = mongoRepository;
        }

        public bool Save(CentersData request)
        {
            try
            {
                _mongoCenters = new MongoRepository<MongoOperations.DataModels.Centers>(_settings);
                var centersCollection = new List<MongoOperations.DataModels.Centers>();
                foreach (var item in request.Centers)
                {
                    var center = new MongoOperations.DataModels.Centers()
                    {
                        block_name = item.block_name,
                        center_id = item.center_id,
                        district_name = item.district_name,
                        fee_type = item.fee_type,
                        name = item.name,
                        pincode = item.pincode,
                        state_name = item.state_name
                    };
                    centersCollection.Add(center);
                }
                _mongoCenters.CreateOrUpdate(centersCollection);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogTrace(ex.Message, ex);
                throw ex;
            }
        }

        public bool CreateOne(MongoOperations.DataModels.Centers request)
        {
            try
            {
                _mongoCenters.Create(request);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogTrace(ex.Message, ex);
                throw ex;
            }
        }

        public bool CreateMany(MongoOperations.DataModels.Centers request)
        {
            try
            {
                _mongoCenters.Create(request);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogTrace(ex.Message, ex);
                throw ex;
            }
        }

        public Task<List<MongoOperations.DataModels.Centers>> GetBangaloreCenterFor18yrs()
        {

            throw new NotImplementedException();
        }

        public Task<List<MongoOperations.DataModels.Centers>> GetBangaloreCenterFor18yrsCovaxin()
        {
            throw new NotImplementedException();
        }

        public Task<List<MongoOperations.DataModels.Centers>> GetBangaloreCenterFor45yrs()
        {
            throw new NotImplementedException();
        }

        public Task<List<MongoOperations.DataModels.Centers>> GetBangaloreCenterFor45yrsCovaxin()
        {
            throw new NotImplementedException();
        }
    }
}
