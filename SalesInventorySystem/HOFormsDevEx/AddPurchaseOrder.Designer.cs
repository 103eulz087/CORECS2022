namespace SalesInventorySystem.HOFormsDevEx
{
    partial class AddPurchaseOrder
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.repoMetrics = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.label12 = new System.Windows.Forms.Label();
            this.txtdate = new DevExpress.XtraEditors.DateEdit();
            this.label2 = new System.Windows.Forms.Label();
            this.txtbranch = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.txtremakrs = new DevExpress.XtraEditors.MemoEdit();
            this.label9 = new System.Windows.Forms.Label();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.radServices = new System.Windows.Forms.RadioButton();
            this.radProducts = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.btnnew = new DevExpress.XtraEditors.SimpleButton();
            this.searchLookUpEdit1 = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.gridview = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.txtshipmentno = new DevExpress.XtraEditors.TextEdit();
            this.btnsave = new DevExpress.XtraEditors.SimpleButton();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repoMetrics)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtdate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtdate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtbranch.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtremakrs.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridview)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtshipmentno.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.gridControl1);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 311);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(2270, 905);
            this.groupBox2.TabIndex = 66;
            this.groupBox2.TabStop = false;
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.gridControl1.Location = new System.Drawing.Point(3, 29);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repoMetrics});
            this.gridControl1.Size = new System.Drawing.Size(2264, 873);
            this.gridControl1.TabIndex = 9;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridView1.Appearance.HeaderPanel.Options.UseFont = true;
            this.gridView1.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridView1.Appearance.Row.Options.UseFont = true;
            this.gridView1.DetailHeight = 673;
            this.gridView1.FixedLineWidth = 3;
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsSelection.MultiSelect = true;
            this.gridView1.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            this.gridView1.OptionsView.ColumnAutoWidth = false;
            this.gridView1.OptionsView.RowAutoHeight = true;
            this.gridView1.OptionsView.ShowIndicator = false;
            this.gridView1.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.gridView1_RowCellStyle);
            this.gridView1.CustomRowCellEdit += new DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventHandler(this.gridView1_CustomRowCellEdit);
            this.gridView1.ShowingEditor += new System.ComponentModel.CancelEventHandler(this.gridView1_ShowingEditor);
            // 
            // repoMetrics
            // 
            this.repoMetrics.AutoHeight = false;
            this.repoMetrics.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repoMetrics.Name = "repoMetrics";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.groupControl2);
            this.groupBox1.Controls.Add(this.groupControl1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(2270, 311);
            this.groupBox1.TabIndex = 65;
            this.groupBox1.TabStop = false;
            // 
            // groupControl2
            // 
            this.groupControl2.Controls.Add(this.label12);
            this.groupControl2.Controls.Add(this.txtdate);
            this.groupControl2.Controls.Add(this.label2);
            this.groupControl2.Controls.Add(this.txtbranch);
            this.groupControl2.Controls.Add(this.txtremakrs);
            this.groupControl2.Controls.Add(this.label9);
            this.groupControl2.Location = new System.Drawing.Point(867, 28);
            this.groupControl2.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(1382, 273);
            this.groupControl2.TabIndex = 83;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(22, 67);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(94, 29);
            this.label12.TabIndex = 53;
            this.label12.Text = "Branch:";
            // 
            // txtdate
            // 
            this.txtdate.EditValue = null;
            this.txtdate.Enabled = false;
            this.txtdate.Location = new System.Drawing.Point(249, 112);
            this.txtdate.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.txtdate.Name = "txtdate";
            this.txtdate.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtdate.Properties.Appearance.Options.UseFont = true;
            this.txtdate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtdate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtdate.Size = new System.Drawing.Size(219, 44);
            this.txtdate.TabIndex = 46;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(22, 116);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(149, 29);
            this.label2.TabIndex = 45;
            this.label2.Text = "Target Date:";
            // 
            // txtbranch
            // 
            this.txtbranch.Enabled = false;
            this.txtbranch.Location = new System.Drawing.Point(249, 62);
            this.txtbranch.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.txtbranch.Name = "txtbranch";
            this.txtbranch.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtbranch.Properties.Appearance.Options.UseFont = true;
            this.txtbranch.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtbranch.Properties.DisplayMember = "SupplierName";
            this.txtbranch.Properties.NullText = "";
            this.txtbranch.Properties.PopupView = this.gridView2;
            this.txtbranch.Properties.ValueMember = "SupplierName";
            this.txtbranch.Size = new System.Drawing.Size(219, 44);
            this.txtbranch.TabIndex = 69;
            // 
            // gridView2
            // 
            this.gridView2.DetailHeight = 547;
            this.gridView2.FixedLineWidth = 3;
            this.gridView2.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView2.Name = "gridView2";
            this.gridView2.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView2.OptionsView.ShowGroupPanel = false;
            // 
            // txtremakrs
            // 
            this.txtremakrs.Location = new System.Drawing.Point(626, 64);
            this.txtremakrs.Name = "txtremakrs";
            this.txtremakrs.Size = new System.Drawing.Size(729, 200);
            this.txtremakrs.TabIndex = 74;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(499, 114);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(114, 29);
            this.label9.TabIndex = 73;
            this.label9.Text = "Remarks:";
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.radServices);
            this.groupControl1.Controls.Add(this.radProducts);
            this.groupControl1.Controls.Add(this.label1);
            this.groupControl1.Controls.Add(this.btnnew);
            this.groupControl1.Controls.Add(this.searchLookUpEdit1);
            this.groupControl1.Controls.Add(this.txtshipmentno);
            this.groupControl1.Controls.Add(this.btnsave);
            this.groupControl1.Controls.Add(this.label4);
            this.groupControl1.Location = new System.Drawing.Point(10, 28);
            this.groupControl1.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(845, 273);
            this.groupControl1.TabIndex = 82;
            // 
            // radServices
            // 
            this.radServices.AutoSize = true;
            this.radServices.Location = new System.Drawing.Point(406, 158);
            this.radServices.Margin = new System.Windows.Forms.Padding(5);
            this.radServices.Name = "radServices";
            this.radServices.Size = new System.Drawing.Size(119, 29);
            this.radServices.TabIndex = 88;
            this.radServices.Text = "Services";
            this.radServices.UseVisualStyleBackColor = true;
            // 
            // radProducts
            // 
            this.radProducts.AutoSize = true;
            this.radProducts.Checked = true;
            this.radProducts.Location = new System.Drawing.Point(252, 158);
            this.radProducts.Margin = new System.Windows.Forms.Padding(5);
            this.radProducts.Name = "radProducts";
            this.radProducts.Size = new System.Drawing.Size(124, 29);
            this.radProducts.TabIndex = 87;
            this.radProducts.TabStop = true;
            this.radProducts.Text = "Products";
            this.radProducts.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(29, 67);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(163, 29);
            this.label1.TabIndex = 38;
            this.label1.Text = "Shipment No.:";
            // 
            // btnnew
            // 
            this.btnnew.ImageOptions.Image = global::SalesInventorySystem.Properties.Resources.New_16x16__5_;
            this.btnnew.Location = new System.Drawing.Point(442, 198);
            this.btnnew.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.btnnew.Name = "btnnew";
            this.btnnew.Size = new System.Drawing.Size(171, 66);
            this.btnnew.TabIndex = 84;
            this.btnnew.Text = "New";
            this.btnnew.Click += new System.EventHandler(this.btnnew_Click);
            // 
            // searchLookUpEdit1
            // 
            this.searchLookUpEdit1.Enabled = false;
            this.searchLookUpEdit1.Location = new System.Drawing.Point(252, 109);
            this.searchLookUpEdit1.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.searchLookUpEdit1.Name = "searchLookUpEdit1";
            this.searchLookUpEdit1.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.searchLookUpEdit1.Properties.Appearance.Options.UseFont = true;
            this.searchLookUpEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.searchLookUpEdit1.Properties.DisplayMember = "SupplierName";
            this.searchLookUpEdit1.Properties.NullText = "";
            this.searchLookUpEdit1.Properties.PopupView = this.gridview;
            this.searchLookUpEdit1.Properties.ValueMember = "SupplierName";
            this.searchLookUpEdit1.Size = new System.Drawing.Size(542, 44);
            this.searchLookUpEdit1.TabIndex = 55;
            this.searchLookUpEdit1.EditValueChanged += new System.EventHandler(this.searchLookUpEdit1_EditValueChanged);
            // 
            // gridview
            // 
            this.gridview.DetailHeight = 547;
            this.gridview.FixedLineWidth = 3;
            this.gridview.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridview.Name = "gridview";
            this.gridview.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridview.OptionsView.ShowGroupPanel = false;
            // 
            // txtshipmentno
            // 
            this.txtshipmentno.Enabled = false;
            this.txtshipmentno.Location = new System.Drawing.Point(252, 59);
            this.txtshipmentno.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.txtshipmentno.Name = "txtshipmentno";
            this.txtshipmentno.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtshipmentno.Properties.Appearance.Options.UseFont = true;
            this.txtshipmentno.Properties.ReadOnly = true;
            this.txtshipmentno.Size = new System.Drawing.Size(238, 44);
            this.txtshipmentno.TabIndex = 40;
            // 
            // btnsave
            // 
            this.btnsave.ImageOptions.Image = global::SalesInventorySystem.Properties.Resources.Save_16x16__5_;
            this.btnsave.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btnsave.Location = new System.Drawing.Point(624, 198);
            this.btnsave.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.btnsave.Name = "btnsave";
            this.btnsave.Size = new System.Drawing.Size(170, 66);
            this.btnsave.TabIndex = 86;
            this.btnsave.Text = "Save (F5)";
            this.btnsave.Click += new System.EventHandler(this.btnsave_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(33, 112);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(176, 29);
            this.label4.TabIndex = 39;
            this.label4.Text = "Select Supplier:";
            // 
            // AddPurchaseOrder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(2270, 1216);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "AddPurchaseOrder";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AddPurchaseOrder";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.AddPurchaseOrder_Load);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repoMetrics)).EndInit();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            this.groupControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtdate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtdate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtbranch.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtremakrs.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridview)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtshipmentno.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox1;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private System.Windows.Forms.Label label12;
        public DevExpress.XtraEditors.DateEdit txtdate;
        private System.Windows.Forms.Label label2;
        private DevExpress.XtraEditors.SimpleButton btnsave;
        public DevExpress.XtraEditors.SearchLookUpEdit txtbranch;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        public DevExpress.XtraEditors.MemoEdit txtremakrs;
        private System.Windows.Forms.Label label9;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private System.Windows.Forms.Label label1;
        public DevExpress.XtraEditors.SimpleButton btnnew;
        public DevExpress.XtraEditors.SearchLookUpEdit searchLookUpEdit1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridview;
        public DevExpress.XtraEditors.TextEdit txtshipmentno;
        private System.Windows.Forms.Label label4;
        public DevExpress.XtraGrid.GridControl gridControl1;
        public DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox repoMetrics;
        private System.Windows.Forms.RadioButton radServices;
        private System.Windows.Forms.RadioButton radProducts;
    }
}