namespace StudentManager.ConnectDB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class MON_THI
    {
        public int ID { get; set; }

        [Column("MON_THI")]
        [Required]
        [StringLength(50)]
        public string MON_THI1 { get; set; }
        public int? TIN_CHI { get; set; }
        public bool IsDelete { get; set; }
        public MON_THI() { }
        public MON_THI(int id, string mon_thi)
        {
            ID = id;
            MON_THI1 = mon_thi;
        }
    }
}
