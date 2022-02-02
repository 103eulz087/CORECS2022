namespace SalesInventorySystem.DevExReportTemplate
{
    partial class ConsolidatedInventory
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConsolidatedInventory));
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.xrcompanyname = new DevExpress.XtraReports.UI.XRLabel();
            this.xrcaption1 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrcaption2 = new DevExpress.XtraReports.UI.XRLabel();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.ReportHeader = new DevExpress.XtraReports.UI.ReportHeaderBand();
            this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
            this.inventoryowner = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrPictureBox1 = new DevExpress.XtraReports.UI.XRPictureBox();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.HeightF = 100F;
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
            this.TopMargin.HeightF = 83.33334F;
            this.TopMargin.Name = "TopMargin";
            this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrcompanyname
            // 
            this.xrcompanyname.Font = new System.Drawing.Font("Arial Black", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrcompanyname.LocationFloat = new DevExpress.Utils.PointFloat(211.4583F, 10.00004F);
            this.xrcompanyname.Name = "xrcompanyname";
            this.xrcompanyname.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrcompanyname.SizeF = new System.Drawing.SizeF(296.25F, 23F);
            this.xrcompanyname.StylePriority.UseFont = false;
            this.xrcompanyname.Text = "ENZO\'S MEAT MARKET FOODS CORP";
            // 
            // xrcaption1
            // 
            this.xrcaption1.Font = new System.Drawing.Font("Arial", 9.75F);
            this.xrcaption1.LocationFloat = new DevExpress.Utils.PointFloat(211.4583F, 33.00006F);
            this.xrcaption1.Name = "xrcaption1";
            this.xrcaption1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrcaption1.SizeF = new System.Drawing.SizeF(296.25F, 18.83334F);
            this.xrcaption1.StylePriority.UseFont = false;
            this.xrcaption1.Text = "Unitop Shopping Mall Lapulapu Branch";
            // 
            // xrcaption2
            // 
            this.xrcaption2.Font = new System.Drawing.Font("Arial", 9.75F);
            this.xrcaption2.LocationFloat = new DevExpress.Utils.PointFloat(211.4583F, 51.83334F);
            this.xrcaption2.Name = "xrcaption2";
            this.xrcaption2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrcaption2.SizeF = new System.Drawing.SizeF(317.0833F, 18.83334F);
            this.xrcaption2.StylePriority.UseFont = false;
            this.xrcaption2.Text = "Mangubat St., Lapulapu City 6015 Cebu Philippines";
            // 
            // BottomMargin
            // 
            this.BottomMargin.HeightF = 100F;
            this.BottomMargin.Name = "BottomMargin";
            this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // ReportHeader
            // 
            this.ReportHeader.HeightF = 114.5833F;
            this.ReportHeader.Name = "ReportHeader";
            // 
            // PageHeader
            // 
            this.PageHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.inventoryowner,
            this.xrLabel1});
            this.PageHeader.HeightF = 57.29167F;
            this.PageHeader.Name = "PageHeader";
            // 
            // inventoryowner
            // 
            this.inventoryowner.Font = new System.Drawing.Font("Tahoma", 12.25F, System.Drawing.FontStyle.Bold);
            this.inventoryowner.LocationFloat = new DevExpress.Utils.PointFloat(0F, 34.29165F);
            this.inventoryowner.Name = "inventoryowner";
            this.inventoryowner.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.inventoryowner.SizeF = new System.Drawing.SizeF(211.4583F, 23F);
            this.inventoryowner.StylePriority.UseFont = false;
            this.inventoryowner.StylePriority.UseTextAlignment = false;
            this.inventoryowner.Text = "Inventory Owner";
            this.inventoryowner.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel1
            // 
            this.xrLabel1.Font = new System.Drawing.Font("Tahoma", 12.25F, System.Drawing.FontStyle.Bold);
            this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(209.3751F, 0F);
            this.xrLabel1.Name = "xrLabel1";
            this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel1.SizeF = new System.Drawing.SizeF(209.375F, 23F);
            this.xrLabel1.StylePriority.UseFont = false;
            this.xrLabel1.StylePriority.UseTextAlignment = false;
            this.xrLabel1.Text = "Consolidated Inventory";
            this.xrLabel1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            // 
            // xrPictureBox1
            // 
            this.xrPictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("xrPictureBox1.Image")));
            this.xrPictureBox1.LocationFloat = new DevExpress.Utils.PointFloat(144.7916F, 10.00004F);
            this.xrPictureBox1.Name = "xrPictureBox1";
            this.xrPictureBox1.SizeF = new System.Drawing.SizeF(66.66669F, 60.66668F);
            this.xrPictureBox1.Sizing = DevExpress.XtraPrinting.ImageSizeMode.ZoomImage;
            // 
            // ConsolidatedInventory
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.ReportHeader,
            this.PageHeader});
            this.Margins = new System.Drawing.Printing.Margins(100, 100, 83, 100);
            this.Version = "15.1";
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
        private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
        private DevExpress.XtraReports.UI.ReportHeaderBand ReportHeader;
        private DevExpress.XtraReports.UI.PageHeaderBand PageHeader;
        private DevExpress.XtraReports.UI.XRLabel xrLabel1;
        public DevExpress.XtraReports.UI.XRLabel inventoryowner;
        public DevExpress.XtraReports.UI.XRLabel xrcompanyname;
        public DevExpress.XtraReports.UI.XRLabel xrcaption1;
        public DevExpress.XtraReports.UI.XRLabel xrcaption2;
        public DevExpress.XtraReports.UI.XRPictureBox xrPictureBox1;
    }
}
