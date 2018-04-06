using MonitorNetwork.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MonitorNetwork.Models
{
    public class RelayModel
    {
        public relay relay { get; set; }

        public IList<CheckboxRelayModel> checkboxRelayModel { get; set; }
    }
}