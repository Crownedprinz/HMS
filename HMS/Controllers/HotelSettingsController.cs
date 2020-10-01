using anchor1.Filters;
using anchor1.utilities;
using HMS.Models;
using HMS.utilities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HMS.Controllers
{
    public class HotelSettingsController : Controller
    {
        company_settings company_settings = new company_settings();
        MainContext db = new MainContext();
        vw_genlay tempvar = new vw_genlay();
        hutility util = new hutility();
        bool up1_flag;
        worksess worksess;
        bool err_flag = true;
        HttpPostedFileBase photo1;
        HttpPostedFileBase[] photo2;

        [EncryptionActionAttribute]
        public ActionResult Edit(string pc = null)
        {
            ViewBag.action_flag = "Edit";
            worksess = (worksess)Session["worksess"];
            worksess.idrep = "";
            company_settings = (from bk in db.company_settings
                                select bk).FirstOrDefault();

            if (company_settings != null)
            {
                read_record();
                //read_pst();
            }
            tempvar.datmode = "E";
            select_query();
            return View(tempvar);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(vw_genlay tempvar_in, string id_xhrt, HttpPostedFileBase photofiles, HttpPostedFileBase[] picture1, string coybtn)
        {
            tempvar = tempvar_in;
            tempvar.datmode = "E";
            worksess = (worksess)Session["worksess"];
            if (id_xhrt == "D")
            {
                delete_record();
                return RedirectToAction("Index", null, new { anc = Ccheckg.convert_pass2("pc=") });
            }

            
            photo1 = photofiles;
            photo2 = picture1;
            update_file();
            if (err_flag)
                return RedirectToAction("Index", "Dashboard");
            select_query();
            return View(tempvar);
        }

        private void read_record()
        {
            company_settings hcominfo = db.company_settings.Find("HInfo");
            if (hcominfo != null)
            {

                tempvar.vwstring0 = company_settings.id_code;
                tempvar.vwstring1 = company_settings.field1;
                tempvar.vwstring2 = company_settings.field2;
                tempvar.vwstring3 = company_settings.field3;
                tempvar.vwstring4 = company_settings.field4;
                tempvar.vwstring5 = company_settings.field5;
                tempvar.vwstring6 = company_settings.field6;
                tempvar.vwstring7 = company_settings.field7;
                tempvar.vwstring8 = company_settings.field8;
                tempvar.vwstring9 = company_settings.field9;
            }

        }
        private void select_query()
        {
            ViewBag.country = util.all_select_query("GEN", tempvar.vwstring3,"CTRY");
        }
        private void delete_record() {
            company_settings = db.company_settings.Find(tempvar.vwstring0);
            if (company_settings != null)
            {
                db.company_settings.Remove(company_settings);
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
            bool passed = false;
            string s = String.Empty;
            DateTime dt;
            try
            {
                s = tempvar.vwstring9; //Whatever you are getting the time from
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


        private void update_record()
        {
            company_settings = db.company_settings.Find("HInfo");
            up1_flag = false;
            if (company_settings == null)
            {
                company_settings = new company_settings();
                company_settings.created_by = util.find_name("3", "", worksess.userid); ;
                company_settings.created_date = DateTime.UtcNow.ToLocalTime();
                company_settings.id_code = "HInfo";
                up1_flag = true;
            }

            company_settings.field1 = string.IsNullOrWhiteSpace(tempvar.vwstring1) ? "" : tempvar.vwstring1;
            company_settings.field2 = string.IsNullOrWhiteSpace(tempvar.vwstring2) ? "" : tempvar.vwstring2;
            company_settings.field3 = string.IsNullOrWhiteSpace(tempvar.vwstring3) ? "" : tempvar.vwstring3;
            company_settings.field4 = string.IsNullOrWhiteSpace(tempvar.vwstring4) ? "" : tempvar.vwstring4;
            company_settings.field5 = string.IsNullOrWhiteSpace(tempvar.vwstring5) ? "" : tempvar.vwstring5;
            company_settings.field6 = string.IsNullOrWhiteSpace(tempvar.vwstring6) ? "" : tempvar.vwstring6;
            company_settings.field7 = string.IsNullOrWhiteSpace(tempvar.vwstring7) ? "" : tempvar.vwstring7;
            company_settings.field8 = string.IsNullOrWhiteSpace(tempvar.vwstring8) ? "" : tempvar.vwstring8;
            company_settings.field9 = string.IsNullOrWhiteSpace(tempvar.vwstring9) ? "" : tempvar.vwstring9;
            company_settings.modified_by = util.find_name("3", "", worksess.userid); ;
            company_settings.modified_date = DateTime.UtcNow.ToLocalTime();
            if (photo1 != null)
            {

                byte[] uploaded = new byte[photo1.InputStream.Length];
                photo1.InputStream.Read(uploaded, 0, uploaded.Length);
                company_settings.com_logo = uploaded;
                string myfile = "company.png"; //getting file name without extension  
                                               // store the file inside ~/project folder(Img)  
                var path = Path.Combine(Server.MapPath("~/img"), myfile);
                photo1.SaveAs(path);
            }
            if (up1_flag)
                db.Entry(company_settings).State = EntityState.Added;
            else
                db.Entry(company_settings).State = EntityState.Modified;

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
        [OverrideActionFilters]
        public ActionResult show(string id)
        {
            var dir = "";
            company_settings = db.company_settings.Find(id);
            if (company_settings != null)
            {
                byte[] imagedata = company_settings.com_logo;
                return File(imagedata, "png");
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