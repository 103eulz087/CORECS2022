namespace SalesInventorySystem.HOFormsDevEx
{
    partial class ReceivedTransferBranchInventory
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReceivedTransferBranchInventory));
            this.tabMain = new DevExpress.XtraTab.XtraTabControl();
            this.tabForReceiving = new DevExpress.XtraTab.XtraTabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.gridControlForReceiving = new DevExpress.XtraGrid.GridControl();
            this.gridViewForReceiving = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnforrcvng = new DevExpress.XtraEditors.SimpleButton();
            this.txtdatetoforrcvng = new System.Windows.Forms.DateTimePicker();
            this.txtdatefromforrcvng = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.tabReceived = new DevExpress.XtraTab.XtraTabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.gridControlMyReq = new DevExpress.XtraGrid.GridControl();
            this.gridViewMyReq = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.panelControl7 = new DevExpress.XtraEditors.PanelControl();
            this.btnMyReqExcel = new DevExpress.XtraEditors.SimpleButton();
            this.btnMyReq = new DevExpress.XtraEditors.SimpleButton();
            this.label8 = new System.Windows.Forms.Label();
            this.datetorcvd = new System.Windows.Forms.DateTimePicker();
            this.datefromrcvd = new System.Windows.Forms.DateTimePicker();
            this.label7 = new System.Windows.Forms.Label();
            this.contextMenuStripForReceiving = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.showForReceivingItemsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStripReceived = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.tabMain)).BeginInit();
            this.tabMain.SuspendLayout();
            this.tabForReceiving.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlForReceiving)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewForReceiving)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.tabReceived.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlMyReq)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewMyReq)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl7)).BeginInit();
            this.panelControl7.SuspendLayout();
            this.contextMenuStripForReceiving.SuspendLayout();
            this.contextMenuStripReceived.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabMain
            // 
            this.tabMain.AppearancePage.Header.Font = new System.Drawing.Font("Tahoma", 9.25F);
            this.tabMain.AppearancePage.Header.Options.UseFont = true;
            this.tabMain.AppearancePage.HeaderActive.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabMain.AppearancePage.HeaderActive.Options.UseFont = true;
            this.tabMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabMain.Location = new System.Drawing.Point(0, 0);
            this.tabMain.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabMain.Name = "tabMain";
            this.tabMain.SelectedTabPage = this.tabForReceiving;
            this.tabMain.Size = new System.Drawing.Size(1434, 731);
            this.tabMain.TabIndex = 8;
            this.tabMain.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tabForReceiving,
            this.tabReceived});
            // 
            // tabForReceiving
            // 
            this.tabForReceiving.Controls.Add(this.groupBox1);
            this.tabForReceiving.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("tabForReceiving.ImageOptions.Image")));
            this.tabForReceiving.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabForReceiving.Name = "tabForReceiving";
            this.tabForReceiving.Size = new System.Drawing.Size(1432, 699);
            this.tabForReceiving.Text = "For Receiving";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.gridControlForReceiving);
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Size = new System.Drawing.Size(1432, 699);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            // 
            // gridControlForReceiving
            // 
            this.gridControlForReceiving.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControlForReceiving.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gridControlForReceiving.Location = new System.Drawing.Point(3, 81);
            this.gridControlForReceiving.MainView = this.gridViewForReceiving;
            this.gridControlForReceiving.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gridControlForReceiving.Name = "gridControlForReceiving";
            this.gridControlForReceiving.Size = new System.Drawing.Size(1426, 614);
            this.gridControlForReceiving.TabIndex = 3;
            this.gridControlForReceiving.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewForReceiving});
            this.gridControlForReceiving.MouseUp += new System.Windows.Forms.MouseEventHandler(this.gridControlForReceiving_MouseUp);
            // 
            // gridViewForReceiving
            // 
            this.gridViewForReceiving.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridViewForReceiving.Appearance.HeaderPanel.Options.UseFont = true;
            this.gridViewForReceiving.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridViewForReceiving.Appearance.Row.Options.UseFont = true;
            this.gridViewForReceiving.DetailHeight = 431;
            this.gridViewForReceiving.GridControl = this.gridControlForReceiving;
            this.gridViewForReceiving.Name = "gridViewForReceiving";
            this.gridViewForReceiving.OptionsBehavior.Editable = false;
            this.gridViewForReceiving.OptionsBehavior.ReadOnly = true;
            this.gridViewForReceiving.OptionsView.ColumnAutoWidth = false;
            this.gridViewForReceiving.OptionsView.RowAutoHeight = true;
            this.gridViewForReceiving.OptionsView.ShowFooter = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnforrcvng);
            this.groupBox3.Controls.Add(this.txtdatetoforrcvng);
            this.groupBox3.Controls.Add(this.txtdatefromforrcvng);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox3.Font = new System.Drawing.Font("Tahoma", 7.75F);
            this.groupBox3.Location = new System.Drawing.Point(3, 20);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox3.Size = new System.Drawing.Size(1426, 61);
            this.groupBox3.TabIndex = 8;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Filter Date";
            // 
            // btnforrcvng
            // 
            this.btnforrcvng.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnforrcvng.ImageOptions.Image")));
            this.btnforrcvng.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btnforrcvng.Location = new System.Drawing.Point(395, 21);
            this.btnforrcvng.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnforrcvng.Name = "btnforrcvng";
            this.btnforrcvng.Size = new System.Drawing.Size(100, 28);
            this.btnforrcvng.TabIndex = 7;
            this.btnforrcvng.Text = "Generate";
            this.btnforrcvng.Click += new System.EventHandler(this.btnforrcvng_Click);
            // 
            // txtdatetoforrcvng
            // 
            this.txtdatetoforrcvng.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.txtdatetoforrcvng.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.txtdatetoforrcvng.Location = new System.Drawing.Point(252, 21);
            this.txtdatetoforrcvng.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtdatetoforrcvng.Name = "txtdatetoforrcvng";
            this.txtdatetoforrcvng.Size = new System.Drawing.Size(137, 27);
            this.txtdatetoforrcvng.TabIndex = 3;
            // 
            // txtdatefromforrcvng
            // 
            this.txtdatefromforrcvng.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.txtdatefromforrcvng.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.txtdatefromforrcvng.Location = new System.Drawing.Point(68, 21);
            this.txtdatefromforrcvng.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtdatefromforrcvng.Name = "txtdatefromforrcvng";
            this.txtdatefromforrcvng.Size = new System.Drawing.Size(137, 27);
            this.txtdatefromforrcvng.TabIndex = 2;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.label5.Location = new System.Drawing.Point(212, 27);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(37, 21);
            this.label5.TabIndex = 1;
            this.label5.Text = "To:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.label6.Location = new System.Drawing.Point(9, 27);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(60, 21);
            this.label6.TabIndex = 0;
            this.label6.Text = "From:";
            // 
            // tabReceived
            // 
            this.tabReceived.Controls.Add(this.groupBox2);
            this.tabReceived.Controls.Add(this.panelControl7);
            this.tabReceived.Name = "tabReceived";
            this.tabReceived.Size = new System.Drawing.Size(1432, 699);
            this.tabReceived.Text = "Received Items";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.gridControlMyReq);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 52);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox2.Size = new System.Drawing.Size(1432, 647);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            // 
            // gridControlMyReq
            // 
            this.gridControlMyReq.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControlMyReq.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gridControlMyReq.Location = new System.Drawing.Point(3, 20);
            this.gridControlMyReq.MainView = this.gridViewMyReq;
            this.gridControlMyReq.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gridControlMyReq.Name = "gridControlMyReq";
            this.gridControlMyReq.Size = new System.Drawing.Size(1426, 623);
            this.gridControlMyReq.TabIndex = 3;
            this.gridControlMyReq.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewMyReq});
            this.gridControlMyReq.MouseUp += new System.Windows.Forms.MouseEventHandler(this.gridControlMyReq_MouseUp);
            // 
            // gridViewMyReq
            // 
            this.gridViewMyReq.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridViewMyReq.Appearance.HeaderPanel.Options.UseFont = true;
            this.gridViewMyReq.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridViewMyReq.Appearance.Row.Options.UseFont = true;
            this.gridViewMyReq.DetailHeight = 431;
            this.gridViewMyReq.GridControl = this.gridControlMyReq;
            this.gridViewMyReq.Name = "gridViewMyReq";
            this.gridViewMyReq.OptionsBehavior.Editable = false;
            this.gridViewMyReq.OptionsBehavior.ReadOnly = true;
            this.gridViewMyReq.OptionsView.ColumnAutoWidth = false;
            this.gridViewMyReq.OptionsView.RowAutoHeight = true;
            this.gridViewMyReq.OptionsView.ShowFooter = true;
            // 
            // panelControl7
            // 
            this.panelControl7.Controls.Add(this.btnMyReqExcel);
            this.panelControl7.Controls.Add(this.btnMyReq);
            this.panelControl7.Controls.Add(this.label8);
            this.panelControl7.Controls.Add(this.datetorcvd);
            this.panelControl7.Controls.Add(this.datefromrcvd);
            this.panelControl7.Controls.Add(this.label7);
            this.panelControl7.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl7.Location = new System.Drawing.Point(0, 0);
            this.panelControl7.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panelControl7.Name = "panelControl7";
            this.panelControl7.Size = new System.Drawing.Size(1432, 52);
            this.panelControl7.TabIndex = 9;
            // 
            // btnMyReqExcel
            // 
            this.btnMyReqExcel.ImageOptions.Image = global::SalesInventorySystem.Properties.Resources.ExportToExcel_16x16;
            this.btnMyReqExcel.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btnMyReqExcel.Location = new System.Drawing.Point(514, 10);
            this.btnMyReqExcel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnMyReqExcel.Name = "btnMyReqExcel";
            this.btnMyReqExcel.Size = new System.Drawing.Size(138, 28);
            this.btnMyReqExcel.TabIndex = 14;
            this.btnMyReqExcel.Text = "Export to Excel";
            // 
            // btnMyReq
            // 
            this.btnMyReq.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnMyReq.ImageOptions.Image")));
            this.btnMyReq.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btnMyReq.Location = new System.Drawing.Point(407, 10);
            this.btnMyReq.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnMyReq.Name = "btnMyReq";
            this.btnMyReq.Size = new System.Drawing.Size(100, 28);
            this.btnMyReq.TabIndex = 11;
            this.btnMyReq.Text = "Generate";
            this.btnMyReq.Click += new System.EventHandler(this.btnMyReq_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.label8.Location = new System.Drawing.Point(12, 16);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(60, 21);
            this.label8.TabIndex = 4;
            this.label8.Text = "From:";
            // 
            // datetorcvd
            // 
            this.datetorcvd.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.datetorcvd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.datetorcvd.Location = new System.Drawing.Point(262, 10);
            this.datetorcvd.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.datetorcvd.Name = "datetorcvd";
            this.datetorcvd.Size = new System.Drawing.Size(137, 27);
            this.datetorcvd.TabIndex = 7;
            // 
            // datefromrcvd
            // 
            this.datefromrcvd.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.datefromrcvd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.datefromrcvd.Location = new System.Drawing.Point(78, 10);
            this.datefromrcvd.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.datefromrcvd.Name = "datefromrcvd";
            this.datefromrcvd.Size = new System.Drawing.Size(137, 27);
            this.datefromrcvd.TabIndex = 6;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.label7.Location = new System.Drawing.Point(223, 16);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(37, 21);
            this.label7.TabIndex = 5;
            this.label7.Text = "To:";
            // 
            // contextMenuStripForReceiving
            // 
            this.contextMenuStripForReceiving.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.contextMenuStripForReceiving.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showForReceivingItemsToolStripMenuItem});
            this.contextMenuStripForReceiving.Name = "contextMenuStripForReceiving";
            this.contextMenuStripForReceiving.Size = new System.Drawing.Size(246, 28);
            // 
            // showForReceivingItemsToolStripMenuItem
            // 
            this.showForReceivingItemsToolStripMenuItem.Name = "showForReceivingItemsToolStripMenuItem";
            this.showForReceivingItemsToolStripMenuItem.Size = new System.Drawing.Size(245, 24);
            this.showForReceivingItemsToolStripMenuItem.Text = "Show for Receiving Items";
            this.showForReceivingItemsToolStripMenuItem.Click += new System.EventHandler(this.showForReceivingItemsToolStripMenuItem_Click);
            // 
            // contextMenuStripReceived
            // 
            this.contextMenuStripReceived.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.contextMenuStripReceived.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1});
            this.contextMenuStripReceived.Name = "contextMenuStripForReceiving";
            this.contextMenuStripReceived.Size = new System.Drawing.Size(219, 28);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(218, 24);
            this.toolStripMenuItem1.Text = "Show Received Items";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // ReceivedTransferBranchInventory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1434, 731);
            this.Controls.Add(this.tabMain);
            this.Name = "ReceivedTransferBranchInventory";
            this.Text = "ReceivedTransferBranchInventory";
            ((System.ComponentModel.ISupportInitialize)(this.tabMain)).EndInit();
            this.tabMain.ResumeLayout(false);
            this.tabForReceiving.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlForReceiving)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewForReceiving)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.tabReceived.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlMyReq)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewMyReq)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl7)).EndInit();
            this.panelControl7.ResumeLayout(false);
            this.panelControl7.PerformLayout();
            this.contextMenuStripForReceiving.ResumeLayout(false);
            this.contextMenuStripReceived.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraTab.XtraTabControl tabMain;
        private DevExpress.XtraTab.XtraTabPage tabForReceiving;
        private System.Windows.Forms.GroupBox groupBox1;
        private DevExpress.XtraGrid.GridControl gridControlForReceiving;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewForReceiving;
        private System.Windows.Forms.GroupBox groupBox3;
        private DevExpress.XtraEditors.SimpleButton btnforrcvng;
        private System.Windows.Forms.DateTimePicker txtdatetoforrcvng;
        private System.Windows.Forms.DateTimePicker txtdatefromforrcvng;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripForReceiving;
        private System.Windows.Forms.ToolStripMenuItem showForReceivingItemsToolStripMenuItem;
        private DevExpress.XtraTab.XtraTabPage tabReceived;
        private System.Windows.Forms.GroupBox groupBox2;
        private DevExpress.XtraGrid.GridControl gridControlMyReq;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewMyReq;
        private DevExpress.XtraEditors.PanelControl panelControl7;
        private DevExpress.XtraEditors.SimpleButton btnMyReqExcel;
        private DevExpress.XtraEditors.SimpleButton btnMyReq;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DateTimePicker datetorcvd;
        private System.Windows.Forms.DateTimePicker datefromrcvd;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripReceived;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
    }
}