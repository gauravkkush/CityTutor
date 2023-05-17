using CityTutor1.App_Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;

namespace CityTutor1.Controllers
{
    public class HomeController : Controller
    {
        DBMANAGER db = new DBMANAGER();
        
        public ActionResult Index()
        {
            db.cmdtxt = "select * from tblnews";
            DataTable dt = db.GetBulkData();
            if (dt.Rows.Count > 0)
            {
                string msg = "<marquee>";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    msg += "<span style=color:red; background:gray;>" + dt.Rows[i][1] + "</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                }
                msg = msg + "</marquee>"; 
                ViewBag.news = msg;
            }
            return View();
        }
        [HttpGet]
        public ActionResult SearchMaster()
        {
            db.cmdtxt = "select * from tblregistration where city like '%city%' and area like '%area%'";
            DataTable dt = db.GetBulkData();
            if (dt.Rows.Count > 0)
            {
                string str ="";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    str += "<tr><td>" + dt.Rows[i][1] + "</td><td>" + dt.Rows[i][2] + "</td><td>" + dt.Rows[i][3] + "</td><td>" + dt.Rows[i][4] + "</td><td>" + dt.Rows[i][5] + "</td><td>" + dt.Rows[i][6] + "</td><td>" + dt.Rows[i][7] + "</td><td>" + dt.Rows[i][8] + "</td><td>" + dt.Rows[i][9] + "</td><td>" + dt.Rows[i][10] + "</td><td>" + dt.Rows[i][11] + "</td><td>" + dt.Rows[i][12] + "</td><td>" + dt.Rows[i][13] + "</td><td>" + dt.Rows[i][14] + "</td><td>" + dt.Rows[i][15] + "</td></tr>";
                }
                ViewBag.request = str;
            }
            return View();
        }
        public ActionResult SendRequest() 
        {
            return View();        
        }
        public ActionResult Gallery()
        {
            db.cmdtxt = "select * from tblgallery";
            DataTable dt = db.GetBulkData();
            if (dt.Rows.Count > 0)
            { 
                string str="";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    str +="<div class='col-sm-4'><div class='col-sm-12 gal'><img src='../Content/Gallery/"+dt.Rows[i][2]+"' height='200px' width='100%' /></div></div>";
                }
                ViewBag.image = str;
            }
            return View();
        }
        public ActionResult Contact()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Contact( string name, string email, string mob, string msg)
        {
            DBMANAGER db = new DBMANAGER();
            db.cmdtxt = "insert into tblenquiry(name,email,mobno,msg,edate)values('"+name+"','"+email+"','"+mob+"','"+msg+"','"+DateTime.Now+"')";
            Boolean b = db.ExecuteInsertUpdateDelete();
            if (b == true)
                Response.Write("<script>alert('Thank You For Enquiry')</script>");
            else
                Response.Write("<script>alert('Data Insertion Unsuccessful')</script>");
            return View();
        }
        public ActionResult genleads()
        {
            return View();        
        }
        [HttpPost]
        public ActionResult genleads(string type,string name, string mail, string mob, string cls, string medium, string city, string area)
        {
            DBMANAGER db = new DBMANAGER();
            db.cmdtxt = "insert into tblleads(ltype,name,email,mobno,class,medium,city,area,ldate)values('" + type + "','" + name + "','" + mail + "','" + mob + "','" + cls + "','" + medium + "','" + city + "','" + area + "','" + DateTime.Now + "')";
            Boolean b = db.ExecuteInsertUpdateDelete();
            if (b == true)
                Response.Write("<script>alert('Lead Generated Successfully.')</script>");
            else
                Response.Write("<script>alert('Leads Generation Unsuccessful')</script>");
            return View();
        }
        public ActionResult register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult register( string name, string mob, string level, string city, string experience, string status, string session, HttpPostedFileBase pic, string mail, string age, string gender, string area, string medium, string language, string qualification, string mode, string password )
        {
            String filename = System.IO.Path.GetFileName(pic.FileName);
            String path = System.IO.Path.Combine(Server.MapPath("/Content/UserPic"),filename);
            pic.SaveAs(path);
            db.cmdtxt = "insert into tblregistration(pass,name,email,mobno,age,tlevel,educationmedium,city,area,experience,language,status,qualification,tsession,tmode,pic,regdate,gender)values('"+password+"','" + name + "','" + mail + "','" + mob + "','" + age + "','" + level + "','" + medium + "','" + city + "','" + area + "','" + experience + "','" + language + "','" + status + "','" + qualification + "','" + session + "','" + mode + "','" + filename + "','" + DateTime.Now + "','" + gender + "')";
            Boolean b = db.ExecuteInsertUpdateDelete();
            if (b == true)
            {
                Response.Write("<script>alert('Thanks You For Showing Your Interest')</script>");
                Response.Redirect("../Home/login");
            }
            else
                Response.Write("<script>alert('Registraion Failed!')</script>");
            return View();
        }
        public ActionResult login(string username, string password)
        {
            db.cmdtxt = "select * from tblregistration where email='"+username+"' and pass='"+password+"'";
            DataTable dt = db.GetBulkData();
            if (dt.Rows.Count > 0)
            {
                Session["master"]=username;
                Response.Redirect("../Master/Index");
            }
            else
            {
                Response.Write("<script>alert('Your Username or Password is Incorrect!')</script>");
            }
            return View();
        }
        public ActionResult devloper()
        {
            return View();
        }
        public ActionResult profile()
        {
            db.cmdtxt = "select * from tblregistration ";
            DataTable dt = db.GetBulkData();
            if (dt.Rows.Count >0)
            {
                string str = "";
                for (int i = 1; i < dt.Rows.Count; i++)
                { 
                    str="<div class='col-sm-4 img' style='text-align:center;'><img src='../Content/UserPic/"+dt.Rows[i][15]+"' height='200px' width='200px' /></div><div class='col-sm-8'><div class='col-sm-12  info'><div><p>Name:</p><span>" + dt.Rows[i][1] + "</span></div><div><p>E-mail:</p><span>" + dt.Rows[i][2] + "</span></div><div><p>Mobile No.:</p><span>" + dt.Rows[i][3] + "</span></div><div><p>Age:</p><span>" + dt.Rows[i][4] + "</span></div><div><p>Tuition Level:</p><span>" + dt.Rows[i][5] + "</span></div><div><p>Medium:</p><span>" + dt.Rows[i][6] + "</span></div><div><p>City:</p><span>" + dt.Rows[i][7] + "</span></div><div><p>Area:</p><span>" + dt.Rows[i][8] + "</span></div><div><p>Experience:</p><span>" + dt.Rows[i][9] + "</span></div><div><p>Language:</p><span>" + dt.Rows[i][10] + "</span></div><div><p>Status:</p><span>" + dt.Rows[i][11] + "</span></div><div><p>Qualification:</p><span>" + dt.Rows[i][12] + "</span></div><div><p>Timing:</p><span>" + dt.Rows[i][13] + "</span></div><div><p>Mode:</p><span>" + dt.Rows[i][14] + "</span></div><div><p>Gender:</p><span>" + dt.Rows[i][16] + "</span></div></div></div>";
                }
                ViewBag.profile = str;
            }
            return View();
        }
    }

}
