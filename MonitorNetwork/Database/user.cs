namespace MonitorNetwork.Database
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("user")]
    public partial class user
    {
        public int userID { get; set; }

        [Required]
        [StringLength(45)]
        public string username { get; set; }

        [Required]
        [StringLength(45)]
        public string password { get; set; }

        [Required]
        [StringLength(100)]
        public string security1 { get; set; }

        [Required]
        [StringLength(45)]
        public string answer1 { get; set; }

        [Required]
        [StringLength(100)]
        public string security2 { get; set; }

        [Required]
        [StringLength(45)]
        public string answer2 { get; set; }

        [Required]
        [StringLength(100)]
        public string security3 { get; set; }

        [Required]
        [StringLength(45)]
        public string answer3 { get; set; }

        public bool isBlocked { get; set; }
    }
}
