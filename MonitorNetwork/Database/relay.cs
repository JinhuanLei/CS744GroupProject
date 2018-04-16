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
        [RegularExpression(@"^((25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$", ErrorMessage ="Must be a valid IP address")]
        public string relayIP { get; set; }

        public bool isActive { get; set; }

		[Required]
		[Range(0, Int32.MaxValue, ErrorMessage = "Please enter a positive value")]
        public int queueLimit { get; set; }

        public bool isProcessingCenter { get; set; }

        public int regionID { get; set; }

        public bool isGateway { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<connections> connections { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<connections> connections1 { get; set; }

        public virtual region region { get; set; }
    }
}
