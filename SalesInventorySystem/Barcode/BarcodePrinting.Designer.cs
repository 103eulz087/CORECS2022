namespace SalesInventorySystem.Barcode
{
    partial class BarcodePrinting
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
            this.lblxpirydate = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel4 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
            this.lblmanufdate = new DevExpress.XtraReports.UI.XRLabel();
            this.lblprodtype = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel5 = new DevExpress.XtraReports.UI.XRLabel();
            this.lbltotalkilos = new DevExpress.XtraReports.UI.XRLabel();
            this.xrBarCode2 = new DevExpress.XtraReports.UI.XRBarCode();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.xrLabel3 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrshipno = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel6 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrbatchcode = new DevExpress.XtraReports.UI.XRLabel();
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
            this.TopMargin.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel5,
            this.lbltotalkilos,
            this.lblxpirydate,
            this.xrLabel4,
            this.xrLabel1,
            this.xrLabel2,
            this.lblmanufdate,
            this.xrBarCode2,
            this.xrLabel3,
            this.xrshipno,
            this.xrLabel6,
            this.xrbatchcode,
            this.lblprodtype});
            this.TopMargin.HeightF = 200F;
            this.TopMargin.Name = "TopMargin";
            this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.TopMargin.SnapLinePadding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // lblxpirydate
            // 
            this.lblxpirydate.Font = new System.Drawing.Font("Tahoma", 9.2F);
            this.lblxpirydate.LocationFloat = new DevExpress.Utils.PointFloat(296.875F, 17.79167F);
            this.lblxpirydate.Name = "lblxpirydate";
            this.lblxpirydate.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblxpirydate.SizeF = new System.Drawing.SizeF(75.375F, 13.45837F);
            this.lblxpirydate.StylePriority.UseFont = false;
            this.lblxpirydate.Text = "12/12/2016";
            // 
            // xrLabel4
            // 
            this.xrLabel4.Font = new System.Drawing.Font("Tahoma", 9.2F);
            this.xrLabel4.LocationFloat = new DevExpress.Utils.PointFloat(204.1667F, 17.79167F);
            this.xrLabel4.Name = "xrLabel4";
            this.xrLabel4.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel4.SizeF = new System.Drawing.SizeF(92.70833F, 14.58335F);
            this.xrLabel4.StylePriority.UseFont = false;
            this.xrLabel4.Text = "EXPIRY DATE:";
            // 
            // xrLabel1
            // 
            this.xrLabel1.Font = new System.Drawing.Font("Arial Black", 12.75F);
            this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(17.70833F, 0F);
            this.xrLabel1.Name = "xrLabel1";
            this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel1.SizeF = new System.Drawing.SizeF(354.5417F, 17.79167F);
            this.xrLabel1.StylePriority.UseFont = false;
            this.xrLabel1.StylePriority.UseTextAlignment = false;
            this.xrLabel1.Text = "ENZO MEAT MARKET FOODS CORP.";
            this.xrLabel1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel2
            // 
            this.xrLabel2.Font = new System.Drawing.Font("Tahoma", 9.2F);
            this.xrLabel2.LocationFloat = new DevExpress.Utils.PointFloat(17.70833F, 17.79167F);
            this.xrLabel2.Name = "xrLabel2";
            this.xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel2.SizeF = new System.Drawing.SizeF(106.25F, 14.58335F);
            this.xrLabel2.StylePriority.UseFont = false;
            this.xrLabel2.Text = "DELIVERY DATE:";
            this.xrLabel2.TextTrimming = System.Drawing.StringTrimming.None;
            // 
            // lblmanufdate
            // 
            this.lblmanufdate.Font = new System.Drawing.Font("Tahoma", 9.2F);
            this.lblmanufdate.LocationFloat = new DevExpress.Utils.PointFloat(123.9583F, 17.79167F);
            this.lblmanufdate.Name = "lblmanufdate";
            this.lblmanufdate.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblmanufdate.SizeF = new System.Drawing.SizeF(80.20833F, 14.58335F);
            this.lblmanufdate.StylePriority.UseFont = false;
            this.lblmanufdate.Text = "12/12/2016";
            // 
            // lblprodtype
            // 
            this.lblprodtype.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblprodtype.LocationFloat = new DevExpress.Utils.PointFloat(17.70833F, 32.37502F);
            this.lblprodtype.Name = "lblprodtype";
            this.lblprodtype.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblprodtype.SizeF = new System.Drawing.SizeF(354.5417F, 18.29164F);
            this.lblprodtype.StylePriority.UseFont = false;
            this.lblprodtype.Text = "Large Intestine";
            // 
            // xrLabel5
            // 
            this.xrLabel5.Font = new System.Drawing.Font("Arial Black", 14.75F);
            this.xrLabel5.LocationFloat = new DevExpress.Utils.PointFloat(140.8333F, 87.08338F);
            this.xrLabel5.Name = "xrLabel5";
            this.xrLabel5.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.xrLabel5.SizeF = new System.Drawing.SizeF(144.7918F, 17.79169F);
            this.xrLabel5.StylePriority.UseFont = false;
            this.xrLabel5.StylePriority.UsePadding = false;
            this.xrLabel5.StylePriority.UseTextAlignment = false;
            this.xrLabel5.Text = "Total Kilos";
            this.xrLabel5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // lbltotalkilos
            // 
            this.lbltotalkilos.Font = new System.Drawing.Font("Arial Black", 14.75F);
            this.lbltotalkilos.LocationFloat = new DevExpress.Utils.PointFloat(140.8333F, 104.8751F);
            this.lbltotalkilos.Name = "lbltotalkilos";
            this.lbltotalkilos.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.lbltotalkilos.SizeF = new System.Drawing.SizeF(144.7918F, 20.29169F);
            this.lbltotalkilos.StylePriority.UseFont = false;
            this.lbltotalkilos.StylePriority.UsePadding = false;
            this.lbltotalkilos.StylePriority.UseTextAlignment = false;
            this.lbltotalkilos.Text = "88.888";
            this.lbltotalkilos.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrBarCode2
            // 
            this.xrBarCode2.AutoModule = true;
            this.xrBarCode2.Font = new System.Drawing.Font("Arial", 8.75F);
            this.xrBarCode2.LocationFloat = new DevExpress.Utils.PointFloat(17.70833F, 50.66666F);
            this.xrBarCode2.Name = "xrBarCode2";
            this.xrBarCode2.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.xrBarCode2.SizeF = new System.Drawing.SizeF(174.7917F, 121.4167F);
            this.xrBarCode2.StylePriority.UseFont = false;
            this.xrBarCode2.StylePriority.UsePadding = false;
            this.xrBarCode2.StylePriority.UseTextAlignment = false;
            this.xrBarCode2.Symbology = qrCodeGenerator1;
            this.xrBarCode2.Text = "A010000110015000111123";
            this.xrBarCode2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // BottomMargin
            // 
            this.BottomMargin.HeightF = 0F;
            this.BottomMargin.Name = "BottomMargin";
            this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.BottomMargin.SnapLinePadding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrLabel3
            // 
            this.xrLabel3.Font = new System.Drawing.Font("Arial Black", 10.75F);
            this.xrLabel3.LocationFloat = new DevExpress.Utils.PointFloat(140.8333F, 50.66666F);
            this.xrLabel3.Name = "xrLabel3";
            this.xrLabel3.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.xrLabel3.SizeF = new System.Drawing.SizeF(99.37502F, 17.79169F);
            this.xrLabel3.StylePriority.UseFont = false;
            this.xrLabel3.StylePriority.UsePadding = false;
            this.xrLabel3.StylePriority.UseTextAlignment = false;
            this.xrLabel3.Text = "Shipment #:";
            this.xrLabel3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrshipno
            // 
            this.xrshipno.Font = new System.Drawing.Font("Arial Black", 10.75F);
            this.xrshipno.LocationFloat = new DevExpress.Utils.PointFloat(240.2083F, 50.66666F);
            this.xrshipno.Name = "xrshipno";
            this.xrshipno.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.xrshipno.SizeF = new System.Drawing.SizeF(99.37502F, 17.79169F);
            this.xrshipno.StylePriority.UseFont = false;
            this.xrshipno.StylePriority.UsePadding = false;
            this.xrshipno.StylePriority.UseTextAlignment = false;
            this.xrshipno.Text = "12345";
            this.xrshipno.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel6
            // 
            this.xrLabel6.Font = new System.Drawing.Font("Arial Black", 10.75F);
            this.xrLabel6.LocationFloat = new DevExpress.Utils.PointFloat(140.8333F, 68.45835F);
            this.xrLabel6.Name = "xrLabel6";
            this.xrLabel6.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.xrLabel6.SizeF = new System.Drawing.SizeF(99.375F, 18.62503F);
            this.xrLabel6.StylePriority.UseFont = false;
            this.xrLabel6.StylePriority.UsePadding = false;
            this.xrLabel6.StylePriority.UseTextAlignment = false;
            this.xrLabel6.Text = "BatchCode:";
            this.xrLabel6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrbatchcode
            // 
            this.xrbatchcode.Font = new System.Drawing.Font("Arial Black", 10.75F);
            this.xrbatchcode.LocationFloat = new DevExpress.Utils.PointFloat(240.2083F, 68.45835F);
            this.xrbatchcode.Name = "xrbatchcode";
            this.xrbatchcode.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.xrbatchcode.SizeF = new System.Drawing.SizeF(99.37502F, 18.62503F);
            this.xrbatchcode.StylePriority.UseFont = false;
            this.xrbatchcode.StylePriority.UsePadding = false;
            this.xrbatchcode.StylePriority.UseTextAlignment = false;
            this.xrbatchcode.Text = "12345";
            this.xrbatchcode.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // BarcodePrinting
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin});
            this.Margins = new System.Drawing.Printing.Margins(0, 0, 200, 0);
            this.PageHeight = 190;
            this.PageWidth = 400;
            this.PaperKind = System.Drawing.Printing.PaperKind.Custom;
            this.ShowPrintMarginsWarning = false;
            this.ShowPrintStatusDialog = false;
            this.Version = "18.1";
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
        private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
        private DevExpress.XtraReports.UI.XRLabel xrLabel1;
        public DevExpress.XtraReports.UI.XRLabel lblmanufdate;
        private DevExpress.XtraReports.UI.XRLabel xrLabel2;
        private DevExpress.XtraReports.UI.XRLabel xrLabel5;
        public DevExpress.XtraReports.UI.XRLabel lblprodtype;
        public DevExpress.XtraReports.UI.XRBarCode xrBarCode2;
        public DevExpress.XtraReports.UI.XRLabel lbltotalkilos;
        public DevExpress.XtraReports.UI.XRLabel lblxpirydate;
        private DevExpress.XtraReports.UI.XRLabel xrLabel4;
        private DevExpress.XtraReports.UI.XRLabel xrLabel3;
        public DevExpress.XtraReports.UI.XRLabel xrshipno;
        private DevExpress.XtraReports.UI.XRLabel xrLabel6;
        public DevExpress.XtraReports.UI.XRLabel xrbatchcode;
    }
}
