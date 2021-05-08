using System;
using System.Collections.Generic;
using System.Text;

namespace Vaxometer.MongoOperations.DbSettings
{
    public interface IVexoDatabaseSettings
    {
        string DatabaseName { get; set; }
        string ConnectionString { get; set; }
    }
}
