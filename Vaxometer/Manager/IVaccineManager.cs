using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vaxometer.Models;

namespace Vaxometer.Manager
{
    public interface IVaccineManager
    {
        Task<IEnumerable<Centers>> GetVaccineCenters(int age,decimal latitude, decimal longitude,long pincode, string vaccineType);
    }
}
