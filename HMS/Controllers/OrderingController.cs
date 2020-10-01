using anchor1.Filters;
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
    public class OrderingController : Controller
    {
        // GET: Ordering
        MainContext db = new MainContext();
        vw_genlay tempvar = new vw_genlay();
        hutility utils = new hutility();
        bool err_flag = true;
        string action_flag = "";
        ordering_table ordering_table = new ordering_table();
        worksess worksess;
        HttpPostedFileBase photo1;
        HttpPostedFileBase[] photo2;
        [EncryptionActionAttribute]
        public ActionResult Index(string pc = null)
        {
            worksess = (worksess)Session["worksess"];
            worksess.idrep = "";
            Session["worksess"] = worksess;
            var bglist = from bg in db.ordering_table
                         select new vw_genlay
                         {
                             vwint0 = bg.item_id,
                             vwstring1 = bg.item_name,
                             vwstring2 = bg.description,
                             vwdecimal0 = bg.price,
                             vwint1 = bg.quantity,
                             vwstring3 = bg.purpose,
                             vwdecimal1 = bg.flat_discount

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
            worksess = (worksess)Session["worksess"];
            ViewBag.action_flag = "Create";
            action_flag = "Create";

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
            ordering_table = db.ordering_table.Find(Convert.ToInt32(key1));
            if (ordering_table != null)
                read_record();

            select_query();
            return View(tempvar);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(vw_genlay tempvar_in, string id_xhrt, HttpPostedFileBase photofiles, HttpPostedFileBase[] picture1)
        {
            worksess = (worksess)Session["worksess"];
            ViewBag.action_flag = "Edit";
            action_flag = "Edit";
            photo1 = photofiles;
            photo2 = picture1;

            tempvar = tempvar_in;

            if (id_xhrt == "D")
            {
                delete_record();
                return RedirectToAction("Index");
            }

            update_file();
            if (err_flag)
                return RedirectToAction("Index");

            select_query();
            return View(tempvar);
        }
        private void select_query()
        {
            if (action_flag == "Create")
                ViewBag.selectroom = utils.all_select_query("001", tempvar.vwstring5, "A");
            else
                ViewBag.selectroom = utils.all_select_query("001", tempvar.vwstring5);
            ViewBag.idType = utils.all_select_query("GEN", tempvar.vwstring8, "IDTY");
        }
        private void delete_record()
        {
            ordering_table = db.ordering_table.Find(tempvar.vwint0);
            if (ordering_table != null)
            {
                db.ordering_table.Remove(ordering_table);
                db.SaveChanges();
            }
        }
        private void read_record()
        {
            tempvar.vwint0 = ordering_table.item_id;
            tempvar.vwstring1 = ordering_table.item_name;
            tempvar.vwstring2 = ordering_table.description;
            tempvar.vwstring3 = ordering_table.purpose;
            tempvar.vwstring4 = ordering_table.uom;
            tempvar.vwdecimal0 = ordering_table.price;
            tempvar.vwint1 = ordering_table.quantity;
            tempvar.vwdecimal1 = ordering_table.flat_discount;
            tempvar.vwdecimal2 = ordering_table.per_discount;
        }

        private void validation_routine()
        {
            if (string.IsNullOrWhiteSpace(tempvar.vwstring1))
            {
                ModelState.AddModelError(String.Empty, "must not be spaces");
                err_flag = false;
            }

        }
        private void update_record()
        {
            if (action_flag == "Create")
            {
                ordering_table = new ordering_table();
                ordering_table.created_by = utils.find_name("3", "", worksess.userid); ;
                ordering_table.created_date = DateTime.UtcNow.ToLocalTime();
            }
            else
            {
                ordering_table = db.ordering_table.Find(tempvar.vwint0);
            }


            //ordering_table.item_id ;
            ordering_table.item_name = string.IsNullOrWhiteSpace(tempvar.vwstring1) ? "" : tempvar.vwstring1;
            ordering_table.description = string.IsNullOrWhiteSpace(tempvar.vwstring2) ? "" : tempvar.vwstring2;
            ordering_table.purpose = string.IsNullOrWhiteSpace(tempvar.vwstring3) ? "" : tempvar.vwstring3;
            ordering_table.uom = string.IsNullOrWhiteSpace(tempvar.vwstring4) ? "" : tempvar.vwstring4;
            ordering_table.price = tempvar.vwdecimal0;
            ordering_table.quantity = tempvar.vwint1;
            ordering_table.flat_discount = tempvar.vwdecimal1;
            ordering_table.per_discount = tempvar.vwdecimal2;
            ordering_table.modified_date = DateTime.UtcNow.ToLocalTime();
            ordering_table.modified_by = utils.find_name("3", "", worksess.userid); ;

            if (photo1 != null)
            {

                byte[] uploaded = new byte[photo1.InputStream.Length];
                photo1.InputStream.Read(uploaded, 0, uploaded.Length);
                ordering_table.picture = uploaded;                
            }

            if (action_flag == "Create")
                db.Entry(ordering_table).State = EntityState.Added;
            else
                db.Entry(ordering_table).State = EntityState.Modified;

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

        [OverrideActionFilters]
        public ActionResult show(string id)
        {
            var dir = "";
            ordering_table = db.ordering_table.Find(id);
            if (ordering_table != null)
            {
                byte[] imagedata = ordering_table.picture;
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
