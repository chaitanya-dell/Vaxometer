using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vaxometer.MongoOperations.DataModels;

namespace Vaxometer.Helpers
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Centers, Vaxometer.Models.Centers>();
            CreateMap<Sessions, Vaxometer.Models.Sessions>();
            CreateMap<Vaccine_fees, Vaxometer.Models.Vaccine_fees>();
        }
    }
}
