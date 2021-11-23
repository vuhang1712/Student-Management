using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentManager.Models
{
    public class DiemViewModel
    {
        public int ID { get; set; }
        public int ID_SINHVIEN { get; set; }
        public int? ID_MONTHI { get; set; }
        public string NAME { get; set; }
        public List<string> MON_THI1 { get; set; }
        public List<double?> DIEM { get; set; }

        public List<ConnectDB.MON_THI> DANH_SACH_MON_THI { get; set; }

        public DiemViewModel()
        {
            DANH_SACH_MON_THI = new List<ConnectDB.MON_THI>();
        }

    }
}