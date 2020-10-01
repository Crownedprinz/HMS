using HMS.Models;
using HMS.utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using System.Web;
using System.Web.Mvc;

namespace HMS.Controllers
{
    public class DashboardController : Controller
    {
        Timer myTimer = new Timer(1000);
        room_table room_table = new room_table();
        vw_genlay glay = new vw_genlay();
        cust_table cust_table = new cust_table();
        MainContext db = new MainContext();
        worksess worksess;
        MainContext db1 = new MainContext();
        hutility util = new hutility();
        // GET: Dashboard
        public ActionResult Index()
        {
            worksess = (worksess)Session["worksess"];
            worksess.idrep = "";
            Session["worksess"] = worksess;
            DateTime systemDate = DateTime.UtcNow.ToLocalTime();
            DateTime compareDate = DateTime.Today.AddHours(12D);
            
            if (systemDate>compareDate || compareDate==systemDate) { 
                var datenow = util.date_yyyymmdd(DateTime.UtcNow.ToLocalTime().ToString("dd/MM/yyyy"));
            string getd = "select room_number c1 , guest_id intc1 from checkin_table where flag='I' and exp_checkout_date=" + util.sqlquote(datenow);
            var getdd = db.Database.SqlQuery<tempquery>(getd);
            foreach(var item in getdd)
            {
                string query = "update checkin_table set flag = 'O', act_checkout_date=" + util.date_yyyymmdd(DateTime.UtcNow.ToLocalTime().ToString("dd/MM/yyyy"))+", modified_date = " +util.sqlquote(DateTime.UtcNow.ToLocalTime().ToString())+ ", modified_by='SYSTEM'  where guest_id=" + Convert.ToInt32(item.intc1)+
                    ";update room_table set neat_status = '02' , modified_date = " + util.sqlquote(DateTime.UtcNow.ToLocalTime().ToString())+", modified_by = 'SYSTEM' where room_no=" + util.sqlquote(item.c1);
                    db1.Database.ExecuteSqlCommand(query);
                }
            }
            myTimer = new System.Timers.Timer(1000);
            myTimer.AutoReset = true;
            myTimer.Enabled = true;
            myTimer.Interval = 1000;
            
            var bglist = from bg in db.room_table
                         select bg;

            glay.vwint0 = bglist.Count();
            var bglist1 = from bg in db.room_table
                          where bg.room_status == "A"
                         select bg;
            glay.vwint1 = bglist1.Count();
            var bglist2 = from bg in db.room_table
                          where bg.room_status != "A"
                          select bg;
            glay.vwint2 = bglist2.Count();
            var bglist3 = from bg in db.checkin_table
                          where bg.flag=="I"
                          select bg;
            glay.vwint3 = bglist3.Count();
            var bglist4 = from bg in db.checkin_table
                      where bg.gender == "M" && bg.flag == "I"
                          select bg;
            glay.vwint4 = bglist4.Count();
            var bglist5 = from bg in db.checkin_table
                          where bg.gender == "F" && bg.flag== "I"
                          select bg;
            glay.vwint5 = bglist5.Count();

            string bglist6 = "select count(*) intc1 from checkin_table where flag='I' and act_checkout_date = " + util.date_yyyymmdd(DateTime.UtcNow.ToLocalTime().ToString("dd/MM/yyyy"));
            var selq = db.Database.SqlQuery<tempquery>(bglist6).FirstOrDefault();
            glay.vwint6 = selq.intc1;

            var str1 = from bg in db.room_table
                         join bh in db.msg_file
                         on new { a1 = bg.room_type, a2 = "RMT" } equals new { a1 = bh.id_code, a2 = bh.para_code }
                         into bh1
                         from bh2 in bh1.DefaultIfEmpty()
                         join bk in db.msg_file
                         on new { a1 = bg.neat_status, a2 = "NST" } equals new { a1 = bk.id_code, a2 = bk.para_code }
                         into bk1
                         from bk2 in bk1.DefaultIfEmpty()
                         orderby bk2.id_code descending
                         select new vw_genlay
                         {
                             vwstring0 = bg.room_no,
                             vwstring1 = bg.room_desc,
                             vwstring3 = bh2.desc1,
                             vwstring2 = bk2.desc1,
                             vwstring4 = bg.neat_status
                         };
            ViewBag.x1 = str1.ToList();
            //ViewBag.x1 = db.Database.SqlQuery<vw_genlay>(str1).ToList();
            return View(glay);



        }

        // GET: Dashboard/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Dashboard/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Dashboard/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Dashboard/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Dashboard/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Dashboard/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Dashboard/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

      
    }
}
