namespace SalesInventorySystem.HOFormsDevEx
{
    partial class InventoryConversion
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
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.bandedGridView1 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridView();
            this.advBandedGridView1 = new DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView();
            this.gridBand1 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.gridView3 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.txtcost = new DevExpress.XtraEditors.TextEdit();
            this.txtparentprodcode = new DevExpress.XtraEditors.TextEdit();
            this.txtbranch = new DevExpress.XtraEditors.TextEdit();
            this.txtdate = new DevExpress.XtraEditors.TextEdit();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bandedGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.advBandedGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtcost.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtparentprodcode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtbranch.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtdate.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl2
            // 
            this.groupControl2.Controls.Add(this.gridControl1);
            this.groupControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl2.Location = new System.Drawing.Point(3, 3);
            this.groupControl2.Margin = new System.Windows.Forms.Padding(6, 4, 6, 4);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(2268, 827);
            this.groupControl2.TabIndex = 16;
            this.groupControl2.Text = "Result Set";
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.gridControl1.Location = new System.Drawing.Point(3, 45);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(2262, 779);
            this.gridControl1.TabIndex = 3;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1,
            this.bandedGridView1,
            this.advBandedGridView1,
            this.gridView3});
            // 
            // gridView1
            // 
            this.gridView1.Appearance.GroupRow.BackColor = System.Drawing.Color.ForestGreen;
            this.gridView1.Appearance.GroupRow.ForeColor = System.Drawing.Color.Gold;
            this.gridView1.Appearance.GroupRow.Options.UseBackColor = true;
            this.gridView1.Appearance.GroupRow.Options.UseForeColor = true;
            this.gridView1.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridView1.Appearance.HeaderPanel.Options.UseFont = true;
            this.gridView1.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridView1.Appearance.Row.Options.UseFont = true;
            this.gridView1.ColumnPanelRowHeight = 0;
            this.gridView1.DetailHeight = 673;
            this.gridView1.FixedLineWidth = 4;
            this.gridView1.FooterPanelHeight = 0;
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.GroupRowHeight = 0;
            this.gridView1.LevelIndent = 0;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.gridView1.OptionsView.ColumnAutoWidth = false;
            this.gridView1.OptionsView.RowAutoHeight = true;
            this.gridView1.OptionsView.ShowFooter = true;
            this.gridView1.PreviewIndent = 0;
            this.gridView1.RowHeight = 0;
            this.gridView1.ViewCaptionHeight = 0;
            this.gridView1.CustomRowCellEdit += new DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventHandler(this.gridView1_CustomRowCellEdit);
            this.gridView1.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.gridView1_CellValueChanged);
            // 
            // bandedGridView1
            // 
            this.bandedGridView1.BandPanelRowHeight = 0;
            this.bandedGridView1.ColumnPanelRowHeight = 0;
            this.bandedGridView1.DetailHeight = 673;
            this.bandedGridView1.FixedLineWidth = 4;
            this.bandedGridView1.FooterPanelHeight = 0;
            this.bandedGridView1.GridControl = this.gridControl1;
            this.bandedGridView1.GroupRowHeight = 0;
            this.bandedGridView1.LevelIndent = 0;
            this.bandedGridView1.Name = "bandedGridView1";
            this.bandedGridView1.PreviewIndent = 0;
            this.bandedGridView1.RowHeight = 0;
            this.bandedGridView1.ViewCaptionHeight = 0;
            // 
            // advBandedGridView1
            // 
            this.advBandedGridView1.BandPanelRowHeight = 0;
            this.advBandedGridView1.Bands.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] {
            this.gridBand1});
            this.advBandedGridView1.ColumnPanelRowHeight = 0;
            this.advBandedGridView1.DetailHeight = 673;
            this.advBandedGridView1.FixedLineWidth = 4;
            this.advBandedGridView1.FooterPanelHeight = 0;
            this.advBandedGridView1.GridControl = this.gridControl1;
            this.advBandedGridView1.GroupRowHeight = 0;
            this.advBandedGridView1.LevelIndent = 0;
            this.advBandedGridView1.Name = "advBandedGridView1";
            this.advBandedGridView1.PreviewIndent = 0;
            this.advBandedGridView1.RowHeight = 0;
            this.advBandedGridView1.ViewCaptionHeight = 0;
            // 
            // gridBand1
            // 
            this.gridBand1.Caption = "gridBand1";
            this.gridBand1.MinWidth = 22;
            this.gridBand1.Name = "gridBand1";
            this.gridBand1.VisibleIndex = 0;
            this.gridBand1.Width = 160;
            // 
            // gridView3
            // 
            this.gridView3.ColumnPanelRowHeight = 0;
            this.gridView3.DetailHeight = 673;
            this.gridView3.FixedLineWidth = 4;
            this.gridView3.FooterPanelHeight = 0;
            this.gridView3.GridControl = this.gridControl1;
            this.gridView3.GroupRowHeight = 0;
            this.gridView3.LevelIndent = 0;
            this.gridView3.Name = "gridView3";
            this.gridView3.PreviewIndent = 0;
            this.gridView3.RowHeight = 0;
            this.gridView3.ViewCaptionHeight = 0;
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.groupControl2);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 103);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(2274, 833);
            this.panelControl1.TabIndex = 17;
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.txtcost);
            this.panelControl2.Controls.Add(this.txtparentprodcode);
            this.panelControl2.Controls.Add(this.txtbranch);
            this.panelControl2.Controls.Add(this.txtdate);
            this.panelControl2.Controls.Add(this.simpleButton1);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl2.Location = new System.Drawing.Point(0, 0);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(2274, 103);
            this.panelControl2.TabIndex = 18;
            // 
            // txtcost
            // 
            this.txtcost.Location = new System.Drawing.Point(810, 33);
            this.txtcost.Name = "txtcost";
            this.txtcost.Size = new System.Drawing.Size(200, 40);
            this.txtcost.TabIndex = 4;
            // 
            // txtparentprodcode
            // 
            this.txtparentprodcode.Location = new System.Drawing.Point(604, 31);
            this.txtparentprodcode.Name = "txtparentprodcode";
            this.txtparentprodcode.Size = new System.Drawing.Size(200, 40);
            this.txtparentprodcode.TabIndex = 3;
            // 
            // txtbranch
            // 
            this.txtbranch.Location = new System.Drawing.Point(398, 33);
            this.txtbranch.Name = "txtbranch";
            this.txtbranch.Size = new System.Drawing.Size(200, 40);
            this.txtbranch.TabIndex = 2;
            // 
            // txtdate
            // 
            this.txtdate.Location = new System.Drawing.Point(192, 33);
            this.txtdate.Name = "txtdate";
            this.txtdate.Size = new System.Drawing.Size(200, 40);
            this.txtdate.TabIndex = 1;
            // 
            // simpleButton1
            // 
            this.simpleButton1.Location = new System.Drawing.Point(21, 27);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(150, 46);
            this.simpleButton1.TabIndex = 0;
            this.simpleButton1.Text = "Convert";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // InventoryConversion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(2274, 936);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.panelControl2);
            this.Name = "InventoryConversion";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "InventoryConversion";
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bandedGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.advBandedGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtcost.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtparentprodcode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtbranch.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtdate.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridView bandedGridView1;
        private DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView advBandedGridView1;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView3;
        public DevExpress.XtraGrid.GridControl gridControl1;
        public DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        public DevExpress.XtraEditors.TextEdit txtdate;
        public DevExpress.XtraEditors.TextEdit txtbranch;
        public DevExpress.XtraEditors.TextEdit txtparentprodcode;
        public DevExpress.XtraEditors.TextEdit txtcost;
    }
}