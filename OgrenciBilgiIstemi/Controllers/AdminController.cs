using OgrenciBilgiIstemi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Security;

namespace OgrenciBilgiIstemi.Controllers
{
    public class AdminController : Controller
    {
        StudentInfoEntities1 db = new StudentInfoEntities1();

        // GET: Admin
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Table_KULLANICI p, string SIFRE)
        {
            if (ModelState.IsValid)
            {
                var md5password = Crypto.Hash(SIFRE, "MD5");
                var log = db.Table_KULLANICI.FirstOrDefault(x=>x.KULLANICIADI == p.KULLANICIADI && x.SIFRE == md5password);
                if(log != null)
                {
                    FormsAuthentication.SetAuthCookie(log.KULLANICIADI, false);
                    Session["username"] = log.KULLANICIADI.ToString();
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.hata = "kullanıcı adı veya parola hatalı";
                    return View();
                }
            }
            return View();
        }

        public ActionResult LogOff()
        {
            Session.Clear();
            Session.Abandon();
            Session.RemoveAll();
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Admin");

        }


    }
}