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
        public IList<transaction> transactions { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public IList<PCTransactions> pcTransactions { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public IList<Connections> connections { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public IList<Relays> relays { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public IList<Stores> stores { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public IList<CytoscapeData> cytoscapeNodes { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public IList<CytoscapeData> cytoscapeEdges { get; set; }
    }

    public class CytoscapeData
    {
        public object data { get; set; }
    }

    public class CytoscapeNode
    {
        public string id { get; set; }
        public string label { get; set; }
        public string parent { get; set; }
        public string name { get; set; }
    }

    public class CytoscapeEdge
    {
        public string id { get; set; }
        public int weight { get; set; }
        public string source { get; set; }
        public string target { get; set; }
    }

    public class PCTransactions
    {
        public int transactionID { get; set; }
        public int storeID { get; set; }
    }

    public class Connections
    {
        public int connID { get; set; }
        public int? storeID { get; set; }
        public int? relayID { get; set; }
        public int destRelayID { get; set; }
        public int weight { get; set; }
        public bool isActive { get; set; }
    }

    public class Relays
    {
        public int relayID { get; set; }
        public string relayIP { get; set; }
        public bool isActive { get; set; }
        public int queueLimit { get; set; }
        public bool isProcessingCenter { get; set; }
    }

    public class Stores
    {
        public int storeID { get; set; }
        public string storeIP { get; set; }
        public string merchantName { get; set; }
    }
}