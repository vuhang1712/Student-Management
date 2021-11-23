namespace StudentManager.ConnectDB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class KY_THI_MON_THI
    {
        public int ID { get; set; }

        public int? KY_THI { get; set; }

        public int? MON_THI { get; set; }
    }
}
