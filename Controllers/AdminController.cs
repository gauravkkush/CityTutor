using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CityTutor1.App_Code;
using System.Data;

namespace CityTutor1.Controllers
{
    public class AdminController : Controller
    {
        DBMANAGER db = new DBMANAGER();
        //
        // GET: /Admin/
        public ActionResult login(string username, string password)
        {
            db.cmdtxt = "select * from tbllogin where username='" + username + "' and pass='" + password + "'";
            DataTable dt = db.GetBulkData();
            if (dt.Rows.Count > 0)
            {
                Session["admin"] = username;
                Response.Redirect("../Admin/Index");
            }
            else
            {
                Response.Write("<script>alert('Your Username or Password is Incorrect!')</script>");
            }
            return View();
        }
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Notification()
        {
            db.cmdtxt = "select * from tblnews";
            DataTable dt = db.GetBulkData();
            if (dt.Rows.Count > 0)
            {
                string str = "<tr>";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    str = str + "<tr><td>" + (i+1) + "</td><td>" + dt.Rows[i][1] + "</td><td>" + dt.Rows[i][2] + "</td><td><a href='notiDel?msg=" + dt.Rows[i][0] + "' style='color:red;'><span class='fa fa-trash'></span></a></td></tr>";
                }
                ViewBag.news = str;
            }

            return View();
        }
        [HttpPost]
        public ActionResult Notification(string news)
        {
            db.cmdtxt = "insert into tblnews(news,ndate)values('" + news + "','" + DateTime.Now + "')";
            bool b = db.ExecuteInsertUpdateDelete();
            if (b == true)
                Response.Write("<script>alert('Notification Addition Done.')</script>");
            else
                Response.Write("<script>alert('Notification Addition Unsuccessful !...Try Again.')</script>");
            return View();
        }
        public ActionResult Gallerymgmt()
        {
            db.cmdtxt = "select * from tblgallery";
            DataTable dt = db.GetBulkData();
            if (dt.Rows.Count > 0)
            { 
                string str="";
                for (int i = 0; i < dt.Rows.Count; i++)
                 {
                    str += "<tr><td>" + (i + 1) + "</td><td>" + dt.Rows[i][1] + "</td><td><img src='../Content/Gallery/" + dt.Rows[i][2] + "' height='100px' width='100px'/></td><td><a href='Gdel?msg=" + dt.Rows[i][0] + "' style='color:red;'><span class='fa fa-trash'></span></a></td></tr>";
                }
                ViewBag.image = str;
            }

            return View();
        }
        [HttpPost]
        public ActionResult Gallerymgmt(string name, HttpPostedFileBase image)
        {
            String filename = System.IO.Path.GetFileName(image.FileName);
            String path = System.IO.Path.Combine(Server.MapPath("/Content/Gallery"), filename);
            image.SaveAs(path);
            db.cmdtxt = "insert into tblgallery(name,image)values('" + name + "','" + filename + "')";
                Boolean b = db.ExecuteInsertUpdateDelete();
            if (b == true)
            {
                Response.Write("<script>alert('Image inserted successfully.')</script>");
            }
            else
                Response.Write("<script>alert('Image Insertion Failed!')</script>");
            return View();
        }
        public ActionResult  MasterMgmt()
        {
            db.cmdtxt = "select * from tblregistration";
            DataTable dt = db.GetBulkData();
            if (dt.Rows.Count > 0)
            {
                string str = "";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    str += "<tr><td>" + dt.Rows[i][1] + "</td><td>" + dt.Rows[i][2] + "</td><td>" + dt.Rows[i][0] + "</td><td>" + dt.Rows[i][3] + "</td><td>" + dt.Rows[i][4] + "</td><td>" + dt.Rows[i][5] + "</td><td>" + dt.Rows[i][6] + "</td><td>" + dt.Rows[i][7] + "</td><td>" + dt.Rows[i][8] + "</td><td>" + dt.Rows[i][9] + "</td><td>" + dt.Rows[i][10] + "</td><td>" + dt.Rows[i][11] + "</td><td>" + dt.Rows[i][12] + "</td><td>" + dt.Rows[i][13] + "</td><td>" + dt.Rows[i][14] + "</td><td><img src='../Content/UserPic/" + dt.Rows[i][15] + "' height='80px' width='80px'/></td><td>" + dt.Rows[i][16] + "</td><td>" + dt.Rows[i][17] + "</td><td><a href='Masterdel?msg=" + dt.Rows[i][2] + "' style='color:red;'><span class='fa fa-trash'></span></a></td><td><a href='profile?msg=" + dt.Rows[i][2] + "' style='color:blue;'><span class='fa fa-eye'></span></a></td></tr>";
                }
                ViewBag.msg = str;
            }
            return View();
        }
        [HttpGet]
        public ActionResult LeadsMgmt()
        {
            db.cmdtxt = "select * from tblleads";
            DataTable dt = db.GetBulkData();
            if (dt.Rows.Count > 0)
            {
                string str = "<tr>";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    str += "<tr><td>" + (i+1) + "</td><td>" + dt.Rows[i][1] + "</td><td>" + dt.Rows[i][2] + "</td><td>" + dt.Rows[i][3] + "</td><td>" + dt.Rows[i][4] + "</td><td>" + dt.Rows[i][5] + "</td><td>" + dt.Rows[i][6] + "</td><td>" + dt.Rows[i][7] + "</td><td>" + dt.Rows[i][8] + "</td><td>" + dt.Rows[i][9] + "</td><td><a href='LEADSdel?msg=" + dt.Rows[i][0] + "' style='color:red;'><span class='fa fa-trash'></span></a></td></tr>";
                }
                ViewBag.msg = str;
            }
            return View();
        }
        public ActionResult RequestMgmt()
        {
            return View();
        }
        public ActionResult Password()
        {
            return View();
        }
        [HttpGet]
        public ActionResult contactusmgmt()
        {
            DBMANAGER db = new DBMANAGER();
            db.cmdtxt = "select * from tblenquiry";
            DataTable dt = db.GetBulkData();
            if (dt.Rows.Count > 0)
            {
                string str = "<tr>";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    str = str + "<tr><td>" + (i+1) + "</td><td>" + dt.Rows[i][1] + "</td><td>" + dt.Rows[i][2] + "</td><td>" + dt.Rows[i][3] + "</td><td>" + dt.Rows[i][4] + "</td><td>" + dt.Rows[i][5] + "</td><td><a href='Cdel?msg="+dt.Rows[i][0]+"' style='color:red;'><span class='fa fa-trash'></span></a></td></tr>";
                }
                ViewBag.msg = str;
            }
            return View();
        }
        public ActionResult LogOut()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Cdel()
        {
            db.cmdtxt = "delete from tblenquiry where eid='" + Request.QueryString["msg"].ToString() + "'";
            bool b = db.ExecuteInsertUpdateDelete();
            if (b == true)
                Response.Write("<script>alert('RECORD Deleted Successfuly');window.location.href='contactusmgmt'</script>");
            else
                Response.Write("<script>alert('Deletion Unsuccessfull');window.location.href='contactusmgmt'</script>");

            return View();
        }
        [HttpGet]
        public ActionResult notiDel()
        {
            db.cmdtxt = "delete from tblnews where id='" + Request.QueryString["msg"].ToString() + "'";
            bool b = db.ExecuteInsertUpdateDelete();
            if (b == true)
                Response.Write("<script>alert('RECORD Deleted Successfuly');window.location.href='Notification'</script>");
            else
                Response.Write("<script>alert('Deletion Unsuccessfull');window.location.href='Notification'</script>");

            return View();
        }
        [HttpGet]
        public ActionResult Masterdel()
        {
            db.cmdtxt = "delete from tblregistration where email='" + Request.QueryString["msg"].ToString() + "'";
            bool b = db.ExecuteInsertUpdateDelete();
            if (b == true)
                Response.Write("<script>alert('RECORD Deleted Successfuly');window.location.href='MasterMgmt'</script>");
            else
                Response.Write("<script>alert('Deletion Unsuccessfull');window.location.href='MasterMgmt'</script>");

            return View();
        }
        [HttpGet]
        public ActionResult LEADSdel()
        {
            db.cmdtxt = "delete from tblleads where lid='" + Request.QueryString["msg"].ToString() + "'";
            bool b = db.ExecuteInsertUpdateDelete();
            if (b == true)
                Response.Write("<script>alert('RECORD Deleted Successfuly');window.location.href='LeadsMgmt'</script>");
            else
                Response.Write("<script>alert('Deletion Unsuccessfull');window.location.href='LeadsMgmt'</script>");

            return View();
        }
        [HttpGet]
        public ActionResult Gdel()
        {
            db.cmdtxt = "delete from tblgallery where eid='" + Request.QueryString["msg"].ToString() + "'";
            bool b = db.ExecuteInsertUpdateDelete();
            if (b == true)
                Response.Write("<script>alert('Image Deleted Successfuly');window.location.href='Gallerymgmt'</script>");
            else
                Response.Write("<script>alert('Deletion Unsuccessfull');window.location.href='Gallerymgmt'</script>");

            return View();
        }
    }
}