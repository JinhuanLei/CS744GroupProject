using MonitorNetwork.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MonitorNetwork.Models
{
    public class NetworkModel
    {

        public IEnumerable<transaction> transactions { get; set; }
    }
}