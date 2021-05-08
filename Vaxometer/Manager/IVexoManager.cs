using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vaxometer.ResponseModels;

namespace Vaxometer.Manager
{
    public interface IVexoManager
    {
        public Task<bool> RefershData();

        public Task<List<VaccineCenter>> GetBangaloreCenterFor18yrs();

        public Task<List<VaccineCenter>> GetBangaloreCenterFor18yrsCovaxin();

        public Task<List<VaccineCenter>> GetBangaloreCenterFor45yrs();

        public Task<List<VaccineCenter>> GetBangaloreCenterFor45yrsCovaxin();
    }
}
