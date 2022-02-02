namespace SalesInventorySystem.DevExReportTemplate
{
    partial class SalesInvoice
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
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.ReportHeader = new DevExpress.XtraReports.UI.ReportHeaderBand();
            this.xrterms = new DevExpress.XtraReports.UI.XRLabel();
            this.xrbusinessstyle = new DevExpress.XtraReports.UI.XRLabel();
            this.xrdate = new DevExpress.XtraReports.UI.XRLabel();
            this.xrcustname = new DevExpress.XtraReports.UI.XRLabel();
            this.xrtin = new DevExpress.XtraReports.UI.XRLabel();
            this.xraddress = new DevExpress.XtraReports.UI.XRLabel();
            this.PageFooter = new DevExpress.XtraReports.UI.PageFooterBand();
            this.xrlblvatablesales = new DevExpress.XtraReports.UI.XRLabel();
            this.xrlblamountdue = new DevExpress.XtraReports.UI.XRLabel();
            this.xrlbladdvat = new DevExpress.XtraReports.UI.XRLabel();
            this.xrdeliveredby = new DevExpress.XtraReports.UI.XRLabel();
            this.xrpreparedby = new DevExpress.XtraReports.UI.XRLabel();
            this.xrlbltotalamountdue = new DevExpress.XtraReports.UI.XRLabel();
            this.xrlblvatexemptsales = new DevExpress.XtraReports.UI.XRLabel();
            this.xrlblvatamount = new DevExpress.XtraReports.UI.XRLabel();
            this.xrlblzeroratedsales = new DevExpress.XtraReports.UI.XRLabel();
            this.xrlblnetofvat = new DevExpress.XtraReports.UI.XRLabel();
            this.xrlbdiscount = new DevExpress.XtraReports.UI.XRLabel();
            this.xrlbllessvat = new DevExpress.XtraReports.UI.XRLabel();
            this.xrlbltotalsales = new DevExpress.XtraReports.UI.XRLabel();
            this.xrcontrolno = new DevExpress.XtraReports.UI.XRLabel();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.HeightF = 346.6667F;
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // TopMargin
            // 
            this.TopMargin.Name = "TopMargin";
            this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // BottomMargin
            // 
            this.BottomMargin.HeightF = 65.83328F;
            this.BottomMargin.Name = "BottomMargin";
            this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // ReportHeader
            // 
            this.ReportHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrcontrolno,
            this.xrterms,
            this.xrbusinessstyle,
            this.xrdate,
            this.xrcustname,
            this.xrtin,
            this.xraddress});
            this.ReportHeader.HeightF = 101.6667F;
            this.ReportHeader.Name = "ReportHeader";
            // 
            // xrterms
            // 
            this.xrterms.Font = new System.Drawing.Font("Century Gothic", 9.2F);
            this.xrterms.LocationFloat = new DevExpress.Utils.PointFloat(698F, 18.00001F);
            this.xrterms.Name = "xrterms";
            this.xrterms.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.xrterms.SizeF = new System.Drawing.SizeF(223.3334F, 18F);
            this.xrterms.StylePriority.UseFont = false;
            this.xrterms.StylePriority.UsePadding = false;
            this.xrterms.Text = "15 days";
            // 
            // xrbusinessstyle
            // 
            this.xrbusinessstyle.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrbusinessstyle.LocationFloat = new DevExpress.Utils.PointFloat(176.6666F, 54.00002F);
            this.xrbusinessstyle.Name = "xrbusinessstyle";
            this.xrbusinessstyle.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrbusinessstyle.SizeF = new System.Drawing.SizeF(401.6667F, 18F);
            this.xrbusinessstyle.StylePriority.UseFont = false;
            // 
            // xrdate
            // 
            this.xrdate.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrdate.LocationFloat = new DevExpress.Utils.PointFloat(698F, 0F);
            this.xrdate.Name = "xrdate";
            this.xrdate.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.xrdate.SizeF = new System.Drawing.SizeF(223.3334F, 18F);
            this.xrdate.StylePriority.UseFont = false;
            this.xrdate.StylePriority.UsePadding = false;
            this.xrdate.Text = "January 21 2018";
            // 
            // xrcustname
            // 
            this.xrcustname.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrcustname.LocationFloat = new DevExpress.Utils.PointFloat(176.6666F, 0F);
            this.xrcustname.Name = "xrcustname";
            this.xrcustname.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrcustname.SizeF = new System.Drawing.SizeF(401.6667F, 18F);
            this.xrcustname.StylePriority.UseFont = false;
            this.xrcustname.Text = "EULZ AVANCENA";
            // 
            // xrtin
            // 
            this.xrtin.Font = new System.Drawing.Font("Century Gothic", 9.2F);
            this.xrtin.LocationFloat = new DevExpress.Utils.PointFloat(176.6666F, 18.00001F);
            this.xrtin.Name = "xrtin";
            this.xrtin.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrtin.SizeF = new System.Drawing.SizeF(401.6667F, 14.66667F);
            this.xrtin.StylePriority.UseFont = false;
            // 
            // xraddress
            // 
            this.xraddress.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xraddress.LocationFloat = new DevExpress.Utils.PointFloat(176.6666F, 32.66668F);
            this.xraddress.Name = "xraddress";
            this.xraddress.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xraddress.SizeF = new System.Drawing.SizeF(325.625F, 13F);
            this.xraddress.StylePriority.UseFont = false;
            this.xraddress.Text = "UNITOP SHOPPING MALL LLC BR., MANGUBAT ST. LAPULAPU";
            // 
            // PageFooter
            // 
            this.PageFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrlblvatablesales,
            this.xrlblamountdue,
            this.xrlbladdvat,
            this.xrdeliveredby,
            this.xrpreparedby,
            this.xrlbltotalamountdue,
            this.xrlblvatexemptsales,
            this.xrlblvatamount,
            this.xrlblzeroratedsales,
            this.xrlblnetofvat,
            this.xrlbdiscount,
            this.xrlbllessvat,
            this.xrlbltotalsales});
            this.PageFooter.HeightF = 147.5001F;
            this.PageFooter.Name = "PageFooter";
            // 
            // xrlblvatablesales
            // 
            this.xrlblvatablesales.Font = new System.Drawing.Font("Times New Roman", 8.8F);
            this.xrlblvatablesales.LocationFloat = new DevExpress.Utils.PointFloat(725.8334F, 16.74947F);
            this.xrlblvatablesales.Name = "xrlblvatablesales";
            this.xrlblvatablesales.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.xrlblvatablesales.SizeF = new System.Drawing.SizeF(113.3334F, 11.5002F);
            this.xrlblvatablesales.StylePriority.UseFont = false;
            this.xrlblvatablesales.StylePriority.UsePadding = false;
            this.xrlblvatablesales.StylePriority.UseTextAlignment = false;
            this.xrlblvatablesales.Text = "99,99,999.00";
            this.xrlblvatablesales.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleJustify;
            // 
            // xrlblamountdue
            // 
            this.xrlblamountdue.Font = new System.Drawing.Font("Times New Roman", 8.8F);
            this.xrlblamountdue.LocationFloat = new DevExpress.Utils.PointFloat(725.8334F, 107.9995F);
            this.xrlblamountdue.Name = "xrlblamountdue";
            this.xrlblamountdue.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.xrlblamountdue.SizeF = new System.Drawing.SizeF(113.3334F, 13.16687F);
            this.xrlblamountdue.StylePriority.UseFont = false;
            this.xrlblamountdue.StylePriority.UsePadding = false;
            this.xrlblamountdue.StylePriority.UseTextAlignment = false;
            this.xrlblamountdue.Text = "99,99,999.00";
            this.xrlblamountdue.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleJustify;
            // 
            // xrlbladdvat
            // 
            this.xrlbladdvat.Font = new System.Drawing.Font("Times New Roman", 8.8F);
            this.xrlbladdvat.LocationFloat = new DevExpress.Utils.PointFloat(725.8334F, 121.1663F);
            this.xrlbladdvat.Name = "xrlbladdvat";
            this.xrlbladdvat.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.xrlbladdvat.SizeF = new System.Drawing.SizeF(113.3334F, 13.16687F);
            this.xrlbladdvat.StylePriority.UseFont = false;
            this.xrlbladdvat.StylePriority.UsePadding = false;
            this.xrlbladdvat.StylePriority.UseTextAlignment = false;
            this.xrlbladdvat.Text = "99,99,999.00";
            this.xrlbladdvat.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleJustify;
            // 
            // xrdeliveredby
            // 
            this.xrdeliveredby.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrdeliveredby.LocationFloat = new DevExpress.Utils.PointFloat(355.6666F, 74.25049F);
            this.xrdeliveredby.Name = "xrdeliveredby";
            this.xrdeliveredby.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrdeliveredby.SizeF = new System.Drawing.SizeF(128.3334F, 18F);
            this.xrdeliveredby.StylePriority.UseFont = false;
            // 
            // xrpreparedby
            // 
            this.xrpreparedby.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrpreparedby.LocationFloat = new DevExpress.Utils.PointFloat(125.8333F, 74.25047F);
            this.xrpreparedby.Name = "xrpreparedby";
            this.xrpreparedby.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrpreparedby.SizeF = new System.Drawing.SizeF(199.1667F, 18F);
            this.xrpreparedby.StylePriority.UseFont = false;
            this.xrpreparedby.Text = "EULZ AVANCENA";
            // 
            // xrlbltotalamountdue
            // 
            this.xrlbltotalamountdue.Font = new System.Drawing.Font("Times New Roman", 12.2F, System.Drawing.FontStyle.Bold);
            this.xrlbltotalamountdue.LocationFloat = new DevExpress.Utils.PointFloat(725.8334F, 134.3332F);
            this.xrlbltotalamountdue.Name = "xrlbltotalamountdue";
            this.xrlbltotalamountdue.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.xrlbltotalamountdue.SizeF = new System.Drawing.SizeF(113.3334F, 13.16687F);
            this.xrlbltotalamountdue.StylePriority.UseFont = false;
            this.xrlbltotalamountdue.StylePriority.UsePadding = false;
            this.xrlbltotalamountdue.StylePriority.UseTextAlignment = false;
            this.xrlbltotalamountdue.Text = "99,99,999.00";
            this.xrlbltotalamountdue.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleJustify;
            // 
            // xrlblvatexemptsales
            // 
            this.xrlblvatexemptsales.Font = new System.Drawing.Font("Times New Roman", 8.8F);
            this.xrlblvatexemptsales.LocationFloat = new DevExpress.Utils.PointFloat(725.8334F, 28.24966F);
            this.xrlblvatexemptsales.Name = "xrlblvatexemptsales";
            this.xrlblvatexemptsales.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.xrlblvatexemptsales.SizeF = new System.Drawing.SizeF(113.3334F, 11.5002F);
            this.xrlblvatexemptsales.StylePriority.UseFont = false;
            this.xrlblvatexemptsales.StylePriority.UsePadding = false;
            this.xrlblvatexemptsales.StylePriority.UseTextAlignment = false;
            this.xrlblvatexemptsales.Text = "99,99,999.00";
            this.xrlblvatexemptsales.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleJustify;
            // 
            // xrlblvatamount
            // 
            this.xrlblvatamount.Font = new System.Drawing.Font("Times New Roman", 8.8F);
            this.xrlblvatamount.LocationFloat = new DevExpress.Utils.PointFloat(725.8334F, 51.25005F);
            this.xrlblvatamount.Name = "xrlblvatamount";
            this.xrlblvatamount.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.xrlblvatamount.SizeF = new System.Drawing.SizeF(113.3334F, 11.5002F);
            this.xrlblvatamount.StylePriority.UseFont = false;
            this.xrlblvatamount.StylePriority.UsePadding = false;
            this.xrlblvatamount.StylePriority.UseTextAlignment = false;
            this.xrlblvatamount.Text = "99,99,999.00";
            this.xrlblvatamount.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleJustify;
            // 
            // xrlblzeroratedsales
            // 
            this.xrlblzeroratedsales.Font = new System.Drawing.Font("Times New Roman", 8.8F);
            this.xrlblzeroratedsales.LocationFloat = new DevExpress.Utils.PointFloat(725.8334F, 39.74986F);
            this.xrlblzeroratedsales.Name = "xrlblzeroratedsales";
            this.xrlblzeroratedsales.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.xrlblzeroratedsales.SizeF = new System.Drawing.SizeF(113.3334F, 11.5002F);
            this.xrlblzeroratedsales.StylePriority.UseFont = false;
            this.xrlblzeroratedsales.StylePriority.UsePadding = false;
            this.xrlblzeroratedsales.StylePriority.UseTextAlignment = false;
            this.xrlblzeroratedsales.Text = "0.00";
            this.xrlblzeroratedsales.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleJustify;
            // 
            // xrlblnetofvat
            // 
            this.xrlblnetofvat.Font = new System.Drawing.Font("Times New Roman", 8.8F);
            this.xrlblnetofvat.LocationFloat = new DevExpress.Utils.PointFloat(725.8333F, 85.75068F);
            this.xrlblnetofvat.Name = "xrlblnetofvat";
            this.xrlblnetofvat.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.xrlblnetofvat.SizeF = new System.Drawing.SizeF(113.3334F, 11.5002F);
            this.xrlblnetofvat.StylePriority.UseFont = false;
            this.xrlblnetofvat.StylePriority.UsePadding = false;
            this.xrlblnetofvat.StylePriority.UseTextAlignment = false;
            this.xrlblnetofvat.Text = "99,99,999.00";
            this.xrlblnetofvat.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleJustify;
            // 
            // xrlbdiscount
            // 
            this.xrlbdiscount.Font = new System.Drawing.Font("Times New Roman", 8.8F);
            this.xrlbdiscount.LocationFloat = new DevExpress.Utils.PointFloat(725.8334F, 97.25088F);
            this.xrlbdiscount.Name = "xrlbdiscount";
            this.xrlbdiscount.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.xrlbdiscount.SizeF = new System.Drawing.SizeF(113.3334F, 11.5002F);
            this.xrlbdiscount.StylePriority.UseFont = false;
            this.xrlbdiscount.StylePriority.UsePadding = false;
            this.xrlbdiscount.StylePriority.UseTextAlignment = false;
            this.xrlbdiscount.Text = "0.00";
            this.xrlbdiscount.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleJustify;
            // 
            // xrlbllessvat
            // 
            this.xrlbllessvat.Font = new System.Drawing.Font("Times New Roman", 8.8F);
            this.xrlbllessvat.LocationFloat = new DevExpress.Utils.PointFloat(725.8334F, 74.25049F);
            this.xrlbllessvat.Name = "xrlbllessvat";
            this.xrlbllessvat.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.xrlbllessvat.SizeF = new System.Drawing.SizeF(113.3334F, 11.5002F);
            this.xrlbllessvat.StylePriority.UseFont = false;
            this.xrlbllessvat.StylePriority.UsePadding = false;
            this.xrlbllessvat.StylePriority.UseTextAlignment = false;
            this.xrlbllessvat.Text = "99,99,999.00";
            this.xrlbllessvat.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleJustify;
            // 
            // xrlbltotalsales
            // 
            this.xrlbltotalsales.Font = new System.Drawing.Font("Times New Roman", 8.8F);
            this.xrlbltotalsales.LocationFloat = new DevExpress.Utils.PointFloat(725.8334F, 62.75024F);
            this.xrlbltotalsales.Name = "xrlbltotalsales";
            this.xrlbltotalsales.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.xrlbltotalsales.SizeF = new System.Drawing.SizeF(113.3334F, 11.5002F);
            this.xrlbltotalsales.StylePriority.UseFont = false;
            this.xrlbltotalsales.StylePriority.UsePadding = false;
            this.xrlbltotalsales.StylePriority.UseTextAlignment = false;
            this.xrlbltotalsales.Text = "99,99,999.00";
            this.xrlbltotalsales.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleJustify;
            // 
            // xrcontrolno
            // 
            this.xrcontrolno.Font = new System.Drawing.Font("Century Gothic", 9.2F);
            this.xrcontrolno.LocationFloat = new DevExpress.Utils.PointFloat(698.0001F, 36.00001F);
            this.xrcontrolno.Name = "xrcontrolno";
            this.xrcontrolno.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.xrcontrolno.SizeF = new System.Drawing.SizeF(223.3334F, 18F);
            this.xrcontrolno.StylePriority.UseFont = false;
            this.xrcontrolno.StylePriority.UsePadding = false;
            // 
            // SalesInvoice
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.ReportHeader,
            this.PageFooter});
            this.Margins = new System.Drawing.Printing.Margins(0, 2, 100, 66);
            this.PageHeight = 750;
            this.PageWidth = 950;
            this.PaperKind = System.Drawing.Printing.PaperKind.Custom;
            this.ShowPreviewMarginLines = false;
            this.ShowPrintMarginsWarning = false;
            this.Version = "19.2";
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
        private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
        private DevExpress.XtraReports.UI.ReportHeaderBand ReportHeader;
        private DevExpress.XtraReports.UI.PageFooterBand PageFooter;
        public DevExpress.XtraReports.UI.XRLabel xrlbltotalamountdue;
        public DevExpress.XtraReports.UI.XRLabel xrlblamountdue;
        public DevExpress.XtraReports.UI.XRLabel xrlbladdvat;
        public DevExpress.XtraReports.UI.XRLabel xrlblvatexemptsales;
        public DevExpress.XtraReports.UI.XRLabel xrlblvatamount;
        public DevExpress.XtraReports.UI.XRLabel xrlblzeroratedsales;
        public DevExpress.XtraReports.UI.XRLabel xrlblnetofvat;
        public DevExpress.XtraReports.UI.XRLabel xrlbdiscount;
        public DevExpress.XtraReports.UI.XRLabel xrlbllessvat;
        public DevExpress.XtraReports.UI.XRLabel xrlbltotalsales;
        public DevExpress.XtraReports.UI.XRLabel xrlblvatablesales;
        public DevExpress.XtraReports.UI.XRLabel xrpreparedby;
        public DevExpress.XtraReports.UI.XRLabel xrdeliveredby;
        public DevExpress.XtraReports.UI.XRLabel xrcustname;
        public DevExpress.XtraReports.UI.XRLabel xrtin;
        public DevExpress.XtraReports.UI.XRLabel xraddress;
        public DevExpress.XtraReports.UI.XRLabel xrbusinessstyle;
        public DevExpress.XtraReports.UI.XRLabel xrdate;
        public DevExpress.XtraReports.UI.XRLabel xrterms;
        public DevExpress.XtraReports.UI.XRLabel xrcontrolno;
    }
}
