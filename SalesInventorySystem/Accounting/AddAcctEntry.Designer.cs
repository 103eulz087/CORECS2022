namespace SalesInventorySystem.Accounting
{
    partial class AddAcctEntry
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
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.txtacctcode = new DevExpress.XtraEditors.TextEdit();
            this.txtaccttitle = new DevExpress.XtraEditors.TextEdit();
            this.txtdebit = new DevExpress.XtraEditors.TextEdit();
            this.txtcredit = new DevExpress.XtraEditors.TextEdit();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.txtacctcode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtaccttitle.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtdebit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtcredit.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl6
            // 
            this.labelControl6.Appearance.Font = new System.Drawing.Font("Tahoma", 10.25F);
            this.labelControl6.Location = new System.Drawing.Point(12, 12);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(88, 17);
            this.labelControl6.TabIndex = 11;
            this.labelControl6.Text = "AccountCode:";
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 10.25F);
            this.labelControl1.Location = new System.Drawing.Point(12, 42);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(80, 17);
            this.labelControl1.TabIndex = 12;
            this.labelControl1.Text = "AccountTitle:";
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 10.25F);
            this.labelControl2.Location = new System.Drawing.Point(12, 72);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(90, 17);
            this.labelControl2.TabIndex = 13;
            this.labelControl2.Text = "Debit Amount:";
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Tahoma", 10.25F);
            this.labelControl3.Location = new System.Drawing.Point(12, 102);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(94, 17);
            this.labelControl3.TabIndex = 14;
            this.labelControl3.Text = "Credit Amount:";
            // 
            // txtacctcode
            // 
            this.txtacctcode.Location = new System.Drawing.Point(116, 9);
            this.txtacctcode.Name = "txtacctcode";
            this.txtacctcode.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10.25F);
            this.txtacctcode.Properties.Appearance.Options.UseFont = true;
            this.txtacctcode.Properties.ReadOnly = true;
            this.txtacctcode.Size = new System.Drawing.Size(118, 24);
            this.txtacctcode.TabIndex = 15;
            // 
            // txtaccttitle
            // 
            this.txtaccttitle.Location = new System.Drawing.Point(116, 39);
            this.txtaccttitle.Name = "txtaccttitle";
            this.txtaccttitle.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10.25F);
            this.txtaccttitle.Properties.Appearance.Options.UseFont = true;
            this.txtaccttitle.Properties.ReadOnly = true;
            this.txtaccttitle.Size = new System.Drawing.Size(118, 24);
            this.txtaccttitle.TabIndex = 16;
            // 
            // txtdebit
            // 
            this.txtdebit.EditValue = "0";
            this.txtdebit.Location = new System.Drawing.Point(116, 69);
            this.txtdebit.Name = "txtdebit";
            this.txtdebit.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10.25F);
            this.txtdebit.Properties.Appearance.Options.UseFont = true;
            this.txtdebit.Size = new System.Drawing.Size(118, 24);
            this.txtdebit.TabIndex = 17;
            // 
            // txtcredit
            // 
            this.txtcredit.EditValue = "0";
            this.txtcredit.Location = new System.Drawing.Point(116, 99);
            this.txtcredit.Name = "txtcredit";
            this.txtcredit.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10.25F);
            this.txtcredit.Properties.Appearance.Options.UseFont = true;
            this.txtcredit.Size = new System.Drawing.Size(118, 24);
            this.txtcredit.TabIndex = 18;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(240, 10);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(30, 23);
            this.button1.TabIndex = 19;
            this.button1.Text = "...";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(116, 138);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(118, 30);
            this.button2.TabIndex = 20;
            this.button2.Text = "Submit";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // AddAcctEntry
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(283, 178);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txtcredit);
            this.Controls.Add(this.txtdebit);
            this.Controls.Add(this.txtaccttitle);
            this.Controls.Add(this.txtacctcode);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.labelControl6);
            this.Name = "AddAcctEntry";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AddAcctEntry";
            ((System.ComponentModel.ISupportInitialize)(this.txtacctcode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtaccttitle.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtdebit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtcredit.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        public DevExpress.XtraEditors.TextEdit txtacctcode;
        public DevExpress.XtraEditors.TextEdit txtaccttitle;
        public DevExpress.XtraEditors.TextEdit txtdebit;
        public DevExpress.XtraEditors.TextEdit txtcredit;
    }
}