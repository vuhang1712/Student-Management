using StudentManager.ConnectDB;
using System.Linq;
using System.Threading;
using System.Web.Mvc;
using FormCollection = System.Web.Mvc.FormCollection;

namespace StudentManager.Controllers
{
    public class HomeController : Controller
    {
        private DBcontext dataContext = new DBcontext();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ViewResult LogIn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LogIn(FormCollection form)
        {
            var userName = form["TEN"];
            var password = form["MATKHAU"];

            var user = dataContext.DANG_NHAP.Where(x => x.MAT_KHAU == password && x.TEN_DANG_NHAP == userName)
                .FirstOrDefault();
            if (user == null)
            {
                ViewBag.Message = "Tên đăng nhập hoặc mật khẩu không đúng";
                return View("LogIn");
            }

            Session["name"] = user.HO_TEN.ToString();
            Session.Timeout = 30;

            return RedirectToAction("Index", "Home");
        }
        public ViewResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignIn(FormCollection form)
        {
            var userName = form["TEN_DANG_NHAP"];
            var password = form["MAT_KHAU"];
            var fullName = form["HO_TEN"];

            DANG_NHAP account = new DANG_NHAP();
            account.TEN_DANG_NHAP = userName;
            account.MAT_KHAU = password;
            account.HO_TEN = fullName;

            var groupAccounts = dataContext.DANG_NHAP.ToList();

            //if (groupAccounts.Contains(account))
            //{
            //    TempData["message"] = "Tài khoản đã tồn tại";
            //}
            int check = 0;
            foreach (var item in groupAccounts)
            {
                if (fullName == item.HO_TEN && userName == item.TEN_DANG_NHAP && password == item.MAT_KHAU)
                {
                    check++;
                }
            }
            if (check == 0)
            {
                dataContext.DANG_NHAP.Add(account);
                dataContext.SaveChanges();
            }
            Session["name"] = account.HO_TEN.ToString();
            Session.Timeout = 30;
            if (check != 0)
            {
                TempData["notification"] = "Tài khoản đã tồn tại";
                return RedirectToAction("Register");
            }
            else
            {
                return RedirectToAction("LogIn");
            }
        }

    }
}