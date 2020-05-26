using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace proanimafilms_api.Models
{
    public class ProAnimaFILMS_botDatabaseSettings : IProAnimaFILMS_botDatabaseSettings
    {
        public string FilmsCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

    public interface IProAnimaFILMS_botDatabaseSettings
    {
        string FilmsCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
