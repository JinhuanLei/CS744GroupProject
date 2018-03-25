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
        public int connID { get; set; }

        public int? storeID { get; set; }

        public int? relayID { get; set; }

        public int destRelayID { get; set; }

		[Required]
		[Range(0, Int32.MaxValue, ErrorMessage = "Please enter a value equal to or greater than 0")]
        public int weight { get; set; }

        public bool isActive { get; set; }

        public virtual relay relay { get; set; }

        public virtual relay relay1 { get; set; }

        public virtual store store { get; set; }
    }
}
