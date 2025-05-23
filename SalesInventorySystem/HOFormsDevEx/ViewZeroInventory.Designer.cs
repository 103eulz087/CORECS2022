namespace SalesInventorySystem.HOFormsDevEx
{
    partial class ViewZeroInventory
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ViewZeroInventory));
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.searchLookUpEdit1 = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.searchLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.btnforapprovalsalesorderexcel = new DevExpress.XtraEditors.SimpleButton();
            this.btnForApprovalSalesOrder = new DevExpress.XtraEditors.SimpleButton();
            this.radlessthan = new System.Windows.Forms.RadioButton();
            this.radzero = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.White;
            this.groupBox2.Controls.Add(this.gridControl1);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 95);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1163, 457);
            this.groupBox2.TabIndex = 13;
            this.groupBox2.TabStop = false;
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(3, 16);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(1157, 438);
            this.gridControl1.TabIndex = 1;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 9.25F, System.Drawing.FontStyle.Bold);
            this.gridView1.Appearance.HeaderPanel.Options.UseFont = true;
            this.gridView1.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 9.25F);
            this.gridView1.Appearance.Row.Options.UseFont = true;
            this.gridView1.DetailHeight = 157;
            this.gridView1.FixedLineWidth = 1;
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.LevelIndent = 0;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsSelection.MultiSelect = true;
            this.gridView1.OptionsView.ColumnAutoWidth = false;
            this.gridView1.OptionsView.RowAutoHeight = true;
            this.gridView1.OptionsView.ShowFooter = true;
            this.gridView1.PreviewIndent = 0;
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.searchLookUpEdit1);
            this.groupControl1.Controls.Add(this.btnforapprovalsalesorderexcel);
            this.groupControl1.Controls.Add(this.btnForApprovalSalesOrder);
            this.groupControl1.Controls.Add(this.radlessthan);
            this.groupControl1.Controls.Add(this.radzero);
            this.groupControl1.Controls.Add(this.label1);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(1163, 95);
            this.groupControl1.TabIndex = 14;
            // 
            // searchLookUpEdit1
            // 
            this.searchLookUpEdit1.Location = new System.Drawing.Point(97, 34);
            this.searchLookUpEdit1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.searchLookUpEdit1.Name = "searchLookUpEdit1";
            this.searchLookUpEdit1.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9F);
            this.searchLookUpEdit1.Properties.Appearance.Options.UseFont = true;
            this.searchLookUpEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.searchLookUpEdit1.Properties.NullText = "";
            this.searchLookUpEdit1.Properties.PopupView = this.searchLookUpEdit1View;
            this.searchLookUpEdit1.Size = new System.Drawing.Size(116, 20);
            this.searchLookUpEdit1.TabIndex = 11;
            this.searchLookUpEdit1.EditValueChanged += new System.EventHandler(this.searchLookUpEdit1_EditValueChanged);
            // 
            // searchLookUpEdit1View
            // 
            this.searchLookUpEdit1View.DetailHeight = 157;
            this.searchLookUpEdit1View.FixedLineWidth = 1;
            this.searchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.searchLookUpEdit1View.LevelIndent = 0;
            this.searchLookUpEdit1View.Name = "searchLookUpEdit1View";
            this.searchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.searchLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            this.searchLookUpEdit1View.PreviewIndent = 0;
            // 
            // btnforapprovalsalesorderexcel
            // 
            this.btnforapprovalsalesorderexcel.ImageOptions.Image = global::SalesInventorySystem.Properties.Resources.ExportToExcel_16x16;
            this.btnforapprovalsalesorderexcel.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btnforapprovalsalesorderexcel.Location = new System.Drawing.Point(302, 34);
            this.btnforapprovalsalesorderexcel.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnforapprovalsalesorderexcel.Name = "btnforapprovalsalesorderexcel";
            this.btnforapprovalsalesorderexcel.Size = new System.Drawing.Size(110, 23);
            this.btnforapprovalsalesorderexcel.TabIndex = 10;
            this.btnforapprovalsalesorderexcel.Text = "Export to Excel";
            this.btnforapprovalsalesorderexcel.Click += new System.EventHandler(this.btnforapprovalsalesorderexcel_Click);
            // 
            // btnForApprovalSalesOrder
            // 
            this.btnForApprovalSalesOrder.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnForApprovalSalesOrder.ImageOptions.Image")));
            this.btnForApprovalSalesOrder.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btnForApprovalSalesOrder.Location = new System.Drawing.Point(217, 34);
            this.btnForApprovalSalesOrder.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnForApprovalSalesOrder.Name = "btnForApprovalSalesOrder";
            this.btnForApprovalSalesOrder.Size = new System.Drawing.Size(80, 23);
            this.btnForApprovalSalesOrder.TabIndex = 9;
            this.btnForApprovalSalesOrder.Text = "Generate";
            this.btnForApprovalSalesOrder.Click += new System.EventHandler(this.btnForApprovalSalesOrder_Click);
            // 
            // radlessthan
            // 
            this.radlessthan.AutoSize = true;
            this.radlessthan.Location = new System.Drawing.Point(98, 75);
            this.radlessthan.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.radlessthan.Name = "radlessthan";
            this.radlessthan.Size = new System.Drawing.Size(143, 17);
            this.radlessthan.TabIndex = 8;
            this.radlessthan.Text = "Less than ReOrder Level";
            this.radlessthan.UseVisualStyleBackColor = true;
            // 
            // radzero
            // 
            this.radzero.AutoSize = true;
            this.radzero.Checked = true;
            this.radzero.Location = new System.Drawing.Point(98, 58);
            this.radzero.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.radzero.Name = "radzero";
            this.radzero.Size = new System.Drawing.Size(68, 17);
            this.radzero.TabIndex = 7;
            this.radzero.TabStop = true;
            this.radzero.Text = "Zero Out";
            this.radzero.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(14, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "Select Branch:";
            // 
            // ViewZeroInventory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1163, 552);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupControl1);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "ViewZeroInventory";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ViewZeroInventory";
            this.Load += new System.EventHandler(this.ViewZeroInventory_Load);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton radlessthan;
        private System.Windows.Forms.RadioButton radzero;
        private DevExpress.XtraEditors.SimpleButton btnforapprovalsalesorderexcel;
        private DevExpress.XtraEditors.SimpleButton btnForApprovalSalesOrder;
        private DevExpress.XtraEditors.SearchLookUpEdit searchLookUpEdit1;
        private DevExpress.XtraGrid.Views.Grid.GridView searchLookUpEdit1View;
    }
}