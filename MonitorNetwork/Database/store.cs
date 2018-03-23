namespace MonitorNetwork.Database
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("store")]
    public partial class store
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public store()
        {
            connections = new HashSet<connections>();
            transaction = new HashSet<transaction>();
        }

        public int storeID { get; set; }

        [Required]
        [StringLength(15)]
        public string storeIP { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "Invalid merchant name length.")]
        public string merchantName { get; set; }

        public int regionID { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<connections> connections { get; set; }

        public virtual region region { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<transaction> transaction { get; set; }
    }
}
