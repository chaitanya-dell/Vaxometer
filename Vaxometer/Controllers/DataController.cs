using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vaxometer.MongoOperations.DataModels;
using Vaxometer.Repository;

namespace Vaxometer.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class DataController : ControllerBase
    {
        private readonly IDataRepository _dataRepository;
        public DataController(IDataRepository dataRepository)
        {
            _dataRepository = dataRepository;
        }
#if DEBUG
        [HttpPost("CreateOne")]
        public IActionResult  CreateOnceCenter(Centers data)
        {
            var response =  _dataRepository.CreateOne(data);
            return Ok(response);
        }
#endif
    }
}
