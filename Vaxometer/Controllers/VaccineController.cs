using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Vaxometer.Manager;

namespace Vaxometer.Controllers
{
    [Route("api/Centers")]
    [ApiController]
    public class VaccineController : ControllerBase
    {
        private readonly IVaccineManager _vaccineManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public VaccineController(IVaccineManager vaccineManager, IHttpContextAccessor httpContextAccessor)
        {
            _vaccineManager = vaccineManager;
            _httpContextAccessor = httpContextAccessor;
        }

#if DEBUG
        [HttpGet("Get")]
        public async Task<IActionResult> GetVaccineCenters([FromQuery] int age, [FromQuery] decimal latitude,
            [FromQuery] decimal longitude, [FromQuery] long pincode, [FromQuery] string vaccineType)
        {
            var response = await _vaccineManager.GetVaccineCenters(age, latitude, longitude, pincode, vaccineType);
            if (response == null)
                await BuildHttpResponseForNotFound("Centers not found");
            return Ok(response);
        }


#endif
        private async Task BuildHttpResponseForNotFound(string displayMessage)
        {
            _httpContextAccessor.HttpContext.Response.ContentType = "application/json";
            int statusCode = (int)HttpStatusCode.NotFound;
            _httpContextAccessor.HttpContext.Response.StatusCode = statusCode;
            var message = JsonConvert.SerializeObject(new
            {
                StatusCode = statusCode,
                ErrorMessage = displayMessage
            });
            await _httpContextAccessor.HttpContext.Response.WriteAsync(message);
        }
    }
}