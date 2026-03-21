namespace SalesInventorySystem.DevExReportTemplate
{
    partial class CreditMemoNew
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
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.ReportHeader = new DevExpress.XtraReports.UI.ReportHeaderBand();
            this.xrcustname = new DevExpress.XtraReports.UI.XRLabel();
            this.xrdate = new DevExpress.XtraReports.UI.XRLabel();
            this.xrinvoiceno = new DevExpress.XtraReports.UI.XRLabel();
            this.PageFooter = new DevExpress.XtraReports.UI.PageFooterBand();
            this.xrpreparedby = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // TopMargin
            // 
            this.TopMargin.HeightF = 230F;
            this.TopMargin.Name = "TopMargin";
            // 
            // BottomMargin
            // 
            this.BottomMargin.Name = "BottomMargin";
            // 
            // Detail
            // 
            this.Detail.HeightF = 536.6667F;
            this.Detail.Name = "Detail";
            // 
            // ReportHeader
            // 
            this.ReportHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrinvoiceno,
            this.xrdate,
            this.xrcustname});
            this.ReportHeader.HeightF = 155F;
            this.ReportHeader.Name = "ReportHeader";
            // 
            // xrcustname
            // 
            this.xrcustname.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrcustname.LocationFloat = new DevExpress.Utils.PointFloat(169.1666F, 0F);
            this.xrcustname.Name = "xrcustname";
            this.xrcustname.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrcustname.SizeF = new System.Drawing.SizeF(401.6667F, 18F);
            this.xrcustname.StylePriority.UseFont = false;
            this.xrcustname.Text = "EULZ AVANCENA";
            // 
            // xrdate
            // 
            this.xrdate.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrdate.LocationFloat = new DevExpress.Utils.PointFloat(603.6666F, 0F);
            this.xrdate.Name = "xrdate";
            this.xrdate.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.xrdate.SizeF = new System.Drawing.SizeF(213.3334F, 18F);
            this.xrdate.StylePriority.UseFont = false;
            this.xrdate.StylePriority.UsePadding = false;
            this.xrdate.Text = "January 21 2018";
            // 
            // xrinvoiceno
            // 
            this.xrinvoiceno.Font = new System.Drawing.Font("Century Gothic", 9.2F);
            this.xrinvoiceno.LocationFloat = new DevExpress.Utils.PointFloat(169.1666F, 28.83331F);
            this.xrinvoiceno.Name = "xrinvoiceno";
            this.xrinvoiceno.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrinvoiceno.SizeF = new System.Drawing.SizeF(401.6667F, 14.66667F);
            this.xrinvoiceno.StylePriority.UseFont = false;
            // 
            // PageFooter
            // 
            this.PageFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel1,
            this.xrpreparedby});
            this.PageFooter.Name = "PageFooter";
            // 
            // xrpreparedby
            // 
            this.xrpreparedby.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrpreparedby.LocationFloat = new DevExpress.Utils.PointFloat(27.49998F, 20.08382F);
            this.xrpreparedby.Name = "xrpreparedby";
            this.xrpreparedby.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrpreparedby.SizeF = new System.Drawing.SizeF(199.1667F, 18F);
            this.xrpreparedby.StylePriority.UseFont = false;
            this.xrpreparedby.Text = "EULZ AVANCENA";
            // 
            // xrLabel1
            // 
            this.xrLabel1.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(603.6666F, 20.08382F);
            this.xrLabel1.Name = "xrLabel1";
            this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel1.SizeF = new System.Drawing.SizeF(199.1667F, 18F);
            this.xrLabel1.StylePriority.UseFont = false;
            this.xrLabel1.Text = "EULZ AVANCENA";
            // 
            // CreditMemoNew
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.TopMargin,
            this.BottomMargin,
            this.Detail,
            this.ReportHeader,
            this.PageFooter});
            this.Font = new System.Drawing.Font("Arial", 9.75F);
            this.Margins = new System.Drawing.Printing.Margins(0, 0, 230, 100);
            this.PageHeight = 1169;
            this.PageWidth = 827;
            this.PaperKind = System.Drawing.Printing.PaperKind.A4;
            this.ShowPreviewMarginLines = false;
            this.ShowPrintMarginsWarning = false;
            this.Version = "19.2";
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
        private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.XtraReports.UI.ReportHeaderBand ReportHeader;
        public DevExpress.XtraReports.UI.XRLabel xrcustname;
        public DevExpress.XtraReports.UI.XRLabel xrdate;
        public DevExpress.XtraReports.UI.XRLabel xrinvoiceno;
        private DevExpress.XtraReports.UI.PageFooterBand PageFooter;
        public DevExpress.XtraReports.UI.XRLabel xrLabel1;
        public DevExpress.XtraReports.UI.XRLabel xrpreparedby;
    }
}
