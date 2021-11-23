using StudentManager.ConnectDB;
using StudentManager.DTOs;
using StudentManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace StudentManager.Controllers
{
    public class LopController : Controller
    {
        // GET: Lop
        private DBcontext dataContext = new DBcontext();

        public ActionResult ClassStudent()
        {

            var groupStudent = dataContext.SINHVIENs.ToList();
            var groupClasses = (from u in dataContext.LOPs
                                select new LopDTO()
                                {
                                    MaLop = u.MA_LOP,
                                    TenLop = u.TEN_LOP,
                                }).ToList();
            foreach (var item in groupClasses)
            {
                item.SiSo = groupStudent.Where(x => x.ID_CLASS == item.MaLop).Count();
            }
            return View(groupClasses);
        }
        public ActionResult ThongTinLop()
        {

            var groupStudent = dataContext.SINHVIENs.ToList();
            var groupClasses = (from u in dataContext.LOPs
                                select new LopDTO()
                                {
                                    MaLop = u.MA_LOP,
                                    TenLop = u.TEN_LOP,
                                }).ToList();
            foreach (var item in groupClasses)
            {
                item.SiSo = groupStudent.Where(x => x.ID_CLASS == item.MaLop).Count();
            }
            return View(groupClasses);
        }
        public ActionResult AddClass(FormCollection form)
        {
            var check = 0;
            var nameClass = form["TEN_LOP"];
            var groupStudent = dataContext.SINHVIENs.ToList();
            var groupClasses = (from u in dataContext.LOPs
                                select new LopDTO()
                                {
                                    MaLop = u.MA_LOP,
                                    TenLop = u.TEN_LOP,
                                }).ToList();

            foreach (var item in groupClasses)
            {
                item.SiSo = groupStudent.Where(x => x.ID_CLASS == item.MaLop).Count();
                if ((nameClass == item.TenLop) || (nameClass == null))
                {
                    check++;
                    TempData["Message"] = "Tên lớp đã tồn tại";
                }
            }
            if (check == 0)
            {
                ConnectDB.LOP newClass = new ConnectDB.LOP();
                newClass.TEN_LOP = nameClass;
                dataContext.LOPs.Add(newClass);
                dataContext.SaveChanges();
            }
            return RedirectToAction("ClassStudent", "Lop", groupClasses);
        }

        public ViewResult ListStudent(int idClass)
        {

            ViewBag.IdClass = idClass;
            var nameClass = dataContext.LOPs.Where(u => u.MA_LOP == idClass).Select(u => u.TEN_LOP).FirstOrDefault();
            ViewBag.NameClass = nameClass;
            Models.DanhSachLop danhSachLop = new Models.DanhSachLop();
            danhSachLop.GroupDataHocSinh.AddRange(dataContext.SINHVIENs.Where(x => x.ID_CLASS == idClass).ToList());
            danhSachLop.GroupDataLop.AddRange(dataContext.LOPs.Where(x => x.MA_LOP != idClass).ToList());
            return View(danhSachLop);
        }

        public ViewResult AddStudent(int idClass)
        {
            var entityClass = dataContext.LOPs.Where(x => x.MA_LOP == idClass).FirstOrDefault();
            return View(entityClass);
        }

        public ActionResult DeleteClass(int idClass)
        {
            ViewBag.IdClass = idClass;
            ConnectDB.LOP classToDelete = dataContext.LOPs.Where(x => x.MA_LOP == idClass).SingleOrDefault();

            dataContext.LOPs.Remove(classToDelete);
            dataContext.SaveChanges();

            var groupStudent = dataContext.SINHVIENs.ToList();
            var groupClasses = (from u in dataContext.LOPs
                                select new LopDTO()
                                {
                                    MaLop = u.MA_LOP,
                                    TenLop = u.TEN_LOP,
                                }).ToList();
            foreach (var item in groupClasses)
            {
                item.SiSo = groupStudent.Where(x => x.ID_CLASS == item.MaLop).Count();
            }
            return View("ClassStudent", groupClasses);
        }

        public ViewResult EditClass(int idCLass)
        {
            var nameClass = dataContext.LOPs.Where(u => u.MA_LOP == idCLass).Select(u => u.TEN_LOP).FirstOrDefault();
            ViewBag.NameClass = nameClass;
            ViewBag.IdClass = idCLass;

            //var groupStudent = dataContext.SINHVIENs.ToList();
            //var findClass = dataContext.LOPs.Where(x => x.MA_LOP == idCLass).FirstOrDefault();
            //var classToEdit = new LopDTO()
            //{
            //    MaLop = findClass.MA_LOP,
            //    TenLop = findClass.TEN_LOP,
            //    SiSo = groupStudent.Where(x => x.ID_CLASS == idCLass).Count(),

            //};
            var groupStudent = dataContext.SINHVIENs.ToList();
            var groupClasses = (from u in dataContext.LOPs
                                select new LopDTO()
                                {
                                    MaLop = u.MA_LOP,
                                    TenLop = u.TEN_LOP,
                                }).ToList();
            foreach (var item in groupClasses)
            {
                item.SiSo = groupStudent.Where(x => x.ID_CLASS == item.MaLop).Count();
            }
            var classToEdit = groupClasses.Where(x => x.MaLop == idCLass).FirstOrDefault();
            return View(classToEdit);
        }

        [HttpPost]
        public ActionResult UpdateClass(FormCollection collect)
        {
            int idClass = Int32.Parse(collect["MA_LOP"]);
            LOP classToEdit = dataContext.LOPs.Where(x => x.MA_LOP == idClass).FirstOrDefault();
            string name = classToEdit.TEN_LOP;
            classToEdit.TEN_LOP = collect["TEN_LOP"];

            var groupNameClasses = (from u in dataContext.LOPs select u.TEN_LOP).ToList();

            if (groupNameClasses.Contains(classToEdit.TEN_LOP) && (classToEdit.TEN_LOP != name))
            {
                TempData["Message"] = "Tên lớp đã tồn tại";
            }
            else
            {
                dataContext.SaveChanges();
            }

            return RedirectToAction("ClassStudent");
        }

        [HttpPost]
        public ActionResult CreateStudent(FormCollection collect)
        {
            int idClass = Int32.Parse(collect["MA_LOP"]);
            ViewBag.IdClass = idClass;
            //dd/mm/yyyyy
            var strNgaySinh = collect["NGAYSINH"];

            //check khác null
            var splitNgaySinh = strNgaySinh.Split('/', '-');

            ConnectDB.SINHVIEN sinhvien = new ConnectDB.SINHVIEN();

            sinhvien.NAME = collect["NAME"];
            sinhvien.ID_CLASS = idClass;
            sinhvien.Address = collect["DIACHI"];
            sinhvien.Email = collect["EMAIL"];
            sinhvien.Date = DateTime.Now;

            if (int.Parse(splitNgaySinh[0]) > 2020 || (int.Parse(splitNgaySinh[1]) > 12 && int.Parse(splitNgaySinh[1]) < 1) || (int.Parse(splitNgaySinh[2]) > 31 && int.Parse(splitNgaySinh[2]) < 1))
            {
                //MessageBox.Show("Ngày sinh không tồn tại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                sinhvien.DateOfBirth = new DateTime(int.Parse(splitNgaySinh[0]), int.Parse(splitNgaySinh[1]), int.Parse(splitNgaySinh[2]));
                dataContext.SINHVIENs.Add(sinhvien);
                dataContext.SaveChanges();
            }

            var groupStudents = dataContext.SINHVIENs.Where(x => x.ID_CLASS == idClass).ToList();
            var dsLop = new DanhSachLop()
            {
                GroupDataHocSinh = groupStudents
            };
            return View("ListStudent", dsLop);
        }

        public ActionResult Delete(int idClass, int idStudent)
        {
            ViewBag.IdClass = idClass;
            ConnectDB.SINHVIEN sinhvien = dataContext.SINHVIENs.Where(x => x.ID == idStudent).SingleOrDefault();

            dataContext.SINHVIENs.Remove(sinhvien);
            dataContext.SaveChanges();

            var groupStudent = new DanhSachLop()
            {
                GroupDataHocSinh = dataContext.SINHVIENs.Where(x => x.ID_CLASS == idClass).ToList()
            };
            return View("ListStudent", groupStudent);
        }

        public ViewResult EditStudent(int idCLass, int idStudent)
        {
            ViewBag.IdClass = idCLass;
            var studentToEdit = dataContext.SINHVIENs.Where(x => x.ID == idStudent).FirstOrDefault();
            return View(studentToEdit);
        }

        [HttpPost]
        public ActionResult UpdateStudent(FormCollection collect)
        {
            int id = Int32.Parse(collect["ID"]);
            int idClass = Int32.Parse(collect["ID_CLASS"]);
            ViewBag.IdClass = idClass;
            ConnectDB.SINHVIEN sinhvien = dataContext.SINHVIENs.Where(x => x.ID == id).SingleOrDefault();

            var strNgaySinh = collect["NGAYSINH"];
            //check khác null
            var splitNgaySinh = strNgaySinh.Split('/');

            if (collect["NAME"] != null)
            {
                sinhvien.NAME = collect["NAME"];
            }
            else
            {
                //MessageBox.Show("Sinh viên không tồn tại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            sinhvien.Address = collect["DIACHI"];
            sinhvien.Email = collect["EMAIL"];
            sinhvien.Date = DateTime.Now;

            if (int.Parse(splitNgaySinh[2]) > 2020 || (int.Parse(splitNgaySinh[1]) > 12 && int.Parse(splitNgaySinh[1]) < 1) || (int.Parse(splitNgaySinh[0]) > 31 && int.Parse(splitNgaySinh[0]) < 1))
            {
                //MessageBox.Show("Ngày sinh không tồn tại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                sinhvien.DateOfBirth = new DateTime(int.Parse(splitNgaySinh[2]), int.Parse(splitNgaySinh[1]), int.Parse(splitNgaySinh[0]));

                dataContext.SaveChanges();
            }

            var nameClass = dataContext.LOPs.Where(u => u.MA_LOP == idClass).Select(u => u.TEN_LOP).FirstOrDefault();
            ViewBag.NameClass = nameClass;
            var groupStudent = new DanhSachLop()
            {
                GroupDataHocSinh = dataContext.SINHVIENs.Where(x => x.ID_CLASS == idClass).ToList()
            };
            return View("ListStudent", groupStudent);
        }

        public ViewResult SearchStudent(int idClass, FormCollection form)
        {
            var name = form["QR_NAME"];
            var dateBegin = form["QR_DateBegin"];
            var dateEnd = form["QR_DateEnd"];
            var email = form["QR_EMAIL"];
            var groupStudents = dataContext.SINHVIENs.Where(x => x.ID_CLASS == idClass).ToList();
            ViewBag.IdClass = idClass;
            if (!string.IsNullOrEmpty(name))
            {
                name = name.ToLower();
                groupStudents = groupStudents.Where(x => x.NAME.ToLower().Contains(name)).ToList();
            }

            if (!string.IsNullOrEmpty(dateBegin) || !string.IsNullOrEmpty(dateEnd))
            {
                DateTime? start = null;
                DateTime? finish = null;
                try
                {
                    var splitDateBegin = dateBegin.Split('/', '-');
                    start = new DateTime(int.Parse(splitDateBegin[2]), int.Parse(splitDateBegin[1]), int.Parse(splitDateBegin[0]));
                }
                catch (Exception ex)
                {
                    string err = ex.Message;
                }

                try
                {
                    var splitDateEnd = dateEnd.Split('/', '-');
                    finish = new DateTime(int.Parse(splitDateEnd[2]), int.Parse(splitDateEnd[1]), int.Parse(splitDateEnd[0]));
                }
                catch (Exception ex)
                {
                    string err = ex.Message;
                }

                if (start == null && finish != null)
                {
                    groupStudents = groupStudents.Where(x => x.DateOfBirth < finish.Value).ToList();
                }

                if (start != null && finish == null)
                {
                    groupStudents = groupStudents.Where(x => x.DateOfBirth > start.Value).ToList();
                }

                if (start != null && finish != null)
                {
                    groupStudents = groupStudents.Where(x => (x.DateOfBirth > start.Value) && (x.DateOfBirth < finish.Value)).ToList();
                }
                ViewBag.SearchDateBeginValue = String.Format("{0:dd/MM/yyyy}", start);
                ViewBag.SearchDateEndValue = String.Format("{0:dd/MM/yyyy}", finish);
            }

            if (!string.IsNullOrEmpty(email))
            {
                email = email.ToLower();
                groupStudents = groupStudents.Where(x => x.Email.ToLower().Contains(email)).ToList();
            }

            ViewBag.SearchNameValue = name;
            ViewBag.SearchEmailValue = email;

            return View(groupStudents);
        }

        public ViewResult DetailStudent(int idClass, int idStudent)
        {
            ViewBag.IdClass = idClass;
            var studentToView = dataContext.SINHVIENs.Where(x => x.ID == idStudent).FirstOrDefault();
            return View(studentToView);
        }
        [HttpPost]
        public ActionResult ChuyenLop(FormCollection form)
        {
            var idStudent = int.Parse(form["ID_SINHVIEN_CAN_CHUYENLOP"]);
            string nameClass = form["NAME_CLASS"];
            var idClassStudent = dataContext.LOPs.Where(u => u.TEN_LOP == nameClass).Select(u => u.MA_LOP).FirstOrDefault();

            ConnectDB.SINHVIEN student = dataContext.SINHVIENs.Where(x => x.ID == idStudent).FirstOrDefault();
            student.ID_CLASS = idClassStudent;
            dataContext.SaveChanges();

            return RedirectToAction("ClassStudent");
        }
    }
}