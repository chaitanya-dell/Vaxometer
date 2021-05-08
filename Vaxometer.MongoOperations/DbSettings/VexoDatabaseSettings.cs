using System;
using System.Collections.Generic;
using System.Text;

namespace Vaxometer.MongoOperations.DbSettings
{
   
    public class VexoDatabaseSettings : IVexoDatabaseSettings
    {
        public string DatabaseName { get; set; }
        public string ConnectionString { get; set; }
    }
}
