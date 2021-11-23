namespace StudentManager.ConnectDB
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class DBcontext : DbContext
    {
        public DBcontext()
            : base("name=DBcontext3")
        {
        }

        public virtual DbSet<DANG_NHAP> DANG_NHAP { get; set; }
        public virtual DbSet<KET_QUA_THI> KET_QUA_THI { get; set; }
        public virtual DbSet<KY_THI> KY_THI { get; set; }
        public virtual DbSet<KY_THI_MON_THI> KY_THI_MON_THI { get; set; }
        public virtual DbSet<LOP> LOPs { get; set; }
        public virtual DbSet<MON_THI> MON_THI { get; set; }
        public virtual DbSet<SINHVIEN> SINHVIENs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
