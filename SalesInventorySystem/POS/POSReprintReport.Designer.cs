namespace SalesInventorySystem.POS
{
    partial class POSReprintReport
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
            this.radx = new System.Windows.Forms.RadioButton();
            this.radz = new System.Windows.Forms.RadioButton();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txtdate = new DevExpress.XtraEditors.DateEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.txtcashier = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.searchLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.txtdate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtdate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtcashier.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).BeginInit();
            this.SuspendLayout();
            // 
            // radx
            // 
            this.radx.AutoSize = true;
            this.radx.Checked = true;
            this.radx.Font = new System.Drawing.Font("Tahoma", 9.8F);
            this.radx.Location = new System.Drawing.Point(355, 27);
            this.radx.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.radx.Name = "radx";
            this.radx.Size = new System.Drawing.Size(146, 40);
            this.radx.TabIndex = 0;
            this.radx.TabStop = true;
            this.radx.Text = "X READ";
            this.radx.UseVisualStyleBackColor = true;
            this.radx.CheckedChanged += new System.EventHandler(this.radx_CheckedChanged);
            // 
            // radz
            // 
            this.radz.AutoSize = true;
            this.radz.Font = new System.Drawing.Font("Tahoma", 9.8F);
            this.radz.Location = new System.Drawing.Point(533, 27);
            this.radz.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.radz.Name = "radz";
            this.radz.Size = new System.Drawing.Size(146, 40);
            this.radz.TabIndex = 1;
            this.radz.Text = "Z READ";
            this.radz.UseVisualStyleBackColor = true;
            this.radz.CheckedChanged += new System.EventHandler(this.radz_CheckedChanged);
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 9.8F);
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(33, 96);
            this.labelControl1.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(155, 35);
            this.labelControl1.TabIndex = 2;
            this.labelControl1.Text = "Select Date:";
            // 
            // txtdate
            // 
            this.txtdate.EditValue = null;
            this.txtdate.Location = new System.Drawing.Point(238, 91);
            this.txtdate.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.txtdate.Name = "txtdate";
            this.txtdate.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.8F);
            this.txtdate.Properties.Appearance.Options.UseFont = true;
            this.txtdate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtdate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtdate.Size = new System.Drawing.Size(253, 50);
            this.txtdate.TabIndex = 3;
            this.txtdate.EditValueChanged += new System.EventHandler(this.txtdate_EditValueChanged);
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 9.8F);
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Location = new System.Drawing.Point(33, 154);
            this.labelControl2.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(188, 35);
            this.labelControl2.TabIndex = 4;
            this.labelControl2.Text = "Select Cashier:";
            // 
            // txtcashier
            // 
            this.txtcashier.Location = new System.Drawing.Point(238, 149);
            this.txtcashier.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.txtcashier.Name = "txtcashier";
            this.txtcashier.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.8F);
            this.txtcashier.Properties.Appearance.Options.UseFont = true;
            this.txtcashier.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtcashier.Properties.NullText = "";
            this.txtcashier.Properties.PopupView = this.searchLookUpEdit1View;
            this.txtcashier.Size = new System.Drawing.Size(500, 50);
            this.txtcashier.TabIndex = 5;
            this.txtcashier.EditValueChanged += new System.EventHandler(this.txtcashier_EditValueChanged);
            // 
            // searchLookUpEdit1View
            // 
            this.searchLookUpEdit1View.DetailHeight = 634;
            this.searchLookUpEdit1View.FixedLineWidth = 4;
            this.searchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.searchLookUpEdit1View.Name = "searchLookUpEdit1View";
            this.searchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.searchLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Tahoma", 9.8F);
            this.labelControl3.Appearance.Options.UseFont = true;
            this.labelControl3.Location = new System.Drawing.Point(33, 33);
            this.labelControl3.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(285, 35);
            this.labelControl3.TabIndex = 7;
            this.labelControl3.Text = "Select Type of Report:";
            // 
            // simpleButton1
            // 
            this.simpleButton1.ImageOptions.Image = global::SalesInventorySystem.Properties.Resources.Print_32x32__2_;
            this.simpleButton1.Location = new System.Drawing.Point(238, 207);
            this.simpleButton1.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(253, 82);
            this.simpleButton1.TabIndex = 6;
            this.simpleButton1.Text = "Print";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // POSReprintReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 29F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(771, 312);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.simpleButton1);
            this.Controls.Add(this.txtcashier);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.txtdate);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.radz);
            this.Controls.Add(this.radx);
            this.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.Name = "POSReprintReport";
            this.Text = "POSReprintReport";
            this.Load += new System.EventHandler(this.POSReprintReport_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtdate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtdate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtcashier.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton radx;
        private System.Windows.Forms.RadioButton radz;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.DateEdit txtdate;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.SearchLookUpEdit txtcashier;
        private DevExpress.XtraGrid.Views.Grid.GridView searchLookUpEdit1View;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.LabelControl labelControl3;
    }
}