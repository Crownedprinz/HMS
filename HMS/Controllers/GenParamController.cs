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
    public class GenParamController : Controller
    {
        MainContext db = new MainContext();
        vw_genlay tempvar = new vw_genlay();
        worksess worksess;
        bool err_flag = true;
        string action_flag = "";
        msg_file msg_file = new msg_file();
        hutility utils = new hutility();

        [EncryptionActionAttribute]
        public ActionResult Index(string pc = null)
        {
           if (pc == null)
                return RedirectToAction("Index", "Dashboard");
            worksess = (worksess)Session["worksess"];
            worksess.idrep = "";
            Session["worksess"] = worksess;
            if (pc != "1")
                worksess.temp7 = pc;
            else
                pc = worksess.temp7;
            set_titles(pc);
            Session["worksess"] = worksess;

            var bglist = from bg in db.msg_file
                         where bg.para_code == worksess.temp7
                         select new vw_genlay
                         {
                             vwstring0 = bg.id_code,
                             vwstring1 = bg.desc1,
                             vwstring2 = bg.desc2,
                             vwstring3 = bg.desc3
                         };
            return View(bglist.ToList());
        }
        [EncryptionActionAttribute]
        public ActionResult Create(string pc = null)
        {
            if (pc == null)
                return RedirectToAction("Index", "Dashboard");
            worksess = (worksess)Session["worksess"];
            tempvar.vwstring0 = worksess.temp0;
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
        public ActionResult Edit(string key1, string key2)
        {
            ViewBag.action_flag = "Edit";
            action_flag = "Edit";worksess = (worksess)Session["worksess"];
            tempvar.vwstring0 = worksess.temp0;
            msg_file = db.msg_file.Find(key1, key2);
            set_titles(tempvar.vwstring0);
            Session["worksess"] = worksess;
            if (msg_file != null)
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
            msg_file = db.msg_file.Find(worksess.temp7,tempvar.vwstring0);
            if (msg_file != null)
            {
                db.msg_file.Remove(msg_file);
                db.SaveChanges();
            }
        }

        [HttpPost]
        public ActionResult delete_list(string id)
        {
            worksess = (worksess)Session["worksess"];
            // write your query statement
            string sqlstr = "delete from msg_file where para_code='"+ worksess.temp7+"' and id_code ='" + id + "'";
            int delctr = db.Database.ExecuteSqlCommand(sqlstr);
            return RedirectToAction("Index", null, new { anc = Ccheckg.convert_pass2("pc=1") });
        }

        private void read_record()
        {
            
            tempvar.vwstring0 = msg_file.id_code;
            tempvar.vwstring1 = msg_file.desc1;
            tempvar.vwstring2 = msg_file.desc2;
            tempvar.vwstring3 = msg_file.desc3;
            tempvar.vwstring4 = msg_file.desc4;
            tempvar.vwstring5 = msg_file.desc5;
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
                msg_file = new msg_file();
                msg_file.created_by = utils.find_name("3", "", worksess.userid); ;
                msg_file.created_date = DateTime.UtcNow.ToLocalTime();
            }
            else
            {
                msg_file = db.msg_file.Find(worksess.temp7,tempvar.vwstring0);
            }


            msg_file.para_code = string.IsNullOrWhiteSpace(worksess.temp7) ? "" : worksess.temp7;
            msg_file.id_code = string.IsNullOrWhiteSpace(tempvar.vwstring0) ? "" : tempvar.vwstring0;
            msg_file.desc1 = string.IsNullOrWhiteSpace(tempvar.vwstring1) ? "" : tempvar.vwstring1;
            msg_file.desc2 = string.IsNullOrWhiteSpace(tempvar.vwstring2) ? "" : tempvar.vwstring2;
            msg_file.desc3 = string.IsNullOrWhiteSpace(tempvar.vwstring3) ? "" : tempvar.vwstring3;
            msg_file.desc4 = string.IsNullOrWhiteSpace(tempvar.vwstring5) ? "" : tempvar.vwstring5;
            msg_file.desc5= string.IsNullOrWhiteSpace(tempvar.vwstring6) ? "" : tempvar.vwstring6;
            msg_file.desc6 = "";
            msg_file.desc7 = "";
            msg_file.desc8 = "";
            msg_file.modified_date = DateTime.UtcNow.ToLocalTime();
            msg_file.modified_by = utils.find_name("3", "", worksess.userid); ;

            if (action_flag == "Create")
                db.Entry(msg_file).State = EntityState.Added;
            else
                db.Entry(msg_file).State = EntityState.Modified;

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
            ViewBag.ftitle = utils.all_select_query("GEN", tempvar.vwstring0, worksess.temp7);
              }
        private void set_titles(string val)
        {
            if (val == "RMT")
            {
                    worksess.ptitle = "Room Type";
                    worksess.temp1= "Room Type ID";
                    worksess.temp2 = "Room Type";
                    worksess.temp3 = "Description";
                    worksess.temp4 = "Price";
            }
        }
    }
}