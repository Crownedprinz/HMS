using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using HMS.Models;
using System.Data.SqlClient;
using System.Web;
using System.IO;
using HMS.utilities;

namespace HMS.Report
{
    /// <summary>
    /// Summary description for IReceipt.
    /// </summary>
    public partial class IReceipt : GrapeCity.ActiveReports.SectionReport
    {
        MainContext db;
        worksess worksess;
        vw_genlay tempvar;
        queryhead qheader;
        SqlConnection con;
        SqlDataReader reader;
        hutility util = new hutility();
        string query;
        public IReceipt()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

        }

        private void IReceipt_ReportStart(object sender, EventArgs e)
        {
            db = new MainContext();
            worksess = (worksess)HttpContext.Current.Session["worksess"];
            tempvar = (vw_genlay)HttpContext.Current.Session["tempvar"];
            qheader = (queryhead)HttpContext.Current.Session["qheader"];
            string conString = db.Database.Connection.ConnectionString;

            con = new SqlConnection(conString);
            string str4 = "select * from trans_details";
            var str5 = db.Database.SqlQuery<tempquery>(str4).FirstOrDefault();
            string orderbyt = "";
            if (str5 != null)
                orderbyt = str5.c1;

            string query = "select  *,bloyd.find_name('1','003',item_id) as item_name,amount*quantity as subtotal from trans_details where bill_no = " + util.sqlquote(qheader.intquery0.ToString());
            //if (psess.sarrayt1[11] != "")
            //    query += ", cast(columintc2 as int)";
            SqlCommand ad = new SqlCommand(query, con);
            con.Open();
            reader = ad.ExecuteReader();
            this.DataSource = reader;

            main01();
        }

        private void IReceipt_ReportEnd(object sender, EventArgs e)
        {
            string view_name = HttpContext.Current.Server.MapPath("~/print/zx" + worksess.userid + ".rdf");

            this.Document.Save(view_name);

            reader.Close();
            con.Close();
        }
        private void main01()
        {
            this.company_name.Text = worksess.pname;

            query = "select  field4 c4, field7 c7 from company_settings where id_code ='HInfo'";
            var gett = db.Database.SqlQuery<tempquery>(query).FirstOrDefault();

            if (gett != null)
            {
                this.company_address.Text = "Company Address: " + gett.c4;
                this.company_email.Text = "Company Email: " + gett.c7;
            }
            this.bill_no.Text = "Bill No: " + qheader.intquery0.ToString();
            this.bill_date.Text = "Bill Date: " + qheader.query6;
            if(worksess.userid=="Admin")
            this.user_id.Text = "Operator ID: ADMIN";
            else
            this.user_id.Text = "Operator ID: " + util.find_name("1", "005", worksess.userid);
            this.guest_name.Text = "Guest Name: " + util.find_name("1", "004", qheader.intquery1.ToString());
            
            this.stotal_txt.SummaryFunc = GrapeCity.ActiveReports.SectionReportModel.SummaryFunc.Sum;
            this.stotal_txt.SummaryRunning = GrapeCity.ActiveReports.SectionReportModel.SummaryRunning.Group;
            this.stotal_txt.SummaryType = GrapeCity.ActiveReports.SectionReportModel.SummaryType.SubTotal;
            this.stotal_txt.SummaryGroup = "";
            this.stotal_txt.SummaryRunning = GrapeCity.ActiveReports.SectionReportModel.SummaryRunning.All;
            this.stotal_txt.SummaryType = GrapeCity.ActiveReports.SectionReportModel.SummaryType.GrandTotal;
            this.stotal_txt.DataField = "subtotal";
            this.discount_txt.SummaryFunc = GrapeCity.ActiveReports.SectionReportModel.SummaryFunc.Sum;
            this.discount_txt.SummaryRunning = GrapeCity.ActiveReports.SectionReportModel.SummaryRunning.Group;
            this.discount_txt.SummaryType = GrapeCity.ActiveReports.SectionReportModel.SummaryType.SubTotal;
            this.discount_txt.SummaryGroup = "";
            this.discount_txt.SummaryRunning = GrapeCity.ActiveReports.SectionReportModel.SummaryRunning.All;
            this.discount_txt.SummaryType = GrapeCity.ActiveReports.SectionReportModel.SummaryType.GrandTotal;

            this.discount_txt.DataField = "discount_amount";
            this.textBox1.SummaryFunc = GrapeCity.ActiveReports.SectionReportModel.SummaryFunc.Sum;
            this.textBox1.SummaryRunning = GrapeCity.ActiveReports.SectionReportModel.SummaryRunning.Group;
            this.textBox1.SummaryType = GrapeCity.ActiveReports.SectionReportModel.SummaryType.SubTotal;
            this.textBox1.SummaryGroup = "";
            this.textBox1.SummaryRunning = GrapeCity.ActiveReports.SectionReportModel.SummaryRunning.All;
            this.textBox1.SummaryType = GrapeCity.ActiveReports.SectionReportModel.SummaryType.GrandTotal;

            this.textBox1.DataField = "total_amount";
            stotal_txt.OutputFormat = "#,##0.00";
            discount_txt.OutputFormat = "#,##0.00";
            textBox1.OutputFormat = "#,##0.00";

            item_txt.DataField = "item_name";
            rate_txt.DataField = "amount";
            rate_txt.OutputFormat = "#,##0.00"; 
            qty_txt.DataField = "quantity";
            amount_txt.DataField = "subtotal";
            amount_txt.OutputFormat = "#,##0.00";


            string query1 = "select count(item_id) intc1 from trans_details where bill_no=" + qheader.intquery0;
            var chk = db.Database.SqlQuery<tempquery>(query1).FirstOrDefault();
            this.no_of_items.Text = chk.intc1.ToString();
            this.no_of_quantity.SummaryFunc = GrapeCity.ActiveReports.SectionReportModel.SummaryFunc.Sum;
            this.no_of_quantity.SummaryRunning = GrapeCity.ActiveReports.SectionReportModel.SummaryRunning.Group;
            this.no_of_quantity.SummaryType = GrapeCity.ActiveReports.SectionReportModel.SummaryType.SubTotal;
            this.no_of_quantity.SummaryGroup = "";
            this.no_of_quantity.SummaryRunning = GrapeCity.ActiveReports.SectionReportModel.SummaryRunning.All;
            this.no_of_quantity.SummaryType = GrapeCity.ActiveReports.SectionReportModel.SummaryType.GrandTotal;

            this.no_of_quantity.DataField = "quantity";

        }
    }
}
