using StudentManager.ConnectDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentManager.Models
{
    public class DanhSachMon
    {

        public List<MonViewModel> GroupDataMonThi { get; set; }

        public DanhSachMon()
        {
            GroupDataMonThi = new List<MonViewModel>();
        }

    }

}