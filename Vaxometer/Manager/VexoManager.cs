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

        public async Task<IEnumerable<Centers>> GetBangaloreCenterFor18yrsCovaxin()
        {
            return await _dataRepository.GetBangaloreCenterFor18yrsCovaxin();
        }

        
        public async Task<IEnumerable<Centers>> GetBangaloreCenterFor45yrsCovaxin()
        {
            return await _dataRepository.GetBangaloreCenterFor45yrsCovaxin();
        }

        public async Task<IEnumerable<Centers>> GetBangaloreCenterFor18yrsCoviShield()
        {
            return await _dataRepository.GetBangaloreCenterFor18yrsCoviShield();
        }

        public async Task<IEnumerable<Centers>> GetBangaloreCenterFor45yrsCovishield()
        {
            return await _dataRepository.GetBangaloreCenterFor45yrsCovishield();
        }
    }
}
