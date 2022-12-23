using Microsoft.Office.Server.Search.RankerTuning;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.SharePoint.WebPartPages.WebPartToolPart;

namespace SingTaxiAPI.Model
{

    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class ApiInfo
    {
        public string status { get; set; }
    }

    public class Crs
    {
        public string type { get; set; }
        public Properties properties { get; set; }
    }

    public class Feature
    {
        public string type { get; set; }
        public Geometry geometry { get; set; }
        public Properties properties { get; set; }
    }

    public class Geometry
    {
        public string type { get; set; }
        public List<List<double>> coordinates { get; set; }
    }

    public class Properties
    {
        public string href { get; set; }
        public string type { get; set; }
        public DateTime timestamp { get; set; }
        public int taxi_count { get; set; }
        public ApiInfo api_info { get; set; }
    }

    public class RootObject
    {
        public string type { get; set; }
        public Crs crs { get; set; }
        public List<Feature> features { get; set; }
    }


}
