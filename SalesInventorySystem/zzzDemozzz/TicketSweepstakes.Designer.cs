namespace SalesInventorySystem.zzzDemozzz
{
    partial class TicketSweepstakes
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
            DevExpress.XtraPrinting.BarCode.QRCodeGenerator qrCodeGenerator1 = new DevExpress.XtraPrinting.BarCode.QRCodeGenerator();
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.xrLabelDate = new DevExpress.XtraReports.UI.XRLabel();
            this.xrserial = new DevExpress.XtraReports.UI.XRBarCode();
            this.xrNum = new DevExpress.XtraReports.UI.XRLabel();
            this.SubBand1 = new DevExpress.XtraReports.UI.SubBand();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrserial,
            this.xrLabelDate,
            this.xrNum});
            this.Detail.HeightF = 101.6667F;
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.Detail.SubBands.AddRange(new DevExpress.XtraReports.UI.SubBand[] {
            this.SubBand1});
            this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // TopMargin
            // 
            this.TopMargin.HeightF = 0F;
            this.TopMargin.Name = "TopMargin";
            this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // BottomMargin
            // 
            this.BottomMargin.HeightF = 0F;
            this.BottomMargin.Name = "BottomMargin";
            this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrLabelDate
            // 
            this.xrLabelDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.xrLabelDate.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrLabelDate.Multiline = true;
            this.xrLabelDate.Name = "xrLabelDate";
            this.xrLabelDate.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabelDate.SizeF = new System.Drawing.SizeF(100F, 23F);
            this.xrLabelDate.StylePriority.UseFont = false;
            this.xrLabelDate.StylePriority.UseTextAlignment = false;
            this.xrLabelDate.Text = "xrLabelDate";
            this.xrLabelDate.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrserial
            // 
            this.xrserial.AutoModule = true;
            this.xrserial.LocationFloat = new DevExpress.Utils.PointFloat(0F, 46.00001F);
            this.xrserial.Name = "xrserial";
            this.xrserial.Padding = new DevExpress.XtraPrinting.PaddingInfo(10, 10, 0, 0, 100F);
            this.xrserial.SizeF = new System.Drawing.SizeF(62.5F, 47.16668F);
            this.xrserial.Symbology = qrCodeGenerator1;
            // 
            // xrNum
            // 
            this.xrNum.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.xrNum.LocationFloat = new DevExpress.Utils.PointFloat(0F, 23.00001F);
            this.xrNum.Multiline = true;
            this.xrNum.Name = "xrNum";
            this.xrNum.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrNum.SizeF = new System.Drawing.SizeF(100F, 23F);
            this.xrNum.StylePriority.UseFont = false;
            this.xrNum.StylePriority.UseTextAlignment = false;
            this.xrNum.Text = "xrLabel1";
            this.xrNum.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // SubBand1
            // 
            this.SubBand1.Name = "SubBand1";
            // 
            // TicketSweepstakes
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin});
            this.Margins = new System.Drawing.Printing.Margins(0, 1, 0, 0);
            this.PageWidth = 307;
            this.PaperKind = System.Drawing.Printing.PaperKind.Custom;
            this.Version = "18.1";
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
        private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
        public DevExpress.XtraReports.UI.XRLabel xrLabelDate;
        public DevExpress.XtraReports.UI.XRBarCode xrserial;
        public DevExpress.XtraReports.UI.XRLabel xrNum;
        private DevExpress.XtraReports.UI.SubBand SubBand1;
    }
}
