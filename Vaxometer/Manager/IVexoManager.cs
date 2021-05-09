using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vaxometer.Models;
using Vaxometer.ResponseModels;

namespace Vaxometer.Manager
{
    public interface IVexoManager
    {
        public Task<bool> RefershData();

        public Task<IEnumerable<Centers>> GetCentersByPinCode(int pincode);

        public Task<IEnumerable<Centers>> GetBangaloreCenterFor18yrs();

        public Task<IEnumerable<Centers>> GetBangaloreCenterFor45yrs();

        public Task<IEnumerable<Centers>> GetBangaloreCenterFor18yrsCovaxin();

        public Task<IEnumerable<Centers>> GetBangaloreCenterFor45yrsCovaxin();
    }
}
