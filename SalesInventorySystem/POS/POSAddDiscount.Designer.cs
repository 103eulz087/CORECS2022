namespace SalesInventorySystem.POS
{
    partial class POSAddDiscount
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
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.txtdiscountypes = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.searchLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.txtpercentageamount = new System.Windows.Forms.TextBox();
            this.labelControl12 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl11 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txtamnttobediscount = new System.Windows.Forms.TextBox();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.txtdiscountamount = new System.Windows.Forms.TextBox();
            this.txtcontrolno = new System.Windows.Forms.TextBox();
            this.txtname = new System.Windows.Forms.TextBox();
            this.txtvatadj = new System.Windows.Forms.TextBox();
            this.labelControl13 = new DevExpress.XtraEditors.LabelControl();
            this.txtvatexadj = new System.Windows.Forms.TextBox();
            this.txttransactionno = new System.Windows.Forms.TextBox();
            this.txtorderno = new System.Windows.Forms.TextBox();
            this.txtcashiertansno = new System.Windows.Forms.TextBox();
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.btnshowdiscounteditems = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.txtdiscountypes.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 12.25F);
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Location = new System.Drawing.Point(26, 17);
            this.labelControl2.Margin = new System.Windows.Forms.Padding(4, 8, 4, 8);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(217, 40);
            this.labelControl2.TabIndex = 15;
            this.labelControl2.Text = "Discount Type:";
            // 
            // txtdiscountypes
            // 
            this.txtdiscountypes.Location = new System.Drawing.Point(253, 14);
            this.txtdiscountypes.Margin = new System.Windows.Forms.Padding(6);
            this.txtdiscountypes.Name = "txtdiscountypes";
            this.txtdiscountypes.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.txtdiscountypes.Properties.Appearance.Options.UseFont = true;
            this.txtdiscountypes.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtdiscountypes.Properties.NullText = "";
            this.txtdiscountypes.Properties.PopupView = this.searchLookUpEdit1View;
            this.txtdiscountypes.Size = new System.Drawing.Size(511, 50);
            this.txtdiscountypes.TabIndex = 16;
            this.txtdiscountypes.EditValueChanged += new System.EventHandler(this.txtdiscountypes_EditValueChanged);
            // 
            // searchLookUpEdit1View
            // 
            this.searchLookUpEdit1View.ColumnPanelRowHeight = 0;
            this.searchLookUpEdit1View.DetailHeight = 546;
            this.searchLookUpEdit1View.FixedLineWidth = 4;
            this.searchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.searchLookUpEdit1View.FooterPanelHeight = 0;
            this.searchLookUpEdit1View.GroupRowHeight = 0;
            this.searchLookUpEdit1View.LevelIndent = 0;
            this.searchLookUpEdit1View.Name = "searchLookUpEdit1View";
            this.searchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.searchLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            this.searchLookUpEdit1View.PreviewIndent = 0;
            this.searchLookUpEdit1View.RowHeight = 0;
            this.searchLookUpEdit1View.ViewCaptionHeight = 0;
            // 
            // txtpercentageamount
            // 
            this.txtpercentageamount.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.txtpercentageamount.Location = new System.Drawing.Point(350, 247);
            this.txtpercentageamount.Margin = new System.Windows.Forms.Padding(4, 8, 4, 8);
            this.txtpercentageamount.Name = "txtpercentageamount";
            this.txtpercentageamount.Size = new System.Drawing.Size(240, 42);
            this.txtpercentageamount.TabIndex = 37;
            this.txtpercentageamount.Text = "0";
            this.txtpercentageamount.TextChanged += new System.EventHandler(this.txtpercentageamount_TextChanged);
            // 
            // labelControl12
            // 
            this.labelControl12.Appearance.Font = new System.Drawing.Font("Tahoma", 12.25F);
            this.labelControl12.Appearance.Options.UseFont = true;
            this.labelControl12.Location = new System.Drawing.Point(110, 245);
            this.labelControl12.Margin = new System.Windows.Forms.Padding(4, 8, 4, 8);
            this.labelControl12.Name = "labelControl12";
            this.labelControl12.Size = new System.Drawing.Size(214, 40);
            this.labelControl12.TabIndex = 36;
            this.labelControl12.Text = "Percentage %:";
            // 
            // labelControl11
            // 
            this.labelControl11.Appearance.Font = new System.Drawing.Font("Tahoma", 12.25F);
            this.labelControl11.Appearance.Options.UseFont = true;
            this.labelControl11.Location = new System.Drawing.Point(26, 195);
            this.labelControl11.Margin = new System.Windows.Forms.Padding(4, 8, 4, 8);
            this.labelControl11.Name = "labelControl11";
            this.labelControl11.Size = new System.Drawing.Size(299, 40);
            this.labelControl11.TabIndex = 35;
            this.labelControl11.Text = "Amount to Discount:";
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 12.25F);
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(26, 83);
            this.labelControl1.Margin = new System.Windows.Forms.Padding(4, 8, 4, 8);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(90, 40);
            this.labelControl1.TabIndex = 28;
            this.labelControl1.Text = "ID #.:";
            // 
            // txtamnttobediscount
            // 
            this.txtamnttobediscount.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.txtamnttobediscount.Enabled = false;
            this.txtamnttobediscount.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.txtamnttobediscount.Location = new System.Drawing.Point(350, 197);
            this.txtamnttobediscount.Margin = new System.Windows.Forms.Padding(4, 8, 4, 8);
            this.txtamnttobediscount.Name = "txtamnttobediscount";
            this.txtamnttobediscount.Size = new System.Drawing.Size(240, 42);
            this.txtamnttobediscount.TabIndex = 34;
            this.txtamnttobediscount.TextChanged += new System.EventHandler(this.txtamnttobediscount_TextChanged);
            // 
            // simpleButton1
            // 
            this.simpleButton1.Location = new System.Drawing.Point(597, 197);
            this.simpleButton1.Margin = new System.Windows.Forms.Padding(4, 8, 4, 8);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(264, 146);
            this.simpleButton1.TabIndex = 27;
            this.simpleButton1.Text = "SET";
            this.simpleButton1.Visible = false;
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Font = new System.Drawing.Font("Tahoma", 12.25F);
            this.labelControl4.Appearance.Options.UseFont = true;
            this.labelControl4.Location = new System.Drawing.Point(22, 297);
            this.labelControl4.Margin = new System.Windows.Forms.Padding(4, 8, 4, 8);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(295, 40);
            this.labelControl4.TabIndex = 33;
            this.labelControl4.Text = "Discounted Amount:";
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Tahoma", 12.25F);
            this.labelControl3.Appearance.Options.UseFont = true;
            this.labelControl3.Location = new System.Drawing.Point(26, 143);
            this.labelControl3.Margin = new System.Windows.Forms.Padding(4, 8, 4, 8);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(96, 40);
            this.labelControl3.TabIndex = 29;
            this.labelControl3.Text = "Name:";
            // 
            // txtdiscountamount
            // 
            this.txtdiscountamount.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.txtdiscountamount.Location = new System.Drawing.Point(350, 299);
            this.txtdiscountamount.Margin = new System.Windows.Forms.Padding(4, 8, 4, 8);
            this.txtdiscountamount.Name = "txtdiscountamount";
            this.txtdiscountamount.ReadOnly = true;
            this.txtdiscountamount.Size = new System.Drawing.Size(240, 42);
            this.txtdiscountamount.TabIndex = 32;
            // 
            // txtcontrolno
            // 
            this.txtcontrolno.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.txtcontrolno.Location = new System.Drawing.Point(253, 81);
            this.txtcontrolno.Margin = new System.Windows.Forms.Padding(4, 8, 4, 8);
            this.txtcontrolno.Name = "txtcontrolno";
            this.txtcontrolno.Size = new System.Drawing.Size(608, 42);
            this.txtcontrolno.TabIndex = 30;
            // 
            // txtname
            // 
            this.txtname.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.txtname.Location = new System.Drawing.Point(253, 139);
            this.txtname.Margin = new System.Windows.Forms.Padding(4, 8, 4, 8);
            this.txtname.Name = "txtname";
            this.txtname.Size = new System.Drawing.Size(608, 42);
            this.txtname.TabIndex = 31;
            // 
            // txtvatadj
            // 
            this.txtvatadj.Font = new System.Drawing.Font("Tahoma", 10.25F);
            this.txtvatadj.Location = new System.Drawing.Point(297, 597);
            this.txtvatadj.Margin = new System.Windows.Forms.Padding(4, 8, 4, 8);
            this.txtvatadj.Name = "txtvatadj";
            this.txtvatadj.ReadOnly = true;
            this.txtvatadj.Size = new System.Drawing.Size(188, 40);
            this.txtvatadj.TabIndex = 39;
            this.txtvatadj.Text = "0";
            // 
            // labelControl13
            // 
            this.labelControl13.Appearance.Font = new System.Drawing.Font("Tahoma", 12.25F);
            this.labelControl13.Appearance.Options.UseFont = true;
            this.labelControl13.Location = new System.Drawing.Point(29, 597);
            this.labelControl13.Margin = new System.Windows.Forms.Padding(4, 8, 4, 8);
            this.labelControl13.Name = "labelControl13";
            this.labelControl13.Size = new System.Drawing.Size(235, 40);
            this.labelControl13.TabIndex = 38;
            this.labelControl13.Text = "Vat Adjustment:";
            // 
            // txtvatexadj
            // 
            this.txtvatexadj.Font = new System.Drawing.Font("Tahoma", 10.25F);
            this.txtvatexadj.Location = new System.Drawing.Point(300, 653);
            this.txtvatexadj.Margin = new System.Windows.Forms.Padding(4, 8, 4, 8);
            this.txtvatexadj.Name = "txtvatexadj";
            this.txtvatexadj.ReadOnly = true;
            this.txtvatexadj.Size = new System.Drawing.Size(196, 40);
            this.txtvatexadj.TabIndex = 40;
            this.txtvatexadj.Text = "0";
            this.txtvatexadj.Visible = false;
            // 
            // txttransactionno
            // 
            this.txttransactionno.Font = new System.Drawing.Font("Tahoma", 10.25F);
            this.txttransactionno.Location = new System.Drawing.Point(253, 550);
            this.txttransactionno.Margin = new System.Windows.Forms.Padding(4, 8, 4, 8);
            this.txttransactionno.Name = "txttransactionno";
            this.txttransactionno.ReadOnly = true;
            this.txttransactionno.Size = new System.Drawing.Size(608, 40);
            this.txttransactionno.TabIndex = 43;
            // 
            // txtorderno
            // 
            this.txtorderno.Font = new System.Drawing.Font("Tahoma", 10.25F);
            this.txtorderno.Location = new System.Drawing.Point(253, 495);
            this.txtorderno.Margin = new System.Windows.Forms.Padding(4, 8, 4, 8);
            this.txtorderno.Name = "txtorderno";
            this.txtorderno.ReadOnly = true;
            this.txtorderno.Size = new System.Drawing.Size(608, 40);
            this.txtorderno.TabIndex = 42;
            // 
            // txtcashiertansno
            // 
            this.txtcashiertansno.Font = new System.Drawing.Font("Tahoma", 10.25F);
            this.txtcashiertansno.Location = new System.Drawing.Point(253, 437);
            this.txtcashiertansno.Margin = new System.Windows.Forms.Padding(4, 8, 4, 8);
            this.txtcashiertansno.Name = "txtcashiertansno";
            this.txtcashiertansno.ReadOnly = true;
            this.txtcashiertansno.Size = new System.Drawing.Size(608, 40);
            this.txtcashiertansno.TabIndex = 41;
            // 
            // simpleButton2
            // 
            this.simpleButton2.Appearance.Font = new System.Drawing.Font("Tahoma", 8.8F);
            this.simpleButton2.Appearance.Options.UseFont = true;
            this.simpleButton2.ImageOptions.Image = global::SalesInventorySystem.Properties.Resources.ShowProduct_16x16;
            this.simpleButton2.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.simpleButton2.Location = new System.Drawing.Point(781, 17);
            this.simpleButton2.Margin = new System.Windows.Forms.Padding(4, 8, 4, 8);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new System.Drawing.Size(80, 47);
            this.simpleButton2.TabIndex = 44;
            this.simpleButton2.Text = "...";
            // 
            // btnSave
            // 
            this.btnSave.ImageOptions.Image = global::SalesInventorySystem.Properties.Resources.Save_16x16__5_;
            this.btnSave.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btnSave.Location = new System.Drawing.Point(565, 359);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4, 8, 4, 8);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(296, 63);
            this.btnSave.TabIndex = 45;
            this.btnSave.Text = "SAVE";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnshowdiscounteditems
            // 
            this.btnshowdiscounteditems.Appearance.Font = new System.Drawing.Font("Tahoma", 8.8F);
            this.btnshowdiscounteditems.Appearance.Options.UseFont = true;
            this.btnshowdiscounteditems.ImageOptions.Image = global::SalesInventorySystem.Properties.Resources.ShowProduct_16x16;
            this.btnshowdiscounteditems.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btnshowdiscounteditems.Location = new System.Drawing.Point(221, 359);
            this.btnshowdiscounteditems.Margin = new System.Windows.Forms.Padding(4, 8, 4, 8);
            this.btnshowdiscounteditems.Name = "btnshowdiscounteditems";
            this.btnshowdiscounteditems.Size = new System.Drawing.Size(336, 63);
            this.btnshowdiscounteditems.TabIndex = 46;
            this.btnshowdiscounteditems.Text = "Show Discounted Items";
            this.btnshowdiscounteditems.Visible = false;
            this.btnshowdiscounteditems.Click += new System.EventHandler(this.btnshowdiscounteditems_Click);
            // 
            // POSAddDiscount
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(905, 444);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnshowdiscounteditems);
            this.Controls.Add(this.simpleButton2);
            this.Controls.Add(this.txtvatadj);
            this.Controls.Add(this.labelControl13);
            this.Controls.Add(this.txtvatexadj);
            this.Controls.Add(this.txttransactionno);
            this.Controls.Add(this.txtorderno);
            this.Controls.Add(this.txtcashiertansno);
            this.Controls.Add(this.txtpercentageamount);
            this.Controls.Add(this.labelControl12);
            this.Controls.Add(this.labelControl11);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.txtamnttobediscount);
            this.Controls.Add(this.simpleButton1);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.txtdiscountamount);
            this.Controls.Add(this.txtcontrolno);
            this.Controls.Add(this.txtname);
            this.Controls.Add(this.txtdiscountypes);
            this.Controls.Add(this.labelControl2);
            this.Name = "POSAddDiscount";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "POSAddDiscount";
            this.Load += new System.EventHandler(this.POSAddDiscount_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtdiscountypes.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.SearchLookUpEdit txtdiscountypes;
        private DevExpress.XtraGrid.Views.Grid.GridView searchLookUpEdit1View;
        public System.Windows.Forms.TextBox txtpercentageamount;
        private DevExpress.XtraEditors.LabelControl labelControl12;
        private DevExpress.XtraEditors.LabelControl labelControl11;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        public System.Windows.Forms.TextBox txtamnttobediscount;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        public System.Windows.Forms.TextBox txtdiscountamount;
        public System.Windows.Forms.TextBox txtcontrolno;
        public System.Windows.Forms.TextBox txtname;
        public System.Windows.Forms.TextBox txtvatadj;
        private DevExpress.XtraEditors.LabelControl labelControl13;
        public System.Windows.Forms.TextBox txtvatexadj;
        public System.Windows.Forms.TextBox txttransactionno;
        public System.Windows.Forms.TextBox txtorderno;
        public System.Windows.Forms.TextBox txtcashiertansno;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.SimpleButton btnshowdiscounteditems;
    }
}