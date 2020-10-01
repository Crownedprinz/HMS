using anchor1.Filters;
using anchor1.utilities;
using HMS.Models;
using HMS.utilities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HMS.Controllers
{
    public class AdminController : Controller
    {

        MainContext db = new MainContext();
        vw_genlay tempvar = new vw_genlay();
        bool err_flag = true;
        string action_flag = "";
        worksess worksess;
        user_table user_table = new user_table();
        hutility util = new hutility();

        [EncryptionActionAttribute]
        public ActionResult Index(string pc = null)
        {
            pc = "1";
            if (pc == null)
                return RedirectToAction("Index", "Dashboard");
            worksess = (worksess)Session["worksess"];
            worksess.idrep = "";
            Session["worksess"] = worksess;
            var bglist = from bg in db.user_table
                         join bh in db.role_table
                         on new { a1 = bg.user_role, a2 = "H" } equals new { a1 = bh.role_id, a2 = bh.flag }
                         into bh1
                         from bh2 in bh1.DefaultIfEmpty()
                         join bk in db.staff_table
                         on new { a1 = bg.staff_id } equals new { a1 = bk.staff_id }
                         into bk1
                         from bk2 in bk1.DefaultIfEmpty()
                         select new vw_genlay
                         {
                             vwstring0 = bg.user_id,
                             vwstring1 = bk2.first_name + " "+bk2.surname,
                             vwint0 = bg.staff_id,
                             vwstring2 = bh2.Role_description,
                         };
            return View(bglist.ToList());
        }

      

        [EncryptionActionAttribute]
        public ActionResult Create(string pc = null)
        {
            if (pc == null)
                return RedirectToAction("Index", "Dashboard");
            worksess = (worksess)Session["worksess"];
            ViewBag.action_flag = "Create";
            action_flag = "Create";

            select_query();
            return View("Edit", tempvar);
        }

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
                return RedirectToAction("Index", null, new { anc = Ccheckg.convert_pass2("pc=1") });

            select_query();
            return View("Edit", tempvar);
        }

        [EncryptionActionAttribute]
        public ActionResult Edit(string key1)
        {
            ViewBag.action_flag = "Edit";
            action_flag = "Edit";
            worksess = (worksess)Session["worksess"];
            user_table = db.user_table.Find(key1);
            if (user_table != null)
                read_record();

            select_query();
            return View(tempvar);
        }

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
            if (!string.IsNullOrWhiteSpace(tempvar.vwstring2))
                tempvar.vwstring5 = "";
            update_file();
            if (err_flag)
                return RedirectToAction("Index", null, new { anc = Ccheckg.convert_pass2("pc=1") });

            select_query();
            return View(tempvar);
        }

        private void delete_record()
        {
            user_table = db.user_table.Find(tempvar.vwstring0);
            if (user_table != null)
            {
                db.user_table.Remove(user_table);
                db.SaveChanges();
            }
        }

        [HttpPost]
        public ActionResult delete_list(string id)
        {
            // write your query statement
            string sqlstr = "delete from user_table where user_id ='" + id + "'";
            int delctr = db.Database.ExecuteSqlCommand(sqlstr);
            return RedirectToAction("Index", null, new { anc = Ccheckg.convert_pass2("pc=1") });
        }

       
        private void read_record()
        {
            tempvar.vwstring0 = user_table.user_id;
            tempvar.vwstring1 = user_table.user_role;
            tempvar.vwint0 = user_table.staff_id;
            tempvar.vwstring5 = user_table.user_password;
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

        }
        private void update_record()
        {
            if (action_flag == "Create")
            {
                user_table = new user_table();
                user_table.created_by = util.find_name("3", "", worksess.userid); ;
                user_table.created_date = DateTime.UtcNow.ToLocalTime();
                user_table.last_access = DateTime.UtcNow.ToLocalTime();
            }
            else
            {
                user_table = db.user_table.Find(tempvar.vwstring0);
            }


            user_table.user_id = string.IsNullOrWhiteSpace(tempvar.vwstring0) ? "" : tempvar.vwstring0;
            user_table.staff_id = tempvar.vwint0;
            user_table.user_role = string.IsNullOrWhiteSpace(tempvar.vwstring1) ? "" : tempvar.vwstring1;
            if(!string.IsNullOrWhiteSpace(tempvar.vwstring5))
            user_table.user_password = string.IsNullOrWhiteSpace(tempvar.vwstring5) ? "" : tempvar.vwstring5;
            else
                user_table.user_password = string.IsNullOrWhiteSpace(tempvar.vwstring2) ? "" : util.HashString(tempvar.vwstring2);
            user_table.last_access = DateTime.UtcNow.ToLocalTime();
           user_table.modified_date = DateTime.UtcNow.ToLocalTime();
            user_table.modified_by = util.find_name("3", "", worksess.userid);

            if (action_flag == "Create")
                db.Entry(user_table).State = EntityState.Added;
            else
                db.Entry(user_table).State = EntityState.Modified;

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
        private void select_query()
        {
            ViewBag.staff = util.all_select_query("005", tempvar.vwint0.ToString());
            ViewBag.role = util.all_select_query("007", tempvar.vwstring1);
        }
    }
}