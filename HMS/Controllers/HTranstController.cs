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
    public class HTranstController : Controller
    {
        // GET: Transt
        MainContext db = new MainContext();
        vw_genlay tempvar = new vw_genlay();
        bool err_flag = true;
        string action_flag = "";
        item_trans item_trans = new item_trans();
        trans_details trans_details = new trans_details();
        hutility util = new hutility();
        worksess worksess;
        string ptype = "";
        string str1 = "";
        char sp = '"';
        queryhead qheader = new queryhead();

        public ActionResult Index(string pc = null)
        {

            pc = "";
            worksess = (worksess)Session["worksess"];
            worksess.idrep = "";
            Session["worksess"] = worksess;
            if (pc != "")
            {
                worksess.temp0 = pc;
                ptype = pc;
            }

            //ptype = worksess.temp0.ToString();

            string query = "select a.bill_no vwint0, a.guest_id vwint1, isnull(Bloyd.find_name('1','004',a.guest_id),'') vwstring1, Bloyd.date_out(a.transaction_date) vwstring2, a.created_by vwstring3" +
                " from item_trans a order by a.transaction_date asc,bill_no";
            var bglist2 = db.Database.SqlQuery<vw_genlay>(query);
            return View(bglist2.OrderByDescending(x => x.vwstring2).ToList());
        }

        [EncryptionActionAttribute]
        public ActionResult CreateHeader(string pc)
        {
            pc = "";
            if (pc == null)
                return RedirectToAction("Welcome", "home");

            worksess = (worksess)Session["worksess"];
            worksess.idrep = "";
            ViewBag.action_flag = "CreateHeader";
            action_flag = "CreateHeader";

            initial_rtn();
            select_query_head("H");

            return View("EditDetails", tempvar);
        }

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
            tempvar.vwint1 = 1;
            tempvar.vwlist0 = new List<tempquery>[20];
            if (action_flag.IndexOf("Header") > 0)
            {
                tempvar.vwstrarray0[0] = DateTime.UtcNow.ToLocalTime().ToString("dd/MM/yyyy");
                string sqlstr = "select isnull(max(bill_no),0) vwint1 from item_trans";
                var sql1 = db.Database.SqlQuery<vw_genlay>(sqlstr).FirstOrDefault();
                tempvar.vwint0= sql1.vwint1 + 1;
            }



        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateHeader(vw_genlay tempvar_in, string headtype)
        {
            worksess = (worksess)Session["worksess"];
            //ptype = worksess.temp0.ToString();
            ViewBag.action_flag = "CreateHeader";
            action_flag = "CreateHeader";
            tempvar = tempvar_in;
            worksess.idrep = "";
            update_file("H");

            if (err_flag)
            {
                return RedirectToAction("CreateDetails", null, new { anc = Ccheckg.convert_pass2("pc=1") });
            }

            select_query_head("H");

            return View("EditDetails", tempvar);

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
            //ptype = worksess.temp0.ToString();
            qheader = (queryhead)Session["qheader"];
            initial_rtn();

            detail_intial();
            select_query_head("D");

            show_screen_info();

            return View("EditDetails", tempvar);
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
                getDialogue(tempvar);
                Session["tempvar"] = tempvar;
                return RedirectToAction("CreateDetails", null, new { anc = Ccheckg.convert_pass2("pc=1") });
            }

            show_screen_info();
            select_query_head("D");

            return View("EditDetails", tempvar);

        }

        [EncryptionActionAttribute]
        public ActionResult Edit(string key1)
        {
            int key2 = 0;
            int.TryParse(key1, out key2);
            ViewBag.action_flag = "Create";
            worksess = (worksess)Session["worksess"];
            worksess.idrep = "";

            item_trans = db.item_trans.Find(0, key2);
            if (item_trans != null)
            {
                set_qheader();
                return RedirectToAction("CreateDetails", null, new { anc = Ccheckg.convert_pass2("pc=1") });
            }
            return View("EditDetails", tempvar);

        }

        private void set_qheader()
        {
            qheader.intquery0 = item_trans.bill_no;
            qheader.query6 = util.date_slash(item_trans.transaction_date);
            qheader.intquery1 = item_trans.guest_id;

            Session["qheader"] = qheader;
        }

        private void show_screen_info()
        {
            display_header();
            read_details();

        }
        private void read_details()
        {
            string str1 = "select ";
            str1 += " vwstring2 = bh.transaction_date,";
            str1 += " vwint0 = bh.bill_no, ";
            str1 += " vwint1 = bh.sequence_no, ";
            str1 += " vwstring0 = Bloyd.find_name('1','003',bh.item_id), ";
            str1 += " vwint2 = bh.quantity, ";
            str1 += " vwdecimal1 = discount_flat, ";
            str1 += " vwdecimal4 = discount_per, ";
            str1 += " vwdecimal2 = bh.discount_amount, ";
            str1 += " vwdecimal3 = bh.total_amount, ";
            str1 += " vwdecimal0 = bh.amount, ";
            str1 += " vwstring10 = bh.item_id ";
            str1 += " from trans_details bh where bh.bill_no = " + qheader.intquery0.ToString();
            ViewBag.x1 = db.Database.SqlQuery<vw_genlay>(str1).ToList();

        }

        private void display_header()
        {
            var bg2list = (from bh in db.item_trans
                           join bg in db.checkin_table
                           on new { a1 = bh.guest_id } equals new { a1 = bg.guest_id }
                           into bg1
                           from bg2 in bg1.DefaultIfEmpty()
                           where bh.bill_no == qheader.intquery0
                           select new { bh, bg2 }).FirstOrDefault();

            vw_genlay glayhead = new vw_genlay();
            if (bg2list != null)
            {
                glayhead.vwstrarray1 = new string[20];
                glayhead.vwstrarray1[0] = bg2list.bh.bill_no.ToString();
                glayhead.vwstrarray1[1] = util.date_slash(bg2list.bh.transaction_date);
                if(bg2list.bg2!=null)
                glayhead.vwstrarray1[2] = bg2list.bg2.room_number;
                glayhead.vwstrarray1[3] = bg2list.bh.guest_id.ToString();
                if (bg2list.bg2 != null)
                    glayhead.vwstrarray1[4] = bg2list.bg2.first_name + " " + bg2list.bg2.surname;
                //string sqlstr = "select  IsNull(sum(case amount_type when 'D' then (base_amount) else 0 end),0) dbt, IsNull(sum(case amount_type when 'C' then (base_amount) else 0 end),0) crdt from AP_002_VTRAD where document_number=" + qheader.intquery0;
                //var str = db.Database.SqlQuery<addr>(sqlstr).FirstOrDefault();
                //decimal cntrl = str.dbt - str.crdt;
                //glayhead.vwstrarray1[4] = str.dbt.ToString("#,##0.00");
                //glayhead.vwstrarray1[5] = str.crdt.ToString("#,##0.00");
                //glayhead.vwstrarray1[6] = cntrl.ToString();
                //glayhead.vwstrarray1[7] = bg2list.batch_information;
                //glayhead.vwstrarray1[8] = util.period_convert2(bg2list.period);

                //string str1 = "select bank_code + ' - ' + bank_name query0 from vw_allcust where qcode=" + util.sqlquote(bg2list.module_account_type);
                //str1 += " and bank_code=" + util.sqlquote(bg2list.transaction_code);
                //var bh2 = db.Database.SqlQuery<querylay>(str1).FirstOrDefault();
                //if (bh2 != null)
                //glayhead.vwstrarray1[0] = bh2.query0;

            }
            ViewBag.x2 = glayhead;


        }
        public ActionResult EditHeader(string pc)
        {
            pc = "";
            if (pc == null)
                return RedirectToAction("Dashboard", "home");

            worksess = (worksess)Session["worksess"];
            worksess.idrep = "";
            ViewBag.action_flag = "EditHeader";
            action_flag = "EditHeader";
            qheader = (queryhead)Session["qheader"];
            initial_rtn();
            item_trans = db.item_trans.Find(qheader.intquery0);
            if (item_trans != null)
                read_header();

            select_query_head("H");

            return View("EditDetails", tempvar);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditHeader(vw_genlay tempvar_in, string headtype)
        {
            worksess = (worksess)Session["worksess"];
            ptype = worksess.temp0.ToString();
            worksess.idrep = "";
            qheader = (queryhead)Session["qheader"];
            ViewBag.action_flag = "EditHeader";
            action_flag = "EditHeader";
            tempvar = tempvar_in;
            update_file(headtype);

            if (err_flag)
            {
                return RedirectToAction("CreateDetails", null, new { anc = Ccheckg.convert_pass2("xy=1") });
            }

            select_query_head("H");

            return View("EditDetails", tempvar);

        }

        [EncryptionActionAttribute]
        public ActionResult EditDetails(string key1)
        {
            ViewBag.action_flag = "EditDetails";
            action_flag = "EditDetails";
            worksess = (worksess)Session["worksess"];
            worksess.idrep = "";
            worksess = (worksess)Session["worksess"];
            qheader = (queryhead)Session["qheader"];
            //ptype = worksess.temp0.ToString();
            int key2 = 0;
            int.TryParse(key1, out key2);
            trans_details = db.trans_details.Find(qheader.intquery0, key2);
            if (trans_details == null)
                return View(tempvar);


            initial_rtn();
            detail_intial();
            move_detail();
            select_query_head("D");
            show_screen_info();
            ModelState.Remove("vwdecimal1");
            return View("EditDetails", tempvar);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditDetails(vw_genlay tempvar_in, string headtype, string id_xhrt)
        {
            worksess = (worksess)Session["worksess"];
            qheader = (queryhead)Session["qheader"];
            ViewBag.action_flag = "EditDetails";
            action_flag = "EditDetails";
            worksess.idrep = "";
            tempvar = tempvar_in;
            if (id_xhrt == "D")
            {
                delete_record();
                return RedirectToAction("CreateDetails", null, new { anc = Ccheckg.convert_pass2("xy= ") });
            }

            update_file(headtype);

            if (err_flag)
            {
                return RedirectToAction("CreateDetails", null, new { anc = Ccheckg.convert_pass2("xy=1") });
            }


            //detail_intial();
            show_screen_info();
            select_query_head("D");

            return View("EditDetails", tempvar);

        }
        [HttpPost]
        public ActionResult delete_list(string id)
        {
            // write your query statement
            string sqlstr = "delete from [dbo].[item_trans] where sequence_no=0 and cast(bill_no as varchar)=" + util.sqlquote(id);
            db.Database.ExecuteSqlCommand(sqlstr);
            sqlstr = "delete from [dbo].[trans_details] where cast(bill_no as varchar)=" + util.sqlquote(id);
            db.Database.ExecuteSqlCommand(sqlstr);
            return RedirectToAction("Index", null, new { anc = Ccheckg.convert_pass2("pc=1 ") });
        }

        [HttpPost]
        public ActionResult delete_detail(string id)
        {
            // write your query statement
            string  sqlstr = "delete from [dbo].[trans_details] where cast(bill_no as varchar) +'[]'+  cast(sequence_no as varchar)=" + util.sqlquote(id);
            db.Database.ExecuteSqlCommand(sqlstr);
            return RedirectToAction("Index", null, new { anc = Ccheckg.convert_pass2("pc=1 ") });
        }

        private void delete_record()
        {
            trans_details = db.trans_details.Find(tempvar.vwint0, tempvar.vwint1);
            if (trans_details != null)
            {
                db.trans_details.Remove(trans_details);
                db.SaveChanges();
            }
        }
        private void read_header()
        {
            var bg2list = (from bh in db.item_trans
                           join bg in db.checkin_table
                           on new { a1 = bh.guest_id } equals new { a1 = bg.guest_id }
                           into bg1
                           from bg2 in bg1.DefaultIfEmpty()
                           where bh.bill_no == qheader.intquery0
                           select new { bh, bg2 }).FirstOrDefault();

            if (bg2list != null)
            {
                tempvar.vwstrarray1[0] = bg2list.bh.bill_no.ToString();
                tempvar.vwstrarray1[1] = util.date_out(bg2list.bh.transaction_date);
                tempvar.vwstrarray1[2] = bg2list.bg2.room_number;
                tempvar.vwstrarray1[3] = bg2list.bh.guest_id.ToString();
                tempvar.vwstrarray1[4] = bg2list.bg2.first_name + " " + bg2list.bg2.surname;
            }

        }
        private void detail_intial()
        {
            tempvar.vwstrarray0 = new string[20];
            tempvar.vwstrarray2 = new string[20];
            tempvar.vwstring1 = qheader.query0;
            tempvar.vwstring7 = qheader.query7;
            tempvar.vwstring3 = qheader.query6;
            tempvar.vwstring10 = "N";
            tempvar.vwdecimal1 = qheader.dquery0;
            tempvar.vwstrarray6 = qheader.sarray4;
            tempvar.vwstrarray0[0] = qheader.query8;

        }
        private void select_query_head(string type1)
        {

            if (type1 == "H")
            {

                ViewBag.guestid = util.all_select_query("004", tempvar.vwint1.ToString(), "I");
                //ViewBag.payment = util.all_select_query("GEN", tempvar.vwstrarray0[1],"PYM");
            }
            if (type1 == "D")
            {
                ViewBag.item = util.all_select_query("003", tempvar.vwstring1);
            }

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
            if (cr_flag.IndexOf("Header") > 0)
            {
                if (action_flag == "Create")
                {
                    item_trans bnk = db.item_trans.Find(0, tempvar.vwstring1);
                    if (bnk != null)
                    {
                        ModelState.AddModelError(String.Empty, "Bill No already exist");
                        err_flag = false;
                        worksess.idrep = "";
                    }

                }
            }
            if (cr_flag.IndexOf("Details") > 0)
            {

                if (!util.date_validate(tempvar.vwstring3))
                {
                    ModelState.AddModelError(String.Empty, "Please insert a Valid transaction  date");
                    err_flag = false;
                    worksess.idrep = "";
                }
                ordering_table bnk = db.ordering_table.Find(Convert.ToInt32(tempvar.vwstring1));
                if (bnk.purpose == "B") { 
                if (bnk != null)
                {
                    if (bnk.quantity == 0)
                    {
                        ModelState.AddModelError(String.Empty, bnk.item_name+" cannot be selected because stock is empty");
                        err_flag = false;
                            worksess.idrep = "";
                        }
                    if (bnk.quantity != 0) { 
                    if (bnk.quantity < tempvar.vwint1)
                    {
                        ModelState.AddModelError(String.Empty, bnk.item_name+" quantity in stock is not up to the quantity selected");
                        err_flag = false;
                                worksess.idrep = "";
                            }
                    }
                    }
                }
            }
        }


        private void update_record(string headtype)
        {
            string cr_flag = action_flag;

            if (cr_flag.IndexOf("Header") > 0)
            {
                if (cr_flag == "CreateHeader")
                {
                    item_trans = new item_trans();
                    //init_header();
                    item_trans.created_date = DateTime.UtcNow.ToLocalTime();
                    item_trans.created_by = util.find_name("3", "", worksess.userid); ;
                }
                else
                {
                    item_trans = db.item_trans.Find(qheader.intquery0);
                }
                item_trans.bill_no = tempvar.vwint0;
                item_trans.guest_id = tempvar.vwint1;
                item_trans.transaction_date = string.IsNullOrWhiteSpace(tempvar.vwstrarray0[0]) ? "" : util.date_yyyymmdd(tempvar.vwstrarray0[0]);
                item_trans.modified_by = util.find_name("3", "", worksess.userid); ;
                item_trans.modified_date = DateTime.UtcNow.ToLocalTime();


                if (cr_flag == "CreateHeader")
                    db.Entry(item_trans).State = EntityState.Added;

                else
                    db.Entry(item_trans).State = EntityState.Modified;
            }
            else
            {
                if (cr_flag == "CreateDetails")
                {
                    trans_details = new trans_details();
                    trans_details.created_by = util.find_name("3", "", worksess.userid); ;
                    trans_details.created_date = DateTime.UtcNow.ToLocalTime();
                    tempvar.vwint2 = get_next_line_sequence();
                }
                else
                {
                    trans_details = db.trans_details.Find(qheader.intquery0, tempvar.vwint2);
                }

                trans_details.bill_no = qheader.intquery0;
                trans_details.sequence_no = tempvar.vwint2;
                trans_details.item_id = tempvar.vwstring1;
                trans_details.guest_id = qheader.intquery1;
                trans_details.quantity = tempvar.vwint1;
                trans_details.transaction_date = string.IsNullOrWhiteSpace(tempvar.vwstring3) ? "" : util.date_yyyymmdd(tempvar.vwstring3);
                trans_details.discount_flat = tempvar.vwdecimal7;
                trans_details.discount_per = tempvar.vwdecimal6;
                trans_details.amount = tempvar.vwdecimal5;
                trans_details.discount_amount = tempvar.vwdecimal8;
                trans_details.total_amount = tempvar.vwdecimal9;
                trans_details.modified_date = DateTime.UtcNow.ToLocalTime();
                trans_details.modified_by = util.find_name("3", "", worksess.userid); ;
                if (cr_flag == "CreateDetails")
                    db.Entry(trans_details).State = EntityState.Added;
                else
                    db.Entry(trans_details).State = EntityState.Modified;


            }

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





            if (err_flag && cr_flag.IndexOf("Header") < 0)
            {
                qheader.query6 = util.date_slash(trans_details.transaction_date);
                Session["qheader"] = qheader;
            }

            if (err_flag && cr_flag.IndexOf("Header") > 0)
            {
                set_qheader();
                str1 = "update trans_details set bill_no=b.bill_no, guest_id=b.guest_id ";
                str1 += " from trans_details a, item_trans b where a.bill_no=b.bill_no and a.bill_no=" + qheader.intquery0.ToString();
                db.Database.ExecuteSqlCommand(str1);
            }
            if (cr_flag == "CreateDetails" && err_flag)
            {

                str1 = "update ordering_table set quantity= quantity-"+ tempvar.vwint1+" where item_id ="+Convert.ToInt32(tempvar.vwstring1)+" and purpose = 'B'";
                db.Database.ExecuteSqlCommand(str1);
            }


            }

        private int get_next_line_sequence()
        {
            string sqlstr = "select isnull(max(sequence_no),0) vwint1 from trans_details where bill_no=" + util.sqlquote(qheader.intquery0.ToString()) + " and guest_id=" + qheader.intquery1;
            var sql1 = db.Database.SqlQuery<vw_genlay>(sqlstr).FirstOrDefault();
            return sql1.vwint1 + 1;

        }

        private void move_detail()
        {
            tempvar.vwstrarray6 = new string[20];
            tempvar.vwint2 = trans_details.sequence_no;
            tempvar.vwstring0 = util.find_name("1", "003", trans_details.item_id);
            tempvar.vwstring1 = trans_details.item_id;
            tempvar.vwdecimal5 = trans_details.amount;
            tempvar.vwstring3 = util.date_slash(trans_details.transaction_date);
            tempvar.vwint1 = trans_details.quantity;
            tempvar.vwdecimal6 = trans_details.discount_amount;
            tempvar.vwdecimal7 = trans_details.discount_per;
            tempvar.vwdecimal8 = trans_details.discount_flat;
            tempvar.vwdecimal9 = trans_details.total_amount;

        }

        [EncryptionActionAttribute]
        public ActionResult print_receipt(string key1)
        {
            worksess = (worksess)Session["worksess"];
            int key2;
            int.TryParse(key1, out key2);
            item_trans = db.item_trans.Find(0, key2);
            if (item_trans != null)
            {
                set_qheader();
            }
            string repname = Url.Action("View1");
            worksess.ptitle = "Receipt";
            if (worksess.viewflag == null)
                worksess.viewflag = "0";
            worksess.filep = "print/zx" + worksess.userid + ".rdf";

            worksess.temp6 = "2";
            worksess.temp5 = repname;
            Session["worksess"] = worksess;
            return RedirectToAction("coldisp", "PTransferE");
        }


        [HttpPost]
        public ActionResult get_amounts(string id = "")
        {
            worksess = (worksess)Session["worksess"];
            int temid = 0;
            if (!string.IsNullOrWhiteSpace(id))
            {
                temid = Convert.ToInt32(id);
            }
            var bglist = (from bh in db.ordering_table
                          where bh.item_id == temid
                          select bh).FirstOrDefault();
            List<SelectListItem> ary = new List<SelectListItem>();
            if (bglist != null)
            {
                ary.Add(new SelectListItem { Value = "item_rate", Text = bglist.price.ToString() });
                ary.Add(new SelectListItem { Value = "item_disc", Text = bglist.flat_discount.ToString() });
                ary.Add(new SelectListItem { Value = "item_discp", Text = bglist.per_discount.ToString() });
            }
            else
            {
                ary.Add(new SelectListItem { Value = "item_rate", Text = "0" });
                ary.Add(new SelectListItem { Value = "item_disc", Text = "0" });
                ary.Add(new SelectListItem { Value = "item_discp", Text = "0" });

            }
            if (HttpContext.Request.IsAjaxRequest())
                return Json(new SelectList(
                            ary.ToArray(),
                            "Value",
                            "Text")
                       , JsonRequestBehavior.AllowGet);


            return RedirectToAction("Index", null, new { anc = Ccheckg.convert_pass2("pc = ") });

        }

        [HttpPost]
        public ActionResult get_items(string id)
        {
            string id1 = "";
            if (id == "F")
                id1 = "K";
            else if (id == "D")
                id1 = "B";
            //var bgassign4 = util.all_select_query("003", "", id1);

            var bgassign4 = from bg in db.ordering_table
                            where bg.purpose == id1
                            select new { c1 = bg.item_id, c2 = bg.item_name+ "--" +bg.description +" "+bg.quantity };

            if (HttpContext.Request.IsAjaxRequest())
                return Json(new SelectList(
                                bgassign4.ToArray(),
                                "c1",
                                "c2")
                               , JsonRequestBehavior.AllowGet);
            return View();
        }
  
        public void getDialogue(vw_genlay tempvar)
        {
            tempvar.vwstrarray6 = new string[20];
            tempvar.vwint2 = trans_details.sequence_no;
            tempvar.vwstring0 = util.find_name("1", "003", trans_details.item_id);
            tempvar.vwstring1 = trans_details.item_id;
            tempvar.vwdecimal5 = trans_details.amount;
            tempvar.vwstring3 = util.date_slash(trans_details.transaction_date);
            tempvar.vwint1 = trans_details.quantity;
            tempvar.vwdecimal6 = trans_details.discount_amount;
            tempvar.vwdecimal7 = trans_details.discount_per;
            tempvar.vwdecimal8 = trans_details.discount_flat;
            tempvar.vwdecimal9 = trans_details.total_amount;
            worksess = (worksess)Session["worksess"];
            string strDiag = "";
            strDiag = "<div class=" + sp + "row" + sp + ">\r\n<div class=" + sp + "col-sm-7" + sp + ">\r\n<label class=" + sp + "col-sm-5 text-right " + sp + ">Bill No</label>\r\n";
            strDiag += "<div class=" + sp + "col-sm-6" + sp +  ">" + tempvar.vwstring0 + "  " + tempvar.vwstring1 + "</div>\r\n</div>\r\n";
            strDiag += "<div class=" + sp + "col-sm-5" + sp + ">\r\n<div class=" + sp + "row" + sp + ">\r\n<label class=" + sp + "col-sm-5 text-right" + sp + ">Guest Name</label>\r\n";
            strDiag += "<div class=" + sp + "col-sm-6" + sp + ">" + tempvar.vwint1 + "</div>\r\n</div>\r\n</div>\r\n</div>";

            strDiag += "<div class=" + sp + "row" + sp + ">\r\n<div class=" + sp + "col-sm-7" + sp + ">\r\n<label class=" + sp + "col-sm-5 text-right " + sp + ">Room No</label>\r\n";
            strDiag += "<div class=" + sp + "col-sm-6" + sp + ">" + tempvar.vwstring4 + "</div>\r\n</div>\r\n";
            strDiag += "<div class=" + sp + "col-sm-5" + sp + ">\r\n<div class=" + sp + "row" + sp + ">\r\n<label class=" + sp + "col-sm-5 text-right" + sp + ">Item</label>\r\n";
            strDiag += "<div class=" + sp + "col-sm-6" + sp + ">" + tempvar.vwstring0 + "</div>\r\n</div>\r\n</div>\r\n</div>";

            strDiag += "<div class=" + sp + "row" + sp + ">\r\n<div class=" + sp + "col-sm-7" + sp + ">\r\n<label class=" + sp + "col-sm-5 text-right " + sp + ">Amount</label>\r\n";
            strDiag += "<div class=" + sp + "col-sm-6" + sp + ">" + tempvar.vwdecimal5 + "</div>\r\n</div>\r\n";
            strDiag += "<div class=" + sp + "col-sm-5" + sp + ">\r\n<div class=" + sp + "row" + sp + ">\r\n<label class=" + sp + "col-sm-5 text-right" + sp + ">Date</label>\r\n";
            strDiag += "<div class=" + sp + "col-sm-6" + sp + ">" + tempvar.vwstring3 + "</div>\r\n</div>\r\n</div>\r\n</div>";

            strDiag += "<div class=" + sp + "row" + sp + ">\r\n<div class=" + sp + "col-sm-7" + sp + ">\r\n<label class=" + sp + "col-sm-5 text-right " + sp + ">Discount</label>\r\n";
            strDiag += "<div class=" + sp + "col-sm-6" + sp + ">" + tempvar.vwdecimal7 + "</div>\r\n</div>\r\n";
            strDiag += "<div class=" + sp + "col-sm-5" + sp + ">\r\n<div class=" + sp + "row" + sp + ">\r\n<label class=" + sp + "col-sm-5 text-right" + sp + ">Discount Amount</label>\r\n";
            strDiag += "<div class=" + sp + "col-sm-6" + sp + ">" + tempvar.vwdecimal6 + "</div>\r\n</div>\r\n</div>\r\n</div>";

            strDiag += "<div class=" + sp + "row" + sp + ">\r\n<div class=" + sp + "col-sm-7" + sp + ">\r\n<label class=" + sp + "col-sm-5 text-right " + sp + ">Total Amount</label>\r\n";
            strDiag += "<div class=" + sp + "col-sm-6" + sp + ">" + tempvar.vwdecimal9 + "</div>\r\n</div>\r\n";


            worksess.idrep = strDiag;
            Session["worksess"] = worksess;

        }

    }
}