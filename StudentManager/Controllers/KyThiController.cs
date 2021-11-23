using StudentManager.ConnectDB;
using StudentManager.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using System.Web.WebPages;

namespace StudentManager.Controllers
{
    public class KyThiController : Controller
    {
        // GET: KyThi
        DBcontext dBcontext = new DBcontext();
        List<MonViewModel> monViewModel = new List<MonViewModel>();

        public ActionResult Exam()
        {
            var groupKyThi = (from kythi in dBcontext.KY_THI
                              select new MonViewModel()
                              {
                                  ID = kythi.ID,
                                  TEN_KY_THI = kythi.TEN_KY_THI,
                                  NGAY_BAT_DAU = kythi.NGAY_BAT_DAU,
                                  NGAY_KET_THUC = kythi.NGAY_KET_THUC,
                                  TRANG_THAI = kythi.TRANG_THAI,
                                  IsDelete = kythi.IsDelete,
                              }).ToList();

            var dulieuKyThiMonThi = dBcontext.KY_THI_MON_THI.ToList();
            var dulieuMonThi = dBcontext.MON_THI.ToList();
            foreach (var itemKyThi in groupKyThi)
            {
                var idsMonThi = dulieuKyThiMonThi.Where(x => x.KY_THI == itemKyThi.ID)
                    .Select(x => x.MON_THI).ToList();
                itemKyThi.MON_THI1 = dulieuMonThi.Where(x => idsMonThi.Contains(x.ID))
                    .Select(x => x.MON_THI1).ToList();

                if (itemKyThi.TRANG_THAI != 0)
                {
                    TempData["ThongBao"] = itemKyThi.ID;
                }
            }

            return View(groupKyThi);
        }
        public ActionResult ThongTinKyThi()
        {
            var groupKyThi = (from kythi in dBcontext.KY_THI
                              select new MonViewModel()
                              {
                                  ID = kythi.ID,
                                  TEN_KY_THI = kythi.TEN_KY_THI,
                                  NGAY_BAT_DAU = kythi.NGAY_BAT_DAU,
                                  NGAY_KET_THUC = kythi.NGAY_KET_THUC,
                                  TRANG_THAI = kythi.TRANG_THAI,
                                  IsDelete = kythi.IsDelete,
                              }).ToList();

            var dulieuKyThiMonThi = dBcontext.KY_THI_MON_THI.ToList();
            var dulieuMonThi = dBcontext.MON_THI.ToList();
            foreach (var itemKyThi in groupKyThi)
            {
                var idsMonThi = dulieuKyThiMonThi.Where(x => x.KY_THI == itemKyThi.ID)
                    .Select(x => x.MON_THI).ToList();
                itemKyThi.MON_THI1 = dulieuMonThi.Where(x => idsMonThi.Contains(x.ID))
                    .Select(x => x.MON_THI1).ToList();

                if (itemKyThi.TRANG_THAI != 0)
                {
                    TempData["ThongBao"] = itemKyThi.ID;
                }
            }

            return View(groupKyThi);
        }
        [HttpPost]
        public ActionResult AddExam(FormCollection form)
        {
            var ten = form["TEN_KY_THI"];
            var ngay_bat_dau = form["NGAY_BAT_DAU"];
            var ngay_ket_thuc = form["NGAY_KET_THUC"];
            var trang_thai = form["TRANG_THAI"];
            var mon_thi = form["DATA_MONTHI"];

            var splitNgayBatDau = ngay_bat_dau.Split('/', '-');
            var splitNgayKetThuc = ngay_ket_thuc.Split('/', '-');
            var splitMonThi = mon_thi.Split(',');

            KY_THI exam = new KY_THI();

            exam.TEN_KY_THI = ten;
            if (ngay_bat_dau != "" && splitNgayBatDau.Length == 3)
            {
                exam.NGAY_BAT_DAU = new DateTime(int.Parse(splitNgayBatDau[2]), int.Parse(splitNgayBatDau[1]), int.Parse(splitNgayBatDau[0]));
            }
            if (ngay_ket_thuc != "" && splitNgayKetThuc.Length == 3)
            {
                exam.NGAY_KET_THUC = new DateTime(int.Parse(splitNgayKetThuc[2]), int.Parse(splitNgayKetThuc[1]), int.Parse(splitNgayKetThuc[0]));
            }

            if (trang_thai == "Đã hoàn thành")
            {
                exam.TRANG_THAI = 1;
            }
            else
            {
                exam.TRANG_THAI = 0;
            }
            dBcontext.KY_THI.Add(exam);
            dBcontext.SaveChanges();

            KY_THI_MON_THI ky_thi_mon_thi = new KY_THI_MON_THI();
            foreach (var item in splitMonThi)
            {
                ky_thi_mon_thi.KY_THI = exam.ID;
                ky_thi_mon_thi.MON_THI = Int32.Parse(item);
                dBcontext.KY_THI_MON_THI.Add(ky_thi_mon_thi);
                dBcontext.SaveChanges();

            }
            return RedirectToAction("Exam");
        }
        [HttpPost]
        public ActionResult EditExam(FormCollection form)
        {

            var idKyThi = Int32.Parse(form["ID_KY_THI"]);

            var exam = dBcontext.KY_THI.Where(x => x.ID == idKyThi).FirstOrDefault();
            var ky_thi = dBcontext.KY_THI_MON_THI.Where(x => x.KY_THI == idKyThi).ToList();
            dBcontext.KY_THI_MON_THI.RemoveRange(ky_thi);
            KY_THI_MON_THI ky_thi_mon_thi = new KY_THI_MON_THI();
            ky_thi_mon_thi.KY_THI = idKyThi;

            var ten = form["TEN_KY_THI"];
            var ngay_bat_dau = form["NGAY_BAT_DAU"];
            var ngay_ket_thuc = form["NGAY_KET_THUC"];
            var trang_thai = form["TRANG_THAI"];
            var mon_thi = form["DATA_MONTHI"];

            var splitNgayBatDau = ngay_bat_dau.Split('/', '-');
            var splitNgayKetThuc = ngay_ket_thuc.Split('/', '-');

            var splitIDMonThi = mon_thi.Split(',');
            foreach (var item in splitIDMonThi)
            {
                int itemID = Int32.Parse(item);
                ky_thi_mon_thi.MON_THI = itemID;
                dBcontext.KY_THI_MON_THI.Add(ky_thi_mon_thi);
                dBcontext.SaveChanges();
            }

            exam.TEN_KY_THI = ten;
            if (ngay_bat_dau != "" && splitNgayBatDau.Length == 3)
            {
                exam.NGAY_BAT_DAU = new DateTime(int.Parse(splitNgayBatDau[2]), int.Parse(splitNgayBatDau[1]), int.Parse(splitNgayBatDau[0]));
            }
            if (ngay_ket_thuc != "" && splitNgayKetThuc.Length == 3)
            {
                exam.NGAY_KET_THUC = new DateTime(int.Parse(splitNgayKetThuc[2]), int.Parse(splitNgayKetThuc[1]), int.Parse(splitNgayKetThuc[0]));
            }

            if (trang_thai == "Đã hoàn thành")
            {
                exam.TRANG_THAI = 1;
            }
            else
            {
                exam.TRANG_THAI = 0;
            }

            dBcontext.SaveChanges();
            return RedirectToAction("Exam");
        }

        public ActionResult DeleteExam(int idExam)
        {
            var exam = dBcontext.KY_THI.Where(x => x.ID == idExam).FirstOrDefault();
            exam.IsDelete = true;
            dBcontext.SaveChanges();

            return RedirectToAction("Exam");
        }

        public PartialViewResult EditKyThi(int idKyThi)
        {
            var groupKyThi = dBcontext.KY_THI.ToList();

            foreach (var item in groupKyThi)
            {
                monViewModel.Add(new MonViewModel(item.ID, item.TEN_KY_THI, item.NGAY_BAT_DAU, item.NGAY_KET_THUC, item.TRANG_THAI, item.IsDelete));
            }
            var dulieuKyThiMonThi = dBcontext.KY_THI_MON_THI.ToList();
            var dulieuMonThi = dBcontext.MON_THI.ToList();

            foreach (var itemKyThi in monViewModel)
            {
                var idsMonThi = dulieuKyThiMonThi.Where(x => x.KY_THI == itemKyThi.ID)
                    .Select(x => x.MON_THI).ToList();
                itemKyThi.MON_THI1 = dulieuMonThi.Where(x => idsMonThi.Contains(x.ID))
                    .Select(x => x.MON_THI1).ToList();
                itemKyThi.DANH_SACH_MON_THI = dulieuMonThi.ToList();
            }
            var entityKyThi = monViewModel.Where(x => x.ID == idKyThi).FirstOrDefault();

            return PartialView("_EditKyThi", entityKyThi);
        }
        public PartialViewResult AddKyThi()
        {
            var groupMonTHi = dBcontext.MON_THI.ToList();
            return PartialView("_AddKyThi", groupMonTHi);
        }
        public ActionResult UpdateScore(int idExam)
        {
            var name = dBcontext.KY_THI.Where(x => x.ID == idExam).Select(x => x.TEN_KY_THI).FirstOrDefault();
            ViewBag.TenKyThi = name;

            var monthi = dBcontext.KY_THI_MON_THI.Where(x => x.KY_THI == idExam)
                    .Select(x => x.MON_THI).ToList();
            var tenMonthi = dBcontext.MON_THI.Where(x => monthi.Contains(x.ID))
                    .Select(x => x.MON_THI1).ToList();
            var danhSachMonThi = dBcontext.MON_THI.Where(x => monthi.Contains(x.ID)).ToList();

            var diemThi = (from sinhvien in dBcontext.SINHVIENs
                           select new DiemViewModel()
                           {
                               ID = idExam,
                               ID_SINHVIEN = sinhvien.ID,
                               NAME = sinhvien.NAME,
                               MON_THI1 = tenMonthi,
                           }).ToList();

            Random random = new Random();

            foreach (var item in diemThi)
            {
                List<double?> diem = new List<double?>();

                for (int i = 0; i < item.MON_THI1.Count; i++)
                {
                    diem.Add(random.Next(0, 10));
                }

                item.DIEM = diem;

            }

            return View(diemThi);
        }
    }
}