namespace SalesInventorySystem.Barcode
{
    partial class PalletPrinting
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
            this.components = new System.ComponentModel.Container();
            DevExpress.XtraPrinting.BarCode.Code128Generator code128Generator1 = new DevExpress.XtraPrinting.BarCode.Code128Generator();
            DevExpress.DataAccess.Sql.StoredProcQuery storedProcQuery1 = new DevExpress.DataAccess.Sql.StoredProcQuery();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PalletPrinting));
            DevExpress.DataAccess.Sql.StoredProcQuery storedProcQuery2 = new DevExpress.DataAccess.Sql.StoredProcQuery();
            DevExpress.DataAccess.Sql.QueryParameter queryParameter1 = new DevExpress.DataAccess.Sql.QueryParameter();
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.lbltotalkilos = new DevExpress.XtraReports.UI.XRLabel();
            this.lblpalletno = new DevExpress.XtraReports.UI.XRLabel();
            this.lblshipmentno = new DevExpress.XtraReports.UI.XRLabel();
            this.xrBarCode2 = new DevExpress.XtraReports.UI.XRBarCode();
            this.xrLabel5 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel4 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel3 = new DevExpress.XtraReports.UI.XRLabel();
            this.lblmanufdate = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.sqlDataSource1 = new DevExpress.DataAccess.Sql.SqlDataSource(this.components);
            this.sqlDataSource2 = new DevExpress.DataAccess.Sql.SqlDataSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.lbltotalkilos,
            this.lblpalletno,
            this.lblshipmentno,
            this.xrBarCode2,
            this.xrLabel5,
            this.xrLabel4,
            this.xrLabel3,
            this.lblmanufdate,
            this.xrLabel2,
            this.xrLabel1});
            this.Detail.HeightF = 190F;
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.Detail.SnapLinePadding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // lbltotalkilos
            // 
            this.lbltotalkilos.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "spp_PrintPallete.TotalWeight")});
            this.lbltotalkilos.Font = new System.Drawing.Font("Arial Black", 18.75F);
            this.lbltotalkilos.LocationFloat = new DevExpress.Utils.PointFloat(194.7917F, 100.3333F);
            this.lbltotalkilos.Name = "lbltotalkilos";
            this.lbltotalkilos.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lbltotalkilos.SizeF = new System.Drawing.SizeF(135.4167F, 30.29166F);
            this.lbltotalkilos.StylePriority.UseFont = false;
            this.lbltotalkilos.Text = "lbltotalkilos";
            // 
            // lblpalletno
            // 
            this.lblpalletno.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "spp_PrintPallete.PalletNo")});
            this.lblpalletno.Font = new System.Drawing.Font("Arial Black", 18.75F);
            this.lblpalletno.LocationFloat = new DevExpress.Utils.PointFloat(175F, 70.04166F);
            this.lblpalletno.Name = "lblpalletno";
            this.lblpalletno.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblpalletno.SizeF = new System.Drawing.SizeF(155.2084F, 30.29164F);
            this.lblpalletno.StylePriority.UseFont = false;
            this.lblpalletno.Text = "lblpalletno";
            // 
            // lblshipmentno
            // 
            this.lblshipmentno.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "spp_PrintPallete.ShipmentNo")});
            this.lblshipmentno.Font = new System.Drawing.Font("Arial", 11.25F);
            this.lblshipmentno.LocationFloat = new DevExpress.Utils.PointFloat(126.0417F, 50.16667F);
            this.lblshipmentno.Name = "lblshipmentno";
            this.lblshipmentno.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblshipmentno.SizeF = new System.Drawing.SizeF(100F, 23F);
            this.lblshipmentno.StylePriority.UseFont = false;
            this.lblshipmentno.Text = "lblshipmentno";
            // 
            // xrBarCode2
            // 
            this.xrBarCode2.AutoModule = true;
            this.xrBarCode2.Font = new System.Drawing.Font("Arial", 15.75F);
            this.xrBarCode2.LocationFloat = new DevExpress.Utils.PointFloat(22.91667F, 130.625F);
            this.xrBarCode2.Name = "xrBarCode2";
            this.xrBarCode2.Padding = new DevExpress.XtraPrinting.PaddingInfo(10, 10, 0, 0, 100F);
            this.xrBarCode2.SizeF = new System.Drawing.SizeF(349.3334F, 43.74997F);
            this.xrBarCode2.StylePriority.UseFont = false;
            this.xrBarCode2.StylePriority.UseTextAlignment = false;
            this.xrBarCode2.Symbology = code128Generator1;
            this.xrBarCode2.Text = "0123456789123";
            this.xrBarCode2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel5
            // 
            this.xrLabel5.CanShrink = true;
            this.xrLabel5.Font = new System.Drawing.Font("Arial Black", 18.75F);
            this.xrLabel5.LocationFloat = new DevExpress.Utils.PointFloat(22.91667F, 100.3332F);
            this.xrLabel5.Name = "xrLabel5";
            this.xrLabel5.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.xrLabel5.SizeF = new System.Drawing.SizeF(171.875F, 30.29169F);
            this.xrLabel5.StylePriority.UseFont = false;
            this.xrLabel5.StylePriority.UsePadding = false;
            this.xrLabel5.StylePriority.UseTextAlignment = false;
            this.xrLabel5.Text = "Total Kilos:";
            this.xrLabel5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrLabel5.TextTrimming = System.Drawing.StringTrimming.None;
            // 
            // xrLabel4
            // 
            this.xrLabel4.Font = new System.Drawing.Font("Arial Black", 18.75F);
            this.xrLabel4.LocationFloat = new DevExpress.Utils.PointFloat(22.91667F, 70.04167F);
            this.xrLabel4.Name = "xrLabel4";
            this.xrLabel4.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel4.SizeF = new System.Drawing.SizeF(152.0833F, 30.29167F);
            this.xrLabel4.StylePriority.UseFont = false;
            this.xrLabel4.StylePriority.UseTextAlignment = false;
            this.xrLabel4.Text = "PALLET #:";
            this.xrLabel4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel3
            // 
            this.xrLabel3.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel3.LocationFloat = new DevExpress.Utils.PointFloat(22.91667F, 50.16667F);
            this.xrLabel3.Name = "xrLabel3";
            this.xrLabel3.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel3.SizeF = new System.Drawing.SizeF(103.125F, 19.875F);
            this.xrLabel3.StylePriority.UseFont = false;
            this.xrLabel3.Text = "SHIPMENT #:";
            // 
            // lblmanufdate
            // 
            this.lblmanufdate.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold);
            this.lblmanufdate.LocationFloat = new DevExpress.Utils.PointFloat(160.4167F, 30.29167F);
            this.lblmanufdate.Name = "lblmanufdate";
            this.lblmanufdate.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblmanufdate.SizeF = new System.Drawing.SizeF(100F, 19.875F);
            this.lblmanufdate.StylePriority.UseFont = false;
            this.lblmanufdate.Text = "12/12/2016";
            // 
            // xrLabel2
            // 
            this.xrLabel2.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel2.LocationFloat = new DevExpress.Utils.PointFloat(22.91667F, 30.29167F);
            this.xrLabel2.Name = "xrLabel2";
            this.xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel2.SizeF = new System.Drawing.SizeF(137.5F, 19.875F);
            this.xrLabel2.StylePriority.UseFont = false;
            this.xrLabel2.Text = "DELIVERY DATE:";
            // 
            // xrLabel1
            // 
            this.xrLabel1.Font = new System.Drawing.Font("Arial Black", 16.75F);
            this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(22.91667F, 0F);
            this.xrLabel1.Name = "xrLabel1";
            this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel1.SizeF = new System.Drawing.SizeF(235.4167F, 30.29167F);
            this.xrLabel1.StylePriority.UseFont = false;
            this.xrLabel1.Text = "ENZO MEATSHOP";
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
            // sqlDataSource1
            // 
            this.sqlDataSource1.ConnectionName = "salesinventory";
            this.sqlDataSource1.Name = "sqlDataSource1";
            storedProcQuery1.Name = "spp_PrintPallete";
            storedProcQuery1.StoredProcName = "spp_PrintPallete";
            this.sqlDataSource1.Queries.AddRange(new DevExpress.DataAccess.Sql.SqlQuery[] {
            storedProcQuery1});
            this.sqlDataSource1.ResultSchemaSerializable = resources.GetString("sqlDataSource1.ResultSchemaSerializable");
            // 
            // sqlDataSource2
            // 
            this.sqlDataSource2.ConnectionName = "salesinventory";
            this.sqlDataSource2.Name = "sqlDataSource2";
            storedProcQuery2.Name = "spp_PrintPallete";
            queryParameter1.Name = "@parmshipmentnum";
            queryParameter1.Type = typeof(string);
            queryParameter1.ValueInfo = " ";
            storedProcQuery2.Parameters.Add(queryParameter1);
            storedProcQuery2.StoredProcName = "spp_PrintPallete";
            this.sqlDataSource2.Queries.AddRange(new DevExpress.DataAccess.Sql.SqlQuery[] {
            storedProcQuery2});
            this.sqlDataSource2.ResultSchemaSerializable = resources.GetString("sqlDataSource2.ResultSchemaSerializable");
            // 
            // PalletPrinting
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin});
            this.ComponentStorage.AddRange(new System.ComponentModel.IComponent[] {
            this.sqlDataSource1,
            this.sqlDataSource2});
            this.DataMember = "spp_PrintPallete";
            this.DataSource = this.sqlDataSource2;
            this.Margins = new System.Drawing.Printing.Margins(2, 3, 0, 0);
            this.PageHeight = 200;
            this.PageWidth = 400;
            this.PaperKind = System.Drawing.Printing.PaperKind.Custom;
            this.ShowPrintMarginsWarning = false;
            this.ShowPrintStatusDialog = false;
            this.Version = "15.1";
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
        private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
        private DevExpress.DataAccess.Sql.SqlDataSource sqlDataSource1;
        private DevExpress.XtraReports.UI.XRLabel xrLabel1;
        private DevExpress.XtraReports.UI.XRLabel xrLabel3;
        public DevExpress.XtraReports.UI.XRLabel lblmanufdate;
        private DevExpress.XtraReports.UI.XRLabel xrLabel2;
        private DevExpress.XtraReports.UI.XRLabel xrLabel5;
        private DevExpress.XtraReports.UI.XRLabel xrLabel4;
        public DevExpress.XtraReports.UI.XRBarCode xrBarCode2;
        private DevExpress.DataAccess.Sql.SqlDataSource sqlDataSource2;
        public DevExpress.XtraReports.UI.XRLabel lblshipmentno;
        public DevExpress.XtraReports.UI.XRLabel lblpalletno;
        public DevExpress.XtraReports.UI.XRLabel lbltotalkilos;
    }
}
