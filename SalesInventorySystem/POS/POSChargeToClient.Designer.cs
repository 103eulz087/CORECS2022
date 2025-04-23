namespace SalesInventorySystem.POS
{
    partial class POSChargeToClient
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(POSChargeToClient));
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txtcust = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.searchLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.txtamount = new DevExpress.XtraEditors.SpinEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.txtremarks = new System.Windows.Forms.RichTextBox();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.txtinvoiceno = new DevExpress.XtraEditors.TextEdit();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.txtorderno = new DevExpress.XtraEditors.TextEdit();
            this.txtdiscountamnt = new DevExpress.XtraEditors.SpinEdit();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.btnsubmit = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.txtcust.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtamount.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtinvoiceno.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtorderno.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtdiscountamnt.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Segoe UI Semibold", 12.2F, System.Drawing.FontStyle.Bold);
            this.labelControl1.Appearance.ForeColor = System.Drawing.Color.White;
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Appearance.Options.UseForeColor = true;
            this.labelControl1.Location = new System.Drawing.Point(34, 19);
            this.labelControl1.Margin = new System.Windows.Forms.Padding(4);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(253, 45);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "Select Customer:";
            // 
            // txtcust
            // 
            this.txtcust.Location = new System.Drawing.Point(298, 19);
            this.txtcust.Margin = new System.Windows.Forms.Padding(4);
            this.txtcust.Name = "txtcust";
            this.txtcust.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12.25F);
            this.txtcust.Properties.Appearance.Options.UseFont = true;
            this.txtcust.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtcust.Properties.NullText = "";
            this.txtcust.Properties.PopupView = this.searchLookUpEdit1View;
            this.txtcust.Size = new System.Drawing.Size(506, 54);
            this.txtcust.TabIndex = 1;
            this.txtcust.EditValueChanged += new System.EventHandler(this.txtcust_EditValueChanged);
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
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Segoe UI Semibold", 12.2F, System.Drawing.FontStyle.Bold);
            this.labelControl2.Appearance.ForeColor = System.Drawing.Color.White;
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Appearance.Options.UseForeColor = true;
            this.labelControl2.Location = new System.Drawing.Point(69, 204);
            this.labelControl2.Margin = new System.Windows.Forms.Padding(4);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(214, 45);
            this.labelControl2.TabIndex = 2;
            this.labelControl2.Text = "Total Amount:";
            // 
            // txtamount
            // 
            this.txtamount.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtamount.Location = new System.Drawing.Point(298, 203);
            this.txtamount.Margin = new System.Windows.Forms.Padding(4);
            this.txtamount.Name = "txtamount";
            this.txtamount.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12.8F);
            this.txtamount.Properties.Appearance.Options.UseFont = true;
            this.txtamount.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtamount.Properties.ReadOnly = true;
            this.txtamount.Size = new System.Drawing.Size(506, 56);
            this.txtamount.TabIndex = 3;
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Segoe UI Semibold", 12.2F, System.Drawing.FontStyle.Bold);
            this.labelControl3.Appearance.ForeColor = System.Drawing.Color.White;
            this.labelControl3.Appearance.Options.UseFont = true;
            this.labelControl3.Appearance.Options.UseForeColor = true;
            this.labelControl3.Location = new System.Drawing.Point(140, 389);
            this.labelControl3.Margin = new System.Windows.Forms.Padding(4);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(136, 45);
            this.labelControl3.TabIndex = 4;
            this.labelControl3.Text = "Remarks:";
            // 
            // txtremarks
            // 
            this.txtremarks.Location = new System.Drawing.Point(298, 328);
            this.txtremarks.Margin = new System.Windows.Forms.Padding(4);
            this.txtremarks.Name = "txtremarks";
            this.txtremarks.Size = new System.Drawing.Size(504, 185);
            this.txtremarks.TabIndex = 5;
            this.txtremarks.Text = "";
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Font = new System.Drawing.Font("Segoe UI Semibold", 12.2F, System.Drawing.FontStyle.Bold);
            this.labelControl4.Appearance.ForeColor = System.Drawing.Color.White;
            this.labelControl4.Appearance.Options.UseFont = true;
            this.labelControl4.Appearance.Options.UseForeColor = true;
            this.labelControl4.Location = new System.Drawing.Point(132, 143);
            this.labelControl4.Margin = new System.Windows.Forms.Padding(4);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(146, 45);
            this.labelControl4.TabIndex = 7;
            this.labelControl4.Text = "Invoice #:";
            // 
            // txtinvoiceno
            // 
            this.txtinvoiceno.Location = new System.Drawing.Point(298, 141);
            this.txtinvoiceno.Margin = new System.Windows.Forms.Padding(4);
            this.txtinvoiceno.Name = "txtinvoiceno";
            this.txtinvoiceno.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12.8F);
            this.txtinvoiceno.Properties.Appearance.Options.UseFont = true;
            this.txtinvoiceno.Size = new System.Drawing.Size(506, 56);
            this.txtinvoiceno.TabIndex = 8;
            // 
            // labelControl5
            // 
            this.labelControl5.Appearance.Font = new System.Drawing.Font("Segoe UI Semibold", 12.2F, System.Drawing.FontStyle.Bold);
            this.labelControl5.Appearance.ForeColor = System.Drawing.Color.White;
            this.labelControl5.Appearance.Options.UseFont = true;
            this.labelControl5.Appearance.Options.UseForeColor = true;
            this.labelControl5.Location = new System.Drawing.Point(152, 79);
            this.labelControl5.Margin = new System.Windows.Forms.Padding(4);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(124, 45);
            this.labelControl5.TabIndex = 9;
            this.labelControl5.Text = "Order #:";
            // 
            // txtorderno
            // 
            this.txtorderno.EditValue = "000000000000000000";
            this.txtorderno.Location = new System.Drawing.Point(298, 78);
            this.txtorderno.Margin = new System.Windows.Forms.Padding(4);
            this.txtorderno.Name = "txtorderno";
            this.txtorderno.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12.8F);
            this.txtorderno.Properties.Appearance.Options.UseFont = true;
            this.txtorderno.Properties.ReadOnly = true;
            this.txtorderno.Size = new System.Drawing.Size(506, 56);
            this.txtorderno.TabIndex = 10;
            // 
            // txtdiscountamnt
            // 
            this.txtdiscountamnt.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtdiscountamnt.Location = new System.Drawing.Point(298, 266);
            this.txtdiscountamnt.Margin = new System.Windows.Forms.Padding(4);
            this.txtdiscountamnt.Name = "txtdiscountamnt";
            this.txtdiscountamnt.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12.8F);
            this.txtdiscountamnt.Properties.Appearance.Options.UseFont = true;
            this.txtdiscountamnt.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtdiscountamnt.Properties.ReadOnly = true;
            this.txtdiscountamnt.Size = new System.Drawing.Size(506, 56);
            this.txtdiscountamnt.TabIndex = 12;
            // 
            // labelControl6
            // 
            this.labelControl6.Appearance.Font = new System.Drawing.Font("Segoe UI Semibold", 12.2F, System.Drawing.FontStyle.Bold);
            this.labelControl6.Appearance.ForeColor = System.Drawing.Color.White;
            this.labelControl6.Appearance.Options.UseFont = true;
            this.labelControl6.Appearance.Options.UseForeColor = true;
            this.labelControl6.Location = new System.Drawing.Point(17, 268);
            this.labelControl6.Margin = new System.Windows.Forms.Padding(4);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(271, 45);
            this.labelControl6.TabIndex = 11;
            this.labelControl6.Text = "Discount Amount:";
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(612, 525);
            this.btnClose.Margin = new System.Windows.Forms.Padding(4);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(192, 72);
            this.btnClose.TabIndex = 13;
            this.btnClose.Text = "Close";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnsubmit
            // 
            this.btnsubmit.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnsubmit.ImageOptions.Image")));
            this.btnsubmit.Location = new System.Drawing.Point(411, 525);
            this.btnsubmit.Margin = new System.Windows.Forms.Padding(4);
            this.btnsubmit.Name = "btnsubmit";
            this.btnsubmit.Size = new System.Drawing.Size(192, 72);
            this.btnsubmit.TabIndex = 6;
            this.btnsubmit.Text = "Submit";
            this.btnsubmit.Click += new System.EventHandler(this.btnsubmit_Click);
            // 
            // POSChargeToClient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(825, 621);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.txtdiscountamnt);
            this.Controls.Add(this.labelControl6);
            this.Controls.Add(this.txtorderno);
            this.Controls.Add(this.labelControl5);
            this.Controls.Add(this.txtinvoiceno);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.btnsubmit);
            this.Controls.Add(this.txtremarks);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.txtamount);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.txtcust);
            this.Controls.Add(this.labelControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.Name = "POSChargeToClient";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Charge to Account";
            this.Load += new System.EventHandler(this.POSChargeToClient_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtcust.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtamount.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtinvoiceno.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtorderno.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtdiscountamnt.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.SearchLookUpEdit txtcust;
        private DevExpress.XtraGrid.Views.Grid.GridView searchLookUpEdit1View;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private System.Windows.Forms.RichTextBox txtremarks;
        private DevExpress.XtraEditors.SimpleButton btnsubmit;
        public DevExpress.XtraEditors.SpinEdit txtamount;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        public DevExpress.XtraEditors.TextEdit txtinvoiceno;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        public DevExpress.XtraEditors.TextEdit txtorderno;
        public DevExpress.XtraEditors.SpinEdit txtdiscountamnt;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.SimpleButton btnClose;
    }
}