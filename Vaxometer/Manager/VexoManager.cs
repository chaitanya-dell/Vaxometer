using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vaxometer.Repository;
using Vaxometer.ResponseModels;

namespace Vaxometer.Manager
{
    public class VexoManager : IVexoManager
    {
        private readonly ICowinRepository _cowinRepository;
        private readonly IDataRepository _dataRepository;
        private readonly ILogger _logger;
        
        public VexoManager(ICowinRepository cowinRepository, IDataRepository dataRepository,
            ILoggerFactory loggerFactory)
        {
            _cowinRepository = cowinRepository;
            _dataRepository = dataRepository;
            _logger = loggerFactory.CreateLogger<VexoManager>();
        }


        public async Task<bool> RefershData()
        {
            var centersData = await _cowinRepository.GetCentersForDistrict_294_265();
            bool isUpdated = _dataRepository.Save(centersData);
            return isUpdated;
        }

        public Task<List<VaccineCenter>> GetBangaloreCenterFor18yrs()
        {
            
            throw new NotImplementedException();
        }

        public Task<List<VaccineCenter>> GetBangaloreCenterFor18yrsCovaxin()
        {
            throw new NotImplementedException();
        }

        public Task<List<VaccineCenter>> GetBangaloreCenterFor45yrs()
        {
            throw new NotImplementedException();
        }

        public Task<List<VaccineCenter>> GetBangaloreCenterFor45yrsCovaxin()
        {
            throw new NotImplementedException();
        }

       

    }
}
