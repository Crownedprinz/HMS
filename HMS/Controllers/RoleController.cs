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
    public class RoleController : Controller
    {

        MainContext db = new MainContext();
        vw_genlay tempvar = new vw_genlay();
        bool err_flag = true;
        worksess worksess;
        string action_flag = "";
        string str1 = "";
        role_table role_table = new role_table();
        hutility util = new hutility();
        string type_code = "";
        
        [EncryptionActionAttribute]
        public ActionResult Index(string pc = null)
        {
            if (pc == null)
                return RedirectToAction("Index", "Dashboard");
            worksess = (worksess)Session["worksess"];
            worksess.idrep = "";
            Session["worksess"] = worksess;
            type_code = pc;

            var blist = from s in db.role_table
                        where (s.flag == "H")
                        orderby s.Role_description
                        select new vw_genlay
                        {
                            vwstring0 = s.role_id,
                            vwstring1 = s.Role_description,
                            vwstring2 = s.created_by
                        };

            return View(blist.ToList());
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
        public ActionResult Create(vw_genlay tempvar_in, string[] snumber2)
        {
            ViewBag.action_flag = "Create";
            action_flag = "Create";
            worksess = (worksess)Session["worksess"];
            tempvar = tempvar_in;

            select_write(snumber2);
            update_file();

            if (err_flag)
                return RedirectToAction("Create", null, new { anc = Ccheckg.convert_pass2("pc=1") });

            select_query();
            return View("Edit", tempvar);
        }

        [EncryptionActionAttribute]
        public ActionResult Edit(string key1)
        {
            ViewBag.action_flag = "Edit";
            action_flag = "Edit";
            ViewBag.getaction = "HEdit";
            worksess = (worksess)Session["worksess"];
            role_table = db.role_table.Find(key1,"H","");
            if (role_table != null)
                read_record();

            select_query();
            return View(tempvar);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(vw_genlay tempvar_in, string[] snumber2, string id_xhrt)
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

            select_write(snumber2);
            update_file();
            if (err_flag)
                return RedirectToAction("Index", null, new { anc = Ccheckg.convert_pass2("pc=1") });

            select_query();
            return View(tempvar);
        }

        private void delete_record()
        {
            //role_table = db.role_table.Find(tempvar.vwstring0,"H",);
            //if (role_table != null)
            //{
            //    db.role_table.Remove(role_table);
            //    db.SaveChanges();
            //}
            // write your query statement
            string sqlstr = "delete from role_table where role_id ='" + tempvar.vwstring0 + "'";
            db.Database.ExecuteSqlCommand(sqlstr);
        }

        [HttpPost]
        public ActionResult delete_list(string id)
        {
            // write your query statement
            string sqlstr = "delete from role_table where role_id ='" + id + "'";
            int delctr = db.Database.ExecuteSqlCommand(sqlstr);
            return RedirectToAction("Index", null, new { anc = Ccheckg.convert_pass2("pc=1") });
        }


        private void read_record()
        {
            tempvar.vwstring0 = role_table.role_id;
            tempvar.vwstring1 = role_table.Role_description;
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
                ModelState.AddModelError(String.Empty, " ID must not be spaces");
                err_flag = false;
            }

            if (action_flag == "Create") { 
            role_table = db.role_table.Find(tempvar.vwstring0,"H","");
            if (role_table != null)
            {
                ModelState.AddModelError(String.Empty, "ID already Created, Input another one");
                err_flag = false;
            }
            }

        }
        private void update_record()
        {
            if (action_flag == "Create")
            {
                role_table = new role_table();
                role_table.created_by = util.find_name("3", "", worksess.userid); ;
                role_table.created_date = DateTime.UtcNow.ToLocalTime();
            }
            else
            {
                role_table = db.role_table.Find(tempvar.vwstring0,"H","");
            }


            role_table.role_id = string.IsNullOrWhiteSpace(tempvar.vwstring0) ? "" : tempvar.vwstring0;
            role_table.Role_description = string.IsNullOrWhiteSpace(tempvar.vwstring1) ? "" : tempvar.vwstring1;
            role_table.menu_option = "";
            role_table.flag = "H";
            role_table.modified_date = DateTime.UtcNow.ToLocalTime();
            role_table.modified_by = util.find_name("3", "", worksess.userid); ;

           
            if (action_flag == "Create")
                db.Entry(role_table).State = EntityState.Added;
            else
                db.Entry(role_table).State = EntityState.Modified;

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
            ViewBag.role = util.all_select_query("GEN", tempvar.vwstring2, "ROL");
            var bglist = from bg in db.role_table
                         join bh in db.msg_file
                         on new {a1 = bg.menu_option, a2 = "ITEM"} equals new {a1 = bh.id_code, a2 = bh.para_code}
                         into bh1 
                         from bh2 in bh1.DefaultIfEmpty()
                         where bg.flag == "D" && bg.role_id == tempvar.vwstring0
                         select new { c1 = bg.menu_option, c2 = bh2.desc1 };
            ViewBag.list2 = new SelectList(bglist.ToList(), "c1", "c2");

            str1 = "select id_code c1, desc1 c2 from msg_file  ";
            str1 += " where para_code = 'ITEM' and not id_code in (select menu_option from role_table b where ";
            str1 += " flag='D' and role_id=" + util.sqlquote(tempvar.vwstring0) + ") ";
            str1 += " order by 2";

            var str2 = db.Database.SqlQuery<tempquery>(str1);
            ViewBag.list1 = new SelectList(str2.ToList(), "c1", "c2");
            
        }

        private void select_write(string[] snumber2)
        {
            str1 = "delete role_table  where flag = 'D' and  role_id=" + util.sqlquote(tempvar.vwstring0);
            db.Database.ExecuteSqlCommand(str1);

           
                foreach (var bh in snumber2)
                {
                    str1 = "insert into role_table(flag,role_id,menu_option) values ('D'," + util.sqlquote(tempvar.vwstring0)+ ",";
                    str1 += util.sqlquote(bh) +") ";
                    db.Database.ExecuteSqlCommand(str1);
                }
            }

        }
    }
