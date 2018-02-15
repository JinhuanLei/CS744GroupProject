namespace MonitorNetwork.Database
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class connections
    {
        [Key]
        [Column(Order = 0)]
        public int connID { get; set; }

        public int? storeID { get; set; }

        public int? relayID { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int destRelayID { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int weight { get; set; }

        [Key]
        [Column(Order = 3)]
        public bool active { get; set; }
    }
}
