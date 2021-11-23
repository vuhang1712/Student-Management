using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentManager.Models
{
    public class MonViewModel
    {
        public int ID { get; set; }

        public string TEN_KY_THI { get; set; }

        public DateTime? NGAY_BAT_DAU { get; set; }

        public DateTime? NGAY_KET_THUC { get; set; }

        public int? TRANG_THAI { get; set; }
        public bool IsDelete { get; set; }

        public List<string> MON_THI1 { get; set; }
        public List<ConnectDB.MON_THI> DANH_SACH_MON_THI { get; set; }
        public MonViewModel()
        {
            MON_THI1 = new List<string>();
            DANH_SACH_MON_THI = new List<ConnectDB.MON_THI>();
        }
        public MonViewModel(int id, string name, DateTime? start, DateTime? finish, int? status, bool isDelete)
        {
            ID = id;
            TEN_KY_THI = name;
            NGAY_BAT_DAU = start;
            NGAY_KET_THUC = finish;
            TRANG_THAI = status;
            IsDelete = isDelete;
        }

    }

}