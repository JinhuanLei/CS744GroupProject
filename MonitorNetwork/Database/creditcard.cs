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
        [DisplayFormat(DataFormatString = "{0:MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime expirationDate { get; set; }

        [Range(100, 999, ErrorMessage = "Please enter a value between 100 to 999")]
        public int securityCode { get; set; }

        [Required]
        [StringLength(15, MinimumLength = 2, ErrorMessage = "Invalid name length.")]
        public string customerFirstName { get; set; }

        [Required]
        [StringLength(15, MinimumLength = 2, ErrorMessage = "Invalid name length.")]
        public string customerLastName { get; set; }

        public int accountID { get; set; }

        public virtual account account { get; set; }
    }
}
