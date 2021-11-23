namespace StudentManager.ConnectDB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class DANG_NHAP
    {
        [Key]
        public int STT { get; set; }

        [Required]
        [StringLength(50)]
        public string TEN_DANG_NHAP { get; set; }

        [Required]
        [StringLength(50)]
        public string MAT_KHAU { get; set; }

        [Required]
        [StringLength(50)]
        public string HO_TEN { get; set; }
    }
}
