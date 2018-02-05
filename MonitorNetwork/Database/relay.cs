namespace MonitorNetwork.Database
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("cs744.relay")]
    public partial class relay
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public relay()
        {
            relayconnectionweight = new HashSet<relayconnectionweight>();
            relayconnectionweight1 = new HashSet<relayconnectionweight>();
            store = new HashSet<store>();
        }

        public int relayID { get; set; }

        [StringLength(15)]
        public string relayIP { get; set; }

        public short? status { get; set; }

        public short? isProcessingCenter { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<relayconnectionweight> relayconnectionweight { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<relayconnectionweight> relayconnectionweight1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<store> store { get; set; }
    }
}
