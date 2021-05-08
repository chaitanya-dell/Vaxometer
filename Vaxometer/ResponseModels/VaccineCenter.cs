using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vaxometer.ResponseModels
{
    public class VaccineCenter
    {
        public string Name { get; set; }
        public int Pincode { get; set; }
        public string Area { get; set; }
        public string VaccineFee { get; set; }
        public bool IsCovaxinAvailable { get; set; }
        public bool Is18StartedHere { get; set; }
        public bool IsSlotAvailableFor18plus { get; set; }
        public IList<VaccineSessionByDayOfMonth> VaccineSession { get; set; }
        public bool IsSlotAvailableFor45plus { get; internal set; }
    }

    public class VaccineSessionByDayOfMonth
    {
        public string Date { get; set; }
        public string CurrentAvailableCapacity { get; set; }
        public int AgeLimit { get; set; }
        public string Vaccine { get; set; }

    }
}
