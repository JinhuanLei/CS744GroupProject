namespace MonitorNetwork.Database
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("cs744.user")]
    public partial class user
    {
        public int userID { get; set; }

        [StringLength(45)]
        public string username { get; set; }

        [StringLength(45)]
        public string password { get; set; }

        [StringLength(100)]
        public string security1 { get; set; }

        [StringLength(45)]
        public string answer1 { get; set; }

        [StringLength(100)]
        public string security2 { get; set; }

        [StringLength(45)]
        public string answer2 { get; set; }

        [StringLength(100)]
        public string security3 { get; set; }

        [StringLength(45)]
        public string answer3 { get; set; }

        public short? isBlocked { get; set; }
    }
}
