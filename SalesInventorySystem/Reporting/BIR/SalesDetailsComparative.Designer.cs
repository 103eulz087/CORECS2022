namespace SalesInventorySystem.Reporting.BIR
{
    partial class SalesDetailsComparative
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
            this.btnforapprovalsalesorderexcel = new DevExpress.XtraEditors.SimpleButton();
            this.btnExtract = new DevExpress.XtraEditors.SimpleButton();
            this.txtmachine = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.gridView5 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.labelControl15 = new DevExpress.XtraEditors.LabelControl();
            this.txtbranch = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.searchLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txtsalesdateto = new DevExpress.XtraEditors.DateEdit();
            this.labelControl19 = new DevExpress.XtraEditors.LabelControl();
            this.txtsalesdatefrom = new DevExpress.XtraEditors.DateEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bandedGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.advBandedGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtmachine.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtbranch.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtsalesdateto.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtsalesdateto.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtsalesdatefrom.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtsalesdatefrom.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(6);
            this.gridControl1.Location = new System.Drawing.Point(0, 196);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Margin = new System.Windows.Forms.Padding(6);
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(2219, 1177);
            this.gridControl1.TabIndex = 17;
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
            this.gridView1.DetailHeight = 547;
            this.gridView1.FixedLineWidth = 3;
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
            this.bandedGridView1.DetailHeight = 547;
            this.bandedGridView1.FixedLineWidth = 3;
            this.bandedGridView1.GridControl = this.gridControl1;
            this.bandedGridView1.Name = "bandedGridView1";
            // 
            // advBandedGridView1
            // 
            this.advBandedGridView1.Bands.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] {
            this.gridBand1});
            this.advBandedGridView1.DetailHeight = 547;
            this.advBandedGridView1.FixedLineWidth = 3;
            this.advBandedGridView1.GridControl = this.gridControl1;
            this.advBandedGridView1.Name = "advBandedGridView1";
            // 
            // gridBand1
            // 
            this.gridBand1.Caption = "gridBand1";
            this.gridBand1.MinWidth = 20;
            this.gridBand1.Name = "gridBand1";
            this.gridBand1.VisibleIndex = 0;
            this.gridBand1.Width = 120;
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.txtsalesdateto);
            this.groupControl1.Controls.Add(this.labelControl19);
            this.groupControl1.Controls.Add(this.txtsalesdatefrom);
            this.groupControl1.Controls.Add(this.labelControl2);
            this.groupControl1.Controls.Add(this.txtmachine);
            this.groupControl1.Controls.Add(this.labelControl15);
            this.groupControl1.Controls.Add(this.txtbranch);
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Controls.Add(this.btnforapprovalsalesorderexcel);
            this.groupControl1.Controls.Add(this.btnExtract);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Margin = new System.Windows.Forms.Padding(6, 4, 6, 4);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(2219, 196);
            this.groupControl1.TabIndex = 16;
            // 
            // btnforapprovalsalesorderexcel
            // 
            this.btnforapprovalsalesorderexcel.ImageOptions.Image = global::SalesInventorySystem.Properties.Resources.ExportToExcel_16x16;
            this.btnforapprovalsalesorderexcel.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btnforapprovalsalesorderexcel.Location = new System.Drawing.Point(1214, 64);
            this.btnforapprovalsalesorderexcel.Margin = new System.Windows.Forms.Padding(6);
            this.btnforapprovalsalesorderexcel.Name = "btnforapprovalsalesorderexcel";
            this.btnforapprovalsalesorderexcel.Size = new System.Drawing.Size(236, 101);
            this.btnforapprovalsalesorderexcel.TabIndex = 9;
            this.btnforapprovalsalesorderexcel.Text = "Export to Excel";
            this.btnforapprovalsalesorderexcel.Click += new System.EventHandler(this.btnforapprovalsalesorderexcel_Click);
            // 
            // btnExtract
            // 
            this.btnExtract.Location = new System.Drawing.Point(1044, 63);
            this.btnExtract.Margin = new System.Windows.Forms.Padding(6, 4, 6, 4);
            this.btnExtract.Name = "btnExtract";
            this.btnExtract.Size = new System.Drawing.Size(162, 103);
            this.btnExtract.TabIndex = 4;
            this.btnExtract.Text = "Extract";
            this.btnExtract.Click += new System.EventHandler(this.btnExtract_Click);
            // 
            // txtmachine
            // 
            this.txtmachine.Location = new System.Drawing.Point(173, 116);
            this.txtmachine.Margin = new System.Windows.Forms.Padding(5, 7, 5, 7);
            this.txtmachine.Name = "txtmachine";
            this.txtmachine.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.txtmachine.Properties.Appearance.Options.UseFont = true;
            this.txtmachine.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtmachine.Properties.NullText = "";
            this.txtmachine.Properties.PopupView = this.gridView5;
            this.txtmachine.Size = new System.Drawing.Size(338, 50);
            this.txtmachine.TabIndex = 34;
            // 
            // gridView5
            // 
            this.gridView5.DetailHeight = 547;
            this.gridView5.FixedLineWidth = 3;
            this.gridView5.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView5.Name = "gridView5";
            this.gridView5.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView5.OptionsView.ShowGroupPanel = false;
            // 
            // labelControl15
            // 
            this.labelControl15.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl15.Appearance.Options.UseFont = true;
            this.labelControl15.Location = new System.Drawing.Point(34, 120);
            this.labelControl15.Margin = new System.Windows.Forms.Padding(5, 7, 5, 7);
            this.labelControl15.Name = "labelControl15";
            this.labelControl15.Size = new System.Drawing.Size(104, 36);
            this.labelControl15.TabIndex = 33;
            this.labelControl15.Text = "Machine:";
            // 
            // txtbranch
            // 
            this.txtbranch.Location = new System.Drawing.Point(173, 59);
            this.txtbranch.Margin = new System.Windows.Forms.Padding(5, 7, 5, 7);
            this.txtbranch.Name = "txtbranch";
            this.txtbranch.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.txtbranch.Properties.Appearance.Options.UseFont = true;
            this.txtbranch.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtbranch.Properties.NullText = "";
            this.txtbranch.Properties.PopupView = this.searchLookUpEdit1View;
            this.txtbranch.Size = new System.Drawing.Size(338, 50);
            this.txtbranch.TabIndex = 32;
            this.txtbranch.EditValueChanged += new System.EventHandler(this.txtbranch_EditValueChanged);
            // 
            // searchLookUpEdit1View
            // 
            this.searchLookUpEdit1View.DetailHeight = 547;
            this.searchLookUpEdit1View.FixedLineWidth = 3;
            this.searchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.searchLookUpEdit1View.Name = "searchLookUpEdit1View";
            this.searchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.searchLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(34, 65);
            this.labelControl1.Margin = new System.Windows.Forms.Padding(5, 7, 5, 7);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(85, 36);
            this.labelControl1.TabIndex = 31;
            this.labelControl1.Text = "Branch:";
            // 
            // txtsalesdateto
            // 
            this.txtsalesdateto.EditValue = null;
            this.txtsalesdateto.Location = new System.Drawing.Point(695, 117);
            this.txtsalesdateto.Margin = new System.Windows.Forms.Padding(5, 7, 5, 7);
            this.txtsalesdateto.Name = "txtsalesdateto";
            this.txtsalesdateto.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.txtsalesdateto.Properties.Appearance.Options.UseFont = true;
            this.txtsalesdateto.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtsalesdateto.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtsalesdateto.Size = new System.Drawing.Size(338, 50);
            this.txtsalesdateto.TabIndex = 38;
            // 
            // labelControl19
            // 
            this.labelControl19.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl19.Appearance.Options.UseFont = true;
            this.labelControl19.Location = new System.Drawing.Point(532, 122);
            this.labelControl19.Margin = new System.Windows.Forms.Padding(5, 7, 5, 7);
            this.labelControl19.Name = "labelControl19";
            this.labelControl19.Size = new System.Drawing.Size(96, 36);
            this.labelControl19.TabIndex = 37;
            this.labelControl19.Text = "Date To:";
            // 
            // txtsalesdatefrom
            // 
            this.txtsalesdatefrom.EditValue = null;
            this.txtsalesdatefrom.Location = new System.Drawing.Point(695, 62);
            this.txtsalesdatefrom.Margin = new System.Windows.Forms.Padding(5, 7, 5, 7);
            this.txtsalesdatefrom.Name = "txtsalesdatefrom";
            this.txtsalesdatefrom.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.txtsalesdatefrom.Properties.Appearance.Options.UseFont = true;
            this.txtsalesdatefrom.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtsalesdatefrom.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtsalesdatefrom.Size = new System.Drawing.Size(338, 50);
            this.txtsalesdatefrom.TabIndex = 36;
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Location = new System.Drawing.Point(532, 66);
            this.labelControl2.Margin = new System.Windows.Forms.Padding(5, 7, 5, 7);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(126, 36);
            this.labelControl2.TabIndex = 35;
            this.labelControl2.Text = "Date From:";
            // 
            // SalesDetailsComparative
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(2219, 1373);
            this.Controls.Add(this.gridControl1);
            this.Controls.Add(this.groupControl1);
            this.Name = "SalesDetailsComparative";
            this.Text = "SalesDetailsComparative";
            this.Load += new System.EventHandler(this.SalesDetailsComparative_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bandedGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.advBandedGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtmachine.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtbranch.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtsalesdateto.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtsalesdateto.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtsalesdatefrom.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtsalesdatefrom.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public DevExpress.XtraGrid.GridControl gridControl1;
        public DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridView bandedGridView1;
        private DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView advBandedGridView1;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand1;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.SimpleButton btnforapprovalsalesorderexcel;
        private DevExpress.XtraEditors.SimpleButton btnExtract;
        private DevExpress.XtraEditors.SearchLookUpEdit txtmachine;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView5;
        private DevExpress.XtraEditors.LabelControl labelControl15;
        private DevExpress.XtraEditors.SearchLookUpEdit txtbranch;
        private DevExpress.XtraGrid.Views.Grid.GridView searchLookUpEdit1View;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.DateEdit txtsalesdateto;
        private DevExpress.XtraEditors.LabelControl labelControl19;
        private DevExpress.XtraEditors.DateEdit txtsalesdatefrom;
        private DevExpress.XtraEditors.LabelControl labelControl2;
    }
}