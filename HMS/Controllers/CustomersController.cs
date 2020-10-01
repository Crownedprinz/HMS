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
    public class CustomersController : Controller
    {

        MainContext db = new MainContext();
        vw_genlay tempvar = new vw_genlay();
        bool err_flag = true;
        worksess worksess;
        string action_flag = "";
        cust_table cust_table = new cust_table();
        hutility util = new hutility();

        [EncryptionActionAttribute]
        public ActionResult Index(string pc=null)
        {
            pc = "1";
            if (pc == null)
                return RedirectToAction("Index", "Dashboard");
            worksess = (worksess)Session["worksess"];
            worksess.idrep = "";
            Session["worksess"] = worksess;
            var bglist = from bg in db.cust_table
                         select new vw_genlay {
                             vwint0 = bg.customer_id,
                             vwstring0 = bg.customer_name,
                             vwstring1 = bg.cust_email,
                             vwstring2 = "Yes"
                         };
            return View(bglist.ToList());
        }

        public ActionResult Details(int id)
        {
            return View();
        }

        [EncryptionActionAttribute]
        public ActionResult Create(string pc= null)
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
            cust_table = db.cust_table.Find(Convert.ToInt32(key1));
            if (cust_table != null)
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

            update_file();
            if (err_flag)
                return RedirectToAction("Index", null, new { anc = Ccheckg.convert_pass2("pc=1") });

            select_query();
            return View(tempvar);
        }

            private void delete_record()
            {
                cust_table = db.cust_table.Find(tempvar.vwint0);
                if (cust_table != null)
                {
                    db.cust_table.Remove(cust_table);
                    db.SaveChanges();
                }
            }

        [HttpPost]
        public ActionResult delete_list(int id)
        {
            // write your query statement
            string sqlstr = "delete from cust_table where customer_id ='" + id + "'";
            int delctr = db.Database.ExecuteSqlCommand(sqlstr);
            return RedirectToAction("Index", null, new { anc = Ccheckg.convert_pass2("pc=1") });
        }
        
        private void read_record()
        {
            tempvar.vwint0 = cust_table.customer_id;
            tempvar.vwstring11 = cust_table.title;
            tempvar.vwstring0 = cust_table.customer_name;
            tempvar.vwstring1 = cust_table.customer_surname;
            tempvar.vwstring2 = cust_table.customer_address;
            tempvar.vwstring3 = cust_table.cust_email;
            tempvar.vwstring4 = cust_table.cust_phone;
            tempvar.vwstring5 = cust_table.passport_number;
            tempvar.vwstring6 = cust_table.sex;
            tempvar.vwstring7 = cust_table.cust_city;
            tempvar.vwstring8 = cust_table.cust_state;
            tempvar.vwstring9 = cust_table.cust_country;
            tempvar.vwstring10 = cust_table.nationality;
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
                cust_table = new cust_table();
                cust_table.created_by = util.find_name("3", "", worksess.userid); ;
                cust_table.created_date = DateTime.UtcNow.ToLocalTime();
            }
            else
            {
                cust_table = db.cust_table.Find(tempvar.vwint0);
            }


            cust_table.customer_name = string.IsNullOrWhiteSpace(tempvar.vwstring0) ? "" : tempvar.vwstring0;
            cust_table.customer_surname = string.IsNullOrWhiteSpace(tempvar.vwstring1) ? "" : tempvar.vwstring1;
            cust_table.customer_address = string.IsNullOrWhiteSpace(tempvar.vwstring2) ? "" : tempvar.vwstring2;
            cust_table.cust_city = string.IsNullOrWhiteSpace(tempvar.vwstring7) ? "" : tempvar.vwstring7;
            cust_table.cust_country = string.IsNullOrWhiteSpace(tempvar.vwstring9) ? "" : tempvar.vwstring9;
            cust_table.cust_state = string.IsNullOrWhiteSpace(tempvar.vwstring8) ? "" : tempvar.vwstring8;
            cust_table.cust_phone = string.IsNullOrWhiteSpace(tempvar.vwstring4) ? "" : tempvar.vwstring4;
            cust_table.cust_email = string.IsNullOrWhiteSpace(tempvar.vwstring3) ? "" : tempvar.vwstring3;
            cust_table.title = string.IsNullOrWhiteSpace(tempvar.vwstring11) ? "" : tempvar.vwstring11;
            cust_table.sex = string.IsNullOrWhiteSpace(tempvar.vwstring6) ? "" : tempvar.vwstring6;
            cust_table.passport_number = string.IsNullOrWhiteSpace(tempvar.vwstring5) ? "" : tempvar.vwstring5;
            cust_table.nationality = string.IsNullOrWhiteSpace(tempvar.vwstring10) ? "" : tempvar.vwstring10;
            cust_table.modified_date = DateTime.UtcNow.ToLocalTime();
            cust_table.modified_by = util.find_name("3", "", worksess.userid); ;

            if (action_flag == "Create")
                db.Entry(cust_table).State = EntityState.Added;
            else
                db.Entry(cust_table).State = EntityState.Modified;

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
            var varTitle = from bg in db.msg_file
                           where bg.para_code == "TTL"
                           select bg;
            //ViewBag.ftitle = new SelectList(varTitle.ToList(), "id_code", "desc1", tempvar.vwstring11);
            ViewBag.ftitle = util.all_select_query("GEN", tempvar.vwstring11, "TTL");
            ViewBag.country = util.all_select_query("GEN", tempvar.vwstring3, "CTRY");
        }
    }
}
