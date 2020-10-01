using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using HMS.Models;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.IO;

namespace HMS.Report
{
    /// <summary>
    /// Summary description for CReceipt.
    /// </summary>
    /// 
    public partial class CReceipt : GrapeCity.ActiveReports.SectionReport
    {
        MainContext db;
        worksess worksess;
        vw_genlay tempvar;
        SqlConnection con;
        SqlDataReader reader;
        string query;
        public CReceipt()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
        }

        private void CReceipt_ReportStart(object sender, EventArgs e)
        {
            db = new MainContext();
            worksess = (worksess)HttpContext.Current.Session["worksess"];
            tempvar = (vw_genlay)HttpContext.Current.Session["tempvar"];
            string conString = db.Database.Connection.ConnectionString;

            con = new SqlConnection(conString);
            string str4 = "select * from checkin_table";
            var str5 = db.Database.SqlQuery<tempquery>(str4).FirstOrDefault();
            string orderbyt = "";
            if (str5 != null)
                orderbyt = str5.c1;

            string query = "select  * from checkin_table";
            //if (psess.sarrayt1[11] != "")
            //    query += ", cast(columintc2 as int)";
            SqlCommand ad = new SqlCommand(query, con);
            con.Open();
            reader = ad.ExecuteReader();
            this.DataSource = reader;

            main01();
        }

        private void CReceipt_ReportEnd(object sender, EventArgs e)
        {
            string view_name = HttpContext.Current.Server.MapPath("~/print/zx" + worksess.userid + ".rdf");

            this.Document.Save(view_name);

            reader.Close();
            con.Close();
        }

        private void main01()
        {
            // logo picture
            string filePath = Path.Combine(HttpRuntime.AppDomainAppPath, "img/company.png");
            if (!System.IO.File.Exists(filePath))
                filePath = Path.Combine(HttpRuntime.AppDomainAppPath, "img/nologo.png");

            this.company_logo.Image = Image.FromFile(filePath);
            //dataconnect dat1 = (dataconnect)HttpContext.Current.Session["dat1"];
            this.company_name.Text = worksess.pname;
            
            query = "select  field4 c4, field7 c7 from company_settings where id_code ='HInfo'";
            var gett = db.Database.SqlQuery<tempquery>(query).FirstOrDefault();

            if (gett != null)
            {
                this.company_address.Text = "Company Address: "+gett.c4;
                this.company_email.Text = "Company Email: "+gett.c7;
            }
            this.guest_id.Text = "Guest ID: "+tempvar.vwint0.ToString();
            this.guest_name.Text = "Guest Name: "+tempvar.vwstring0 + " " + tempvar.vwstring1;
                this.gender.Text = "Gender: "+tempvar.vwstring9 == "M" ? "Male" : "Female";
            this.address.Text = "Address: "+tempvar.vwstring3;
            this.contact_no.Text = "Phone: "+ tempvar.vwstring4;
            this.id_type.Text = "ID Type: "+tempvar.vwstring8;
            this.id_no.Text = "ID No: "+ tempvar.vwstring2;
            this.created_date.Text = "";
            this.date_in.Text = "Checkin Date: "+tempvar.vwstring6;
            this.date_out.Text = "Checkout Date: "+tempvar.vwstring7;
            this.room_no.Text = "Room No: "+tempvar.vwstring5;
            this.room_amount.Text = "Amount: "+tempvar.vwdecimal0.ToString();
            this.room_amount.OutputFormat = "#,##0.00 ";
            this.no_of_nights.Text = "No of Nights: "+tempvar.vwint3.ToString();
            this.no_of_adults.Text = "No of Adults: "+tempvar.vwint1.ToString(); ;
            this.no_of_children.Text = "No of Kids: "+tempvar.vwint2.ToString() ;
            this.total_amount.Text = "Total Amt: "+tempvar.vwdecimal1.ToString() ;
            this.total_amount.OutputFormat = "#,##0.00 ";

        }
    }
}
