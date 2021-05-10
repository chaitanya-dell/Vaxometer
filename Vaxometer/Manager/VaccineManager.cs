using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vaxometer.Models;
using Vaxometer.Repository;

namespace Vaxometer.Manager
{
    public class VaccineManager : IVaccineManager
    {
        private readonly IDataRepository _dataRepository;

        public VaccineManager(IDataRepository dataRepository)
        {
            _dataRepository = dataRepository;
        }
        public async Task<IEnumerable<Centers>> GetVaccineCenters(int age, decimal latitude, decimal longitude, long pincode, string vaccineType)
        {
            return await _dataRepository.GetVaccineCenters(age, latitude, longitude, pincode, vaccineType);
        }
    }
}
