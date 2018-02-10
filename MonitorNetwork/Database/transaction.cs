namespace MonitorNetwork.Database
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("transaction")]
    public partial class transaction
    {
        public int transactionID { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime timeOfTransaction { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? timeOfResponse { get; set; }

        public decimal amount { get; set; }

        public bool isCredit { get; set; }

        public bool status { get; set; }

        public bool isEncrypted { get; set; }

        public bool isSent { get; set; }

        public int storeID { get; set; }

        public int accountID { get; set; }

        public virtual account account { get; set; }

        public virtual store store { get; set; }
    }
}
