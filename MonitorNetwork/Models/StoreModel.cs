using MonitorNetwork.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MonitorNetwork.Models
{
    public class StoreModel
    {
        public store store { get; set; }

        public CheckboxRelayModel checkboxRelayModel { get; set; }
    }
}