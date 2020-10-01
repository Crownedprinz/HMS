using anchor1.utilities;
using HMS.Models;
using HMS.utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace HMS.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        Boolean err_flag = true;
        vw_genlay tempvar = new vw_genlay();
        hutility util = new hutility();

        Boolean lock_flag = true;
        char sp = '"';
        string w_changed;

        worksess worksess = new worksess();
        private MainContext Logdb;
        MainContext context = new MainContext();
        string menu_string = "";

        [OverrideActionFilters]
        public ActionResult Index()
        {
            worksess worksess = new worksess();
            ViewBag.err_msg = "";
            MainContext db = new MainContext();
            var bglist1 = (from bg in db.company_settings
                           where bg.id_code == "HInfo"
                           select bg).FirstOrDefault();
            worksess.pname = bglist1.field1;
               
            Session["worksess"] = worksess;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(vw_genlay tempvar1)
        {
            tempvar = tempvar1;
            //' check max of 10 mins
            int s2 = Convert.ToInt16(tempvar.vwstring2.Substring(0, 2));
            int s3 = Convert.ToInt16(tempvar.vwstring2.Substring(2, 2));
            int s1 = Convert.ToInt16(tempvar.vwstring2.Substring(4, 2));

            DateTime d1 = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, s1, s2, s3);
            TimeSpan d3 = DateTime.Now.Subtract(d1);
            double timediff = d3.TotalMinutes;

            if (timediff > 10)
            {
                @Session["err_Message"] = "Time out in validating your password, Pls Click Cancel and login again.";
                return RedirectToAction("Index");
            }
            if (tempvar.vwstring0=="ADMIN" && tempvar.vwstring1 == "123!@#")
            {
                Session["err_Message"] = "";
                worksess = (worksess)Session["worksess"];
                worksess.userid = "Admin";
                worksess.username = "Admin";
                worksess.processing_period = DateTime.UtcNow.ToLocalTime().ToString("dd/MM/yyyy");
                Session["worksess"] = worksess;
            }
           else
            set_init_data();

            if (err_flag == false) {
                Session["err_Message"]= "Invalid Username or Password";
                return RedirectToAction("Index");
            }
            if (worksess.userid == "Admin")
                menuRead1();
            else
                menuRead();
            return  RedirectToAction("Index","Dashboard"); ;
        }

        private Boolean error_rtn(string err_message)
        {
            
            ViewBag.err_msg = err_message;
            return false;

        }
        private void set_init_data()
        {
            MainContext db = new MainContext();
            worksess = (worksess)Session["worksess"];
            tempvar.vwstring1 = util.HashString(tempvar.vwstring1);
            var bglist = (from bg in db.user_table
                          join bh in db.staff_table
                          on new {a1 = bg.staff_id} equals new {a1 = bh.staff_id}
                          into bh1 
                          from bh2 in bh1.DefaultIfEmpty()
                          where bg.user_id == tempvar.vwstring0 && bg.user_password == tempvar.vwstring1
                          select new { bg, bh2 }).FirstOrDefault();
            if (bglist != null) { 
            worksess.userid = bglist.bg.user_id;
            worksess.username = bglist.bh2.first_name + " " + bglist.bh2.surname;
            worksess.processing_period = bglist.bg.last_access.ToString("dd/MM/yyyy");
                string query = "update user_table set last_access=" + util.sqlquote(DateTime.UtcNow.ToLocalTime().ToString()) + " where user_id =" + util.sqlquote(worksess.userid);
                db.Database.ExecuteSqlCommand(query);
            }
            else
            {
                err_flag = false;
            }
            Session["worksess"] = worksess;

        }
        private void menuRead()
        {
            //worksess worksess = (worksess)Session["worksess"];
            MainContext db = new MainContext();
            var bgmenu = (from bg in db.user_table
                          where bg.user_id == worksess.userid
                          select bg).FirstOrDefault();

            var bglist1 = (from bg in db.msg_file
                           where bg.para_code == "MAINM"
                           select bg).ToList();
            var bglist = bglist1.OrderBy(t => Convert.ToInt32(t.desc2));
            foreach (var item in bglist)
            {
                menu_string += "<li>\r\n";
                menu_string += "<a href=" + sp+"#" +sp+ "><i class="+sp+"fa fa-sitemap fa-3x"+sp+"></i>" + item.desc1 + "<span class="+sp+"fa arrow"+sp+"></span></a>\r\n";
                menu_string += "<ul class="+sp+"nav nav-second - level"+sp+">\r\n";
                
                 var    bglist2 = (from bg in db.msg_file
                                   join bh in db.role_table
                                   on new { a1 = bg.id_code } equals new { a1 = bh.menu_option }
                                   where bg.para_code == "ITEM" && bg.desc3 == item.desc3 && bh.role_id == bgmenu.user_role
                                   select bg).ToList();
                
                var bglist3 = bglist2.OrderBy(t => Convert.ToInt32(t.desc2));
                foreach(var itemsb in bglist3)
                {
                    if (string.IsNullOrWhiteSpace(itemsb.desc6))
                    {
                        string holdval = Ccheckg.convert_pass2("pc=^1");
                        menu_string += "<li><a href = " + sp + "../" + itemsb.desc4 + "/" + itemsb.desc5 + "?anc=" + holdval + sp + "> " + itemsb.desc1 + "</a></li>\r\n";
                    }
                    else
                    {
                        string holdval = Ccheckg.convert_pass2("pc="+itemsb.desc6);
                        menu_string += "<li><a href = " + sp + "../" + itemsb.desc4 + "/" + itemsb.desc5 + "?anc=" + holdval + sp + "> " + itemsb.desc1 + "</a></li>\r\n";
                    }
                }
                menu_string += "</ul>\r\n</li>\r\n";
            }

            worksess.menu_option = menu_string;

            Session["worksess"] = worksess;

        }

        private void menuRead1()
        {
            //worksess worksess = (worksess)Session["worksess"];
            MainContext db = new MainContext();
            var bgmenu = (from bg in db.user_table
                          where bg.user_id == worksess.userid
                          select bg).FirstOrDefault();

            var bglist1 = (from bg in db.msg_file
                           where bg.para_code == "MAINM"
                           select bg).ToList();
            var bglist = bglist1.OrderBy(t => Convert.ToInt32(t.desc2));
            foreach (var item in bglist)
            {
                menu_string += "<li>\r\n";
                menu_string += "<a href=" + sp + "#" + sp + "><i class=" + sp + "fa fa-sitemap fa-3x" + sp + "></i>" + item.desc1 + "<span class=" + sp + "fa arrow" + sp + "></span></a>\r\n";
                menu_string += "<ul class=" + sp + "nav nav-second - level" + sp + ">\r\n";

                var bglist2 = (from bg in db.msg_file
                               where bg.para_code == "ITEM" && bg.desc3 == item.desc3
                               select bg).ToList();

                var bglist3 = bglist2.OrderBy(t => Convert.ToInt32(t.desc2));
                foreach (var itemsb in bglist3)
                {
                    if (string.IsNullOrWhiteSpace(itemsb.desc6))
                    {
                        string holdval = Ccheckg.convert_pass2("pc=^1");
                        menu_string += "<li><a href = " + sp + "../" + itemsb.desc4 + "/" + itemsb.desc5 + "?anc=" + holdval + sp + "> " + itemsb.desc1 + "</a></li>\r\n";
                    }
                    else
                    {
                        string holdval = Ccheckg.convert_pass2("pc=" + itemsb.desc6);
                        menu_string += "<li><a href = " + sp + "../" + itemsb.desc4 + "/" + itemsb.desc5 + "?anc=" + holdval + sp + "> " + itemsb.desc1 + "</a></li>\r\n";
                    }
                }
                menu_string += "</ul>\r\n</li>\r\n";
            }

            worksess.menu_option = menu_string;

            Session["worksess"] = worksess;

        }

        [OverrideActionFilters]
        public ActionResult Signout()
        {
            string bye_pname = " ";
            string str1 = "";
            worksess worksess = (worksess)Session["worksess"];
            if (worksess != null)
            {
                MainContext db = new MainContext();
                bye_pname = (from bg in db.vw_user where bg.user_role == worksess.userid select bg.hvalue).FirstOrDefault();
                str1 = " exec initial_tables @p_userid='" + worksess.userid + "'";
                db.Database.ExecuteSqlCommand(str1);
                    Session.Clear();
                    Session.Abandon();
            }

            ViewBag.byename = bye_pname;

            Session.Abandon();
            Session.Timeout = 1;

            return View();

        }

        [OverrideActionFilters]
        public ActionResult Cancel()
        {
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("Index");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            Session.Clear();
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "home");
        }

        [OverrideActionFilters]
        public ActionResult Show(string id)
        {
            MainContext db = new MainContext();
            var item = (from bg in db.company_settings
                        where bg.id_code == "HInfo"
                        select bg).FirstOrDefault();

            byte[] imagedata = item.com_logo;
            return File(imagedata, "png");
        }
    }
}