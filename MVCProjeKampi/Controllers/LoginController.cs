﻿using BusinessLayer.Concrete;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MVCProjeKampi.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        AdminManager am = new AdminManager(new EFAdminDal());
        WriterLoginManager wm = new WriterLoginManager(new EFWriterDal());
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(Admin p)
        {
            //Context c=new Context();
            //var admimuser = c.admins.FirstOrDefault(x=>x.AdminUserName==p.AdminUserName && x.AdminPassword==p.AdminPassword);
            var admimuserinfo = am.GetAdmin(p.AdminUserName, p.AdminPassword);
            if (admimuserinfo != null)
            {
                FormsAuthentication.SetAuthCookie(admimuserinfo.AdminUserName,false);
                Session["AdminUserName"] = admimuserinfo.AdminUserName;
                return RedirectToAction("Index", "AdminCategory");
            }
            else
            {
                return RedirectToAction("Index");
            }
            
        }
        [HttpGet]
        public ActionResult WriterLogin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult WriterLogin(Writer p)
        {
            //Context c = new Context();
            //var writeruser = c.Writers.FirstOrDefault(x => x.WriterMail == p.WriterMail && x.WriterPassword == p.WriterPassword);
            var writeruserinfo = wm.GetWriter(p.WriterMail,p.WriterPassword);
            if (writeruserinfo != null)
            {
                FormsAuthentication.SetAuthCookie(writeruserinfo.WriterMail, false);
                Session["WriterMail"] = writeruserinfo.WriterMail;
                return RedirectToAction("MyContent", "WriterPanelContent");
            }
            else
            {
                return RedirectToAction("WriterLogin");
            }
        }
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Headings","Default");
        }
    }
}