namespace HMS.Report
{
    /// <summary>
    /// Summary description for IReceipt.
    /// </summary>
    partial class IReceipt
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
            System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(IReceipt));
            this.pageHeader = new GrapeCity.ActiveReports.SectionReportModel.PageHeader();
            this.detail = new GrapeCity.ActiveReports.SectionReportModel.Detail();
            this.pageFooter = new GrapeCity.ActiveReports.SectionReportModel.PageFooter();
            this.groupHeader1 = new GrapeCity.ActiveReports.SectionReportModel.GroupHeader();
            this.groupFooter1 = new GrapeCity.ActiveReports.SectionReportModel.GroupFooter();
            this.company_name = new GrapeCity.ActiveReports.SectionReportModel.Label();
            this.company_address = new GrapeCity.ActiveReports.SectionReportModel.Label();
            this.company_email = new GrapeCity.ActiveReports.SectionReportModel.Label();
            this.bill_no = new GrapeCity.ActiveReports.SectionReportModel.TextBox();
            this.bill_date = new GrapeCity.ActiveReports.SectionReportModel.TextBox();
            this.user_id = new GrapeCity.ActiveReports.SectionReportModel.TextBox();
            this.guest_name = new GrapeCity.ActiveReports.SectionReportModel.TextBox();
            this.line1 = new GrapeCity.ActiveReports.SectionReportModel.Line();
            this.line2 = new GrapeCity.ActiveReports.SectionReportModel.Line();
            this.groupHeader2 = new GrapeCity.ActiveReports.SectionReportModel.GroupHeader();
            this.groupFooter2 = new GrapeCity.ActiveReports.SectionReportModel.GroupFooter();
            this.item_name = new GrapeCity.ActiveReports.SectionReportModel.Label();
            this.rate = new GrapeCity.ActiveReports.SectionReportModel.Label();
            this.qty = new GrapeCity.ActiveReports.SectionReportModel.Label();
            this.amount = new GrapeCity.ActiveReports.SectionReportModel.Label();
            this.line3 = new GrapeCity.ActiveReports.SectionReportModel.Line();
            this.item_txt = new GrapeCity.ActiveReports.SectionReportModel.TextBox();
            this.rate_txt = new GrapeCity.ActiveReports.SectionReportModel.TextBox();
            this.qty_txt = new GrapeCity.ActiveReports.SectionReportModel.TextBox();
            this.amount_txt = new GrapeCity.ActiveReports.SectionReportModel.TextBox();
            this.line4 = new GrapeCity.ActiveReports.SectionReportModel.Line();
            this.no_of_items = new GrapeCity.ActiveReports.SectionReportModel.TextBox();
            this.no_of_quantity = new GrapeCity.ActiveReports.SectionReportModel.TextBox();
            this.line5 = new GrapeCity.ActiveReports.SectionReportModel.Line();
            this.shape1 = new GrapeCity.ActiveReports.SectionReportModel.Shape();
            this.stotal_txt = new GrapeCity.ActiveReports.SectionReportModel.TextBox();
            this.discount_txt = new GrapeCity.ActiveReports.SectionReportModel.TextBox();
            this.textBox1 = new GrapeCity.ActiveReports.SectionReportModel.TextBox();
            this.noi_label = new GrapeCity.ActiveReports.SectionReportModel.Label();
            this.noq_label = new GrapeCity.ActiveReports.SectionReportModel.Label();
            this.stotal_l = new GrapeCity.ActiveReports.SectionReportModel.Label();
            this.discount_l = new GrapeCity.ActiveReports.SectionReportModel.Label();
            this.gtotal_label = new GrapeCity.ActiveReports.SectionReportModel.Label();
            ((System.ComponentModel.ISupportInitialize)(this.company_name)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.company_address)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.company_email)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bill_no)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bill_date)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.user_id)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.guest_name)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.item_name)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.qty)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.amount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.item_txt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rate_txt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.qty_txt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.amount_txt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.no_of_items)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.no_of_quantity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stotal_txt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.discount_txt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.noi_label)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.noq_label)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stotal_l)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.discount_l)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gtotal_label)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // pageHeader
            // 
            this.pageHeader.Controls.AddRange(new GrapeCity.ActiveReports.SectionReportModel.ARControl[] {
            this.company_name,
            this.company_address,
            this.company_email,
            this.line1});
            this.pageHeader.Height = 0.8541667F;
            this.pageHeader.Name = "pageHeader";
            // 
            // detail
            // 
            this.detail.Controls.AddRange(new GrapeCity.ActiveReports.SectionReportModel.ARControl[] {
            this.item_txt,
            this.rate_txt,
            this.qty_txt,
            this.amount_txt});
            this.detail.Height = 0.25F;
            this.detail.Name = "detail";
            // 
            // pageFooter
            // 
            this.pageFooter.Height = 1.479167F;
            this.pageFooter.Name = "pageFooter";
            // 
            // groupHeader1
            // 
            this.groupHeader1.Controls.AddRange(new GrapeCity.ActiveReports.SectionReportModel.ARControl[] {
            this.bill_no,
            this.bill_date,
            this.user_id,
            this.guest_name,
            this.line2});
            this.groupHeader1.Height = 0.8541667F;
            this.groupHeader1.Name = "groupHeader1";
            // 
            // groupFooter1
            // 
            this.groupFooter1.Controls.AddRange(new GrapeCity.ActiveReports.SectionReportModel.ARControl[] {
            this.shape1,
            this.stotal_txt,
            this.discount_txt,
            this.textBox1,
            this.stotal_l,
            this.discount_l,
            this.gtotal_label});
            this.groupFooter1.Height = 1.791667F;
            this.groupFooter1.Name = "groupFooter1";
            // 
            // company_name
            // 
            this.company_name.Height = 0.233F;
            this.company_name.HyperLink = null;
            this.company_name.Left = 0F;
            this.company_name.Name = "company_name";
            this.company_name.Style = "font-size: 12pt; font-weight: bold; text-align: center; vertical-align: top; ddo-" +
    "char-set: 0";
            this.company_name.Text = "Company Name:";
            this.company_name.Top = 0F;
            this.company_name.Width = 2.708F;
            // 
            // company_address
            // 
            this.company_address.Height = 0.1805417F;
            this.company_address.HyperLink = null;
            this.company_address.Left = 0F;
            this.company_address.Name = "company_address";
            this.company_address.Style = "";
            this.company_address.Text = "Address:";
            this.company_address.Top = 0.326F;
            this.company_address.Width = 2.708F;
            // 
            // company_email
            // 
            this.company_email.Height = 0.1805417F;
            this.company_email.HyperLink = null;
            this.company_email.Left = 0F;
            this.company_email.Name = "company_email";
            this.company_email.Style = "";
            this.company_email.Text = "Email";
            this.company_email.Top = 0.537F;
            this.company_email.Width = 2.708F;
            // 
            // bill_no
            // 
            this.bill_no.Height = 0.2F;
            this.bill_no.Left = 0F;
            this.bill_no.Name = "bill_no";
            this.bill_no.Text = "Bill No:";
            this.bill_no.Top = 0F;
            this.bill_no.Width = 2.708F;
            // 
            // bill_date
            // 
            this.bill_date.Height = 0.2F;
            this.bill_date.Left = 0F;
            this.bill_date.Name = "bill_date";
            this.bill_date.Text = "Bill Date:";
            this.bill_date.Top = 0.2F;
            this.bill_date.Width = 2.708F;
            // 
            // user_id
            // 
            this.user_id.Height = 0.2F;
            this.user_id.Left = 0F;
            this.user_id.Name = "user_id";
            this.user_id.Text = "Operator ID:";
            this.user_id.Top = 0.4F;
            this.user_id.Width = 2.708F;
            // 
            // guest_name
            // 
            this.guest_name.Height = 0.2F;
            this.guest_name.Left = 0F;
            this.guest_name.Name = "guest_name";
            this.guest_name.Text = "Guest Name:";
            this.guest_name.Top = 0.6F;
            this.guest_name.Width = 2.708F;
            // 
            // line1
            // 
            this.line1.Height = 0F;
            this.line1.Left = 0.031F;
            this.line1.LineWeight = 3F;
            this.line1.Name = "line1";
            this.line1.Top = 0.738F;
            this.line1.Width = 2.677001F;
            this.line1.X1 = 0.031F;
            this.line1.X2 = 2.708001F;
            this.line1.Y1 = 0.738F;
            this.line1.Y2 = 0.738F;
            // 
            // line2
            // 
            this.line2.Height = 0F;
            this.line2.Left = 0F;
            this.line2.LineWeight = 3F;
            this.line2.Name = "line2";
            this.line2.Top = 0.84F;
            this.line2.Width = 2.708F;
            this.line2.X1 = 0F;
            this.line2.X2 = 2.708F;
            this.line2.Y1 = 0.84F;
            this.line2.Y2 = 0.84F;
            // 
            // groupHeader2
            // 
            this.groupHeader2.Controls.AddRange(new GrapeCity.ActiveReports.SectionReportModel.ARControl[] {
            this.item_name,
            this.rate,
            this.qty,
            this.amount,
            this.line3});
            this.groupHeader2.Height = 0.21875F;
            this.groupHeader2.Name = "groupHeader2";
            // 
            // groupFooter2
            // 
            this.groupFooter2.Controls.AddRange(new GrapeCity.ActiveReports.SectionReportModel.ARControl[] {
            this.line4,
            this.no_of_items,
            this.no_of_quantity,
            this.line5,
            this.noi_label,
            this.noq_label});
            this.groupFooter2.Height = 0.65625F;
            this.groupFooter2.Name = "groupFooter2";
            // 
            // item_name
            // 
            this.item_name.Height = 0.1979167F;
            this.item_name.HyperLink = null;
            this.item_name.Left = 0F;
            this.item_name.Name = "item_name";
            this.item_name.Style = "color: Blue";
            this.item_name.Text = "Item";
            this.item_name.Top = 0F;
            this.item_name.Width = 0.772F;
            // 
            // rate
            // 
            this.rate.Height = 0.1979167F;
            this.rate.HyperLink = null;
            this.rate.Left = 0.772F;
            this.rate.Name = "rate";
            this.rate.Style = "color: Blue";
            this.rate.Text = "Rate";
            this.rate.Top = 0F;
            this.rate.Width = 0.706F;
            // 
            // qty
            // 
            this.qty.Height = 0.1979167F;
            this.qty.HyperLink = null;
            this.qty.Left = 1.482F;
            this.qty.Name = "qty";
            this.qty.Style = "color: Blue";
            this.qty.Text = "Qty";
            this.qty.Top = 0F;
            this.qty.Width = 0.4269999F;
            // 
            // amount
            // 
            this.amount.Height = 0.1979167F;
            this.amount.HyperLink = null;
            this.amount.Left = 1.909F;
            this.amount.Name = "amount";
            this.amount.Style = "color: Blue";
            this.amount.Text = "Amount";
            this.amount.Top = 0F;
            this.amount.Width = 0.7700002F;
            // 
            // line3
            // 
            this.line3.Height = 0F;
            this.line3.Left = 0F;
            this.line3.LineWeight = 3F;
            this.line3.Name = "line3";
            this.line3.Top = 0.198F;
            this.line3.Width = 2.708F;
            this.line3.X1 = 0F;
            this.line3.X2 = 2.708F;
            this.line3.Y1 = 0.198F;
            this.line3.Y2 = 0.198F;
            // 
            // item_txt
            // 
            this.item_txt.Height = 0.2F;
            this.item_txt.Left = 0.0309999F;
            this.item_txt.Name = "item_txt";
            this.item_txt.Text = "Item";
            this.item_txt.Top = 0F;
            this.item_txt.Width = 0.7410001F;
            // 
            // rate_txt
            // 
            this.rate_txt.Height = 0.2F;
            this.rate_txt.Left = 0.772F;
            this.rate_txt.Name = "rate_txt";
            this.rate_txt.Style = "text-align: right";
            this.rate_txt.Text = "rate";
            this.rate_txt.Top = 0F;
            this.rate_txt.Width = 0.7100001F;
            // 
            // qty_txt
            // 
            this.qty_txt.Height = 0.2F;
            this.qty_txt.Left = 1.482F;
            this.qty_txt.Name = "qty_txt";
            this.qty_txt.Style = "text-align: right";
            this.qty_txt.Text = "qty";
            this.qty_txt.Top = 0F;
            this.qty_txt.Width = 0.4269999F;
            // 
            // amount_txt
            // 
            this.amount_txt.Height = 0.2F;
            this.amount_txt.Left = 1.909F;
            this.amount_txt.Name = "amount_txt";
            this.amount_txt.Style = "text-align: right";
            this.amount_txt.Text = "amount";
            this.amount_txt.Top = 0F;
            this.amount_txt.Width = 0.7990002F;
            // 
            // line4
            // 
            this.line4.Height = 1.117587E-08F;
            this.line4.Left = 0F;
            this.line4.LineWeight = 3F;
            this.line4.Name = "line4";
            this.line4.Top = 0.03F;
            this.line4.Width = 2.708F;
            this.line4.X1 = 0F;
            this.line4.X2 = 2.708F;
            this.line4.Y1 = 0.03F;
            this.line4.Y2 = 0.03000001F;
            // 
            // no_of_items
            // 
            this.no_of_items.Height = 0.1979167F;
            this.no_of_items.Left = 1.042F;
            this.no_of_items.Name = "no_of_items";
            this.no_of_items.Text = "No of Items:";
            this.no_of_items.Top = 0.04F;
            this.no_of_items.Width = 1.637F;
            // 
            // no_of_quantity
            // 
            this.no_of_quantity.Height = 0.1979167F;
            this.no_of_quantity.Left = 1.042F;
            this.no_of_quantity.Name = "no_of_quantity";
            this.no_of_quantity.Text = "No of Quantity:";
            this.no_of_quantity.Top = 0.25F;
            this.no_of_quantity.Width = 1.637F;
            // 
            // line5
            // 
            this.line5.Height = 0.00999999F;
            this.line5.Left = 0F;
            this.line5.LineWeight = 3F;
            this.line5.Name = "line5";
            this.line5.Top = 0.508F;
            this.line5.Width = 2.708F;
            this.line5.X1 = 0F;
            this.line5.X2 = 2.708F;
            this.line5.Y1 = 0.518F;
            this.line5.Y2 = 0.508F;
            // 
            // shape1
            // 
            this.shape1.Height = 1.657F;
            this.shape1.Left = 0.1040001F;
            this.shape1.LineWeight = 3F;
            this.shape1.Name = "shape1";
            this.shape1.RoundingRadius = new GrapeCity.ActiveReports.Controls.CornersRadius(10F, null, null, null, null);
            this.shape1.Top = 0.083F;
            this.shape1.Width = 2.127F;
            // 
            // stotal_txt
            // 
            this.stotal_txt.Height = 0.1979167F;
            this.stotal_txt.Left = 1F;
            this.stotal_txt.Name = "stotal_txt";
            this.stotal_txt.Text = "Sub Total:";
            this.stotal_txt.Top = 0.156F;
            this.stotal_txt.Width = 1.169F;
            // 
            // discount_txt
            // 
            this.discount_txt.Height = 0.1979167F;
            this.discount_txt.Left = 1F;
            this.discount_txt.Name = "discount_txt";
            this.discount_txt.Text = "Discount:";
            this.discount_txt.Top = 0.494F;
            this.discount_txt.Width = 1.169F;
            // 
            // textBox1
            // 
            this.textBox1.Height = 0.1979167F;
            this.textBox1.Left = 1F;
            this.textBox1.Name = "textBox1";
            this.textBox1.Text = "Grand Total:";
            this.textBox1.Top = 0.812F;
            this.textBox1.Width = 1.169F;
            // 
            // noi_label
            // 
            this.noi_label.Height = 0.1979167F;
            this.noi_label.HyperLink = null;
            this.noi_label.Left = 0F;
            this.noi_label.Name = "noi_label";
            this.noi_label.Style = "";
            this.noi_label.Text = "no of Items";
            this.noi_label.Top = 0.042F;
            this.noi_label.Width = 1F;
            // 
            // noq_label
            // 
            this.noq_label.Height = 0.1979167F;
            this.noq_label.HyperLink = null;
            this.noq_label.Left = 0F;
            this.noq_label.Name = "noq_label";
            this.noq_label.Style = "";
            this.noq_label.Text = "No of Quantity";
            this.noq_label.Top = 0.229F;
            this.noq_label.Width = 1F;
            // 
            // stotal_l
            // 
            this.stotal_l.Height = 0.2F;
            this.stotal_l.HyperLink = null;
            this.stotal_l.Left = 0.1040001F;
            this.stotal_l.Name = "stotal_l";
            this.stotal_l.Style = "font-size: 9.75pt; font-weight: bold; ddo-char-set: 0";
            this.stotal_l.Text = "Sub Total:";
            this.stotal_l.Top = 0.156F;
            this.stotal_l.Width = 0.8959999F;
            // 
            // discount_l
            // 
            this.discount_l.Height = 0.2F;
            this.discount_l.HyperLink = null;
            this.discount_l.Left = 0.104F;
            this.discount_l.Name = "discount_l";
            this.discount_l.Style = "font-size: 9.75pt; font-weight: bold; ddo-char-set: 0";
            this.discount_l.Text = "Disount:";
            this.discount_l.Top = 0.494F;
            this.discount_l.Width = 0.896F;
            // 
            // gtotal_label
            // 
            this.gtotal_label.Height = 0.2F;
            this.gtotal_label.HyperLink = null;
            this.gtotal_label.Left = 0.104F;
            this.gtotal_label.Name = "gtotal_label";
            this.gtotal_label.Style = "font-size: 9.75pt; font-weight: bold; ddo-char-set: 0";
            this.gtotal_label.Text = "Grand Total:";
            this.gtotal_label.Top = 0.7960001F;
            this.gtotal_label.Width = 0.896F;
            // 
            // IReceipt
            // 
            this.MasterReport = false;
            this.PageSettings.PaperHeight = 11F;
            this.PageSettings.PaperWidth = 8.5F;
            this.PrintWidth = 4.458333F;
            this.Sections.Add(this.pageHeader);
            this.Sections.Add(this.groupHeader1);
            this.Sections.Add(this.groupHeader2);
            this.Sections.Add(this.detail);
            this.Sections.Add(this.groupFooter2);
            this.Sections.Add(this.groupFooter1);
            this.Sections.Add(this.pageFooter);
            this.StyleSheet.Add(new DDCssLib.StyleSheetRule("font-family: Arial; font-style: normal; text-decoration: none; font-weight: norma" +
            "l; font-size: 10pt; color: Black", "Normal"));
            this.StyleSheet.Add(new DDCssLib.StyleSheetRule("font-size: 16pt; font-weight: bold", "Heading1", "Normal"));
            this.StyleSheet.Add(new DDCssLib.StyleSheetRule("font-family: Times New Roman; font-size: 14pt; font-weight: bold; font-style: ita" +
            "lic", "Heading2", "Normal"));
            this.StyleSheet.Add(new DDCssLib.StyleSheetRule("font-size: 13pt; font-weight: bold", "Heading3", "Normal"));
            this.ReportStart += new System.EventHandler(this.IReceipt_ReportStart);
            this.ReportEnd += new System.EventHandler(this.IReceipt_ReportEnd);
            ((System.ComponentModel.ISupportInitialize)(this.company_name)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.company_address)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.company_email)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bill_no)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bill_date)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.user_id)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.guest_name)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.item_name)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.qty)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.amount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.item_txt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rate_txt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.qty_txt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.amount_txt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.no_of_items)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.no_of_quantity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stotal_txt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.discount_txt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.noi_label)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.noq_label)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stotal_l)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.discount_l)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gtotal_label)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
        #endregion

        private GrapeCity.ActiveReports.SectionReportModel.GroupHeader groupHeader1;
        private GrapeCity.ActiveReports.SectionReportModel.GroupFooter groupFooter1;
        private GrapeCity.ActiveReports.SectionReportModel.Label company_name;
        private GrapeCity.ActiveReports.SectionReportModel.Label company_address;
        private GrapeCity.ActiveReports.SectionReportModel.Label company_email;
        private GrapeCity.ActiveReports.SectionReportModel.Line line1;
        private GrapeCity.ActiveReports.SectionReportModel.TextBox item_txt;
        private GrapeCity.ActiveReports.SectionReportModel.TextBox rate_txt;
        private GrapeCity.ActiveReports.SectionReportModel.TextBox qty_txt;
        private GrapeCity.ActiveReports.SectionReportModel.TextBox amount_txt;
        private GrapeCity.ActiveReports.SectionReportModel.TextBox bill_no;
        private GrapeCity.ActiveReports.SectionReportModel.TextBox bill_date;
        private GrapeCity.ActiveReports.SectionReportModel.TextBox user_id;
        private GrapeCity.ActiveReports.SectionReportModel.TextBox guest_name;
        private GrapeCity.ActiveReports.SectionReportModel.Line line2;
        private GrapeCity.ActiveReports.SectionReportModel.Shape shape1;
        private GrapeCity.ActiveReports.SectionReportModel.TextBox stotal_txt;
        private GrapeCity.ActiveReports.SectionReportModel.TextBox discount_txt;
        private GrapeCity.ActiveReports.SectionReportModel.TextBox textBox1;
        private GrapeCity.ActiveReports.SectionReportModel.GroupHeader groupHeader2;
        private GrapeCity.ActiveReports.SectionReportModel.Label item_name;
        private GrapeCity.ActiveReports.SectionReportModel.Label rate;
        private GrapeCity.ActiveReports.SectionReportModel.Label qty;
        private GrapeCity.ActiveReports.SectionReportModel.Label amount;
        private GrapeCity.ActiveReports.SectionReportModel.Line line3;
        private GrapeCity.ActiveReports.SectionReportModel.GroupFooter groupFooter2;
        private GrapeCity.ActiveReports.SectionReportModel.Line line4;
        private GrapeCity.ActiveReports.SectionReportModel.TextBox no_of_items;
        private GrapeCity.ActiveReports.SectionReportModel.TextBox no_of_quantity;
        private GrapeCity.ActiveReports.SectionReportModel.Line line5;
        private GrapeCity.ActiveReports.SectionReportModel.Label stotal_l;
        private GrapeCity.ActiveReports.SectionReportModel.Label discount_l;
        private GrapeCity.ActiveReports.SectionReportModel.Label gtotal_label;
        private GrapeCity.ActiveReports.SectionReportModel.Label noi_label;
        private GrapeCity.ActiveReports.SectionReportModel.Label noq_label;
    }
}
