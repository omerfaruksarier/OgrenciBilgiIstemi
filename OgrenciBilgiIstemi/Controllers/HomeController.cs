using OgrenciBilgiIstemi.App_Start;
using OgrenciBilgiIstemi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace OgrenciBilgiIstemi.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
    StudentInfoEntities1 db = new StudentInfoEntities1();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Student()
        {
            var student = db.Table_OGRENCI.Where(x => x.SILINDI != true && x.Table_SINIF.SILINDI != true && x.Table_OKUL.SILINDI != true ).OrderByDescending(x =>x.ID).ToList();

            return View(student);
        }

        public ActionResult Studentdelete(int id)
        {
            var ogrenci = db.Table_OGRENCI.Find(id);
            ogrenci.SILINDI = true;
            db.SaveChanges();
            return RedirectToAction("Student");
        }

        public ActionResult Studentinfo(int id)
        {
            var ogrenci = db.Table_OGRENCI.Find(id);

            var deger = db.Table_OKUL.Where(x=>x.SILINDI != true).ToList();
            ViewData["getokul"] = deger;


            var deger2 = db.Table_SINIF.Where(x => x.SILINDI != true).ToList();
            ViewData["getsınıf"] = deger2;

            return View("Studentinfo", ogrenci);
        }
        [HttpPost]
        public ActionResult Studentupdate(Table_OGRENCI p)
        {
            var ogrenci = db.Table_OGRENCI.Find(p.ID);
            ogrenci.AD = p.AD;
            ogrenci.SOYAD = p.SOYAD;
            ogrenci.NUMARA = p.NUMARA;
            ogrenci.OKUL = p.OKUL;
            ogrenci.TC = p.TC;
            ogrenci.SINIFI = p.SINIFI;
            db.SaveChanges();
            return RedirectToAction("Student");
        }

        public ActionResult Studentadd()
        {
            var deger = db.Table_OKUL.Where(x => x.SILINDI != true).ToList();
            ViewData["getokul"] = deger;


            var deger2 = db.Table_SINIF.Where(x => x.SILINDI != true).ToList();
            ViewData["getsınıf"] = deger2;


            return View();
        }
        
        [HttpPost]
        public ActionResult Studentadd(Table_OGRENCI p)
        {
            db.Table_OGRENCI.Add(p);
            db.SaveChanges();
            return RedirectToAction("Student");
        }

        public async Task<PartialViewResult> StudentSearch(string searchKey)
        {

            studentSearch viewModel = null;
            var tasks = new Task[1];
            int i = 0;
            viewModel = new studentSearch();
            viewModel.SearchKey = searchKey;
            List<Task> TaskList = GetSearchResultSt(searchKey, viewModel);
            foreach (Task tsk in TaskList)
            {
                tasks[i] = tsk;
                i++;
            }
            await Task.WhenAll(tasks);

            return PartialView("~/Views/Partials/StudentSearchView.cshtml", viewModel);

        }

        private List<Task> GetSearchResultSt(string search, studentSearch model)
        {
            List<Task> Tasks = new List<Task>();
            var taskStudent = Task.Factory.StartNew(() =>
            {
                using (StudentInfoEntities1 db = new StudentInfoEntities1())
                {
                    model.OgrenciList = db.Table_OGRENCI.Where(cus => cus.AD.Contains(search) || cus.SOYAD.Contains(search) || cus.NUMARA.Contains(search) || cus.TC.Contains(search)).Take(5).ToList();
                }
            });

            Tasks.Add(taskStudent);
            return Tasks;

        }

        public ActionResult Classes()
        {
            var deger = db.Table_SINIF.Where(x => x.SILINDI != true).OrderByDescending(x => x.ID).ToList();
            return View(deger);
        }
        public ActionResult Classdelete(int id)
        {
            var sınıf = db.Table_SINIF.Find(id);
            sınıf.SILINDI = true;
            db.SaveChanges();
            return RedirectToAction("Classes");
        }

        public ActionResult Addclass()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Addclass(Table_SINIF p)
        {
            var deger = db.Table_SINIF.Where(x => x.SINIF == p.SINIF).Count();
            if(deger == 0)
            {
                db.Table_SINIF.Add(p);
                db.SaveChanges();
                return RedirectToAction("Classes");
            }
            else
            {
                ViewBag.hata = "Hata";
                return View();
            }
        }

        public ActionResult Schools()
        {
            var deger = db.Table_OKUL.Where(x => x.SILINDI != true).OrderByDescending(x => x.ID).ToList();
            return View(deger);
        }

        public ActionResult Schooldelete(int id)
        {
            var oKUL = db.Table_OKUL.Find(id);
            oKUL.SILINDI = true;
            db.SaveChanges();
            return RedirectToAction("Schools");
        }
        [HttpPost]
        public ActionResult Addschool(Table_OKUL p)
        {
            var deger = db.Table_OKUL.Where(x => x.OKULADI == p.OKULADI).Count();
            if (deger == 0)
            {
                db.Table_OKUL.Add(p);
                db.SaveChanges();
                return RedirectToAction("Schools");
            }
            else
            {
                ViewBag.hata = "Hata";
                return View();
            }
        }
        public ActionResult Addschool()
        {
            return View();
        }
        public ActionResult Users()
        {
            var user = db.Table_KULLANICI.Where(x => x.SILINDI != true).OrderByDescending(x => x.ID).ToList();

            return View(user);
        }


        public ActionResult Userdelete(int id)
        {
            var user = db.Table_KULLANICI.Find(id);
            user.SILINDI = true;
            db.SaveChanges();
            return RedirectToAction("Users");
        }

        [HttpPost]
        public ActionResult Adduser(Table_KULLANICI p)
        {
            var deger = db.Table_KULLANICI.Where(x => x.KULLANICIADI == p.KULLANICIADI).Count();
            if (deger == 0)
            {
                Table_KULLANICI yeni = new Table_KULLANICI();
                yeni.ID = p.ID;
                yeni.KULLANICIADI = p.KULLANICIADI;
                yeni.SIFRE = p.SIFRE;
                yeni.SIFRE = Crypto.Hash(yeni.SIFRE, "MD5");
                db.Table_KULLANICI.Add(yeni);
                db.SaveChanges();
                return RedirectToAction("Users");
            }
            else
            {
                ViewBag.hata = "Hata";
                return View();
            }
        }
        public ActionResult Adduser()
        {
            return View();
        }

    }
}