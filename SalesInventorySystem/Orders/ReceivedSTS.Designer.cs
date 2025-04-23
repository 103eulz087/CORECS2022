namespace SalesInventorySystem.Orders
{
    partial class ReceivedSTS
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReceivedSTS));
            this.tabMain = new DevExpress.XtraTab.XtraTabControl();
            this.tabForReceiving = new DevExpress.XtraTab.XtraTabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.gridControlForReceiving = new DevExpress.XtraGrid.GridControl();
            this.gridViewForReceiving = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.tabMyRequest = new DevExpress.XtraTab.XtraTabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.gridControlMyReq = new DevExpress.XtraGrid.GridControl();
            this.gridViewMyReq = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.panelControl7 = new DevExpress.XtraEditors.PanelControl();
            this.btnMyReqExcel = new DevExpress.XtraEditors.SimpleButton();
            this.btnMyReq = new DevExpress.XtraEditors.SimpleButton();
            this.label8 = new System.Windows.Forms.Label();
            this.dateto = new System.Windows.Forms.DateTimePicker();
            this.datefrom = new System.Windows.Forms.DateTimePicker();
            this.label7 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.tabMain)).BeginInit();
            this.tabMain.SuspendLayout();
            this.tabForReceiving.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlForReceiving)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewForReceiving)).BeginInit();
            this.tabMyRequest.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlMyReq)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewMyReq)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl7)).BeginInit();
            this.panelControl7.SuspendLayout();
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
            this.tabMain.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.tabMain.Name = "tabMain";
            this.tabMain.SelectedTabPage = this.tabForReceiving;
            this.tabMain.Size = new System.Drawing.Size(2125, 1219);
            this.tabMain.TabIndex = 7;
            this.tabMain.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tabForReceiving,
            this.tabMyRequest});
            this.tabMain.SelectedPageChanged += new DevExpress.XtraTab.TabPageChangedEventHandler(this.tabMain_SelectedPageChanged);
            // 
            // tabForReceiving
            // 
            this.tabForReceiving.Controls.Add(this.groupBox1);
            this.tabForReceiving.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("tabForReceiving.ImageOptions.Image")));
            this.tabForReceiving.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.tabForReceiving.Name = "tabForReceiving";
            this.tabForReceiving.Size = new System.Drawing.Size(2121, 1171);
            this.tabForReceiving.Text = "For Receiving";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.gridControlForReceiving);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.groupBox1.Size = new System.Drawing.Size(2121, 1171);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            // 
            // gridControlForReceiving
            // 
            this.gridControlForReceiving.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControlForReceiving.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.gridControlForReceiving.Location = new System.Drawing.Point(5, 29);
            this.gridControlForReceiving.MainView = this.gridViewForReceiving;
            this.gridControlForReceiving.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.gridControlForReceiving.Name = "gridControlForReceiving";
            this.gridControlForReceiving.Size = new System.Drawing.Size(2111, 1136);
            this.gridControlForReceiving.TabIndex = 3;
            this.gridControlForReceiving.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewForReceiving});
            // 
            // gridViewForReceiving
            // 
            this.gridViewForReceiving.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridViewForReceiving.Appearance.HeaderPanel.Options.UseFont = true;
            this.gridViewForReceiving.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridViewForReceiving.Appearance.Row.Options.UseFont = true;
            this.gridViewForReceiving.DetailHeight = 619;
            this.gridViewForReceiving.FixedLineWidth = 3;
            this.gridViewForReceiving.GridControl = this.gridControlForReceiving;
            this.gridViewForReceiving.Name = "gridViewForReceiving";
            this.gridViewForReceiving.OptionsBehavior.Editable = false;
            this.gridViewForReceiving.OptionsBehavior.ReadOnly = true;
            this.gridViewForReceiving.OptionsView.ColumnAutoWidth = false;
            this.gridViewForReceiving.OptionsView.RowAutoHeight = true;
            this.gridViewForReceiving.OptionsView.ShowFooter = true;
            this.gridViewForReceiving.DoubleClick += new System.EventHandler(this.gridViewForReceiving_DoubleClick);
            // 
            // tabMyRequest
            // 
            this.tabMyRequest.Controls.Add(this.groupBox2);
            this.tabMyRequest.Controls.Add(this.panelControl7);
            this.tabMyRequest.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("tabMyRequest.ImageOptions.Image")));
            this.tabMyRequest.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.tabMyRequest.Name = "tabMyRequest";
            this.tabMyRequest.Size = new System.Drawing.Size(2121, 1171);
            this.tabMyRequest.Text = "My Request";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.gridControlMyReq);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 75);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.groupBox2.Size = new System.Drawing.Size(2121, 1096);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            // 
            // gridControlMyReq
            // 
            this.gridControlMyReq.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControlMyReq.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.gridControlMyReq.Location = new System.Drawing.Point(5, 29);
            this.gridControlMyReq.MainView = this.gridViewMyReq;
            this.gridControlMyReq.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.gridControlMyReq.Name = "gridControlMyReq";
            this.gridControlMyReq.Size = new System.Drawing.Size(2111, 1061);
            this.gridControlMyReq.TabIndex = 3;
            this.gridControlMyReq.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewMyReq});
            // 
            // gridViewMyReq
            // 
            this.gridViewMyReq.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridViewMyReq.Appearance.HeaderPanel.Options.UseFont = true;
            this.gridViewMyReq.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridViewMyReq.Appearance.Row.Options.UseFont = true;
            this.gridViewMyReq.DetailHeight = 619;
            this.gridViewMyReq.FixedLineWidth = 3;
            this.gridViewMyReq.GridControl = this.gridControlMyReq;
            this.gridViewMyReq.Name = "gridViewMyReq";
            this.gridViewMyReq.OptionsBehavior.Editable = false;
            this.gridViewMyReq.OptionsBehavior.ReadOnly = true;
            this.gridViewMyReq.OptionsView.ColumnAutoWidth = false;
            this.gridViewMyReq.OptionsView.RowAutoHeight = true;
            this.gridViewMyReq.OptionsView.ShowFooter = true;
            this.gridViewMyReq.DoubleClick += new System.EventHandler(this.gridViewMyReq_DoubleClick);
            // 
            // panelControl7
            // 
            this.panelControl7.Controls.Add(this.btnMyReqExcel);
            this.panelControl7.Controls.Add(this.btnMyReq);
            this.panelControl7.Controls.Add(this.label8);
            this.panelControl7.Controls.Add(this.dateto);
            this.panelControl7.Controls.Add(this.datefrom);
            this.panelControl7.Controls.Add(this.label7);
            this.panelControl7.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl7.Location = new System.Drawing.Point(0, 0);
            this.panelControl7.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.panelControl7.Name = "panelControl7";
            this.panelControl7.Size = new System.Drawing.Size(2121, 75);
            this.panelControl7.TabIndex = 7;
            // 
            // btnMyReqExcel
            // 
            this.btnMyReqExcel.ImageOptions.Image = global::SalesInventorySystem.Properties.Resources.ExportToExcel_16x16;
            this.btnMyReqExcel.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btnMyReqExcel.Location = new System.Drawing.Point(723, 14);
            this.btnMyReqExcel.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.btnMyReqExcel.Name = "btnMyReqExcel";
            this.btnMyReqExcel.Size = new System.Drawing.Size(197, 40);
            this.btnMyReqExcel.TabIndex = 14;
            this.btnMyReqExcel.Text = "Export to Excel";
            this.btnMyReqExcel.Click += new System.EventHandler(this.btnMyReqExcel_Click);
            // 
            // btnMyReq
            // 
            this.btnMyReq.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnMyReq.ImageOptions.Image")));
            this.btnMyReq.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btnMyReq.Location = new System.Drawing.Point(570, 14);
            this.btnMyReq.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.btnMyReq.Name = "btnMyReq";
            this.btnMyReq.Size = new System.Drawing.Size(143, 40);
            this.btnMyReq.TabIndex = 11;
            this.btnMyReq.Text = "Generate";
            this.btnMyReq.Click += new System.EventHandler(this.btnMyReq_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.label8.Location = new System.Drawing.Point(17, 23);
            this.label8.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(79, 28);
            this.label8.TabIndex = 4;
            this.label8.Text = "From:";
            // 
            // dateto
            // 
            this.dateto.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.dateto.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateto.Location = new System.Drawing.Point(363, 14);
            this.dateto.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.dateto.Name = "dateto";
            this.dateto.Size = new System.Drawing.Size(194, 35);
            this.dateto.TabIndex = 7;
            // 
            // datefrom
            // 
            this.datefrom.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.datefrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.datefrom.Location = new System.Drawing.Point(100, 14);
            this.datefrom.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.datefrom.Name = "datefrom";
            this.datefrom.Size = new System.Drawing.Size(194, 35);
            this.datefrom.TabIndex = 6;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.label7.Location = new System.Drawing.Point(307, 23);
            this.label7.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(48, 28);
            this.label7.TabIndex = 5;
            this.label7.Text = "To:";
            // 
            // ReceivedSTS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(2125, 1219);
            this.Controls.Add(this.tabMain);
            this.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.Name = "ReceivedSTS";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ReceivedSTS";
            this.Load += new System.EventHandler(this.ReceivedSTS_Load);
            ((System.ComponentModel.ISupportInitialize)(this.tabMain)).EndInit();
            this.tabMain.ResumeLayout(false);
            this.tabForReceiving.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlForReceiving)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewForReceiving)).EndInit();
            this.tabMyRequest.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlMyReq)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewMyReq)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl7)).EndInit();
            this.panelControl7.ResumeLayout(false);
            this.panelControl7.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraTab.XtraTabControl tabMain;
        private DevExpress.XtraTab.XtraTabPage tabForReceiving;
        private DevExpress.XtraTab.XtraTabPage tabMyRequest;
        private System.Windows.Forms.GroupBox groupBox2;
        private DevExpress.XtraGrid.GridControl gridControlMyReq;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewMyReq;
        private DevExpress.XtraEditors.PanelControl panelControl7;
        private DevExpress.XtraEditors.SimpleButton btnMyReqExcel;
        private DevExpress.XtraEditors.SimpleButton btnMyReq;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DateTimePicker dateto;
        private System.Windows.Forms.DateTimePicker datefrom;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox groupBox1;
        private DevExpress.XtraGrid.GridControl gridControlForReceiving;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewForReceiving;
    }
}