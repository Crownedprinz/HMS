using anchor1.Filters;
using HMS.Models;
using HMS.utilities;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using anchor1.utilities;
using System.Collections.Generic;

namespace HMS.Controllers
{
    public class RoomsController : Controller
    {
        // GET: Rooms
        MainContext db = new MainContext();
        vw_genlay tempvar = new vw_genlay();
        bool err_flag = true;
        string action_flag = "";
        room_table room_table = new room_table();
        worksess worksess;
        hutility util = new hutility();

        [EncryptionActionAttribute]
        public ActionResult Index(string pc=null)
        {
            if (pc == null)
                return RedirectToAction("Index", "Dashboard");
            worksess = (worksess)Session["worksess"];
            worksess.idrep = "";
            Session["worksess"] = worksess;
            var bglist = from bg in db.room_table
                         join bh in db.msg_file
                         on new { a1 = bg.room_type, a2 = "RMT" } equals new { a1 = bh.id_code, a2 = bh.para_code }
                         into bh1
                         from bh2 in bh1.DefaultIfEmpty()
                         select new vw_genlay
                         {
                             vwstring0 = bg.room_no,
                             vwstring1 = bg.room_desc,
                             vwstring2 = bg.room_status == "A" ? "Available" : "Not Available",
                             vwstring3 = bh2.desc1,
                             vwstring5 = bh2.desc3
                         };
            return View(bglist.ToList());
        }

        [EncryptionActionAttribute]
        public ActionResult RoomStatIndex(string pc = null)
        {
            if (pc == null)
                return RedirectToAction("Index", "Dashboard");
            worksess = (worksess)Session["worksess"];
            var bglist = from bg in db.room_table
                         join bh in db.msg_file
                         on new { a1 = bg.room_type, a2 = "RMT" } equals new { a1 = bh.id_code, a2 = bh.para_code }
                         into bh1
                         from bh2 in bh1.DefaultIfEmpty()
                         join bk in db.msg_file
                         on new {a1 = bg.neat_status, a2 = "NST"} equals new {a1 = bk.id_code, a2 = bk.para_code}
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
            return View(bglist.ToList());
        }

        [EncryptionActionAttribute]
        public ActionResult RoomStat(string key1 = null)
        {
            worksess = (worksess)Session["worksess"];
            room_table = db.room_table.Find(key1);
            string query = "";
            if (room_table.neat_status == "01")
            {
                query = "update room_table set neat_status ='02', room_status = 'O' where room_no =" + util.sqlquote(key1);
            }
            else if(room_table.neat_status=="02")
                query = "update room_table set neat_status = '01', room_status = 'A' where room_no =" + util.sqlquote(key1);
            
            db.Database.ExecuteSqlCommand(query);
            return  RedirectToAction("RoomStatIndex", null, new { anc = Ccheckg.convert_pass2("pc=1") });

        }
        // GET: Rooms/Details/5
        [EncryptionActionAttribute]
        public ActionResult Details(string pc = null)
        {
            if (pc == null)
                return RedirectToAction("Index", "Dashboard");
            worksess = (worksess)Session["worksess"];
            ViewBag.action_flag = "roomDetails";

            var bglist = from bg in db.room_table
                         join bh in db.checkin_table
                         on new {a1= bg.room_no} equals new {a1 = bh.room_number}
                            into bh1 from bh2 in bh1.DefaultIfEmpty()
                         join bk in db.msg_file
                         on new {a1=bg.room_type, a2="RMT"} equals new {a1 = bk.id_code, a2 = bk.para_code}
                         into bk1 from bk2 in bk1.DefaultIfEmpty()
                         where bh2.flag == "I"
                         select new vw_genlay
                         {
                             vwstring0 = bg.room_no,
                             vwstring1 = bg.room_desc,
                             vwstring2 = bg.room_status == "A" ? "Available" : "Not Available",
                             vwstring3 = bk2.desc1,
                             vwstring5 = bk2.desc3,
                             vwstring4 = bh2.first_name+" "+bh2.surname
                         };
            return View("Index",bglist.ToList());
        }

        // GET: Rooms/Create
        [EncryptionActionAttribute]
        public ActionResult Create(string pc=null)
        {

            if (pc == null)
                return RedirectToAction("Index", "Dashboard");
            worksess = (worksess)Session["worksess"];
            ViewBag.action_flag = "Create";
            action_flag = "Create";

            select_query();
            return View("Edit", tempvar);
        }

        // POST: Rooms/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(vw_genlay tempvar_in)
        {       
            ViewBag.action_flag = "Create";
            action_flag = "Create";
            worksess = (worksess)Session["worksess"];
            tempvar = tempvar_in;
            update_file();

            if (err_flag)
                return RedirectToAction("Create", null, new { anc = Ccheckg.convert_pass2("pc=1") });

            select_query();
            return View("Edit", tempvar);
        }

        // GET: Rooms/Edit/5
        [EncryptionActionAttribute]
        public ActionResult Edit(string key1)
        {
            ViewBag.action_flag = "Edit";
            action_flag = "Edit";
            worksess = (worksess)Session["worksess"];
            room_table = db.room_table.Find(key1);
            if (room_table != null)
                read_record();

            select_query();
            return View(tempvar);
        }

        // POST: Rooms/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(vw_genlay tempvar_in, string id_xhrt)
        {
            ViewBag.action_flag = "Edit";
            action_flag = "Edit";
            worksess = (worksess)Session["worksess"];
            tempvar = tempvar_in;

            if (id_xhrt == "D")
            {
                delete_record();
                return RedirectToAction("Index", null, new { anc = Ccheckg.convert_pass2("pc=1") });
            }

            update_file();
            if (err_flag)
                return RedirectToAction("Index", null, new { anc = Ccheckg.convert_pass2("pc=1") });


            select_query();
             return View(tempvar);

        }
        private void delete_record()
        {
            room_table = db.room_table.Find(tempvar.vwstring0);
            if (room_table != null)
            {
                db.room_table.Remove(room_table);
                db.SaveChanges();
            }
        }

        private void update_file()
        {
            err_flag = true;
            validation_routine();

            if (err_flag)
                update_record();

        }

        private void validation_routine()
        {
            if (string.IsNullOrWhiteSpace(tempvar.vwstring0))
            {
                ModelState.AddModelError(String.Empty, "must not be spaces");
                err_flag = false;
            }
            if (action_flag == "Create")
            {
                room_table bnk = db.room_table.Find(tempvar.vwstring0);
                if (bnk != null)
                {
                    ModelState.AddModelError(String.Empty, "Room No already exist");
                    err_flag = false;
                    worksess.idrep = "";
                }

            }

        }
        private void update_record()
        {
            if (action_flag == "Create")
            {
                room_table = new room_table();
                room_table.neat_status = "01";
                room_table.created_by = util.find_name("3", "", worksess.userid); ;
                room_table.created_date = DateTime.UtcNow.ToLocalTime();
                 }
            else
            {
                room_table = db.room_table.Find(tempvar.vwstring0);
            }


            room_table.room_no = string.IsNullOrWhiteSpace(tempvar.vwstring0) ? "" : tempvar.vwstring0;
            room_table.room_desc = string.IsNullOrWhiteSpace(tempvar.vwstring1) ? "" : tempvar.vwstring1;
            room_table.room_status = string.IsNullOrWhiteSpace(tempvar.vwstring2) ? "" : tempvar.vwstring2;
            room_table.room_type = string.IsNullOrWhiteSpace(tempvar.vwstring3) ? "" : tempvar.vwstring3;
            room_table.room_price = tempvar.vwdecimal0;
            room_table.neat_status = "01";
            room_table.max_no_adults = tempvar.vwint0;
            room_table.max_no_kids = tempvar.vwint1;
            room_table.modified_date = DateTime.UtcNow.ToLocalTime();
            room_table.modified_by = util.find_name("3", "", worksess.userid); ;
           
            if (action_flag == "Create")
                db.Entry(room_table).State = EntityState.Added;
            else
                db.Entry(room_table).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }

            catch (Exception err)
            {
                if (err.InnerException == null)
                    ModelState.AddModelError(String.Empty, err.Message);
                else
                    ModelState.AddModelError(String.Empty, err.InnerException.InnerException.Message);

                err_flag = false;
            }

        }


        // GET: Rooms/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Rooms/Delete/5
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

        private void read_record()
        {
            tempvar.vwstring0 = room_table.room_no;
            tempvar.vwstring1 = room_table.room_desc;
            tempvar.vwstring2 = room_table.room_status;
            tempvar.vwstring3 = room_table.room_type;
            tempvar.vwdecimal0 = room_table.room_price;
            tempvar.vwint0 = room_table.max_no_adults;
            tempvar.vwint1 = room_table.max_no_kids;
        }
        private void select_query()
        {
           
            ViewBag.rmtype = util.all_select_query("GEN", tempvar.vwstring3, "RMT");
        }
        public ActionResult get_price(string id = "")
        {
            worksess = (worksess)Session["worksess"];
            var bglist = (from bh in db.msg_file
                          where bh.id_code == id && bh.para_code=="RMT"
                          select bh).FirstOrDefault();
            List<SelectListItem> ary = new List<SelectListItem>();
            if (bglist != null)
            {
                ary.Add(new SelectListItem { Value = "typeprice", Text = bglist.desc3 });
            }
            else
            {
                ary.Add(new SelectListItem { Value = "typeprice", Text = "0" });

            }
            if (HttpContext.Request.IsAjaxRequest())
                return Json(new SelectList(
                            ary.ToArray(),
                            "Value",
                            "Text")
                       , JsonRequestBehavior.AllowGet);


            return RedirectToAction("Index", null, new { anc = Ccheckg.convert_pass2("pc = ") });

        }
    }
}
