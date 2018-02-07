namespace MonitorNetwork.Database
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("relayconnectionweight")]
    public partial class relayconnectionweight
    {
        public int relayConnectionWeightID { get; set; }

        public int weight { get; set; }

        public int relayID1 { get; set; }

        public int relayID2 { get; set; }

        public virtual relay relay { get; set; }

        public virtual relay relay1 { get; set; }
    }
}
