namespace MonitorNetwork.Database
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("cs744.relayconnectionweight")]
    public partial class relayconnectionweight
    {
        public int relayConnectionWeightID { get; set; }

        public int? weight { get; set; }

        public int? relay1 { get; set; }

        public int? relay2 { get; set; }

        public virtual relay relay { get; set; }

        public virtual relay relay3 { get; set; }
    }
}
