using MonitorNetwork.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MonitorNetwork.Models
{
    public class RegionStoreRelayModel
    {
        public region region { get; set; }
        public store store { get; set; }
		public relay relay { get; set; }
		public colors colors { get; set; }
		public connections connections { get; set; }

		public IList<CheckboxGatewayModel> CheckboxGatewayModel { get; set; }


	}
}