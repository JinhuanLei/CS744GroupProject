using MonitorNetwork.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace MonitorNetwork.Models
{
    public class NetworkModel
    {
        public IEnumerable<transaction> transactions { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public IList<Connections> connections { get; set; }
    }

    public class Connections
    {
        public int connID { get; set; }
        public int? storeID { get; set; }
        public int? relayID { get; set; }
        public int destRelayID { get; set; }
        public int weight { get; set; }
        public bool active { get; set; }
    }
}