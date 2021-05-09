﻿using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Vaxometer.ApplicationSettings;
using Vaxometer.Models;

namespace Vaxometer.Repository
{
    public class CowinRepository : ICowinRepository
    {
        //private readonly HttpClient _httpClient;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IApplicationUrls _applicationUrls;
        private readonly ILogger _logger;

        public CowinRepository(IHttpClientFactory httpClientFactory, IApplicationUrls applicationUrls, ILoggerFactory loggerFactory)
        {
            _httpClientFactory = httpClientFactory;
            _applicationUrls = applicationUrls;
            _logger = loggerFactory.CreateLogger<CowinRepository>();
        }

        public async Task<CentersData> GetCentersForDistrict_294_265()
        {

            var vaccineCenters1 = new CentersData();
            var vaccineCenters2 = new CentersData();

            try
            {
#if DEBUG
                return GetMockData();
#endif



                var _httpClient = _httpClientFactory.CreateClient();
                var getDate = DateTime.Now.ToString("dd-MM-yyyy");
                using (var response = await _httpClient.GetAsync(_applicationUrls.CanlendarUrl265 + "&date=" + getDate))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        vaccineCenters1 = JsonConvert.DeserializeObject<CentersData>(response.Content.ReadAsStringAsync().Result);
                    }
                }

                using (var response = await _httpClient.GetAsync(_applicationUrls.CanlendarUrl294 + "&date=" + getDate))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        vaccineCenters2 = JsonConvert.DeserializeObject<CentersData>(response.Content.ReadAsStringAsync().Result);
                    }
                }

                if ((vaccineCenters1.Centers != null && vaccineCenters1.Centers.Any()) ||
                    (vaccineCenters2.Centers != null && vaccineCenters2.Centers.Any()))
                {
                    var vaccineCenters = new CentersData()
                    {
                        Centers = new List<Centers>()
                    };
                    if (vaccineCenters1.Centers != null && vaccineCenters1.Centers.Any())
                        vaccineCenters.Centers.Concat(vaccineCenters1.Centers);
                    if (vaccineCenters2.Centers != null && vaccineCenters2.Centers.Any())
                        vaccineCenters.Centers.Concat(vaccineCenters2.Centers);
                    return vaccineCenters;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return null;
        }

        private CentersData GetMockData()
        {
            var session = new Sessions()
            {
                session_id = "82d2723c-5760-40a4-97ef-26628458a831",
                date = "09-05-2021",
                available_capacity = 10,
                min_age_limit = 45,
                vaccine = "COVAXIN",
                slots = new List<string>() { "09:00AM-11:00AM","12:00AM-01:00PM"}
            };
            var sessions = new List<Sessions>
            {
                session
            };

            var vaccine_fee = new Vaccine_fees()
            {
                vaccine = "COVAXIN",
                fee = "2000"
            };
            var vaccine_fees = new List<Vaccine_fees>
            {
                vaccine_fee
            };

            var center = new Centers()
            {
                center_id = 557647,
                name = "SUGUNA HOSPITAL",
                address = "DR RAJ KUMAR ROAD4TH N BLOCK RAJAJINAGAR",
                state_name = "Karnataka",
                district_name = "BBMP",
                block_name = "West",
                pincode = 560010,
                lat = 12,
                @long = 77,
                from = "09:00:00",
                to = "17:00:00",
                fee_type = "Paid",
                sessions = sessions,
                vaccine_fees = vaccine_fees
            };
            var centers = new List<Centers>
            {
                center
            };

            var mockData = new CentersData
            {
                Centers = centers
            };

            return mockData;
        }
    }
}
