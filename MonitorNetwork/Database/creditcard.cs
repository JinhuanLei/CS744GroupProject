namespace MonitorNetwork.Database
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("creditcard")]
    public partial class creditcard
    {
        [Key]
        public int cardID { get; set; }

        [Required]
        [StringLength(16)]
        public string cardNumber { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime expirationDate { get; set; }

        public int securityCode { get; set; }

        [Required]
        [StringLength(45)]
        public string customerFirstName { get; set; }

        [Required]
        [StringLength(45)]
        public string customerLastName { get; set; }

        public int accountID { get; set; }

        public virtual account account { get; set; }
    }
}
