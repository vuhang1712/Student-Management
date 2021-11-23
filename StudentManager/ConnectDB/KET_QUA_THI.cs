namespace StudentManager.ConnectDB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class KET_QUA_THI
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }

        public int? KY_THI { get; set; }

        public int? MON_THI { get; set; }

        public int? ID_SINH_VIEN { get; set; }

        public double? DIEM { get; set; }
    }
}
