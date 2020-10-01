namespace HMS.Report
{
    /// <summary>
    /// Summary description for CReceipt.
    /// </summary>
    partial class CReceipt
    {
        private GrapeCity.ActiveReports.SectionReportModel.PageHeader pageHeader;
        private GrapeCity.ActiveReports.SectionReportModel.Detail detail;
        private GrapeCity.ActiveReports.SectionReportModel.PageFooter pageFooter;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
            }
            base.Dispose(disposing);
        }

        #region ActiveReport Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(CReceipt));
            this.pageHeader = new GrapeCity.ActiveReports.SectionReportModel.PageHeader();
            this.guest_id = new GrapeCity.ActiveReports.SectionReportModel.Label();
            this.guest_name = new GrapeCity.ActiveReports.SectionReportModel.Label();
            this.gender = new GrapeCity.ActiveReports.SectionReportModel.Label();
            this.address = new GrapeCity.ActiveReports.SectionReportModel.Label();
            this.contact_no = new GrapeCity.ActiveReports.SectionReportModel.Label();
            this.id_type = new GrapeCity.ActiveReports.SectionReportModel.Label();
            this.id_no = new GrapeCity.ActiveReports.SectionReportModel.Label();
            this.created_date = new GrapeCity.ActiveReports.SectionReportModel.Label();
            this.detail = new GrapeCity.ActiveReports.SectionReportModel.Detail();
            this.pageFooter = new GrapeCity.ActiveReports.SectionReportModel.PageFooter();
            this.pagetctr = new GrapeCity.ActiveReports.SectionReportModel.ReportInfo();
            this.reportInfo1 = new GrapeCity.ActiveReports.SectionReportModel.ReportInfo();
            this.reportHeader1 = new GrapeCity.ActiveReports.SectionReportModel.ReportHeader();
            this.company_logo = new GrapeCity.ActiveReports.SectionReportModel.Picture();
            this.company_name = new GrapeCity.ActiveReports.SectionReportModel.Label();
            this.company_address = new GrapeCity.ActiveReports.SectionReportModel.Label();
            this.company_email = new GrapeCity.ActiveReports.SectionReportModel.Label();
            this.reportFooter1 = new GrapeCity.ActiveReports.SectionReportModel.ReportFooter();
            this.groupHeader1 = new GrapeCity.ActiveReports.SectionReportModel.GroupHeader();
            this.groupFooter1 = new GrapeCity.ActiveReports.SectionReportModel.GroupFooter();
            this.shape1 = new GrapeCity.ActiveReports.SectionReportModel.Shape();
            this.date_in = new GrapeCity.ActiveReports.SectionReportModel.Label();
            this.date_out = new GrapeCity.ActiveReports.SectionReportModel.Label();
            this.room_no = new GrapeCity.ActiveReports.SectionReportModel.Label();
            this.no_of_nights = new GrapeCity.ActiveReports.SectionReportModel.Label();
            this.shape2 = new GrapeCity.ActiveReports.SectionReportModel.Shape();
            this.no_of_adults = new GrapeCity.ActiveReports.SectionReportModel.Label();
            this.no_of_children = new GrapeCity.ActiveReports.SectionReportModel.Label();
            this.line1 = new GrapeCity.ActiveReports.SectionReportModel.Line();
            this.line2 = new GrapeCity.ActiveReports.SectionReportModel.Line();
            this.room_amount = new GrapeCity.ActiveReports.SectionReportModel.TextBox();
            this.total_amount = new GrapeCity.ActiveReports.SectionReportModel.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.guest_id)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.guest_name)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gender)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.address)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.contact_no)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.id_type)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.id_no)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.created_date)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pagetctr)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.reportInfo1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.company_logo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.company_name)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.company_address)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.company_email)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.date_in)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.date_out)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.room_no)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.no_of_nights)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.no_of_adults)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.no_of_children)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.room_amount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.total_amount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // pageHeader
            // 
            this.pageHeader.Controls.AddRange(new GrapeCity.ActiveReports.SectionReportModel.ARControl[] {
            this.guest_id,
            this.guest_name,
            this.gender,
            this.address,
            this.contact_no,
            this.id_type,
            this.id_no,
            this.created_date,
            this.line1});
            this.pageHeader.Height = 1.510416F;
            this.pageHeader.Name = "pageHeader";
            // 
            // guest_id
            // 
            this.guest_id.Height = 0.2F;
            this.guest_id.HyperLink = null;
            this.guest_id.Left = 0F;
            this.guest_id.Name = "guest_id";
            this.guest_id.Style = "";
            this.guest_id.Text = "Guest No";
            this.guest_id.Top = 0F;
            this.guest_id.Width = 2.604F;
            // 
            // guest_name
            // 
            this.guest_name.Height = 0.2F;
            this.guest_name.HyperLink = null;
            this.guest_name.Left = 0F;
            this.guest_name.Name = "guest_name";
            this.guest_name.Style = "";
            this.guest_name.Text = "Guest Name";
            this.guest_name.Top = 0.2F;
            this.guest_name.Width = 2.604F;
            // 
            // gender
            // 
            this.gender.Height = 0.2F;
            this.gender.HyperLink = null;
            this.gender.Left = 0F;
            this.gender.Name = "gender";
            this.gender.Style = "";
            this.gender.Text = "Gender";
            this.gender.Top = 0.4F;
            this.gender.Width = 2.604F;
            // 
            // address
            // 
            this.address.Height = 0.2F;
            this.address.HyperLink = null;
            this.address.Left = 0F;
            this.address.Name = "address";
            this.address.Style = "";
            this.address.Text = "Address";
            this.address.Top = 0.6F;
            this.address.Width = 2.604F;
            // 
            // contact_no
            // 
            this.contact_no.Height = 0.2F;
            this.contact_no.HyperLink = null;
            this.contact_no.Left = 0F;
            this.contact_no.Name = "contact_no";
            this.contact_no.Style = "";
            this.contact_no.Text = "Contact No";
            this.contact_no.Top = 0.8F;
            this.contact_no.Width = 2.604F;
            // 
            // id_type
            // 
            this.id_type.Height = 0.2F;
            this.id_type.HyperLink = null;
            this.id_type.Left = 0F;
            this.id_type.Name = "id_type";
            this.id_type.Style = "";
            this.id_type.Text = "ID Type";
            this.id_type.Top = 1F;
            this.id_type.Width = 2.604F;
            // 
            // id_no
            // 
            this.id_no.Height = 0.2F;
            this.id_no.HyperLink = null;
            this.id_no.Left = 0F;
            this.id_no.Name = "id_no";
            this.id_no.Style = "";
            this.id_no.Text = "ID Number";
            this.id_no.Top = 1.2F;
            this.id_no.Width = 2.604F;
            // 
            // created_date
            // 
            this.created_date.Height = 0.2F;
            this.created_date.HyperLink = null;
            this.created_date.Left = 2.969F;
            this.created_date.Name = "created_date";
            this.created_date.Style = "";
            this.created_date.Text = "Created Date";
            this.created_date.Top = 0F;
            this.created_date.Width = 2.854001F;
            // 
            // detail
            // 
            this.detail.Height = 0F;
            this.detail.Name = "detail";
            // 
            // pageFooter
            // 
            this.pageFooter.Controls.AddRange(new GrapeCity.ActiveReports.SectionReportModel.ARControl[] {
            this.pagetctr,
            this.reportInfo1});
            this.pageFooter.Name = "pageFooter";
            // 
            // pagetctr
            // 
            this.pagetctr.FormatString = "Page {PageNumber} of {PageCount}";
            this.pagetctr.Height = 0.2F;
            this.pagetctr.Left = 3.885F;
            this.pagetctr.Name = "pagetctr";
            this.pagetctr.Style = "vertical-align: middle";
            this.pagetctr.Top = 0.025F;
            this.pagetctr.Width = 2.375F;
            // 
            // reportInfo1
            // 
            this.reportInfo1.CanShrink = true;
            this.reportInfo1.FormatString = "{RunDateTime:dddd, MMMM d yyyy  hh:mm tt} ";
            this.reportInfo1.Height = 0.2F;
            this.reportInfo1.Left = 2.235174E-08F;
            this.reportInfo1.Name = "reportInfo1";
            this.reportInfo1.Style = "vertical-align: middle";
            this.reportInfo1.Top = 0.02500001F;
            this.reportInfo1.Width = 3.438F;
            // 
            // reportHeader1
            // 
            this.reportHeader1.Controls.AddRange(new GrapeCity.ActiveReports.SectionReportModel.ARControl[] {
            this.company_logo,
            this.company_name,
            this.company_address,
            this.company_email,
            this.line2});
            this.reportHeader1.Height = 1.333333F;
            this.reportHeader1.Name = "reportHeader1";
            // 
            // company_logo
            // 
            this.company_logo.Height = 1.143F;
            this.company_logo.ImageData = null;
            this.company_logo.Left = 0.427F;
            this.company_logo.Name = "company_logo";
            this.company_logo.Top = 0F;
            this.company_logo.Width = 2.052F;
            // 
            // company_name
            // 
            this.company_name.Height = 0.2F;
            this.company_name.HyperLink = null;
            this.company_name.Left = 2.563F;
            this.company_name.Name = "company_name";
            this.company_name.Style = "";
            this.company_name.Text = "Company Name:";
            this.company_name.Top = 0.115F;
            this.company_name.Width = 3.697001F;
            // 
            // company_address
            // 
            this.company_address.Height = 0.2F;
            this.company_address.HyperLink = null;
            this.company_address.Left = 2.563F;
            this.company_address.Name = "company_address";
            this.company_address.Style = "";
            this.company_address.Text = "Address:";
            this.company_address.Top = 0.41F;
            this.company_address.Width = 3.697001F;
            // 
            // company_email
            // 
            this.company_email.Height = 0.2F;
            this.company_email.HyperLink = null;
            this.company_email.Left = 2.563F;
            this.company_email.Name = "company_email";
            this.company_email.Style = "";
            this.company_email.Text = "Email";
            this.company_email.Top = 0.6810001F;
            this.company_email.Width = 3.697001F;
            // 
            // reportFooter1
            // 
            this.reportFooter1.Name = "reportFooter1";
            // 
            // groupHeader1
            // 
            this.groupHeader1.Controls.AddRange(new GrapeCity.ActiveReports.SectionReportModel.ARControl[] {
            this.shape1,
            this.date_in,
            this.date_out,
            this.room_no,
            this.no_of_nights,
            this.shape2,
            this.no_of_adults,
            this.no_of_children,
            this.room_amount,
            this.total_amount});
            this.groupHeader1.Height = 1.927083F;
            this.groupHeader1.Name = "groupHeader1";
            // 
            // groupFooter1
            // 
            this.groupFooter1.Height = 0F;
            this.groupFooter1.Name = "groupFooter1";
            // 
            // shape1
            // 
            this.shape1.Height = 1.760417F;
            this.shape1.Left = 0.1564999F;
            this.shape1.Name = "shape1";
            this.shape1.RoundingRadius = new GrapeCity.ActiveReports.Controls.CornersRadius(10F, null, null, null, null);
            this.shape1.Top = -2.980232E-08F;
            this.shape1.Width = 2.823F;
            // 
            // date_in
            // 
            this.date_in.Height = 0.2F;
            this.date_in.HyperLink = null;
            this.date_in.Left = 0.2084999F;
            this.date_in.Name = "date_in";
            this.date_in.Style = "";
            this.date_in.Text = "Date IN";
            this.date_in.Top = 0.08699997F;
            this.date_in.Width = 2.604F;
            // 
            // date_out
            // 
            this.date_out.Height = 0.2F;
            this.date_out.HyperLink = null;
            this.date_out.Left = 0.2084999F;
            this.date_out.Name = "date_out";
            this.date_out.Style = "";
            this.date_out.Text = "Date OUT";
            this.date_out.Top = 0.287F;
            this.date_out.Width = 2.604F;
            // 
            // room_no
            // 
            this.room_no.Height = 0.2F;
            this.room_no.HyperLink = null;
            this.room_no.Left = 0.2084999F;
            this.room_no.Name = "room_no";
            this.room_no.Style = "";
            this.room_no.Text = "Room Number";
            this.room_no.Top = 0.487F;
            this.room_no.Width = 2.604F;
            // 
            // no_of_nights
            // 
            this.no_of_nights.Height = 0.2F;
            this.no_of_nights.HyperLink = null;
            this.no_of_nights.Left = 0.2084999F;
            this.no_of_nights.Name = "no_of_nights";
            this.no_of_nights.Style = "";
            this.no_of_nights.Text = "No of Nights:";
            this.no_of_nights.Top = 0.887F;
            this.no_of_nights.Width = 2.604F;
            // 
            // shape2
            // 
            this.shape2.Height = 1.76F;
            this.shape2.Left = 3.1455F;
            this.shape2.Name = "shape2";
            this.shape2.RoundingRadius = new GrapeCity.ActiveReports.Controls.CornersRadius(10F, null, null, null, null);
            this.shape2.Top = -2.980232E-08F;
            this.shape2.Width = 3.198F;
            // 
            // no_of_adults
            // 
            this.no_of_adults.Height = 0.2F;
            this.no_of_adults.HyperLink = null;
            this.no_of_adults.Left = 3.2505F;
            this.no_of_adults.Name = "no_of_adults";
            this.no_of_adults.Style = "";
            this.no_of_adults.Text = "No of Adults";
            this.no_of_adults.Top = 0.08699997F;
            this.no_of_adults.Width = 2.604F;
            // 
            // no_of_children
            // 
            this.no_of_children.Height = 0.2F;
            this.no_of_children.HyperLink = null;
            this.no_of_children.Left = 3.2505F;
            this.no_of_children.Name = "no_of_children";
            this.no_of_children.Style = "";
            this.no_of_children.Text = "No of Children";
            this.no_of_children.Top = 0.337F;
            this.no_of_children.Width = 2.604F;
            // 
            // line1
            // 
            this.line1.Height = 0F;
            this.line1.Left = 0F;
            this.line1.LineWeight = 4F;
            this.line1.Name = "line1";
            this.line1.Top = 1.452F;
            this.line1.Width = 6.5F;
            this.line1.X1 = 0F;
            this.line1.X2 = 6.5F;
            this.line1.Y1 = 1.452F;
            this.line1.Y2 = 1.452F;
            // 
            // line2
            // 
            this.line2.Height = 0F;
            this.line2.Left = 0F;
            this.line2.LineWeight = 4F;
            this.line2.Name = "line2";
            this.line2.Top = 1.229F;
            this.line2.Width = 6.5F;
            this.line2.X1 = 0F;
            this.line2.X2 = 6.5F;
            this.line2.Y1 = 1.229F;
            this.line2.Y2 = 1.229F;
            // 
            // room_amount
            // 
            this.room_amount.Height = 0.2F;
            this.room_amount.Left = 0.208F;
            this.room_amount.Name = "room_amount";
            this.room_amount.Text = "Room Charges";
            this.room_amount.Top = 0.687F;
            this.room_amount.Width = 2.604F;
            // 
            // total_amount
            // 
            this.total_amount.Height = 0.2F;
            this.total_amount.Left = 3.25F;
            this.total_amount.Name = "total_amount";
            this.total_amount.Text = "Total Room Charges";
            this.total_amount.Top = 0.537F;
            this.total_amount.Width = 2.604F;
            // 
            // CReceipt
            // 
            this.MasterReport = false;
            this.PageSettings.PaperHeight = 11F;
            this.PageSettings.PaperWidth = 8.5F;
            this.PrintWidth = 6.5F;
            this.Sections.Add(this.reportHeader1);
            this.Sections.Add(this.pageHeader);
            this.Sections.Add(this.groupHeader1);
            this.Sections.Add(this.detail);
            this.Sections.Add(this.groupFooter1);
            this.Sections.Add(this.pageFooter);
            this.Sections.Add(this.reportFooter1);
            this.StyleSheet.Add(new DDCssLib.StyleSheetRule("font-family: Arial; font-style: normal; text-decoration: none; font-weight: norma" +
            "l; font-size: 10pt; color: Black", "Normal"));
            this.StyleSheet.Add(new DDCssLib.StyleSheetRule("font-size: 16pt; font-weight: bold", "Heading1", "Normal"));
            this.StyleSheet.Add(new DDCssLib.StyleSheetRule("font-family: Times New Roman; font-size: 14pt; font-weight: bold; font-style: ita" +
            "lic", "Heading2", "Normal"));
            this.StyleSheet.Add(new DDCssLib.StyleSheetRule("font-size: 13pt; font-weight: bold", "Heading3", "Normal"));
            this.ReportStart += new System.EventHandler(this.CReceipt_ReportStart);
            this.ReportEnd += new System.EventHandler(this.CReceipt_ReportEnd);
            ((System.ComponentModel.ISupportInitialize)(this.guest_id)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.guest_name)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gender)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.address)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.contact_no)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.id_type)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.id_no)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.created_date)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pagetctr)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.reportInfo1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.company_logo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.company_name)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.company_address)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.company_email)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.date_in)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.date_out)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.room_no)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.no_of_nights)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.no_of_adults)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.no_of_children)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.room_amount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.total_amount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
        #endregion

        private GrapeCity.ActiveReports.SectionReportModel.ReportHeader reportHeader1;
        private GrapeCity.ActiveReports.SectionReportModel.Picture company_logo;
        private GrapeCity.ActiveReports.SectionReportModel.Label company_name;
        private GrapeCity.ActiveReports.SectionReportModel.Label company_address;
        private GrapeCity.ActiveReports.SectionReportModel.Label company_email;
        private GrapeCity.ActiveReports.SectionReportModel.ReportFooter reportFooter1;
        private GrapeCity.ActiveReports.SectionReportModel.Label guest_id;
        private GrapeCity.ActiveReports.SectionReportModel.Label guest_name;
        private GrapeCity.ActiveReports.SectionReportModel.Label gender;
        private GrapeCity.ActiveReports.SectionReportModel.Label address;
        private GrapeCity.ActiveReports.SectionReportModel.Label contact_no;
        private GrapeCity.ActiveReports.SectionReportModel.Label id_type;
        private GrapeCity.ActiveReports.SectionReportModel.Label id_no;
        private GrapeCity.ActiveReports.SectionReportModel.Label created_date;
        private GrapeCity.ActiveReports.SectionReportModel.ReportInfo pagetctr;
        private GrapeCity.ActiveReports.SectionReportModel.ReportInfo reportInfo1;
        private GrapeCity.ActiveReports.SectionReportModel.Line line1;
        private GrapeCity.ActiveReports.SectionReportModel.Line line2;
        private GrapeCity.ActiveReports.SectionReportModel.GroupHeader groupHeader1;
        private GrapeCity.ActiveReports.SectionReportModel.Shape shape1;
        private GrapeCity.ActiveReports.SectionReportModel.Label date_in;
        private GrapeCity.ActiveReports.SectionReportModel.Label date_out;
        private GrapeCity.ActiveReports.SectionReportModel.Label room_no;
        private GrapeCity.ActiveReports.SectionReportModel.Label no_of_nights;
        private GrapeCity.ActiveReports.SectionReportModel.Shape shape2;
        private GrapeCity.ActiveReports.SectionReportModel.Label no_of_adults;
        private GrapeCity.ActiveReports.SectionReportModel.Label no_of_children;
        private GrapeCity.ActiveReports.SectionReportModel.GroupFooter groupFooter1;
        private GrapeCity.ActiveReports.SectionReportModel.TextBox room_amount;
        private GrapeCity.ActiveReports.SectionReportModel.TextBox total_amount;
    }
}
