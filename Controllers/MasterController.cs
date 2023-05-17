using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CityTutor1.App_Code;
using System.Data;

namespace CityTutor1.Controllers
{
    public class MasterController : Controller
    {
        DBMANAGER db= new DBMANAGER();

        void checksession()
            {
                if (Session["master"].ToString() != null)
                {
                    Response.Write(Session["master"].ToString());
                }
                else
                {
                    Response.Redirect("Index");
                }
            }

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult TutionLeads()
        {
            return View();
        }
        public ActionResult RequestSent()
        {
            return View();
        }
        public ActionResult CurrentStatus()
        {
            return View();
        }
        public ActionResult ChangePassword()
        {
            return View();
        }
        public ActionResult LogOut()
        {
            return View();
        }


    }
}
