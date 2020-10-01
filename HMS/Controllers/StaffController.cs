using anchor1.Filters;
using HMS.Models;
using HMS.utilities;
using System;
using System.Collections.Generic;
using anchor1.utilities;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HMS.Controllers
{
      public class StaffController : Controller
    {

        MainContext db = new MainContext();
        vw_genlay tempvar = new vw_genlay();
        bool err_flag = true;
        string action_flag = "";
        staff_table staff_table = new staff_table();
        hutility util = new hutility();
        worksess worksess;
        HttpPostedFileBase photo1;
        HttpPostedFileBase[] photo2;
        [EncryptionActionAttribute]
        public ActionResult Index(string pc = null)
        {
            pc = "1";
            if (pc == null)
                return RedirectToAction("Index", "Dashboard");
            worksess = (worksess)Session["worksess"];
            worksess.idrep = "";
            Session["worksess"] = worksess;
            var bglist = from bg in db.staff_table
                         join bh in db.role_table
                         on new {a1 = bg.job_role,a2="H",a3=""}equals new {a1 = bh.role_id, a2 = bh.flag,a3=bh.menu_option}
                         into bh1
                         from bh2 in bh1.DefaultIfEmpty()
                         select new vw_genlay
                         {
                             vwint0 = bg.staff_id,
                             vwstring0 = bg.first_name+" "+bg.surname,
                             vwstring1 = bg.email,
                             vwstring2 = bh2.Role_description,
                             vwstring3 = bg.state
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
                return RedirectToAction("Index");

            select_query();
            return View("Edit", tempvar);
        }

        [EncryptionActionAttribute]
        public ActionResult Edit(string key1)
        {
            ViewBag.action_flag = "Edit";
            action_flag = "Edit";
           
            worksess = (worksess)Session["worksess"];
            staff_table = db.staff_table.Find(Convert.ToInt32(key1));
            if (staff_table != null)
                read_record();

            select_query();
            return View(tempvar);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(vw_genlay tempvar_in, string id_xhrt, HttpPostedFileBase photofiles, HttpPostedFileBase[] picture1)
        {
            ViewBag.action_flag = "Edit";
            action_flag = "Edit";
            worksess = (worksess)Session["worksess"];
            tempvar = tempvar_in;

            if (id_xhrt == "D")
            {
                delete_record();
                return RedirectToAction("Index");
            }
            photo1 = photofiles;
            photo2 = picture1;
            update_file();
            if (err_flag)
                return RedirectToAction("Index");

            select_query();
            return View(tempvar);
        }

        private void delete_record()
        {
            staff_table = db.staff_table.Find(tempvar.vwint0);
            if (staff_table != null)
            {
                db.staff_table.Remove(staff_table);
                db.SaveChanges();
            }
            string query = "delete from user_table where staff_id = " + tempvar.vwint0;
            db.Database.ExecuteSqlCommand(query);

        }

        [HttpPost]
        public ActionResult delete_list(int id)
        {

            // write your query statement
            string sqlstr = "delete from staff_table where staff_id ='" + id + "'";
            int delctr = db.Database.ExecuteSqlCommand(sqlstr);

                string query = "delete from user_table where staff_id = " + tempvar.vwint0;
                db.Database.ExecuteSqlCommand(query);
            return RedirectToAction("Index", null, new { anc = Ccheckg.convert_pass2("pc=1") });
        }
            

        private void read_record()
        {
            tempvar.vwint0 = staff_table.staff_id;
            tempvar.vwstring0 = staff_table.first_name;
            tempvar.vwstring1 = staff_table.surname;
            tempvar.vwstring2 = staff_table.job_role;
            tempvar.vwstring3 = staff_table.email;
            tempvar.vwstring4 = staff_table.phone1;
            tempvar.vwstring5 = staff_table.address;
            tempvar.vwstring6 = staff_table.gender;
            tempvar.vwstring7 = staff_table.city;
            tempvar.vwstring8 = staff_table.state;
            tempvar.vwstring9 = staff_table.country;
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
                staff_table = new staff_table();
                staff_table.created_by = util.find_name("3", "", worksess.userid); ;
                staff_table.created_date = DateTime.UtcNow.ToLocalTime();
            }
            else
            {
                staff_table = db.staff_table.Find(tempvar.vwint0);
            }


            staff_table.first_name = string.IsNullOrWhiteSpace(tempvar.vwstring0) ? "" : tempvar.vwstring0;
            staff_table.surname = string.IsNullOrWhiteSpace(tempvar.vwstring1) ? "" : tempvar.vwstring1;
            staff_table.job_role = string.IsNullOrWhiteSpace(tempvar.vwstring2) ? "" : tempvar.vwstring2;
            staff_table.email = string.IsNullOrWhiteSpace(tempvar.vwstring3) ? "" : tempvar.vwstring3;
            staff_table.phone1 = string.IsNullOrWhiteSpace(tempvar.vwstring4) ? "" : tempvar.vwstring4;
            staff_table.address = string.IsNullOrWhiteSpace(tempvar.vwstring5) ? "" : tempvar.vwstring5;
            staff_table.gender = string.IsNullOrWhiteSpace(tempvar.vwstring6) ? "" : tempvar.vwstring6;
            staff_table.city = string.IsNullOrWhiteSpace(tempvar.vwstring7) ? "" : tempvar.vwstring7;
            staff_table.state = string.IsNullOrWhiteSpace(tempvar.vwstring8) ? "" : tempvar.vwstring8;
            staff_table.country = string.IsNullOrWhiteSpace(tempvar.vwstring9) ? "" : tempvar.vwstring9;
            staff_table.modified_date = DateTime.UtcNow.ToLocalTime();
            staff_table.modified_by = util.find_name("3", "", worksess.userid); ;

            if (photo1 != null)
            {

                byte[] uploaded = new byte[photo1.InputStream.Length];
                photo1.InputStream.Read(uploaded, 0, uploaded.Length);
                staff_table.staff_photo = uploaded;
            }

            if (action_flag == "Create")
                db.Entry(staff_table).State = EntityState.Added;
            else
                db.Entry(staff_table).State = EntityState.Modified;

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
            ViewBag.country = util.all_select_query("GEN", tempvar.vwstring3, "CTRY");
            ViewBag.role = util.all_select_query("007", tempvar.vwstring2);
        }
        [OverrideActionFilters]
        public ActionResult show(string id)
        {
            var dir = "";
            staff_table = db.staff_table.Find(Convert.ToInt32(id));
            if (staff_table != null)
            {
                if (staff_table.staff_photo != null)
                {
                    byte[] imagedata = staff_table.staff_photo;
                    return File(imagedata, "png");
                }
                else
                    return null;
            }
            else
            {
                dir = Server.MapPath("~/img");
                var path = Path.Combine(dir, "noLogo.png"); //validate the path for security or use other means to generate the path.
                return File(path, "png");
            }
        }
    }
}