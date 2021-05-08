using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Vaxometer.Manager;
using Vaxometer.ResponseModels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Vaxometer.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class VaxometerController : ControllerBase
    {
        private readonly IVexoManager  _vaxoManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public VaxometerController(IVexoManager vaxometerService, IHttpContextAccessor httpContext)
        {
            _vaxoManager = vaxometerService;
            _httpContextAccessor = httpContext;
        }

        /// <summary>Gets the List of Centers by pincode</summary>
        [HttpGet("Centers/pincode/{pincode}")]
        public async Task<IActionResult> GetBangaloreCenterFor18yrs(int pincode)
        {
            var response = await _vaxoManager.GetCentersByPinCode(pincode);
            if (response == null)
                await BuildHttpResponseForNotFound("Centers not found");
            return Ok(response);
        }

        /// <summary>Gets the List of Centers where vaccination started for 18+</summary>
        [HttpGet("Centers/18")]
        public async Task<IList<VaccineCenter>> GetBangaloreCenterFor18yrs()
        {
            var response = await _vaxoManager.GetBangaloreCenterFor18yrs();
            if (response == null)
                await BuildHttpResponseForNotFound("Centers not found");
            return response;
        }

        /// <summary>Gets the List of Centers where Covaxin started for 18+</summary>
        [HttpGet("Centers/18/Covaxin")]
        public async Task<IList<VaccineCenter>> GetBangaloreCenterFor18yrsCovaxin()
        {
            var response = await _vaxoManager.GetBangaloreCenterFor18yrsCovaxin();
            if (response == null)
                await BuildHttpResponseForNotFound("Centers not found");
            return response;
        }

        /// <summary>Gets the List of Centers where vaccination started for 45+</summary>
        [HttpGet("Centers/45/")]
        public async Task<IList<VaccineCenter>> GetBangaloreCenterFor45yrs()
        {
            var response = await _vaxoManager.GetBangaloreCenterFor45yrs();
            if (response == null)
                await BuildHttpResponseForNotFound("Centers not found");
            return response;
        }

        /// <summary>Gets the List of Centers where Covaxin started for 45+</summary>
        [HttpGet("Centers/45/Covaxin")]
        public async Task<IList<VaccineCenter>> GetBangaloreCenterFor45yrsCovaxin()
        {
            var response = await _vaxoManager.GetBangaloreCenterFor45yrsCovaxin();
            if (response == null)
                await BuildHttpResponseForNotFound("Centers not found");
            return response;
        }

       

        





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
