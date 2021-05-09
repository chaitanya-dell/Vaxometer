using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vaxometer.Models;
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
            try
            {
                var centersData = await _cowinRepository.GetCentersForDistrict_294_265();
                //#if DEBUG
                //            return true;
                //#endif
                return await _dataRepository.Save(centersData);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message, ex);
            }
            return false;
        }

        public async Task<IEnumerable<Centers>> GetCentersByPinCode(int pincode)
        {
            return await _dataRepository.CentersByPinCode(pincode);
        }

        public async Task<IEnumerable<Centers>> GetBangaloreCenterFor18yrs()
        {
            return await _dataRepository.GetBangaloreCenterFor18yrs();
        }

        public async Task<IEnumerable<Centers>> GetBangaloreCenterFor45yrs()
        {
            return await _dataRepository.GetBangaloreCenterFor45yrs();
        }

        public Task<List<VaccineCenter>> GetBangaloreCenterFor18yrsCovaxin()
        {
            throw new NotImplementedException();
        }

        
        public Task<List<VaccineCenter>> GetBangaloreCenterFor45yrsCovaxin()
        {
            throw new NotImplementedException();
        }

      
    }
}
