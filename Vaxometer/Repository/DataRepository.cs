using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vaxometer.MongoOperations;
using Vaxometer.MongoOperations.DbSettings;
using Vaxometer.MongoOperations.DataModels;
using Vaxometer.Models;
using AutoMapper;
using Centers = Vaxometer.Models.Centers;

namespace Vaxometer.Repository
{
    public class DataRepository : IDataRepository
    {
        private IMongoRepository<MongoOperations.DataModels.Centers> _mongoCenters;
        private readonly IVexoDatabaseSettings _settings;
        private readonly ILogger<DataRepository> _logger;
        private readonly IMapper _mapper;

        public DataRepository(
            IVexoDatabaseSettings settings, ILogger<DataRepository> logger,
            IMongoRepository<MongoOperations.DataModels.Centers> mongoRepository, IMapper mapper)
        {
            _settings = settings;
            _logger = logger;
            _mongoCenters = mongoRepository;
            _mapper = mapper;
        }

        public async Task<bool> Save(CentersData request)
        {
            try
            {
                _mongoCenters = new MongoRepository<MongoOperations.DataModels.Centers>(_settings);
                var centersCollection = new List<MongoOperations.DataModels.Centers>();
              
                foreach (var center in request.Centers)
                    centersCollection.Add(_mapper.Map<MongoOperations.DataModels.Centers>(center));
                await _mongoCenters.CreateOrUpdate(centersCollection);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogTrace(ex.Message, ex);
               
            }
            return false;
        }


        public async Task<IEnumerable<Models.Centers>> CentersByPinCode(int pincode)
        {
            _mongoCenters = new MongoRepository<MongoOperations.DataModels.Centers>(_settings);
            var dataModel = await _mongoCenters.CentersByPinCode(pincode);
            var collection = new List<Models.Centers>();
            foreach (var doc in dataModel)
                collection.Add(_mapper.Map<Models.Centers>(doc));
            return collection;
        }

        public async Task<IEnumerable<Models.Centers>> GetBangaloreCenterFor18yrs()
        {
            _mongoCenters = new MongoRepository<MongoOperations.DataModels.Centers>(_settings);
            var dataModel = await _mongoCenters.GetBangaloreCenterFor18yrs();
            var collection = new List<Models.Centers>();
            foreach (var doc in dataModel)
                collection.Add(_mapper.Map<Models.Centers>(doc));
            return collection;
        }


        public async Task<IEnumerable<Models.Centers>> GetBangaloreCenterFor45yrs()
        {
            _mongoCenters = new MongoRepository<MongoOperations.DataModels.Centers>(_settings);
            var dataModel = await _mongoCenters.GetBangaloreCenterFor45yrs();
            var collection = new List<Models.Centers>();
            foreach (var doc in dataModel)
                collection.Add(_mapper.Map<Models.Centers>(doc));
            return collection;
        }

        public bool CreateOne(MongoOperations.DataModels.Centers request)
        {
            try
            {
                _mongoCenters = new MongoRepository<MongoOperations.DataModels.Centers>(_settings);
                _mongoCenters.Create(request);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogTrace(ex.Message, ex);
                throw ex;
            }
        }

        public bool CreateMany(List<MongoOperations.DataModels.Centers> request)
        {
            try
            {
                _mongoCenters = new MongoRepository<MongoOperations.DataModels.Centers>(_settings);
                _mongoCenters.CreateMany(request);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogTrace(ex.Message, ex);
                throw ex;
            }
        }

        public async Task<IEnumerable<Centers>> GetVaccineCenters(int age, decimal latitude, decimal longitude, long pincode, string vaccineType)
        {
            try
            {
                _mongoCenters = new MongoRepository<MongoOperations.DataModels.Centers>(_settings);
               var dataModel = await _mongoCenters.GetVaccineCenters(age,latitude,longitude,pincode,vaccineType);
               var collection = new List<Models.Centers>();
               foreach (var doc in dataModel)
                   collection.Add(_mapper.Map<Models.Centers>(doc));
               return collection;
            }
            catch (Exception ex)
            {
                _logger.LogTrace(ex.Message, ex);
                throw ex;
            }
        }
        Task<IEnumerable<Models.Centers>> IDataRepository.GetBangaloreCenterFor18yrsCovaxin()
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<Models.Centers>> IDataRepository.GetBangaloreCenterFor45yrsCovaxin()
        {
            throw new NotImplementedException();
        }



        public Task<List<MongoOperations.DataModels.Centers>> GetBangaloreCenterFor18yrsCovaxin()
        {
            throw new NotImplementedException();
        }

       

        public Task<List<MongoOperations.DataModels.Centers>> GetBangaloreCenterFor45yrsCovaxin()
        {
            throw new NotImplementedException();
        }

       
    }
}
