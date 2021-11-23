using StudentManager.ConnectDB;
using System.Collections.Generic;

namespace StudentManager.Models
{
    public class DanhSachLop
    {
        public List<ConnectDB.SINHVIEN> GroupDataHocSinh { get; set; }
        public List<LOP> GroupDataLop { get; set; }

        public DanhSachLop()
        {
            GroupDataHocSinh = new List<ConnectDB.SINHVIEN>();
            GroupDataLop = new List<LOP>();
        }
    }
}