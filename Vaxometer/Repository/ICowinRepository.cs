using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vaxometer.Models;

namespace Vaxometer.Repository
{
    public interface ICowinRepository
    {
       public Task<CentersData> GetCentersForDistrict_294_265();
    }
}
