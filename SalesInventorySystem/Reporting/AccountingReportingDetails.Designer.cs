namespace SalesInventorySystem.Reporting
{
    partial class AccountingReportingDetails
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
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.bandedGridView1 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridView();
            this.advBandedGridView1 = new DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView();
            this.gridBand1 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.panelControlsalesvat = new DevExpress.XtraEditors.PanelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.txtvatablesales = new DevExpress.XtraEditors.TextEdit();
            this.txtnetofvatsales = new DevExpress.XtraEditors.TextEdit();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.panelsalesvatex = new DevExpress.XtraEditors.PanelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.txtvatexemptsales = new DevExpress.XtraEditors.TextEdit();
            this.lblBranch = new DevExpress.XtraEditors.LabelControl();
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.lblsalesdate = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bandedGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.advBandedGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControlsalesvat)).BeginInit();
            this.panelControlsalesvat.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtvatablesales.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtnetofvatsales.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelsalesvatex)).BeginInit();
            this.panelsalesvatex.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtvatexemptsales.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gridControl1.Location = new System.Drawing.Point(0, 119);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(1359, 637);
            this.gridControl1.TabIndex = 12;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1,
            this.bandedGridView1,
            this.advBandedGridView1});
            // 
            // gridView1
            // 
            this.gridView1.Appearance.GroupRow.BackColor = System.Drawing.Color.SteelBlue;
            this.gridView1.Appearance.GroupRow.ForeColor = System.Drawing.Color.White;
            this.gridView1.Appearance.GroupRow.Options.UseBackColor = true;
            this.gridView1.Appearance.GroupRow.Options.UseForeColor = true;
            this.gridView1.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridView1.Appearance.HeaderPanel.Options.UseFont = true;
            this.gridView1.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridView1.Appearance.Row.Options.UseFont = true;
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsBehavior.ReadOnly = true;
            this.gridView1.OptionsSelection.MultiSelect = true;
            this.gridView1.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.gridView1.OptionsView.ColumnAutoWidth = false;
            this.gridView1.OptionsView.RowAutoHeight = true;
            this.gridView1.OptionsView.ShowFooter = true;
            // 
            // bandedGridView1
            // 
            this.bandedGridView1.GridControl = this.gridControl1;
            this.bandedGridView1.Name = "bandedGridView1";
            // 
            // advBandedGridView1
            // 
            this.advBandedGridView1.Bands.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] {
            this.gridBand1});
            this.advBandedGridView1.GridControl = this.gridControl1;
            this.advBandedGridView1.Name = "advBandedGridView1";
            // 
            // gridBand1
            // 
            this.gridBand1.Caption = "gridBand1";
            this.gridBand1.MinWidth = 12;
            this.gridBand1.Name = "gridBand1";
            this.gridBand1.VisibleIndex = 0;
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.panelControlsalesvat);
            this.groupControl1.Controls.Add(this.panelsalesvatex);
            this.groupControl1.Controls.Add(this.lblBranch);
            this.groupControl1.Controls.Add(this.simpleButton2);
            this.groupControl1.Controls.Add(this.labelControl3);
            this.groupControl1.Controls.Add(this.lblsalesdate);
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(1359, 119);
            this.groupControl1.TabIndex = 13;
            // 
            // panelControlsalesvat
            // 
            this.panelControlsalesvat.Controls.Add(this.labelControl4);
            this.panelControlsalesvat.Controls.Add(this.txtvatablesales);
            this.panelControlsalesvat.Controls.Add(this.txtnetofvatsales);
            this.panelControlsalesvat.Controls.Add(this.labelControl5);
            this.panelControlsalesvat.Location = new System.Drawing.Point(380, 28);
            this.panelControlsalesvat.Name = "panelControlsalesvat";
            this.panelControlsalesvat.Size = new System.Drawing.Size(313, 79);
            this.panelControlsalesvat.TabIndex = 16;
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Font = new System.Drawing.Font("Tahoma", 9.25F);
            this.labelControl4.Appearance.Options.UseFont = true;
            this.labelControl4.Location = new System.Drawing.Point(15, 15);
            this.labelControl4.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(129, 18);
            this.labelControl4.TabIndex = 12;
            this.labelControl4.Text = "Total Vatable Sales:";
            // 
            // txtvatablesales
            // 
            this.txtvatablesales.Location = new System.Drawing.Point(170, 12);
            this.txtvatablesales.Name = "txtvatablesales";
            this.txtvatablesales.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.25F);
            this.txtvatablesales.Properties.Appearance.Options.UseFont = true;
            this.txtvatablesales.Properties.ReadOnly = true;
            this.txtvatablesales.Size = new System.Drawing.Size(125, 24);
            this.txtvatablesales.TabIndex = 9;
            // 
            // txtnetofvatsales
            // 
            this.txtnetofvatsales.Location = new System.Drawing.Point(170, 44);
            this.txtnetofvatsales.Name = "txtnetofvatsales";
            this.txtnetofvatsales.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.25F);
            this.txtnetofvatsales.Properties.Appearance.Options.UseFont = true;
            this.txtnetofvatsales.Properties.ReadOnly = true;
            this.txtnetofvatsales.Size = new System.Drawing.Size(125, 24);
            this.txtnetofvatsales.TabIndex = 14;
            // 
            // labelControl5
            // 
            this.labelControl5.Appearance.Font = new System.Drawing.Font("Tahoma", 9.25F);
            this.labelControl5.Appearance.Options.UseFont = true;
            this.labelControl5.Location = new System.Drawing.Point(15, 47);
            this.labelControl5.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(149, 18);
            this.labelControl5.TabIndex = 13;
            this.labelControl5.Text = "Total Net of Vat Sales:";
            // 
            // panelsalesvatex
            // 
            this.panelsalesvatex.Controls.Add(this.labelControl2);
            this.panelsalesvatex.Controls.Add(this.txtvatexemptsales);
            this.panelsalesvatex.Location = new System.Drawing.Point(380, 28);
            this.panelsalesvatex.Name = "panelsalesvatex";
            this.panelsalesvatex.Size = new System.Drawing.Size(313, 79);
            this.panelsalesvatex.TabIndex = 15;
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 9.25F);
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Location = new System.Drawing.Point(14, 15);
            this.labelControl2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(158, 18);
            this.labelControl2.TabIndex = 11;
            this.labelControl2.Text = "Total Vat Exempt Sales:";
            // 
            // txtvatexemptsales
            // 
            this.txtvatexemptsales.Location = new System.Drawing.Point(178, 12);
            this.txtvatexemptsales.Name = "txtvatexemptsales";
            this.txtvatexemptsales.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.25F);
            this.txtvatexemptsales.Properties.Appearance.Options.UseFont = true;
            this.txtvatexemptsales.Properties.ReadOnly = true;
            this.txtvatexemptsales.Size = new System.Drawing.Size(125, 24);
            this.txtvatexemptsales.TabIndex = 8;
            // 
            // lblBranch
            // 
            this.lblBranch.Appearance.Font = new System.Drawing.Font("Tahoma", 9.25F);
            this.lblBranch.Appearance.Options.UseFont = true;
            this.lblBranch.Location = new System.Drawing.Point(70, 77);
            this.lblBranch.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lblBranch.Name = "lblBranch";
            this.lblBranch.Size = new System.Drawing.Size(59, 18);
            this.lblBranch.TabIndex = 10;
            this.lblBranch.Text = "Date To:";
            // 
            // simpleButton2
            // 
            this.simpleButton2.ImageOptions.Image = global::SalesInventorySystem.Properties.Resources.ExportToExcel_32x32;
            this.simpleButton2.Location = new System.Drawing.Point(699, 29);
            this.simpleButton2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new System.Drawing.Size(148, 78);
            this.simpleButton2.TabIndex = 7;
            this.simpleButton2.Text = "Export to Excel";
            this.simpleButton2.Click += new System.EventHandler(this.simpleButton2_Click);
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Tahoma", 9.25F);
            this.labelControl3.Appearance.Options.UseFont = true;
            this.labelControl3.Location = new System.Drawing.Point(14, 77);
            this.labelControl3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(50, 18);
            this.labelControl3.TabIndex = 2;
            this.labelControl3.Text = "Branch:";
            // 
            // lblsalesdate
            // 
            this.lblsalesdate.Appearance.Font = new System.Drawing.Font("Tahoma", 9.25F);
            this.lblsalesdate.Appearance.Options.UseFont = true;
            this.lblsalesdate.Location = new System.Drawing.Point(188, 44);
            this.lblsalesdate.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lblsalesdate.Name = "lblsalesdate";
            this.lblsalesdate.Size = new System.Drawing.Size(175, 18);
            this.lblsalesdate.TabIndex = 1;
            this.lblsalesdate.Text = "12/12/2019 to 12/31/2019";
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 9.25F);
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(14, 44);
            this.labelControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(168, 18);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "Group Report Sales as of:";
            // 
            // AccountingReportingDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1359, 756);
            this.Controls.Add(this.gridControl1);
            this.Controls.Add(this.groupControl1);
            this.Name = "AccountingReportingDetails";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AccountingReportingDetails";
            this.Load += new System.EventHandler(this.AccountingReportingDetails_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bandedGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.advBandedGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControlsalesvat)).EndInit();
            this.panelControlsalesvat.ResumeLayout(false);
            this.panelControlsalesvat.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtvatablesales.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtnetofvatsales.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelsalesvatex)).EndInit();
            this.panelsalesvatex.ResumeLayout(false);
            this.panelsalesvatex.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtvatexemptsales.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridView bandedGridView1;
        private DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView advBandedGridView1;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand1;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        public DevExpress.XtraGrid.GridControl gridControl1;
        public DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        public DevExpress.XtraEditors.GroupControl groupControl1;
        public DevExpress.XtraEditors.LabelControl lblsalesdate;
        public DevExpress.XtraEditors.LabelControl lblBranch;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        public DevExpress.XtraEditors.TextEdit txtvatexemptsales;
        public DevExpress.XtraEditors.TextEdit txtvatablesales;
        public DevExpress.XtraEditors.TextEdit txtnetofvatsales;
        public DevExpress.XtraEditors.PanelControl panelsalesvatex;
        public DevExpress.XtraEditors.PanelControl panelControlsalesvat;
    }
}