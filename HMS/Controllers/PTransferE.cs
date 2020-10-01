using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HMS.Models;
using anchor1.Filters;
using System.IO;
using System.Web.Hosting;
using System.Data;
using HMS.utilities;
using anchor1.utilities;
using CittaErp.ReportA;
using HMS.Report;

namespace HMS.Controllers
{
    public class PTransferEController : Controller
    {
        private MainContext db = new MainContext();
        hutility utils = new hutility();
        worksess worksess;
        vw_genlay tempvar = new vw_genlay();
        hutility util = new hutility();
        string sp = new string(' ', 50);
        string opt;
        string act_type;
        string push1 = "";
        int maxcol = 0;
        bool err_flag = true;
        string qflag = "0";
        HttpPostedFileBase upload;

        private void init_class()
        {

            worksess = (worksess)Session["worksess"];
            tempvar.vwstring0 = "";
            tempvar.vwstrarray0 = new string[10];
            tempvar.vwstrarray1 = new string[10];
            tempvar.vwstrarray2 = new string[10];
            tempvar.vwstrarray3 = new string[10];
            tempvar.vwstrarray4 = new string[10];
            tempvar.vwstrarray5 = new string[10];
            tempvar.vwstrarray9 = new string[10];
        }

      

     
        [HttpPost]
        public ActionResult DailyList(string idx)
        {
            string str1 = "";
            int ctr1 = idx.IndexOf("[]");
            string sno = idx.Substring(0, ctr1);
            int ctr2 = idx.IndexOf("[]", ctr1 + 2);
            string opt = idx.Substring(ctr1 + 2, ctr2 - ctr1 - 2);
            string Code = idx.Substring(ctr2 + 2);

            if (sno == "01")
            {
                if (opt == "TT")
                {
                    str1 = "select calc_code as c1, name1 as c2, '' from tab_calc where para_code = 'TTD' and report_name = " + utils.sqlquote(Code) + " order by name1 ";
                }
                else
                {
                    str1 = "select calc_code as c1, name1 as c2, '' from tab_calc a, tab_soft b where a.para_code = 'TT' and b.para_code='TR' and a.report_name=b.report_code ";
                    str1 += " and a.report_name = " + utils.sqlquote(Code) + " order by name1 ";
                }

                var str2 = db.Database.SqlQuery<tempquery>(str1).OrderBy(u => u.c2);
                if (HttpContext.Request.IsAjaxRequest())
                    return Json(new SelectList(
                                    str2.ToArray(),
                                    "c1",
                                    "c2")
                               , JsonRequestBehavior.AllowGet);
            }
            else if (sno == "02")
            {
                List<SelectListItem> loan1 = new List<SelectListItem>();
                SelectListItem ln2;

                if (opt == "TT")
                {
                    ln2 = new SelectListItem() { Value = "staff", Text = "Staff Number" };
                    loan1.Add(ln2);
                }
                else
                {
                    var slist = (from s1 in db.msg_file
                                 where s1.para_code == "TR" && s1.id_code == Code
                                 select s1).FirstOrDefault();
                    if (slist != null)
                    {
                        ln2 = new SelectListItem() { Value = "staff", Text = slist.desc4.Trim() };
                        loan1.Add(ln2);
                        ln2 = new SelectListItem() { Value = "code", Text = slist.desc5.Trim() };
                        loan1.Add(ln2);
                    }
                }

                if (HttpContext.Request.IsAjaxRequest())
                    return Json(new SelectList(
                                    loan1.ToArray(),
                                    "Value",
                                    "Text")
                               , JsonRequestBehavior.AllowGet);
            }


            return RedirectToAction("Index", null, new { anc = Ccheckg.convert_pass2("pc=") });
        }
        [EncryptionActionAttribute]
        public ActionResult preport(string pc = null)

        {

            worksess = (worksess)Session["worksess"];
            worksess.idrep = "";
            Session["worksess"] = worksess;
            tempvar.vwstring7 = DateTime.UtcNow.ToLocalTime().ToString("dd/MM/yyyy");
            tempvar.vwstring6 = "01" + tempvar.vwstring7.Substring(2);

            string str2 = "";
            //string str1 = "select report_name1 c1, report_name2 c2, report_name3 c3,report_name6 c6, report_name7 c7 from tab_soft where para_code='LOG' and report_code=" + utils.sqlquote(pc);

            string str1 = "select desc1 c1, desc2 c2, desc3 c3, desc4 c4, desc5 c5,desc6 c6, desc8 c7 from msg_file where para_code = 'REPCODE' and id_code =" + util.sqlquote(pc);
            var rec3 = db.Database.SqlQuery<tempquery>(str1).FirstOrDefault();
            if (rec3 != null)
            {
                tempvar.vwstring1 = string.IsNullOrWhiteSpace(rec3.c2)?"":rec3.c2;
                tempvar.vwstring2 = string.IsNullOrWhiteSpace(rec3.c3) ? "" : rec3.c3;
                utils.htitles(rec3.c1);
                str1 = (string.IsNullOrWhiteSpace(rec3.c6) ? "" : rec3.c6).Trim();
                str2 = (string.IsNullOrWhiteSpace(rec3.c7) ? "" : rec3.c7).Trim();
                //                If dcode = "H23" Then get_location
            }
            else
            {
                tempvar.vwstring1 = "Staff Number ";
                if (pc == "SKILL")
                    utils.htitles("Staff Skill Transaction Report");

                str1 = "select staff_number, staff_name from tab_staff ";
                str1 = "select desc1 c1, desc2 c2, desc3 c3, desc4 c4, desc5 c5 from msg_file where para_code = 'REPCODE' and id_code =" + util.sqlquote(pc);
            }


            var str4 = db.Database.SqlQuery<tempquery>(str1);
            ViewBag.codequery = new SelectList(str4.ToList(), "c2", "c4");
            if (str2 != "")
            {
                var str5 = db.Database.SqlQuery<tempquery>(str2);
                ViewBag.numquery = new SelectList(str5.ToList(), "c1", "c2");
            }
            
            tempvar.vwstring0 = pc;
            if (pc.Substring(0,2) == "05")
                ViewBag.showdate = "Y";
            return View(tempvar);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult preport(vw_genlay tempvar1)
        {
            worksess = (worksess)Session["worksess"];
            tempvar = tempvar1;
            if (string.IsNullOrWhiteSpace(tempvar.vwstring4))
            {
                tempvar.vwstring4 = "";
                tempvar.vwstring5 = "";
            }

            if (string.IsNullOrWhiteSpace(tempvar.vwstring6))
            {
                tempvar.vwstring6 = "";
                tempvar.vwstring7 = "";
            }
            tempvar.vwstring6 = utils.date_yyyymmdd(tempvar.vwstring6);
            tempvar.vwstring7 = utils.date_yyyymmdd(tempvar.vwstring7);
            if (string.IsNullOrWhiteSpace(tempvar.vwstring5))
                tempvar.vwstring5 = tempvar.vwstring4;
            if (string.IsNullOrWhiteSpace(tempvar.vwstring7))
                tempvar.vwstring7 = tempvar.vwstring6;

            push1 = (tempvar.vwstring0 + sp).Substring(0, 10) + (tempvar.vwstring4 + sp).Substring(0, 10) + (tempvar.vwstring5 + sp).Substring(0, 10);
            push1 += (tempvar.vwstring6 + sp).Substring(0, 10) + (tempvar.vwstring7 + sp).Substring(0, 10);

            string str1 = "execute preport @select_str=" + utils.sqlquote(push1) + ", @p_userid=" + utils.sqlquote(worksess.userid);
            db.Database.ExecuteSqlCommand(str1);

            // to convert date to zone date
            //            zonedate_update(tempvar.vwstring0);

            //string tr1 = Session["id"].ToString();
            var str2 = (from bg in db.msg_file
                        where bg.para_code == "REPCODE" && bg.id_code == tempvar.vwstring0
                        select bg).FirstOrDefault();

            //utils.htitles(str2.desc1);

            return RedirectToAction("coldisp");

        }

        public ActionResult coldisp()
        {
            worksess = (worksess)Session["worksess"];
            string repname = Url.Action("View1");

            if (worksess.viewflag == null)
                worksess.viewflag = "0";
            worksess.filep = "print/zx" + worksess.userid + ".rdf";
            //var me = psess.sarrayt0[4];

            int vflag = Convert.ToInt16(worksess.viewflag);
            if (worksess.temp6 == "1")
            {
                CReceipt report = new CReceipt();
                report.Run();
                report.Document.Dispose();
                report.Dispose();
                report = null;
            }
            else if(worksess.temp6 == "2") {
                IReceipt report = new IReceipt();
                report.Run();
                report.Document.Dispose();
                report.Dispose();
                report = null;
            }
            else
            {
                Sreport1 report = new Sreport1();
                report.Run();
                report.Document.Dispose();
                report.Dispose();
                report = null;
            }

            worksess.temp5 = repname;
                Session["psess"] = worksess;

                string str1 = " exec delete_temp_tables @p_userid=" + util.sqlquote(worksess.userid);
                db.Database.ExecuteSqlCommand(str1);
                return View();
        }


        [HttpPost]
        public ActionResult DailyList1()
        {
            string str1 = "";
            worksess = (worksess)Session["worksess"];
            double all_ctr = 1;

            List<SelectListItem> alrow = new List<SelectListItem>();

            str1 = "<table class='display dataprintv ' ><thead class=' color-blue'>"; // + colhead;
            alrow.Add(new SelectListItem { Text = str1, Value = "0" });

            str1 = "select report_column c1, row_seq_id intc1 from [" + worksess.userid + "sta01] order by row_seq_id ";
            var str3 = db.Database.SqlQuery<tempquery>(str1);
            var str4 = str3.ToList();
            foreach (var item in str4)
            {
                str1 = "<tr class='text-nowrap '>" + item.c1 + "</tr>";
                if (item.intc1 == 0)
                    str1 += "</thead><tbody>";
                alrow.Add(new SelectListItem { Text = str1, Value = all_ctr.ToString() });
                all_ctr++;
            }

            alrow.Add(new SelectListItem { Text = "</tbody></table>", Value = "999999" });

            if (HttpContext.Request.IsAjaxRequest())
            {
                var jsonresult = Json(alrow
                               , JsonRequestBehavior.AllowGet);
                jsonresult.MaxJsonLength = int.MaxValue;
                return jsonresult;
            }

            return RedirectToAction("Index");   //, null, new { anc = Ccheckg.convert_pass2("pc=") });
        }

        public ActionResult View1()
        {
            return View();
        }

    }
}
