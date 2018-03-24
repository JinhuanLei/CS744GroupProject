namespace MonitorNetwork.Database
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("relay")]
    public partial class relay
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public relay()
        {
            connections = new HashSet<connections>();
            connections1 = new HashSet<connections>();
        }

        public int relayID { get; set; }

        [Required]
        [StringLength(15)]
        [RegularExpression(@"^192.168.[0-9]{1,3}.[0-9]{1,3}$", ErrorMessage ="Must be a valid IP address")]
        public string relayIP { get; set; }

        public bool isActive { get; set; }

        public int queueLimit { get; set; }

        public bool isProcessingCenter { get; set; }

        public int? regionID { get; set; }

        public bool isGateway { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<connections> connections { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<connections> connections1 { get; set; }

        public virtual region region { get; set; }
    }
}
