namespace MonitorNetwork.Database
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("account")]
    public partial class account
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public account()
        {
            creditcard = new HashSet<creditcard>();
            transaction = new HashSet<transaction>();
        }

        public int accountID { get; set; }

        [Required]
        [StringLength(15, MinimumLength = 2, ErrorMessage = "Invalid name length.")]
        public string accountFirstName { get; set; }

        [Required]
        [StringLength(15, MinimumLength = 2, ErrorMessage = "Invalid name length.")]
        public string accountLastName { get; set; }

        [Required]
        [StringLength(100)]
        public string address { get; set; }

        [Required]
        [StringLength(45)]
        [RegularExpression(@"^\(([0-9]{3})\)[ ]([0-9]{3})[-]([0-9]{4})$", ErrorMessage = "Not a valid Phone number")]
        public string phoneNumber { get; set; }

        [Range(0, 25000, ErrorMessage = "Please enter a value between 0 to 25000")]
        public decimal spendingLimit { get; set; }

        public decimal balance { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<creditcard> creditcard { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<transaction> transaction { get; set; }
    }
}
