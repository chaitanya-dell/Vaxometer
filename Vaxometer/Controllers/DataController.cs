using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vaxometer.Attributes;
using Vaxometer.Manager;
using Vaxometer.MongoOperations.DataModels;
using Vaxometer.Repository;

namespace Vaxometer.Controllers
{
    [ApiKey]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class DataController : ControllerBase
    {
        private readonly IDataRepository _dataRepository;
        private readonly IVexoManager _vexoManager;
        public DataController(IDataRepository dataRepository, IVexoManager vexoManager)
        {
            _dataRepository = dataRepository;
            _vexoManager = vexoManager;
        }
#if DEBUG
        /// <summary>Protected API. Requires API Key & works only on Debug Mode</summary>
        [HttpPost("CreateOne")]
        public IActionResult  CreateOneCenter(Centers data)
        {
            var response =  _dataRepository.CreateOne(data);
            return Ok(response);
        }

        /// <summary>Protected API. Requires API Key & works only on Debug Mode</summary>
        [HttpPost("CreateMany")]
        public IActionResult CreateManyCenter(List<Centers> data)
        {
            var response = _dataRepository.CreateMany(data);
            return Ok(response);
        }
#endif
        /// <summary>Protected API. Requires API Key</summary>
        [HttpPost("Refresh/{centerCode}")]
        public IActionResult Upsert(string centerCode)
        {
            //if center code is BLR, then refresh 
            //TODO: if centerCode is other than BLR
            var response = _vexoManager.RefershData();
            return Ok(response);
        }
    }
}
 