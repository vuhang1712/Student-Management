namespace StudentManager.ConnectDB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("LOP")]
    public partial class LOP
    {
        [Key]
        public int MA_LOP { get; set; }

        [Required]
        [StringLength(50)]
        public string TEN_LOP { get; set; }
    }
}
