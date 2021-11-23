namespace StudentManager.ConnectDB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class KY_THI
    {
        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        public string TEN_KY_THI { get; set; }

        public DateTime? NGAY_BAT_DAU { get; set; }

        public DateTime? NGAY_KET_THUC { get; set; }

        public int? TRANG_THAI { get; set; }
        public bool IsDelete { get; set; }
    }
}
