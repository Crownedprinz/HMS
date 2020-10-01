using anchor1.Filters;
using anchor1.utilities;
using HMS.Models;
using HMS.Report;
using HMS.utilities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HMS.Controllers
{
    public class CheckinController : Controller
    {
        // GET: Checkin
        MainContext db = new MainContext();
        vw_genlay tempvar = new vw_genlay();
        hutility utils = new hutility();
        worksess worksess;
        bool err_flag = true;
        string action_flag = "";

        checkin_table checkin_table = new checkin_table();
        char sp = '"';
        [EncryptionActionAttribute]
        public ActionResult Index(string pc = null)
        {

            var bglist = from bg in db.checkin_table
                         join bh in db.room_table
                         on new { a1 = bg.room_number } equals new { a1 = bh.room_no }
                         into bh1 from bh2 in bh1.DefaultIfEmpty()
                         join bk in db.cust_table
                          on new { a2 = bh2.customer_id } equals new { a2 = bk.customer_id }
                          into bk1 from bk2 in bk1.DefaultIfEmpty()
                         where bg.flag == "I" && bg.reserved_stat !="R"
                         select new vw_genlay
                         {
                             vwint0 = bg.guest_id,
                             vwstring0 = bg.first_name + " " + bg.surname,
                             vwstring1 = bg.room_number,
                             vwint3 = bg.no_of_nights,
                             vwint1 = bg.adults,
                             vwint2 = bg.children,
                             vwstring5 = bg.checkin_date,
                             vwstring6 = bg.exp_checkout_date,
                             vwstring7 = bg.checkin_time,
                             vwdecimal0 = bg.total_amount,
                             vwstring8 = bg.reserved_stat

                         };
            return View(bglist.ToList());
        }

        [EncryptionActionAttribute]
        public ActionResult Reservation(string pc = null)
        {
            worksess = (worksess)Session["worksess"];
            worksess.idrep = "";
            worksess.err_message = "";
            var bglist = from bg in db.checkin_table
                         join bh in db.room_table
                         on new { a1 = bg.room_number } equals new { a1 = bh.room_no }
                         into bh1
                         from bh2 in bh1.DefaultIfEmpty()
                         join bk in db.cust_table
                          on new { a2 = bh2.customer_id } equals new { a2 = bk.customer_id }
                          into bk1
                         from bk2 in bk1.DefaultIfEmpty()
                         where bg.flag !="O" && bg.reserved_stat=="Y"
                         select new vw_genlay
                         {
                             vwint0 = bg.guest_id,
                             vwstring0 = bg.first_name + " " + bg.surname,
                             vwstring1 = bg.room_number,
                             vwint3 = bg.no_of_nights,
                             vwint1 = bg.adults,
                             vwint2 = bg.children,
                             vwstring5 = bg.checkin_date,
                             vwstring6 = bg.exp_checkout_date,
                             vwstring7 = bg.checkin_time,
                             vwdecimal0 = bg.r_deposit,
                             vwdecimal1 = bg.r_balance,
                             vwstring8 = bg.flag
                         };
            return View(bglist.ToList());
        }

        [EncryptionActionAttribute]
        public ActionResult RCreate(string pc = null)
        {
            if (pc == null)
                return RedirectToAction("Index", "Dashboard");
            worksess = (worksess)Session["worksess"];
            ViewBag.action_flag = "RCreate";
            action_flag = "RCreate";
            worksess.idrep = "";
            worksess.err_message = "";
            init_values();
            select_query();
            return View("REdit", tempvar);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RCreate(vw_genlay tempvar_in)
        {
            ViewBag.action_flag = "RCreate";
            action_flag = "RCreate";
            worksess = (worksess)Session["worksess"];
            tempvar = tempvar_in;
            Rupdate_file();

            if (err_flag)
            {
                getDialogue(tempvar);
                Session["tempvar"] = tempvar;
                return RedirectToAction("Reservation", null, new { anc = Ccheckg.convert_pass2("pc=1") });
            }
            select_query();
            return View("REdit", tempvar);
        }

        [EncryptionActionAttribute]
        public ActionResult REdit(string key1)
        {
            worksess = (worksess)Session["worksess"];
            worksess.idrep = "";
            ViewBag.action_flag = "REdit";
            action_flag = "REdit";
            worksess.err_message = "";
            worksess = (worksess)Session["worksess"];
            init_values();
            checkin_table = db.checkin_table.Find(Convert.ToInt32(key1));
            if (checkin_table != null)
                read_record();

            select_query();
            return View(tempvar);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult REdit(vw_genlay tempvar_in, string id_xhrt)
        {
            ViewBag.action_flag = "REdit";
            action_flag = "REdit";
            worksess = (worksess)Session["worksess"];
            tempvar = tempvar_in;
            worksess.idrep = "";

            if (id_xhrt == "D")
            {
                delete_record();
                Session["worksess"] = worksess;
                return RedirectToAction("Reservation", null, new { anc = Ccheckg.convert_pass2("pc=1") });

            }

            Rupdate_file();
            if (err_flag)
            {
                Session["worksess"] = worksess;
                return RedirectToAction("Reservation", null, new { anc = Ccheckg.convert_pass2("pc=1") });

            }


            select_query();
            return View(tempvar);
        }



        [EncryptionActionAttribute]
        public ActionResult Details(string pc= null)
        {
            worksess = (worksess)Session["worksess"];
            worksess.err_message = "";
            worksess.idrep = "";
            var bglist = from bg in db.checkin_table
                         join bh in db.room_table
                         on new { a1 = bg.room_number } equals new { a1 = bh.room_no }
                         into bh1
                         from bh2 in bh1.DefaultIfEmpty()
                         join bk in db.cust_table
                          on new { a2 = bh2.customer_id } equals new { a2 = bk.customer_id }
                          into bk1
                         from bk2 in bk1.DefaultIfEmpty()
                         select new vw_genlay
                         {
                             vwint0 = bg.guest_id,
                             vwstring0 = bk2.customer_name + " " + bk2.customer_surname,
                             vwstring1 = bg.room_number,
                             vwint1 = bg.adults,
                             vwint2 = bg.children,
                             vwstring4 = bg.checkin_date,
                             vwstring5 = bg.exp_checkout_date

                         };
            return View("Index",bglist.ToList());
        }

        [EncryptionActionAttribute]
        public ActionResult  Create(string pc=null)
        {
            if (pc == null)
                return RedirectToAction("Index", "Dashboard");
            worksess = (worksess)Session["worksess"];
            ViewBag.action_flag = "Create";
            action_flag = "Create";
            worksess.idrep = "";
            worksess.err_message = "";
            init_values();
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

            if (err_flag) {
                getDialogue(tempvar);
                Session["tempvar"] = tempvar;
                return RedirectToAction("Index", null, new { anc = Ccheckg.convert_pass2("pc=1") });
            }
            select_query();
            return View("Edit", tempvar);
        }

        [EncryptionActionAttribute]
        public ActionResult Edit(string key1)
        {
            worksess = (worksess)Session["worksess"];
            worksess.idrep = "";
            ViewBag.action_flag = "Edit";
            action_flag = "Edit";
            worksess.err_message = "";
            worksess = (worksess)Session["worksess"];
            init_values();
            checkin_table = db.checkin_table.Find(Convert.ToInt32(key1));
            if (checkin_table != null)
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
            worksess.idrep = "";

            if (id_xhrt == "D")
            {
                delete_record();
                Session["worksess"] = worksess;
                return RedirectToAction("Index", null, new { anc = Ccheckg.convert_pass2("pc=1") });

            }

            update_file();
            if (err_flag)
            {
                Session["worksess"] = worksess;
                return RedirectToAction("Index", null, new { anc = Ccheckg.convert_pass2("pc=1") });

            }


            select_query();
            return View(tempvar);
        }

            [EncryptionActionAttribute]
            public ActionResult CheckOut(string key1, string key2)
            {
            worksess = (worksess)Session["worksess"];
                string query = "update checkin_table set flag = 'O', act_checkout_date=" + utils.date_yyyymmdd(DateTime.UtcNow.ToLocalTime().ToString("dd/MM/yyyy"))+", modified_date = " +utils.sqlquote(DateTime.UtcNow.ToLocalTime().ToString())+", modified_by="+utils.sqlquote(utils.find_name("3", "", worksess.userid)) + " where guest_id=" + Convert.ToInt32(key1)+
                    ";update room_table set neat_status = '02' , modified_date = " + utils.sqlquote(DateTime.UtcNow.ToLocalTime().ToString())+", modified_by = "+utils.sqlquote(utils.find_name("3", "", worksess.userid)) +  "where room_no=" + utils.sqlquote(key2);
                db.Database.ExecuteSqlCommand(query);
            worksess.err_message = "Please Clean room "+key2 +" and Change Room to vacant when done";
           worksess.idrep = "";
            Session["worksess"] = worksess;

            return RedirectToAction("Index", null, new { anc = Ccheckg.convert_pass2("pc=1") });
        }
        [EncryptionActionAttribute]
        public ActionResult Confirm(string key1, string key2)
        {
            worksess = (worksess)Session["worksess"];
            string query = "update checkin_table set flag = 'I', modified_date = " + utils.sqlquote(DateTime.UtcNow.ToLocalTime().ToString()) + ", modified_by=" + utils.sqlquote(utils.find_name("3", "", worksess.userid)) + " where guest_id=" + Convert.ToInt32(key1) +
                ";update room_table set room_status = 'O',neat_status = '03' , modified_date = " + utils.sqlquote(DateTime.UtcNow.ToLocalTime().ToString()) + ", modified_by = " + utils.sqlquote(utils.find_name("3", "", worksess.userid)) + "where room_no=" + utils.sqlquote(key2);
            db.Database.ExecuteSqlCommand(query);
            return RedirectToAction("Reservation", null, new { anc = Ccheckg.convert_pass2("pc=1") });
        }
        [EncryptionActionAttribute]
        public ViewResult CheckOutList (string idcode)
        {
            var bglist = from bg in db.checkin_table
                         join bh in db.room_table
                         on new { a1 = bg.room_number } equals new { a1 = bh.room_no }
                         into bh1
                         from bh2 in bh1.DefaultIfEmpty()
                         join bk in db.cust_table
                          on new { a2 = bh2.customer_id } equals new { a2 = bk.customer_id }
                          into bk1
                         from bk2 in bk1.DefaultIfEmpty()
                         where bg.flag == "O" orderby bg.checkin_date
                         select new vw_genlay
                         {
                             vwint0 = bg.guest_id,
                             vwstring0 = bg.first_name + " " + bg.surname,
                             vwstring1 = bg.room_number,
                             vwint3 = bg.no_of_nights,
                             vwint1 = bg.adults,
                             vwint2 = bg.children,
                             vwstring2 = bg.checkin_date,
                             vwstring3 = bg.exp_checkout_date,
                             vwstring4 = bg.act_checkout_date,
                             vwstring7 = bg.checkin_time,
                             vwdecimal0 = bg.total_amount

                         };
            return View(bglist.ToList());
        }
        [EncryptionActionAttribute]
        public ViewResult NextCheckOut(string idcode)
        {
            ViewBag.chkval = true;
                var bglist = from bg in db.checkin_table
                         join bh in db.room_table
                         on new { a1 = bg.room_number } equals new { a1 = bh.room_no }
                         into bh1
                         from bh2 in bh1.DefaultIfEmpty()
                         join bk in db.cust_table
                          on new { a2 = bh2.customer_id } equals new { a2 = bk.customer_id }
                          into bk1
                         from bk2 in bk1.DefaultIfEmpty()
                         where bg.flag == "I"
                         select new vw_genlay
                         {
                             vwint0 = bg.guest_id,
                             vwstring0 = bg.first_name + " " + bg.surname,
                             vwstring1 = bg.room_number,
                             vwint3 = bg.no_of_nights,
                             vwint1 = bg.adults,
                             vwint2 = bg.children,
                             vwstring2 = bg.checkin_date,
                             vwstring3 = bg.exp_checkout_date,
                             vwstring4 = bg.act_checkout_date

                         };
            return View("CheckOutList",bglist.ToList());
        }
        private void select_query()
        {
            if(action_flag=="Create")
            ViewBag.selectroom = utils.all_select_query("001", tempvar.vwstring5, "A");
            else
            ViewBag.selectroom = utils.all_select_query("001", tempvar.vwstring5);
            ViewBag.idType = utils.all_select_query("GEN", tempvar.vwstring8,"IDTY");
            ViewBag.customer = utils.all_select_query("002", tempvar.vwint4.ToString());
            ViewBag.ftitle = utils.all_select_query("GEN", tempvar.vwstring11, "TTL");
            ViewBag.pmtype = utils.all_select_query("GEN", tempvar.vwstring12, "PYT");
            
        }
        private void delete_record()
        {
            checkin_table = db.checkin_table.Find(tempvar.vwint0);
               if (checkin_table != null)
            {
                db.checkin_table.Remove(checkin_table);
                db.SaveChanges();
            }
        }
        [HttpPost]
        public ActionResult delete_list(int id)
        {
            // write your query statement
            worksess = (worksess)Session["worksess"];
            worksess.idrep = "";
            Session["worksess"] = worksess;
            string sqlstr = "delete from checkin_table where guest_id ='" + id + "'";
            int delctr = db.Database.ExecuteSqlCommand(sqlstr);
            return RedirectToAction("Index", null, new { anc = Ccheckg.convert_pass2("pc=1") });

        }

        private void init_values()
        {
            tempvar.vwstring6 = DateTime.UtcNow.ToLocalTime().ToString("dd/MM/yyyy");
            tempvar.vwstring7 = DateTime.UtcNow.ToLocalTime().AddDays(1).ToString("dd/MM/yyyy");
            tempvar.vwint1 = 1;
            tempvar.vwint3 = 1;
            tempvar.vwstring9 = "M";
            tempvar.vwstring14 = DateTime.UtcNow.ToLocalTime().ToString("hh: mm tt");
        }
    

            private void read_record()
            {
                tempvar.vwint0 = checkin_table.guest_id;
                tempvar.vwint1 = checkin_table.adults;
                tempvar.vwint2 = checkin_table.children;
                tempvar.vwstring0 = checkin_table.first_name;
                tempvar.vwstring1 = checkin_table.surname;
                tempvar.vwstring2 = checkin_table.passport_no;
                tempvar.vwstring3 = checkin_table.address;
                tempvar.vwstring4 = checkin_table.phone_number;
                tempvar.vwstring5 = checkin_table.room_number;
                tempvar.vwstring6 = utils.date_slash(checkin_table.checkin_date);
                tempvar.vwint3 = checkin_table.no_of_nights;
                tempvar.vwstring8 = checkin_table.passport_type;
                tempvar.vwstring7 = utils.date_slash(checkin_table.exp_checkout_date);
                tempvar.vwstring9 = checkin_table.gender;
                tempvar.vwstring12 = checkin_table.payment_type;
                tempvar.vwstring13 = checkin_table.reserved_stat;
                tempvar.vwstring11 = checkin_table.titles;
                tempvar.vwdecimal0 = checkin_table.amount;
                tempvar.vwdecimal1 = checkin_table.total_amount;
                tempvar.vwstring13 = checkin_table.early_cin;
                tempvar.vwstring14 = checkin_table.checkin_time;
            tempvar.vwdecimal3 = checkin_table.r_balance;
            tempvar.vwdecimal4 = checkin_table.disc_amount;
            tempvar.vwdecimal5 = checkin_table.r_deposit;
            tempvar.vwstring15 = checkin_table.flag;

        }   

            private void validation_routine()
            {
            bool passed = false;
            string s = String.Empty;
            DateTime dt;
            if (string.IsNullOrWhiteSpace(tempvar.vwstring0))
                {
                    ModelState.AddModelError(String.Empty, "must not be spaces");
                    err_flag = false;
                }

            
            try
            {
                s = tempvar.vwstring14; //Whatever you are getting the time from
                dt = Convert.ToDateTime(s);
                s = dt.ToString("HH:mm"); //if you want 12 hour time  ToString("hh:mm")
                passed = true;
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(String.Empty, "Please input the correct Time format (HH:MM AM)");
                err_flag = false;
            }

        }
        private int get_next_line_sequence()
        {
            string sqlstr = "select isnull(max(sequence_number),0) vwint9 from checkin_table where guest_id=" + utils.sqlquote(tempvar.vwint0.ToString());
            var sql1 = db.Database.SqlQuery<vw_genlay>(sqlstr).FirstOrDefault();
            return sql1.vwint9 + 1;

        }
        private int get_next_line_sequence1()
        {
            string sqlstr = "select isnull(max(guest_id),0) vwint8 from checkin_table";
            var sql1 = db.Database.SqlQuery<vw_genlay>(sqlstr).FirstOrDefault();
            return sql1.vwint8 + 1;

        }
        private void update_record()
        {
            if (action_flag == "Create")
            {
                checkin_table = new checkin_table();
                checkin_table.created_by = utils.find_name("3", "", worksess.userid); ;
                checkin_table.room_number = string.IsNullOrWhiteSpace(tempvar.vwstring5) ? "" : tempvar.vwstring5;
                checkin_table.created_date = DateTime.UtcNow.ToLocalTime();
            }
            else
            {
                checkin_table = db.checkin_table.Find(tempvar.vwint0);
            }


            //checkin_table.guest_id = tempvar.vwint0 ;
            checkin_table.first_name = string.IsNullOrWhiteSpace(tempvar.vwstring0) ? "" : tempvar.vwstring0;
            checkin_table.surname = string.IsNullOrWhiteSpace(tempvar.vwstring1) ? "" : tempvar.vwstring1;
            checkin_table.adults = tempvar.vwint1;
            checkin_table.children = tempvar.vwint2;
            checkin_table.passport_no = string.IsNullOrWhiteSpace(tempvar.vwstring2) ? "" : tempvar.vwstring2;
            checkin_table.address = string.IsNullOrWhiteSpace(tempvar.vwstring3) ? "" : tempvar.vwstring3;
            checkin_table.phone_number = string.IsNullOrWhiteSpace(tempvar.vwstring4) ? "" : tempvar.vwstring4;
            checkin_table.checkin_date = utils.date_yyyymmdd(tempvar.vwstring6);
            checkin_table.no_of_nights = tempvar.vwint3;
            checkin_table.titles = string.IsNullOrWhiteSpace(tempvar.vwstring11) ? "" : tempvar.vwstring11;
            checkin_table.amount = tempvar.vwdecimal0;
            checkin_table.total_amount = tempvar.vwdecimal1;
            checkin_table.disc_amount = tempvar.vwdecimal4;
            checkin_table.passport_type = string.IsNullOrWhiteSpace(tempvar.vwstring8) ? "" : tempvar.vwstring8;
            checkin_table.payment_type = string.IsNullOrWhiteSpace(tempvar.vwstring12) ? "" : tempvar.vwstring12;
            checkin_table.early_cin = string.IsNullOrWhiteSpace(tempvar.vwstring13) ? "" : tempvar.vwstring13;
            checkin_table.reserved_stat = "";

            if (action_flag == "Create")
            {
                var bgtime = (from bg in db.company_settings
                              where bg.id_code == "HInfo"
                              select bg.field9).FirstOrDefault();
                DateTime presenttime = Convert.ToDateTime(tempvar.vwstring14);
                //DateTime.UtcNow.ToLocalTime();
                DateTime time12 = Convert.ToDateTime("12:00:00 AM");
                DateTime compareTime = Convert.ToDateTime(bgtime);
                int i = DateTime.Compare(presenttime, time12);
                int i1 = DateTime.Compare(presenttime, compareTime);
                //present time is greater than 1200 if i =1
                //present time is less than 4:30 if i = -1
                if (tempvar.vwstring13 == "Y")
                {
                    checkin_table.exp_checkout_date = utils.date_yyyymmdd(tempvar.vwstring7);
                    checkin_table.act_checkout_date = utils.date_yyyymmdd(tempvar.vwstring7);
                }
                else
                {
                    if (i == 1 && i1 == -1)
                    {
                        if (tempvar.vwint3 > 1)
                        {
                            checkin_table.exp_checkout_date = utils.date_yyyymmdd(DateTime.UtcNow.ToLocalTime().AddDays(tempvar.vwint3).ToString("dd/MM/yyyy"));
                            checkin_table.act_checkout_date = utils.date_yyyymmdd(DateTime.UtcNow.ToLocalTime().AddDays(tempvar.vwint3).ToString("dd/MM/yyyy"));
                            tempvar.vwstring7 = utils.date_yyyymmdd(DateTime.UtcNow.ToLocalTime().AddDays(tempvar.vwint3).ToString("dd/MM/yyyy"));
                        }
                        else
                        {
                            checkin_table.exp_checkout_date = utils.date_yyyymmdd(DateTime.UtcNow.ToLocalTime().ToString("dd/MM/yyyy"));
                            checkin_table.act_checkout_date = utils.date_yyyymmdd(DateTime.UtcNow.ToLocalTime().ToString("dd/MM/yyyy"));
                            tempvar.vwstring7 = utils.date_yyyymmdd(DateTime.UtcNow.ToLocalTime().ToString("dd/MM/yyyy"));
                        }
                    }
                    else
                    {
                        checkin_table.exp_checkout_date = utils.date_yyyymmdd(tempvar.vwstring7);
                        checkin_table.act_checkout_date = utils.date_yyyymmdd(tempvar.vwstring7);
                    }
                }
            }
            else
            {
                checkin_table.exp_checkout_date = utils.date_yyyymmdd(tempvar.vwstring7);
                checkin_table.act_checkout_date = utils.date_yyyymmdd(tempvar.vwstring7);
            }
            checkin_table.flag = "I";
            if (!string.IsNullOrWhiteSpace(tempvar.vwstring10))
                tempvar.vwint4 = Convert.ToInt32(tempvar.vwstring10);
            checkin_table.r_deposit = 0;
            checkin_table.r_balance = 0;
            checkin_table.customer_id = tempvar.vwint4;
            checkin_table.checkin_time = tempvar.vwstring14;
            checkin_table.gender = string.IsNullOrWhiteSpace(tempvar.vwstring9) ? "" : tempvar.vwstring9;
            checkin_table.modified_date = DateTime.UtcNow.ToLocalTime();
            checkin_table.modified_by = utils.find_name("3", "", worksess.userid); ;

            if (action_flag == "Create")
                db.Entry(checkin_table).State = EntityState.Added;
            else
                db.Entry(checkin_table).State = EntityState.Modified;

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

            if (action_flag == "Create" && err_flag == true)
            {
                string query = "update room_table set room_status = 'O',neat_status = '03' where room_no =" + utils.sqlquote(tempvar.vwstring5);
                db.Database.ExecuteSqlCommand(query);
            }

        }
        private void Rupdate_record()
            {
                if (action_flag == "RCreate")
                {
                    checkin_table = new checkin_table();
                    checkin_table.created_by = utils.find_name("3", "", worksess.userid); ;
                    checkin_table.created_date = DateTime.UtcNow.ToLocalTime();
                }
                else
                {
                    checkin_table = db.checkin_table.Find(tempvar.vwint0);
                }

                 //checkin_table.guest_id = tempvar.vwint0 ;
                checkin_table.first_name = string.IsNullOrWhiteSpace(tempvar.vwstring0) ? "" : tempvar.vwstring0;
                checkin_table.surname = string.IsNullOrWhiteSpace(tempvar.vwstring1) ? "" : tempvar.vwstring1;
                checkin_table.adults = tempvar.vwint1;
                checkin_table.children = tempvar.vwint2;
                checkin_table.passport_no = string.IsNullOrWhiteSpace(tempvar.vwstring2) ? "" : tempvar.vwstring2;
                checkin_table.address = string.IsNullOrWhiteSpace(tempvar.vwstring3) ? "" : tempvar.vwstring3;
                checkin_table.phone_number = string.IsNullOrWhiteSpace(tempvar.vwstring4) ? "" : tempvar.vwstring4;
                checkin_table.checkin_date = utils.date_yyyymmdd(tempvar.vwstring6); 
                checkin_table.no_of_nights = tempvar.vwint3;
            checkin_table.titles = string.IsNullOrWhiteSpace(tempvar.vwstring11) ? "" : tempvar.vwstring11;
            checkin_table.amount = tempvar.vwdecimal0;
            checkin_table.disc_amount = tempvar.vwdecimal4;
            checkin_table.total_amount = tempvar.vwdecimal1;
            checkin_table.passport_type = string.IsNullOrWhiteSpace(tempvar.vwstring8) ? "" : tempvar.vwstring8;
            checkin_table.payment_type = string.IsNullOrWhiteSpace(tempvar.vwstring12) ? "" : tempvar.vwstring12;
            checkin_table.early_cin = string.IsNullOrWhiteSpace(tempvar.vwstring13) ? "" : tempvar.vwstring13;
            checkin_table.reserved_stat = "Y";
            checkin_table.room_number = string.IsNullOrWhiteSpace(tempvar.vwstring5) ? "" : tempvar.vwstring5;


            if (action_flag == "RCreate")
            {
                DateTime presenttime = DateTime.UtcNow.ToLocalTime();
                DateTime time12 = Convert.ToDateTime("12:00:00 AM");
                DateTime compareTime = Convert.ToDateTime("4:30:00 AM");
                int i = DateTime.Compare(presenttime, time12);
                int i1 = DateTime.Compare(presenttime, compareTime);
                //present time is greater than 1200 if i =1
                //present time is less than 4:30 if i = -1
                if (tempvar.vwstring13 == "Y")
                {
                    checkin_table.exp_checkout_date = utils.date_yyyymmdd(tempvar.vwstring7);
                    checkin_table.act_checkout_date = utils.date_yyyymmdd(tempvar.vwstring7);
                }
                else
                {
                    if (i == 1 && i1 == -1)
                    {
                        if (tempvar.vwint3 > 1)
                        {
                            checkin_table.exp_checkout_date = utils.date_yyyymmdd(DateTime.UtcNow.ToLocalTime().AddDays(tempvar.vwint3).ToString("dd/MM/yyyy"));
                            checkin_table.act_checkout_date = utils.date_yyyymmdd(DateTime.UtcNow.ToLocalTime().AddDays(tempvar.vwint3).ToString("dd/MM/yyyy"));
                            tempvar.vwstring7 = utils.date_yyyymmdd(DateTime.UtcNow.ToLocalTime().AddDays(tempvar.vwint3).ToString("dd/MM/yyyy"));
                        }
                        else
                        {
                            checkin_table.exp_checkout_date = utils.date_yyyymmdd(DateTime.UtcNow.ToLocalTime().ToString("dd/MM/yyyy"));
                            checkin_table.act_checkout_date = utils.date_yyyymmdd(DateTime.UtcNow.ToLocalTime().ToString("dd/MM/yyyy"));
                            tempvar.vwstring7 = utils.date_yyyymmdd(DateTime.UtcNow.ToLocalTime().ToString("dd/MM/yyyy"));
                        }
                    }
                    else
                    {
                        checkin_table.exp_checkout_date = utils.date_yyyymmdd(tempvar.vwstring7);
                        checkin_table.act_checkout_date = utils.date_yyyymmdd(tempvar.vwstring7);
                    }
                }
            }
            else
            {
                checkin_table.exp_checkout_date = utils.date_yyyymmdd(tempvar.vwstring7);
                checkin_table.act_checkout_date = utils.date_yyyymmdd(tempvar.vwstring7);
            }
            if (tempvar.vwstring15 != "I")
                checkin_table.flag = "R";
            else
                checkin_table.flag = tempvar.vwstring15;
            if (!string.IsNullOrWhiteSpace(tempvar.vwstring10))
                tempvar.vwint4 = Convert.ToInt32(tempvar.vwstring10);
            checkin_table.r_deposit = tempvar.vwdecimal5;
            checkin_table.r_balance = tempvar.vwdecimal3;
            checkin_table.customer_id = tempvar.vwint4;
            checkin_table.checkin_time = tempvar.vwstring14;
            checkin_table.gender= string.IsNullOrWhiteSpace(tempvar.vwstring9) ? "" : tempvar.vwstring9;
               checkin_table.modified_date = DateTime.UtcNow.ToLocalTime();
                checkin_table.modified_by = utils.find_name("3", "", worksess.userid) ;

                if (action_flag == "RCreate")
                    db.Entry(checkin_table).State = EntityState.Added;
                else
                    db.Entry(checkin_table).State = EntityState.Modified;

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
            private void update_file()
            {
                err_flag = true;
                validation_routine();

                if (err_flag)
                    update_record();

            }
        private void Rupdate_file()
        {
            err_flag = true;
            validation_routine();

            if (err_flag)
                Rupdate_record();

        }
        [HttpPost]
        public ActionResult get_cust(string id = "")
        {
            worksess = (worksess)Session["worksess"];
            int temid = 0;
            if (!string.IsNullOrWhiteSpace(id))
            {
                temid = Convert.ToInt32(id);
            }
            var bglist = (from bh in db.cust_table
                          where bh.customer_id == temid
                          select bh).FirstOrDefault();
            List<SelectListItem> ary = new List<SelectListItem>();
            if (bglist != null)
            {
                ary.Add(new SelectListItem { Value = "idtitle", Text = bglist.title });
                ary.Add(new SelectListItem { Value = "cfname", Text = bglist.customer_name});
                ary.Add(new SelectListItem { Value = "clname", Text = bglist.customer_surname });
                ary.Add(new SelectListItem { Value = "cgender", Text = bglist.sex });
                ary.Add(new SelectListItem { Value = "caddress", Text = bglist.customer_address });
                ary.Add(new SelectListItem { Value = "cphone", Text = bglist.cust_phone});
            }
            else
            {
                ary.Add(new SelectListItem { Value = "idtitle", Text = "" });
                ary.Add(new SelectListItem { Value = "cfname", Text = "" });
                ary.Add(new SelectListItem { Value = "clname", Text = "" });
                ary.Add(new SelectListItem { Value = "cgender", Text = "" });
                ary.Add(new SelectListItem { Value = "caddress", Text = "" });
                ary.Add(new SelectListItem { Value = "cphone", Text = "" });

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
        public ActionResult get_amount(string id = "")
        {
            worksess = (worksess)Session["worksess"];
            string[] words = id.Split('[');
            string roomid = words[0];
            string nonte = words[1];
            string temid = roomid;
            var bglist = (from bh in db.room_table
                          join bg in db.msg_file
                          on new {a1=bh.room_type,a2= "RMT"} equals new { a1=bg.id_code, a2=bg.para_code}
                          into bg1 
                          from bg2 in bg1.DefaultIfEmpty()
                          where bh.room_no == temid
                          select new { bh, bg2 }).FirstOrDefault();
            List<SelectListItem> ary = new List<SelectListItem>();
            if (bglist != null)
            {
                ary.Add(new SelectListItem { Value = "ramount", Text = bglist.bg2.desc3});
                ary.Add(new SelectListItem { Value = "tamount", Text = (Convert.ToInt32(bglist.bg2.desc3) * Convert.ToInt32(nonte)).ToString() });
            }
            else
            {
                ary.Add(new SelectListItem { Value = "ramount", Text = "0" });
                ary.Add(new SelectListItem { Value = "tamount", Text = "0" });
            }
            if (HttpContext.Request.IsAjaxRequest())
                return Json(new SelectList(
                            ary.ToArray(),
                            "Value",
                            "Text")
                       , JsonRequestBehavior.AllowGet);


            return RedirectToAction("Index", null, new { anc = Ccheckg.convert_pass2("pc = ") });

        }

        public void getDialogue(vw_genlay tempvar)
        {
            worksess = (worksess)Session["worksess"];
            string strDiag = "";
            tempvar.vwstring12 =utils.find_name("1", "PYM", tempvar.vwstring12);
            strDiag = "<div class=" + sp + "row" + sp + ">\r\n<div class=" + sp + "col-sm-7" + sp + ">\r\n<label class=" + sp + "col-sm-5 text-right " + sp + ">Name</label>\r\n";
            strDiag += "<div class=" + sp + "col-sm-6" + sp + ">"+tempvar.vwstring0+ "  " + tempvar.vwstring1+"</div>\r\n</div>\r\n";
            strDiag += "<div class=" + sp + "col-sm-5" + sp + ">\r\n<div class=" + sp + "row" + sp + ">\r\n<label class=" + sp + "col-sm-5 text-right" + sp + ">Adults</label>\r\n";
            strDiag += "<div class=" + sp + "col-sm-6" + sp + ">"+tempvar.vwint1+"</div>\r\n</div>\r\n</div>\r\n</div>";

            strDiag += "<div class=" + sp + "row" + sp + ">\r\n<div class=" + sp + "col-sm-7" + sp + ">\r\n<label class=" + sp + "col-sm-5 text-right " + sp + ">Phone No</label>\r\n";
            strDiag += "<div class=" + sp + "col-sm-6" + sp + ">" + tempvar.vwstring4 + "</div>\r\n</div>\r\n";
            strDiag += "<div class=" + sp + "col-sm-5" + sp + ">\r\n<div class=" + sp + "row" + sp + ">\r\n<label class=" + sp + "col-sm-5 text-right" + sp + ">Payment Type</label>\r\n";
            strDiag += "<div class=" + sp + "col-sm-6" + sp + ">" + tempvar.vwstring12 + "</div>\r\n</div>\r\n</div>\r\n</div>";

            strDiag += "<div class=" + sp + "row" + sp + ">\r\n<div class=" + sp + "col-sm-7" + sp + ">\r\n<label class=" + sp + "col-sm-5 text-right " + sp + ">Room</label>\r\n";
            strDiag += "<div class=" + sp + "col-sm-6" + sp + ">" + tempvar.vwstring5 + "</div>\r\n</div>\r\n";
            strDiag += "<div class=" + sp + "col-sm-5" + sp + ">\r\n<div class=" + sp + "row" + sp + ">\r\n<label class=" + sp + "col-sm-5 text-right" + sp + ">No of Days</label>\r\n";
            strDiag += "<div class=" + sp + "col-sm-6" + sp + ">" + tempvar.vwint3 + "</div>\r\n</div>\r\n</div>\r\n</div>";

            strDiag += "<div class=" + sp + "row" + sp + ">\r\n<div class=" + sp + "col-sm-7" + sp + ">\r\n<label class=" + sp + "col-sm-5 text-right " + sp + ">Amount</label>\r\n";
            strDiag += "<div class=" + sp + "col-sm-6" + sp + ">" + tempvar.vwdecimal0 + "</div>\r\n</div>\r\n";
            strDiag += "<div class=" + sp + "col-sm-5" + sp + ">\r\n<div class=" + sp + "row" + sp + ">\r\n<label class=" + sp + "col-sm-5 text-right" + sp + ">Total Amount</label>\r\n";
            strDiag += "<div class=" + sp + "col-sm-6" + sp + ">" + tempvar.vwdecimal1 + "</div>\r\n</div>\r\n</div>\r\n</div>";

            strDiag += "<div class=" + sp + "row" + sp + ">\r\n<div class=" + sp + "col-sm-7" + sp + ">\r\n<label class=" + sp + "col-sm-5 text-right " + sp + ">Check-In Date</label>\r\n";
            strDiag += "<div class=" + sp + "col-sm-6" + sp + ">" +tempvar.vwstring6 + "</div>\r\n</div>\r\n";
            strDiag += "<div class=" + sp + "col-sm-5" + sp + ">\r\n<div class=" + sp + "row" + sp + ">\r\n<label class=" + sp + "col-sm-5 text-right" + sp + ">Check-Out Date</label>\r\n";
            strDiag += "<div class=" + sp + "col-sm-6" + sp + ">" + tempvar.vwstring7 + "</div>\r\n</div>\r\n</div>\r\n</div>";


            worksess.idrep = strDiag;
            Session["worksess"] = worksess;

        }

        [EncryptionActionAttribute]
        public ActionResult print_receipt()
        {
            worksess = (worksess)Session["worksess"];
            string repname = Url.Action("View1");
            worksess.ptitle = "Receipt";
            if (worksess.viewflag == null)
                worksess.viewflag = "0";
            worksess.filep = "print/zx" + worksess.userid + ".rdf";
            
            worksess.temp6 = "1";
            worksess.temp5 = repname;
            Session["psess"] = worksess;
            return RedirectToAction("coldisp","PTransferE");
        }

    }
}
