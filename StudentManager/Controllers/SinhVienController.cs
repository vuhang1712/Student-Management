using System;
using System.Linq;
using System.Web.Mvc;
using FormCollection = System.Web.Mvc.FormCollection;

namespace StudentManager.Controllers
{
    public class SinhVienController : Controller
    {
        // GET: SinhVien

        ConnectDB.DBcontext dataContext = new ConnectDB.DBcontext();

        public ActionResult Index()
        {
            var groupStudents = (from u in dataContext.SINHVIENs select u).ToList();

            return View(groupStudents);
        }
        public ActionResult ThongTinSinhVien()
        {
            ConnectDB.DBcontext dBcontext = new ConnectDB.DBcontext();

            var groupStudents = dBcontext.SINHVIENs.ToList();

            return View(groupStudents);
        }
        public ViewResult CreateStudent()
        {
            return View();
        }

        public ViewResult EditStudent(int idStudent)
        {
            var studentToEdit = dataContext.SINHVIENs.Where(x => x.ID == idStudent).FirstOrDefault();
            return View(studentToEdit);
        }

        public ViewResult Search(FormCollection form)
        {
            var name = form["QR_NAME"];
            var dateBegin = form["QR_DateBegin"];
            var dateEnd = form["QR_DateEnd"];
            var email = form["QR_EMAIL"];

            var groupStudents = dataContext.SINHVIENs.ToList();
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

        public ViewResult DetailStudent(int idStudent)
        {
            var studentToView = dataContext.SINHVIENs.Where(x => x.ID == idStudent).FirstOrDefault();
            return View(studentToView);
        }

        public ActionResult Delete(int id)
        {
            //tìm ra sinh viên
            ConnectDB.SINHVIEN sinhvien = dataContext.SINHVIENs.Where(x => x.ID == id).SingleOrDefault();

            //thực hiện xóa bằng datacontext
            dataContext.SINHVIENs.Remove(sinhvien);
            dataContext.SaveChanges();

            //lấy lại danh sách s/v sau khi xóa
            var groupStudents = (from u in dataContext.SINHVIENs select u).ToList();
            return View("Index", groupStudents);
        }

        [HttpPost]
        public ActionResult AddStudent(FormCollection collect)
        {
            //dd/mm/yyyyy
            var strNgaySinh = collect["NGAYSINH"];

            //check khác null
            var splitNgaySinh = strNgaySinh.Split('/', '-');

            ConnectDB.SINHVIEN sinhvien = new ConnectDB.SINHVIEN();

            sinhvien.NAME = collect["NAME"];

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
            //lấy lại danh sách s/v sau khi thêm
            var groupStudents = (from u in dataContext.SINHVIENs select u).ToList();
            return View("Index", groupStudents);
        }

        [HttpPost]
        public ActionResult UpdateStudent(FormCollection collect)
        {
            int id = Int32.Parse(collect["ID"]);
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

            //lấy lại danh sách s/v sau khi sửa
            var groupStudents = (from u in dataContext.SINHVIENs select u).ToList();
            return View("Index", groupStudents);
        }
    }
}