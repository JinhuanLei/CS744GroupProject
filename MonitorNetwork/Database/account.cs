namespace MonitorNetwork.Database
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("cs744.account")]
    public partial class account
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public account()
        {
            creditcard = new HashSet<creditcard>();
            transaction = new HashSet<transaction>();
        }

        public int accountID { get; set; }

        [StringLength(45)]
        public string accountFirstName { get; set; }

        [StringLength(45)]
        public string accountLastName { get; set; }

        [StringLength(100)]
        public string address { get; set; }

        [StringLength(45)]
        public string phoneNumber { get; set; }

        public decimal? spendingLimit { get; set; }

        public decimal? balance { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<creditcard> creditcard { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<transaction> transaction { get; set; }
    }
}
