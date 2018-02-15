using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MonitorNetwork.Database;

namespace MonitorNetwork.Models
{
	public class NetworkModel
	{
		public IList<transaction> transactions { get; set; }
		public IList<connections> connections { get; set; }
		public IList<relay> relays { get; set; }
	}
}