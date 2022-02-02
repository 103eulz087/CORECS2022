namespace SalesInventorySystem.DevExReportTemplate
{
    partial class ShipmentReport
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ShipmentReport));
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.xrPictureBox1 = new DevExpress.XtraReports.UI.XRPictureBox();
            this.xrcompanyname = new DevExpress.XtraReports.UI.XRLabel();
            this.xrcaption1 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrcaption2 = new DevExpress.XtraReports.UI.XRLabel();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.xrinvoiceno = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel5 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrsuppliername = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrreporttype = new DevExpress.XtraReports.UI.XRLabel();
            this.lblshipment = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
            this.ReportFooter = new DevExpress.XtraReports.UI.ReportFooterBand();
            this.preparedby = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel6 = new DevExpress.XtraReports.UI.XRLabel();
            this.ReportHeader = new DevExpress.XtraReports.UI.ReportHeaderBand();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // TopMargin
            // 
            this.TopMargin.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPictureBox1,
            this.xrcompanyname,
            this.xrcaption1,
            this.xrcaption2});
            this.TopMargin.HeightF = 80.83334F;
            this.TopMargin.Name = "TopMargin";
            this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            this.TopMargin.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.TopMargin_BeforePrint);
            // 
            // xrPictureBox1
            // 
            this.xrPictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("xrPictureBox1.Image")));
            this.xrPictureBox1.LocationFloat = new DevExpress.Utils.PointFloat(140.5208F, 10.00001F);
            this.xrPictureBox1.Name = "xrPictureBox1";
            this.xrPictureBox1.SizeF = new System.Drawing.SizeF(80.20839F, 60.66668F);
            this.xrPictureBox1.Sizing = DevExpress.XtraPrinting.ImageSizeMode.ZoomImage;
            // 
            // xrcompanyname
            // 
            this.xrcompanyname.Font = new System.Drawing.Font("Arial Black", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrcompanyname.LocationFloat = new DevExpress.Utils.PointFloat(220.7292F, 10.00001F);
            this.xrcompanyname.Name = "xrcompanyname";
            this.xrcompanyname.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrcompanyname.SizeF = new System.Drawing.SizeF(296.25F, 23F);
            this.xrcompanyname.StylePriority.UseFont = false;
            this.xrcompanyname.Text = "ENZO\'S MEAT MARKET FOODS CORP";
            // 
            // xrcaption1
            // 
            this.xrcaption1.Font = new System.Drawing.Font("Arial", 9.75F);
            this.xrcaption1.LocationFloat = new DevExpress.Utils.PointFloat(220.7292F, 33.00001F);
            this.xrcaption1.Name = "xrcaption1";
            this.xrcaption1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrcaption1.SizeF = new System.Drawing.SizeF(296.25F, 18.83334F);
            this.xrcaption1.StylePriority.UseFont = false;
            this.xrcaption1.Text = "Unitop Shopping Mall Lapulapu Branch";
            // 
            // xrcaption2
            // 
            this.xrcaption2.Font = new System.Drawing.Font("Arial", 9.75F);
            this.xrcaption2.LocationFloat = new DevExpress.Utils.PointFloat(220.7292F, 51.83334F);
            this.xrcaption2.Name = "xrcaption2";
            this.xrcaption2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrcaption2.SizeF = new System.Drawing.SizeF(317.0833F, 18.83334F);
            this.xrcaption2.StylePriority.UseFont = false;
            this.xrcaption2.Text = "Mangubat St., Lapulapu City 6015 Cebu Philippines";
            // 
            // BottomMargin
            // 
            this.BottomMargin.Name = "BottomMargin";
            this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrinvoiceno
            // 
            this.xrinvoiceno.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold);
            this.xrinvoiceno.LocationFloat = new DevExpress.Utils.PointFloat(107.9167F, 57.70829F);
            this.xrinvoiceno.Name = "xrinvoiceno";
            this.xrinvoiceno.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrinvoiceno.SizeF = new System.Drawing.SizeF(310.8333F, 17.08332F);
            this.xrinvoiceno.StylePriority.UseFont = false;
            this.xrinvoiceno.Text = "999999";
            // 
            // xrLabel5
            // 
            this.xrLabel5.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold);
            this.xrLabel5.LocationFloat = new DevExpress.Utils.PointFloat(5.833276F, 57.70829F);
            this.xrLabel5.Multiline = true;
            this.xrLabel5.Name = "xrLabel5";
            this.xrLabel5.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel5.SizeF = new System.Drawing.SizeF(102.0834F, 17.08334F);
            this.xrLabel5.StylePriority.UseFont = false;
            this.xrLabel5.Text = "Invoice No.:";
            // 
            // xrsuppliername
            // 
            this.xrsuppliername.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold);
            this.xrsuppliername.LocationFloat = new DevExpress.Utils.PointFloat(128.75F, 34.70831F);
            this.xrsuppliername.Name = "xrsuppliername";
            this.xrsuppliername.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrsuppliername.SizeF = new System.Drawing.SizeF(310.8333F, 23F);
            this.xrsuppliername.StylePriority.UseFont = false;
            this.xrsuppliername.Text = "999999";
            // 
            // xrLabel2
            // 
            this.xrLabel2.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold);
            this.xrLabel2.LocationFloat = new DevExpress.Utils.PointFloat(5.833276F, 34.70837F);
            this.xrLabel2.Multiline = true;
            this.xrLabel2.Name = "xrLabel2";
            this.xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel2.SizeF = new System.Drawing.SizeF(125.0001F, 23F);
            this.xrLabel2.StylePriority.UseFont = false;
            this.xrLabel2.Text = "Supplier Name:";
            // 
            // xrreporttype
            // 
            this.xrreporttype.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold);
            this.xrreporttype.LocationFloat = new DevExpress.Utils.PointFloat(455.8334F, 34.70837F);
            this.xrreporttype.Multiline = true;
            this.xrreporttype.Name = "xrreporttype";
            this.xrreporttype.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrreporttype.SizeF = new System.Drawing.SizeF(103.125F, 23F);
            this.xrreporttype.StylePriority.UseFont = false;
            this.xrreporttype.Text = "Shipment #:";
            // 
            // lblshipment
            // 
            this.lblshipment.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold);
            this.lblshipment.LocationFloat = new DevExpress.Utils.PointFloat(558.9584F, 34.70831F);
            this.lblshipment.Name = "lblshipment";
            this.lblshipment.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblshipment.SizeF = new System.Drawing.SizeF(71.66656F, 23F);
            this.lblshipment.StylePriority.UseFont = false;
            this.lblshipment.Text = "999999";
            // 
            // xrLabel1
            // 
            this.xrLabel1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(235.8334F, 0F);
            this.xrLabel1.Name = "xrLabel1";
            this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel1.SizeF = new System.Drawing.SizeF(213.9583F, 23F);
            this.xrLabel1.StylePriority.UseFont = false;
            this.xrLabel1.Text = "P.O RECEIVING REPORT";
            // 
            // ReportFooter
            // 
            this.ReportFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.preparedby,
            this.xrLabel6});
            this.ReportFooter.Name = "ReportFooter";
            // 
            // preparedby
            // 
            this.preparedby.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.preparedby.LocationFloat = new DevExpress.Utils.PointFloat(90.62487F, 34.33329F);
            this.preparedby.Name = "preparedby";
            this.preparedby.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.preparedby.SizeF = new System.Drawing.SizeF(154.1667F, 23F);
            this.preparedby.StylePriority.UseFont = false;
            this.preparedby.Text = "Anna Alcuizar";
            // 
            // xrLabel6
            // 
            this.xrLabel6.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel6.LocationFloat = new DevExpress.Utils.PointFloat(0F, 34.33329F);
            this.xrLabel6.Name = "xrLabel6";
            this.xrLabel6.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel6.SizeF = new System.Drawing.SizeF(259.375F, 23F);
            this.xrLabel6.StylePriority.UseFont = false;
            this.xrLabel6.Text = "Prepared By:________________";
            // 
            // ReportHeader
            // 
            this.ReportHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel1,
            this.xrinvoiceno,
            this.xrsuppliername,
            this.lblshipment,
            this.xrreporttype,
            this.xrLabel2,
            this.xrLabel5});
            this.ReportHeader.HeightF = 79.16663F;
            this.ReportHeader.Name = "ReportHeader";
            this.ReportHeader.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.ReportHeader_BeforePrint);
            // 
            // ShipmentReport
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.ReportFooter,
            this.ReportHeader});
            this.Margins = new System.Drawing.Printing.Margins(100, 80, 81, 100);
            this.Version = "18.1";
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
        private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
        private DevExpress.XtraReports.UI.ReportFooterBand ReportFooter;
        public DevExpress.XtraReports.UI.XRLabel xrreporttype;
        private DevExpress.XtraReports.UI.XRLabel xrLabel1;
        public DevExpress.XtraReports.UI.XRLabel preparedby;
        private DevExpress.XtraReports.UI.XRLabel xrLabel6;
        public DevExpress.XtraReports.UI.XRLabel lblshipment;
        public DevExpress.XtraReports.UI.XRLabel xrLabel2;
        public DevExpress.XtraReports.UI.XRLabel xrsuppliername;
        public DevExpress.XtraReports.UI.XRLabel xrLabel5;
        public DevExpress.XtraReports.UI.XRLabel xrinvoiceno;
        private DevExpress.XtraReports.UI.ReportHeaderBand ReportHeader;
        public DevExpress.XtraReports.UI.XRPictureBox xrPictureBox1;
        public DevExpress.XtraReports.UI.XRLabel xrcompanyname;
        public DevExpress.XtraReports.UI.XRLabel xrcaption1;
        public DevExpress.XtraReports.UI.XRLabel xrcaption2;
    }
}
