namespace SalesInventorySystem
{
    partial class ViewBranchOrder
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ViewBranchOrder));
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.confirmOrderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuPending = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.processThisOrderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cancelThisOrderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStripForDelivery = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.printDeliveryReceiptToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabMain = new DevExpress.XtraTab.XtraTabControl();
            this.tabPending = new DevExpress.XtraTab.XtraTabPage();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.bandedGridView1 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridView();
            this.advBandedGridView1 = new DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView();
            this.gridBand1 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnPendingGenerate = new DevExpress.XtraEditors.SimpleButton();
            this.datetopending = new System.Windows.Forms.DateTimePicker();
            this.datefrompending = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tabForDelivery = new DevExpress.XtraTab.XtraTabPage();
            this.gridControl2 = new DevExpress.XtraGrid.GridControl();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.bandedGridView2 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridView();
            this.advBandedGridView2 = new DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView();
            this.gridBand2 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnGenerateDelivery = new DevExpress.XtraEditors.SimpleButton();
            this.datetofordev = new System.Windows.Forms.DateTimePicker();
            this.datefromfordev = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tabRejected = new DevExpress.XtraTab.XtraTabPage();
            this.gridControl3 = new DevExpress.XtraGrid.GridControl();
            this.gridView3 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.bandedGridView3 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridView();
            this.advBandedGridView3 = new DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView();
            this.gridBand3 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnGenerateRejected = new DevExpress.XtraEditors.SimpleButton();
            this.datetorej = new System.Windows.Forms.DateTimePicker();
            this.datefromrej = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.contextMenuStrip1.SuspendLayout();
            this.contextMenuPending.SuspendLayout();
            this.contextMenuStripForDelivery.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tabMain)).BeginInit();
            this.tabMain.SuspendLayout();
            this.tabPending.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bandedGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.advBandedGridView1)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.tabForDelivery.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bandedGridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.advBandedGridView2)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.tabRejected.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bandedGridView3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.advBandedGridView3)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.confirmOrderToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(320, 46);
            // 
            // confirmOrderToolStripMenuItem
            // 
            this.confirmOrderToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("confirmOrderToolStripMenuItem.Image")));
            this.confirmOrderToolStripMenuItem.Name = "confirmOrderToolStripMenuItem";
            this.confirmOrderToolStripMenuItem.Size = new System.Drawing.Size(319, 42);
            this.confirmOrderToolStripMenuItem.Text = "View Order Details";
            this.confirmOrderToolStripMenuItem.Click += new System.EventHandler(this.confirmOrderToolStripMenuItem_Click);
            // 
            // contextMenuPending
            // 
            this.contextMenuPending.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuPending.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.processThisOrderToolStripMenuItem,
            this.cancelThisOrderToolStripMenuItem});
            this.contextMenuPending.Name = "contextMenuStrip2";
            this.contextMenuPending.Size = new System.Drawing.Size(311, 88);
            // 
            // processThisOrderToolStripMenuItem
            // 
            this.processThisOrderToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("processThisOrderToolStripMenuItem.Image")));
            this.processThisOrderToolStripMenuItem.Name = "processThisOrderToolStripMenuItem";
            this.processThisOrderToolStripMenuItem.Size = new System.Drawing.Size(310, 42);
            this.processThisOrderToolStripMenuItem.Text = "Process this Order";
            this.processThisOrderToolStripMenuItem.Click += new System.EventHandler(this.processThisOrderToolStripMenuItem_Click);
            // 
            // cancelThisOrderToolStripMenuItem
            // 
            this.cancelThisOrderToolStripMenuItem.Name = "cancelThisOrderToolStripMenuItem";
            this.cancelThisOrderToolStripMenuItem.Size = new System.Drawing.Size(310, 42);
            this.cancelThisOrderToolStripMenuItem.Text = "Cancel this Order";
            this.cancelThisOrderToolStripMenuItem.Click += new System.EventHandler(this.cancelThisOrderToolStripMenuItem_Click);
            // 
            // contextMenuStripForDelivery
            // 
            this.contextMenuStripForDelivery.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStripForDelivery.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.printDeliveryReceiptToolStripMenuItem});
            this.contextMenuStripForDelivery.Name = "contextMenuStrip2";
            this.contextMenuStripForDelivery.Size = new System.Drawing.Size(351, 46);
            // 
            // printDeliveryReceiptToolStripMenuItem
            // 
            this.printDeliveryReceiptToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("printDeliveryReceiptToolStripMenuItem.Image")));
            this.printDeliveryReceiptToolStripMenuItem.Name = "printDeliveryReceiptToolStripMenuItem";
            this.printDeliveryReceiptToolStripMenuItem.Size = new System.Drawing.Size(350, 42);
            this.printDeliveryReceiptToolStripMenuItem.Text = "Print Delivery Receipt";
            this.printDeliveryReceiptToolStripMenuItem.Click += new System.EventHandler(this.printDeliveryReceiptToolStripMenuItem_Click);
            // 
            // tabMain
            // 
            this.tabMain.AppearancePage.Header.Font = new System.Drawing.Font("Tahoma", 9.25F);
            this.tabMain.AppearancePage.Header.Options.UseFont = true;
            this.tabMain.AppearancePage.HeaderActive.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabMain.AppearancePage.HeaderActive.Options.UseFont = true;
            this.tabMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabMain.Location = new System.Drawing.Point(0, 0);
            this.tabMain.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.tabMain.Name = "tabMain";
            this.tabMain.SelectedTabPage = this.tabPending;
            this.tabMain.Size = new System.Drawing.Size(2717, 1671);
            this.tabMain.TabIndex = 7;
            this.tabMain.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tabPending,
            this.tabForDelivery,
            this.tabRejected});
            this.tabMain.SelectedPageChanged += new DevExpress.XtraTab.TabPageChangedEventHandler(this.tabMain_SelectedPageChanged);
            // 
            // tabPending
            // 
            this.tabPending.Controls.Add(this.gridControl1);
            this.tabPending.Controls.Add(this.groupBox3);
            this.tabPending.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("tabPending.ImageOptions.Image")));
            this.tabPending.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.tabPending.Name = "tabPending";
            this.tabPending.Size = new System.Drawing.Size(2713, 1611);
            this.tabPending.Text = "Pending";
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.gridControl1.Location = new System.Drawing.Point(0, 112);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(2713, 1499);
            this.gridControl1.TabIndex = 5;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1,
            this.bandedGridView1,
            this.advBandedGridView1});
            this.gridControl1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.gridControl1_MouseUp);
            // 
            // gridView1
            // 
            this.gridView1.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridView1.Appearance.HeaderPanel.Options.UseFont = true;
            this.gridView1.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridView1.Appearance.Row.Options.UseFont = true;
            this.gridView1.DetailHeight = 781;
            this.gridView1.FixedLineWidth = 4;
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsBehavior.ReadOnly = true;
            this.gridView1.OptionsView.ColumnAutoWidth = false;
            this.gridView1.OptionsView.RowAutoHeight = true;
            this.gridView1.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.gridView1_RowStyle);
            this.gridView1.DoubleClick += new System.EventHandler(this.gridView1_DoubleClick);
            // 
            // bandedGridView1
            // 
            this.bandedGridView1.DetailHeight = 781;
            this.bandedGridView1.FixedLineWidth = 4;
            this.bandedGridView1.GridControl = this.gridControl1;
            this.bandedGridView1.Name = "bandedGridView1";
            // 
            // advBandedGridView1
            // 
            this.advBandedGridView1.Bands.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] {
            this.gridBand1});
            this.advBandedGridView1.DetailHeight = 781;
            this.advBandedGridView1.FixedLineWidth = 4;
            this.advBandedGridView1.GridControl = this.gridControl1;
            this.advBandedGridView1.Name = "advBandedGridView1";
            // 
            // gridBand1
            // 
            this.gridBand1.Caption = "gridBand1";
            this.gridBand1.MinWidth = 22;
            this.gridBand1.Name = "gridBand1";
            this.gridBand1.VisibleIndex = 0;
            this.gridBand1.Width = 152;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnPendingGenerate);
            this.groupBox3.Controls.Add(this.datetopending);
            this.groupBox3.Controls.Add(this.datefrompending);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox3.Font = new System.Drawing.Font("Tahoma", 7.75F);
            this.groupBox3.Location = new System.Drawing.Point(0, 0);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.groupBox3.Size = new System.Drawing.Size(2713, 112);
            this.groupBox3.TabIndex = 6;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Filter Date";
            // 
            // btnPendingGenerate
            // 
            this.btnPendingGenerate.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnPendingGenerate.ImageOptions.Image")));
            this.btnPendingGenerate.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btnPendingGenerate.Location = new System.Drawing.Point(737, 38);
            this.btnPendingGenerate.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.btnPendingGenerate.Name = "btnPendingGenerate";
            this.btnPendingGenerate.Size = new System.Drawing.Size(186, 51);
            this.btnPendingGenerate.TabIndex = 6;
            this.btnPendingGenerate.Text = "Generate";
            this.btnPendingGenerate.Click += new System.EventHandler(this.btnPendingGenerate_Click);
            // 
            // datetopending
            // 
            this.datetopending.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.datetopending.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.datetopending.Location = new System.Drawing.Point(468, 38);
            this.datetopending.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.datetopending.Name = "datetopending";
            this.datetopending.Size = new System.Drawing.Size(251, 43);
            this.datetopending.TabIndex = 3;
            // 
            // datefrompending
            // 
            this.datefrompending.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.datefrompending.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.datefrompending.Location = new System.Drawing.Point(126, 38);
            this.datefrompending.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.datefrompending.Name = "datefrompending";
            this.datefrompending.Size = new System.Drawing.Size(251, 43);
            this.datefrompending.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(394, 49);
            this.label2.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 36);
            this.label2.TabIndex = 1;
            this.label2.Text = "To:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(17, 49);
            this.label1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(104, 36);
            this.label1.TabIndex = 0;
            this.label1.Text = "From:";
            // 
            // tabForDelivery
            // 
            this.tabForDelivery.Controls.Add(this.gridControl2);
            this.tabForDelivery.Controls.Add(this.groupBox1);
            this.tabForDelivery.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("tabForDelivery.ImageOptions.Image")));
            this.tabForDelivery.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.tabForDelivery.Name = "tabForDelivery";
            this.tabForDelivery.Size = new System.Drawing.Size(2713, 1611);
            this.tabForDelivery.Text = "For Delivery";
            // 
            // gridControl2
            // 
            this.gridControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl2.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.gridControl2.Location = new System.Drawing.Point(0, 112);
            this.gridControl2.MainView = this.gridView2;
            this.gridControl2.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.gridControl2.Name = "gridControl2";
            this.gridControl2.Size = new System.Drawing.Size(2713, 1499);
            this.gridControl2.TabIndex = 6;
            this.gridControl2.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView2,
            this.bandedGridView2,
            this.advBandedGridView2});
            this.gridControl2.MouseUp += new System.Windows.Forms.MouseEventHandler(this.gridControl2_MouseUp);
            // 
            // gridView2
            // 
            this.gridView2.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridView2.Appearance.HeaderPanel.Options.UseFont = true;
            this.gridView2.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridView2.Appearance.Row.Options.UseFont = true;
            this.gridView2.DetailHeight = 781;
            this.gridView2.FixedLineWidth = 4;
            this.gridView2.GridControl = this.gridControl2;
            this.gridView2.Name = "gridView2";
            this.gridView2.OptionsBehavior.Editable = false;
            this.gridView2.OptionsBehavior.ReadOnly = true;
            this.gridView2.OptionsView.ColumnAutoWidth = false;
            this.gridView2.OptionsView.RowAutoHeight = true;
            this.gridView2.DoubleClick += new System.EventHandler(this.gridView2_DoubleClick);
            // 
            // bandedGridView2
            // 
            this.bandedGridView2.DetailHeight = 781;
            this.bandedGridView2.FixedLineWidth = 4;
            this.bandedGridView2.GridControl = this.gridControl2;
            this.bandedGridView2.Name = "bandedGridView2";
            // 
            // advBandedGridView2
            // 
            this.advBandedGridView2.Bands.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] {
            this.gridBand2});
            this.advBandedGridView2.DetailHeight = 781;
            this.advBandedGridView2.FixedLineWidth = 4;
            this.advBandedGridView2.GridControl = this.gridControl2;
            this.advBandedGridView2.Name = "advBandedGridView2";
            // 
            // gridBand2
            // 
            this.gridBand2.Caption = "gridBand1";
            this.gridBand2.MinWidth = 22;
            this.gridBand2.Name = "gridBand2";
            this.gridBand2.VisibleIndex = 0;
            this.gridBand2.Width = 152;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnGenerateDelivery);
            this.groupBox1.Controls.Add(this.datetofordev);
            this.groupBox1.Controls.Add(this.datefromfordev);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Font = new System.Drawing.Font("Tahoma", 7.75F);
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.groupBox1.Size = new System.Drawing.Size(2713, 112);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Filter Date";
            // 
            // btnGenerateDelivery
            // 
            this.btnGenerateDelivery.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnGenerateDelivery.ImageOptions.Image")));
            this.btnGenerateDelivery.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btnGenerateDelivery.Location = new System.Drawing.Point(737, 38);
            this.btnGenerateDelivery.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.btnGenerateDelivery.Name = "btnGenerateDelivery";
            this.btnGenerateDelivery.Size = new System.Drawing.Size(186, 51);
            this.btnGenerateDelivery.TabIndex = 7;
            this.btnGenerateDelivery.Text = "Generate";
            this.btnGenerateDelivery.Click += new System.EventHandler(this.btnGenerateDelivery_Click);
            // 
            // datetofordev
            // 
            this.datetofordev.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.datetofordev.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.datetofordev.Location = new System.Drawing.Point(468, 38);
            this.datetofordev.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.datetofordev.Name = "datetofordev";
            this.datetofordev.Size = new System.Drawing.Size(251, 43);
            this.datetofordev.TabIndex = 3;
            // 
            // datefromfordev
            // 
            this.datefromfordev.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.datefromfordev.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.datefromfordev.Location = new System.Drawing.Point(126, 38);
            this.datefromfordev.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.datefromfordev.Name = "datefromfordev";
            this.datefromfordev.Size = new System.Drawing.Size(251, 43);
            this.datefromfordev.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.label3.Location = new System.Drawing.Point(394, 49);
            this.label3.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 36);
            this.label3.TabIndex = 1;
            this.label3.Text = "To:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.label4.Location = new System.Drawing.Point(17, 49);
            this.label4.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(104, 36);
            this.label4.TabIndex = 0;
            this.label4.Text = "From:";
            // 
            // tabRejected
            // 
            this.tabRejected.Controls.Add(this.gridControl3);
            this.tabRejected.Controls.Add(this.groupBox2);
            this.tabRejected.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("tabRejected.ImageOptions.Image")));
            this.tabRejected.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.tabRejected.Name = "tabRejected";
            this.tabRejected.Size = new System.Drawing.Size(2713, 1611);
            this.tabRejected.Text = "Rejected Request";
            // 
            // gridControl3
            // 
            this.gridControl3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl3.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.gridControl3.Location = new System.Drawing.Point(0, 112);
            this.gridControl3.MainView = this.gridView3;
            this.gridControl3.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.gridControl3.Name = "gridControl3";
            this.gridControl3.Size = new System.Drawing.Size(2713, 1499);
            this.gridControl3.TabIndex = 7;
            this.gridControl3.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView3,
            this.bandedGridView3,
            this.advBandedGridView3});
            // 
            // gridView3
            // 
            this.gridView3.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridView3.Appearance.HeaderPanel.Options.UseFont = true;
            this.gridView3.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridView3.Appearance.Row.Options.UseFont = true;
            this.gridView3.DetailHeight = 781;
            this.gridView3.FixedLineWidth = 4;
            this.gridView3.GridControl = this.gridControl3;
            this.gridView3.Name = "gridView3";
            this.gridView3.OptionsBehavior.Editable = false;
            this.gridView3.OptionsBehavior.ReadOnly = true;
            this.gridView3.OptionsView.ColumnAutoWidth = false;
            this.gridView3.OptionsView.RowAutoHeight = true;
            this.gridView3.DoubleClick += new System.EventHandler(this.gridView3_DoubleClick);
            // 
            // bandedGridView3
            // 
            this.bandedGridView3.DetailHeight = 781;
            this.bandedGridView3.FixedLineWidth = 4;
            this.bandedGridView3.GridControl = this.gridControl3;
            this.bandedGridView3.Name = "bandedGridView3";
            // 
            // advBandedGridView3
            // 
            this.advBandedGridView3.Bands.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] {
            this.gridBand3});
            this.advBandedGridView3.DetailHeight = 781;
            this.advBandedGridView3.FixedLineWidth = 4;
            this.advBandedGridView3.GridControl = this.gridControl3;
            this.advBandedGridView3.Name = "advBandedGridView3";
            // 
            // gridBand3
            // 
            this.gridBand3.Caption = "gridBand1";
            this.gridBand3.MinWidth = 22;
            this.gridBand3.Name = "gridBand3";
            this.gridBand3.VisibleIndex = 0;
            this.gridBand3.Width = 152;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnGenerateRejected);
            this.groupBox2.Controls.Add(this.datetorej);
            this.groupBox2.Controls.Add(this.datefromrej);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Font = new System.Drawing.Font("Tahoma", 7.75F);
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.groupBox2.Size = new System.Drawing.Size(2713, 112);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Filter Date";
            // 
            // btnGenerateRejected
            // 
            this.btnGenerateRejected.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btnGenerateRejected.Location = new System.Drawing.Point(737, 38);
            this.btnGenerateRejected.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.btnGenerateRejected.Name = "btnGenerateRejected";
            this.btnGenerateRejected.Size = new System.Drawing.Size(186, 51);
            this.btnGenerateRejected.TabIndex = 8;
            this.btnGenerateRejected.Text = "Generate";
            this.btnGenerateRejected.Click += new System.EventHandler(this.btnGenerateRejected_Click);
            // 
            // datetorej
            // 
            this.datetorej.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.datetorej.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.datetorej.Location = new System.Drawing.Point(468, 38);
            this.datetorej.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.datetorej.Name = "datetorej";
            this.datetorej.Size = new System.Drawing.Size(251, 43);
            this.datetorej.TabIndex = 3;
            // 
            // datefromrej
            // 
            this.datefromrej.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.datefromrej.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.datefromrej.Location = new System.Drawing.Point(126, 38);
            this.datefromrej.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.datefromrej.Name = "datefromrej";
            this.datefromrej.Size = new System.Drawing.Size(251, 43);
            this.datefromrej.TabIndex = 2;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.label5.Location = new System.Drawing.Point(394, 49);
            this.label5.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(63, 36);
            this.label5.TabIndex = 1;
            this.label5.Text = "To:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.label6.Location = new System.Drawing.Point(17, 49);
            this.label6.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(104, 36);
            this.label6.TabIndex = 0;
            this.label6.Text = "From:";
            // 
            // ViewBranchOrder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 29F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(2717, 1671);
            this.Controls.Add(this.tabMain);
            this.IconOptions.Icon = ((System.Drawing.Icon)(resources.GetObject("ViewBranchOrder.IconOptions.Icon")));
            this.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.Name = "ViewBranchOrder";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ViewBranchOrder";
            this.Load += new System.EventHandler(this.ViewBranchOrder_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            this.contextMenuPending.ResumeLayout(false);
            this.contextMenuStripForDelivery.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tabMain)).EndInit();
            this.tabMain.ResumeLayout(false);
            this.tabPending.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bandedGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.advBandedGridView1)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.tabForDelivery.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bandedGridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.advBandedGridView2)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabRejected.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bandedGridView3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.advBandedGridView3)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem confirmOrderToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuPending;
        private System.Windows.Forms.ToolStripMenuItem processThisOrderToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripForDelivery;
        private System.Windows.Forms.ToolStripMenuItem cancelThisOrderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem printDeliveryReceiptToolStripMenuItem;
        private DevExpress.XtraTab.XtraTabControl tabMain;
        private DevExpress.XtraTab.XtraTabPage tabPending;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridView bandedGridView1;
        private DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView advBandedGridView1;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.DateTimePicker datetopending;
        private System.Windows.Forms.DateTimePicker datefrompending;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraTab.XtraTabPage tabForDelivery;
        private DevExpress.XtraTab.XtraTabPage tabRejected;
        private DevExpress.XtraGrid.GridControl gridControl2;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridView bandedGridView2;
        private DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView advBandedGridView2;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DateTimePicker datetofordev;
        private System.Windows.Forms.DateTimePicker datefromfordev;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private DevExpress.XtraGrid.GridControl gridControl3;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView3;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridView bandedGridView3;
        private DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView advBandedGridView3;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DateTimePicker datetorej;
        private System.Windows.Forms.DateTimePicker datefromrej;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private DevExpress.XtraEditors.SimpleButton btnPendingGenerate;
        private DevExpress.XtraEditors.SimpleButton btnGenerateDelivery;
        private DevExpress.XtraEditors.SimpleButton btnGenerateRejected;
    }
}