namespace SalesInventorySystem.Barcode
{
    partial class CarcassBarcodePrinting
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
            DevExpress.XtraPrinting.BarCode.Code128Generator code128Generator1 = new DevExpress.XtraPrinting.BarCode.Code128Generator();
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.ReportHeader = new DevExpress.XtraReports.UI.ReportHeaderBand();
            this.lblpalletnum = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel8 = new DevExpress.XtraReports.UI.XRLabel();
            this.lblseqnum = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel4 = new DevExpress.XtraReports.UI.XRLabel();
            this.lblmanufdate = new DevExpress.XtraReports.UI.XRLabel();
            this.lblactualweight = new DevExpress.XtraReports.UI.XRLabel();
            this.xrBarCode2 = new DevExpress.XtraReports.UI.XRBarCode();
            this.xrLabel5 = new DevExpress.XtraReports.UI.XRLabel();
            this.lblshipmentno = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel6 = new DevExpress.XtraReports.UI.XRLabel();
            this.lblprodtype = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel7 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel3 = new DevExpress.XtraReports.UI.XRLabel();
            this.lblxpirydate = new DevExpress.XtraReports.UI.XRLabel();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.HeightF = 0F;
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.Detail.SnapLinePadding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // TopMargin
            // 
            this.TopMargin.HeightF = 0F;
            this.TopMargin.Name = "TopMargin";
            this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.TopMargin.SnapLinePadding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // BottomMargin
            // 
            this.BottomMargin.HeightF = 0F;
            this.BottomMargin.Name = "BottomMargin";
            this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.BottomMargin.SnapLinePadding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // ReportHeader
            // 
            this.ReportHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.lblxpirydate,
            this.xrLabel3,
            this.lblpalletnum,
            this.xrLabel8,
            this.lblseqnum,
            this.xrLabel1,
            this.xrLabel2,
            this.xrLabel4,
            this.lblmanufdate,
            this.lblactualweight,
            this.xrBarCode2,
            this.xrLabel5,
            this.lblshipmentno,
            this.xrLabel6,
            this.lblprodtype,
            this.xrLabel7});
            this.ReportHeader.HeightF = 190F;
            this.ReportHeader.Name = "ReportHeader";
            // 
            // lblpalletnum
            // 
            this.lblpalletnum.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold);
            this.lblpalletnum.LocationFloat = new DevExpress.Utils.PointFloat(311.3542F, 30.29167F);
            this.lblpalletnum.Name = "lblpalletnum";
            this.lblpalletnum.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblpalletnum.SizeF = new System.Drawing.SizeF(56.33328F, 19.875F);
            this.lblpalletnum.StylePriority.UseFont = false;
            this.lblpalletnum.Text = "10000";
            // 
            // xrLabel8
            // 
            this.xrLabel8.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel8.LocationFloat = new DevExpress.Utils.PointFloat(231.0625F, 30.29167F);
            this.xrLabel8.Name = "xrLabel8";
            this.xrLabel8.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel8.SizeF = new System.Drawing.SizeF(80.29163F, 19.875F);
            this.xrLabel8.StylePriority.UseFont = false;
            this.xrLabel8.Text = "PALLET #:";
            // 
            // lblseqnum
            // 
            this.lblseqnum.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold);
            this.lblseqnum.LocationFloat = new DevExpress.Utils.PointFloat(311.3542F, 50.16667F);
            this.lblseqnum.Name = "lblseqnum";
            this.lblseqnum.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblseqnum.SizeF = new System.Drawing.SizeF(56.33328F, 19.875F);
            this.lblseqnum.StylePriority.UseFont = false;
            this.lblseqnum.Text = "SEQ #:";
            // 
            // xrLabel1
            // 
            this.xrLabel1.Font = new System.Drawing.Font("Arial Black", 16.75F);
            this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(17.5208F, 0F);
            this.xrLabel1.Name = "xrLabel1";
            this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel1.SizeF = new System.Drawing.SizeF(317.0417F, 30.29167F);
            this.xrLabel1.StylePriority.UseFont = false;
            this.xrLabel1.Text = "ENZO MEAT MARKET";
            // 
            // xrLabel2
            // 
            this.xrLabel2.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel2.LocationFloat = new DevExpress.Utils.PointFloat(17.5208F, 50.16667F);
            this.xrLabel2.Name = "xrLabel2";
            this.xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel2.SizeF = new System.Drawing.SizeF(137.5F, 19.875F);
            this.xrLabel2.StylePriority.UseFont = false;
            this.xrLabel2.Text = "DELIVERY DATE:";
            // 
            // xrLabel4
            // 
            this.xrLabel4.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel4.LocationFloat = new DevExpress.Utils.PointFloat(17.5208F, 87.75001F);
            this.xrLabel4.Name = "xrLabel4";
            this.xrLabel4.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel4.SizeF = new System.Drawing.SizeF(137.5F, 19.875F);
            this.xrLabel4.StylePriority.UseFont = false;
            this.xrLabel4.Text = "ACTUAL WEIGHT:";
            // 
            // lblmanufdate
            // 
            this.lblmanufdate.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold);
            this.lblmanufdate.LocationFloat = new DevExpress.Utils.PointFloat(155.0208F, 50.16667F);
            this.lblmanufdate.Name = "lblmanufdate";
            this.lblmanufdate.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblmanufdate.SizeF = new System.Drawing.SizeF(100F, 19.875F);
            this.lblmanufdate.StylePriority.UseFont = false;
            this.lblmanufdate.Text = "12/12/2016";
            // 
            // lblactualweight
            // 
            this.lblactualweight.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold);
            this.lblactualweight.LocationFloat = new DevExpress.Utils.PointFloat(155.0208F, 87.75001F);
            this.lblactualweight.Name = "lblactualweight";
            this.lblactualweight.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblactualweight.SizeF = new System.Drawing.SizeF(65.62498F, 19.875F);
            this.lblactualweight.StylePriority.UseFont = false;
            this.lblactualweight.Text = "88.888";
            // 
            // xrBarCode2
            // 
            this.xrBarCode2.AutoModule = true;
            this.xrBarCode2.Font = new System.Drawing.Font("Arial", 15.75F);
            this.xrBarCode2.LocationFloat = new DevExpress.Utils.PointFloat(17.5208F, 127.5F);
            this.xrBarCode2.Name = "xrBarCode2";
            this.xrBarCode2.Padding = new DevExpress.XtraPrinting.PaddingInfo(10, 10, 0, 0, 100F);
            this.xrBarCode2.SizeF = new System.Drawing.SizeF(372.4792F, 52.50001F);
            this.xrBarCode2.StylePriority.UseFont = false;
            this.xrBarCode2.StylePriority.UseTextAlignment = false;
            this.xrBarCode2.Symbology = code128Generator1;
            this.xrBarCode2.Text = "100021010005521230001";
            this.xrBarCode2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel5
            // 
            this.xrLabel5.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel5.LocationFloat = new DevExpress.Utils.PointFloat(17.5208F, 30.29167F);
            this.xrLabel5.Name = "xrLabel5";
            this.xrLabel5.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel5.SizeF = new System.Drawing.SizeF(110.4166F, 19.875F);
            this.xrLabel5.StylePriority.UseFont = false;
            this.xrLabel5.Text = "SHIPMENT #:";
            // 
            // lblshipmentno
            // 
            this.lblshipmentno.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold);
            this.lblshipmentno.LocationFloat = new DevExpress.Utils.PointFloat(127.9374F, 30.29167F);
            this.lblshipmentno.Name = "lblshipmentno";
            this.lblshipmentno.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblshipmentno.SizeF = new System.Drawing.SizeF(57.29166F, 19.875F);
            this.lblshipmentno.StylePriority.UseFont = false;
            this.lblshipmentno.Text = "999999";
            // 
            // xrLabel6
            // 
            this.xrLabel6.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel6.LocationFloat = new DevExpress.Utils.PointFloat(15.87479F, 107.625F);
            this.xrLabel6.Name = "xrLabel6";
            this.xrLabel6.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel6.SizeF = new System.Drawing.SizeF(44.87495F, 19.875F);
            this.xrLabel6.StylePriority.UseFont = false;
            this.xrLabel6.Text = "Type:";
            // 
            // lblprodtype
            // 
            this.lblprodtype.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold);
            this.lblprodtype.LocationFloat = new DevExpress.Utils.PointFloat(60.74987F, 107.625F);
            this.lblprodtype.Name = "lblprodtype";
            this.lblprodtype.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblprodtype.SizeF = new System.Drawing.SizeF(329.2501F, 19.875F);
            this.lblprodtype.StylePriority.UseFont = false;
            this.lblprodtype.Text = "Large Intestine";
            // 
            // xrLabel7
            // 
            this.xrLabel7.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel7.LocationFloat = new DevExpress.Utils.PointFloat(255.0208F, 50.16667F);
            this.xrLabel7.Name = "xrLabel7";
            this.xrLabel7.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel7.SizeF = new System.Drawing.SizeF(56.33328F, 19.875F);
            this.xrLabel7.StylePriority.UseFont = false;
            this.xrLabel7.Text = "SEQ #:";
            // 
            // xrLabel3
            // 
            this.xrLabel3.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel3.LocationFloat = new DevExpress.Utils.PointFloat(17.5208F, 67.875F);
            this.xrLabel3.Name = "xrLabel3";
            this.xrLabel3.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel3.SizeF = new System.Drawing.SizeF(110.4166F, 19.875F);
            this.xrLabel3.StylePriority.UseFont = false;
            this.xrLabel3.Text = "EXPIRY DATE:";
            // 
            // lblxpirydate
            // 
            this.lblxpirydate.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold);
            this.lblxpirydate.LocationFloat = new DevExpress.Utils.PointFloat(127.9374F, 67.875F);
            this.lblxpirydate.Name = "lblxpirydate";
            this.lblxpirydate.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblxpirydate.SizeF = new System.Drawing.SizeF(100F, 19.875F);
            this.lblxpirydate.StylePriority.UseFont = false;
            this.lblxpirydate.Text = "12/12/2016";
            // 
            // CarcassBarcodePrinting
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.ReportHeader});
            this.DesignerOptions.ShowPrintingWarnings = false;
            this.Margins = new System.Drawing.Printing.Margins(0, 0, 0, 0);
            this.PageHeight = 200;
            this.PageWidth = 400;
            this.PaperKind = System.Drawing.Printing.PaperKind.Custom;
            this.ShowPreviewMarginLines = false;
            this.ShowPrintMarginsWarning = false;
            this.ShowPrintStatusDialog = false;
            this.Version = "15.1";
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
        private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
        private DevExpress.XtraReports.UI.ReportHeaderBand ReportHeader;
        private DevExpress.XtraReports.UI.XRLabel xrLabel1;
        private DevExpress.XtraReports.UI.XRLabel xrLabel2;
        private DevExpress.XtraReports.UI.XRLabel xrLabel4;
        public DevExpress.XtraReports.UI.XRLabel lblmanufdate;
        public DevExpress.XtraReports.UI.XRLabel lblactualweight;
        public DevExpress.XtraReports.UI.XRBarCode xrBarCode2;
        private DevExpress.XtraReports.UI.XRLabel xrLabel5;
        public DevExpress.XtraReports.UI.XRLabel lblshipmentno;
        private DevExpress.XtraReports.UI.XRLabel xrLabel6;
        public DevExpress.XtraReports.UI.XRLabel lblprodtype;
        private DevExpress.XtraReports.UI.XRLabel xrLabel7;
        public DevExpress.XtraReports.UI.XRLabel lblseqnum;
        public DevExpress.XtraReports.UI.XRLabel lblpalletnum;
        private DevExpress.XtraReports.UI.XRLabel xrLabel8;
        public DevExpress.XtraReports.UI.XRLabel lblxpirydate;
        private DevExpress.XtraReports.UI.XRLabel xrLabel3;
    }
}
