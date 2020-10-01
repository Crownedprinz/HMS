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
    public class ITranstController : Controller
    {
        // GET: Transt
        MainContext db = new MainContext();
        vw_genlay tempvar = new vw_genlay();
        bool err_flag = true;
        string action_flag = "";
        item_trans item_trans = new item_trans();
        stock_details stock_details = new stock_details();
        hutility util = new hutility();
        worksess worksess;
        string ptype = "";
        string str1 = "";
        char sp = '"';
        queryhead qheader = new queryhead();

        private void initial_rtn()
        {
            tempvar.vwdclarray0 = new decimal[50];
            tempvar.vwstrarray0 = new string[20];
            tempvar.vwstrarray1 = new string[20];
            tempvar.vwstrarray2 = new string[20];
            tempvar.vwstrarray6 = new string[20];
            tempvar.vwdclarray0 = new decimal[20];
            tempvar.vwstring3 = DateTime.UtcNow.ToLocalTime().ToString("dd/MM/yyyy");
            for (int wctr = 0; wctr < 20; wctr++)
                tempvar.vwstrarray0[wctr] = "";

            //glay.vwstrarray0[2] = "0" + DateTime.UtcNow.ToLocalTime().Month.ToString();
            tempvar.vwstrarray0[2] = DateTime.UtcNow.ToLocalTime().ToString("dd/MM/yyyy");
            
            tempvar.vwlist0 = new List<tempquery>[20];
            if (action_flag.IndexOf("Header") > 0)
            {
                tempvar.vwstrarray0[0] = DateTime.UtcNow.ToLocalTime().ToString("dd/MM/yyyy");
                string sqlstr = "select isnull(max(bill_no),0) vwint1 from item_trans";
                var sql1 = db.Database.SqlQuery<vw_genlay>(sqlstr).FirstOrDefault();
                tempvar.vwint0= sql1.vwint1 + 1;
            }



        }

        
        [EncryptionActionAttribute]
        public ActionResult CreateDetails(string pc)
        {
            pc = "";
            if (pc == null)
                return RedirectToAction("Welcome", "home");

            ViewBag.action_flag = "CreateDetails";
            action_flag = "CreateDetails";
            worksess = (worksess)Session["worksess"];
            qheader = (queryhead)Session["qheader"];
            worksess.idrep = "";
            initial_rtn();

            select_query_head("D");

            read_details();
            return View(tempvar);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateDetails(vw_genlay tempvar_in, string headtype)
        {
            worksess = (worksess)Session["worksess"];
            //ptype = worksess.temp0.ToString();
            qheader = (queryhead)Session["qheader"];
            ViewBag.action_flag = "CreateDetails";
            action_flag = "CreateDetails";
            worksess.idrep = "";
            tempvar = tempvar_in;
            update_file(headtype);

            if (err_flag)
            {
                Session["tempvar"] = tempvar;
                return RedirectToAction("CreateDetails", null, new { anc = Ccheckg.convert_pass2("pc=1") });
            }

            read_details();
            select_query_head("D");

            return View(tempvar);

        }

      
        private void read_details()
        {
            string str1 = "select ";
            str1 += " vwstring2 = bh.transaction_date,";
            str1 += " vwint0 = bh.sequence_no, ";
            str1 += " vwstring0 = Bloyd.find_name('1','003',bh.item_id), ";
            str1 += " vwint1 = bh.quantity, ";
            str1 += " vwstring1 = bh.item_id, ";
            str1 += " vwstring3 = (select uom from ordering_table e where e.item_id =bh.item_id) ";
            str1 += " from stock_details bh";
            ViewBag.x1 = db.Database.SqlQuery<vw_genlay>(str1).ToList();

        }

    
      [HttpPost]
        public ActionResult delete_list(string id)
        {
            // write your query statement
            string sqlstr = "delete from [dbo].[item_trans] where sequence_no=0 and cast(bill_no as varchar)=" + util.sqlquote(id);
            db.Database.ExecuteSqlCommand(sqlstr);
            sqlstr = "delete from [dbo].[stock_details] where cast(bill_no as varchar)=" + util.sqlquote(id);
            db.Database.ExecuteSqlCommand(sqlstr);
            return RedirectToAction("Index", null, new { anc = Ccheckg.convert_pass2("pc=1 ") });
        }

        [HttpPost]
        public ActionResult delete_detail(string id)
        {
            // write your query statement
            string  sqlstr = "delete from [dbo].[stock_details] where cast(sequence_no as varchar)=" + util.sqlquote(id);
            db.Database.ExecuteSqlCommand(sqlstr);
            return RedirectToAction("CreateDetails", null, new { anc = Ccheckg.convert_pass2("pc=1 ") });
        }

        private void delete_record()
        {
            stock_details = db.stock_details.Find(tempvar.vwint0, tempvar.vwint1);
            if (stock_details != null)
            {
                db.stock_details.Remove(stock_details);
                db.SaveChanges();
            }
        }
        private void select_query_head(string type1)
        {

           
                ViewBag.item = util.all_select_query("003", tempvar.vwstring1,"B");

        }


        private void update_file(string headtype)
        {
            err_flag = true;
            validation_routine(headtype);


            if (err_flag)
                update_record(headtype);

        }

        private void validation_routine(string headtype)
        {
            string cr_flag = action_flag;
           
           
                if (!util.date_validate(tempvar.vwstring3))
                {
                    ModelState.AddModelError(String.Empty, "Please insert a Valid transaction  date");
                    err_flag = false;
                    worksess.idrep = "";
                }
        }


        private void update_record(string headtype)
        {
            string cr_flag = action_flag;
                if (cr_flag == "CreateDetails")
                {
                    stock_details = new stock_details();
                    stock_details.created_by = util.find_name("3", "", worksess.userid); ;
                    stock_details.created_date = DateTime.UtcNow.ToLocalTime();
                }
                else
                {
                    stock_details = db.stock_details.Find(qheader.intquery0, tempvar.vwint2);
                }

                stock_details.sequence_no = tempvar.vwint2;
                stock_details.item_id = tempvar.vwstring1;
                stock_details.quantity = tempvar.vwint1;
                stock_details.transaction_date = string.IsNullOrWhiteSpace(tempvar.vwstring3) ? "" : util.date_yyyymmdd(tempvar.vwstring3);
                stock_details.modified_date = DateTime.UtcNow.ToLocalTime();
                stock_details.modified_by = util.find_name("3", "", worksess.userid); ;
                if (cr_flag == "CreateDetails")
                    db.Entry(stock_details).State = EntityState.Added;
                else
                    db.Entry(stock_details).State = EntityState.Modified;


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
            if (cr_flag == "CreateDetails" && err_flag)
            {

                str1 = "update ordering_table set quantity= quantity+"+ tempvar.vwint1+" where item_id ="+Convert.ToInt32(tempvar.vwstring1)+" and purpose = 'B'";
                db.Database.ExecuteSqlCommand(str1);
            }


            }

        [HttpPost]
        public ActionResult get_items(string id)
        {

            int id1 = Convert.ToInt32(id);
            var bglist = (from bg in db.ordering_table
                            where bg.item_id == id1
                            select bg).FirstOrDefault();
            List<SelectListItem> ary = new List<SelectListItem>();
            if (bglist != null)
            {
                ary.Add(new SelectListItem { Value = "item_uom", Text = bglist.uom });
                ary.Add(new SelectListItem { Value = "item_qty", Text = bglist.quantity.ToString() });
            }
            if (HttpContext.Request.IsAjaxRequest())
                return Json(new SelectList(
                                ary.ToArray(),
                                "Value",
                                "Text")
                               , JsonRequestBehavior.AllowGet);
            return View();
        }
  
    }
}