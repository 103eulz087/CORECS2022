namespace SalesInventorySystem.Branches
{
    partial class BranchInventoryIN
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BranchInventoryIN));
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.btnforapprovalstsexcel = new DevExpress.XtraEditors.SimpleButton();
            this.txtid = new DevExpress.XtraEditors.TextEdit();
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.gridControlRcvd = new DevExpress.XtraGrid.GridControl();
            this.gridViewRcvd = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtid.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlRcvd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewRcvd)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.simpleButton1);
            this.panelControl1.Controls.Add(this.btnforapprovalstsexcel);
            this.panelControl1.Controls.Add(this.txtid);
            this.panelControl1.Controls.Add(this.simpleButton2);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1080, 65);
            this.panelControl1.TabIndex = 0;
            // 
            // simpleButton1
            // 
            this.simpleButton1.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("simpleButton1.ImageOptions.Image")));
            this.simpleButton1.Location = new System.Drawing.Point(5, 13);
            this.simpleButton1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(110, 44);
            this.simpleButton1.TabIndex = 95;
            this.simpleButton1.Text = "Preview";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // btnforapprovalstsexcel
            // 
            this.btnforapprovalstsexcel.ImageOptions.Image = global::SalesInventorySystem.Properties.Resources.ExportToExcel_16x16;
            this.btnforapprovalstsexcel.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btnforapprovalstsexcel.Location = new System.Drawing.Point(121, 13);
            this.btnforapprovalstsexcel.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.btnforapprovalstsexcel.Name = "btnforapprovalstsexcel";
            this.btnforapprovalstsexcel.Size = new System.Drawing.Size(138, 44);
            this.btnforapprovalstsexcel.TabIndex = 94;
            this.btnforapprovalstsexcel.Text = "Export to Excel";
            this.btnforapprovalstsexcel.Click += new System.EventHandler(this.btnforapprovalstsexcel_Click);
            // 
            // txtid
            // 
            this.txtid.Location = new System.Drawing.Point(414, 27);
            this.txtid.Margin = new System.Windows.Forms.Padding(4);
            this.txtid.Name = "txtid";
            this.txtid.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtid.Properties.Appearance.Options.UseFont = true;
            this.txtid.Properties.MaxLength = 13;
            this.txtid.Properties.ReadOnly = true;
            this.txtid.Size = new System.Drawing.Size(99, 30);
            this.txtid.TabIndex = 93;
            // 
            // simpleButton2
            // 
            this.simpleButton2.ImageOptions.Image = global::SalesInventorySystem.Properties.Resources.Save_16x16__5_;
            this.simpleButton2.Location = new System.Drawing.Point(265, 14);
            this.simpleButton2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new System.Drawing.Size(142, 43);
            this.simpleButton2.TabIndex = 92;
            this.simpleButton2.Text = "Save";
            this.simpleButton2.Click += new System.EventHandler(this.simpleButton2_Click);
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.gridControlRcvd);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl2.Location = new System.Drawing.Point(0, 65);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(1080, 606);
            this.panelControl2.TabIndex = 1;
            // 
            // gridControlRcvd
            // 
            this.gridControlRcvd.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControlRcvd.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gridControlRcvd.Location = new System.Drawing.Point(2, 2);
            this.gridControlRcvd.MainView = this.gridViewRcvd;
            this.gridControlRcvd.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gridControlRcvd.Name = "gridControlRcvd";
            this.gridControlRcvd.Size = new System.Drawing.Size(1076, 602);
            this.gridControlRcvd.TabIndex = 5;
            this.gridControlRcvd.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewRcvd});
            // 
            // gridViewRcvd
            // 
            this.gridViewRcvd.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridViewRcvd.Appearance.HeaderPanel.Options.UseFont = true;
            this.gridViewRcvd.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridViewRcvd.Appearance.Row.Options.UseFont = true;
            this.gridViewRcvd.DetailHeight = 431;
            this.gridViewRcvd.GridControl = this.gridControlRcvd;
            this.gridViewRcvd.Name = "gridViewRcvd";
            this.gridViewRcvd.OptionsFind.Behavior = DevExpress.XtraEditors.FindPanelBehavior.Search;
            this.gridViewRcvd.OptionsView.ColumnAutoWidth = false;
            this.gridViewRcvd.OptionsView.RowAutoHeight = true;
            this.gridViewRcvd.OptionsView.ShowFooter = true;
            this.gridViewRcvd.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.gridViewRcvd_RowCellStyle);
            this.gridViewRcvd.ShowingEditor += new System.ComponentModel.CancelEventHandler(this.gridViewRcvd_ShowingEditor);
            this.gridViewRcvd.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.gridViewRcvd_CellValueChanged);
            // 
            // BranchInventoryIN
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1080, 671);
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.panelControl1);
            this.Name = "BranchInventoryIN";
            this.Text = "BranchInventoryIN";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.BranchInventoryIN_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtid.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlRcvd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewRcvd)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        public DevExpress.XtraGrid.GridControl gridControlRcvd;
        public DevExpress.XtraGrid.Views.Grid.GridView gridViewRcvd;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
        private DevExpress.XtraEditors.TextEdit txtid;
        private DevExpress.XtraEditors.SimpleButton btnforapprovalstsexcel;
        public DevExpress.XtraEditors.SimpleButton simpleButton1;
    }
}