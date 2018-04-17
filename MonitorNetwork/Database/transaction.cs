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

        public int storeID { get; set; }

        public int? cardID { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? timeOfTransaction { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? timeOfResponse { get; set; }

        [Range(0, Int32.MaxValue, ErrorMessage = "Please enter a positive value.")]
        public decimal amount { get; set; }

        public bool isCredit { get; set; }

        public bool? status { get; set; }

        public bool isEncrypted { get; set; }

        public bool isSent { get; set; }

        public bool isSelf { get; set; }

        public bool isProcessed { get; set; }

        public bool atProcCenter { get; set; }

        [Required]
        [StringLength(16)]
        public string cardNumber { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime expirationDate { get; set; }

        public int securityCode { get; set; }

        public virtual creditcard creditcard { get; set; }

        public virtual store store { get; set; }
    }
}
