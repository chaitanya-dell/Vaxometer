using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vaxometer.Models;

namespace Vaxometer.Repository
{
    public interface IDataRepository
    {
        Task<bool> Save(CentersData centersData);
        bool CreateOne(MongoOperations.DataModels.Centers request);
        bool CreateMany(List<MongoOperations.DataModels.Centers> request);

        public Task<IEnumerable<Centers>> CentersByPinCode(int pincode);
        public Task<IEnumerable<Centers>> GetBangaloreCenterFor18yrs();
        public Task<IEnumerable<Centers>> GetBangaloreCenterFor45yrs();
        Task<IEnumerable<Centers>> GetBangaloreCenterFor18yrsCovaxin();
        Task<IEnumerable<Centers>> GetBangaloreCenterFor45yrsCovaxin();

        Task<IEnumerable<Centers>> GetVaccineCenters(int age, decimal latitude, decimal longitude, long pincode,
            string vaccineType);
    }
}
