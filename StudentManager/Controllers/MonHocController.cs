using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StudentManager.Controllers
{
    public class MonHocController : Controller
    {
        // GET: MonHoc
        public ActionResult Index()
        {
            return View();
        }
        ConnectDB.DBcontext dBcontext = new ConnectDB.DBcontext();
        public ActionResult Subjects()
        {
            var groupSubjects = dBcontext.MON_THI.ToList();
            return View(groupSubjects);
        }
        public ActionResult ThongTinMonHoc()
        {
            var groupSubjects = dBcontext.MON_THI.ToList();
            return View(groupSubjects);
        }
        [HttpPost]
        public ActionResult AddSubject(FormCollection form)
        {
            var name = form["TEN_MON_HOC"];
            var tin_chi = Int32.Parse(form["SO_TIN_CHI"]);

            ConnectDB.MON_THI subject = new ConnectDB.MON_THI();
            subject.MON_THI1 = name;
            subject.TIN_CHI = tin_chi;
            var groupSubject = dBcontext.MON_THI.ToList();
            var check = 0;
            foreach (var item in groupSubject)
            {
                if (item.MON_THI1 == name)
                {
                    check++;
                    TempData["mon_hoc"] = "Môn thi đã tồn tại";
                }
            }

            if (check == 0)
            {
                dBcontext.MON_THI.Add(subject);
                dBcontext.SaveChanges();
            }

            return RedirectToAction("Subjects");
        }
        public ActionResult DeleteSubject(int idSubject)
        {
            var subject = dBcontext.MON_THI.Where(x => x.ID == idSubject).FirstOrDefault();
            //dBcontext.MON_THI.Remove(subject);
            subject.IsDelete = true;
            dBcontext.SaveChanges();

            return RedirectToAction("Subjects");
        }
        [HttpPost]
        public ActionResult EditSubject(FormCollection form)
        {
            var name = form["TEN_MON_HOC"];
            var tin_chi = Int32.Parse(form["SO_TIN_CHI"]);
            var idSubject = Int32.Parse(form["ID_MON_HOC"]);

            var subjectToEdit = dBcontext.MON_THI.Where(x => x.ID == idSubject).FirstOrDefault();
            subjectToEdit.MON_THI1 = name;
            subjectToEdit.TIN_CHI = tin_chi;

            var groupNameSubject = dBcontext.MON_THI.Select(x => x.MON_THI1).ToList();

            if (groupNameSubject.Contains(name))
            {
                TempData["mon_trung"] = "Môn học đã bị trùng";
            }
            else
            {
                dBcontext.SaveChanges();
            }


            return RedirectToAction("Subjects");
        }
    }
}