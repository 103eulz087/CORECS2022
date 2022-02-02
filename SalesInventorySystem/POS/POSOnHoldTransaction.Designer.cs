namespace SalesInventorySystem
{
    partial class POSOnHoldTransaction
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
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.txtcustname = new DevExpress.XtraEditors.TextEdit();
            this.btnonhold = new DevExpress.XtraEditors.SimpleButton();
            this.txtadvancepayment = new DevExpress.XtraEditors.SpinEdit();
            this.lblrefno = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.lbltranscode = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.txtcustname.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtadvancepayment.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Location = new System.Drawing.Point(14, 15);
            this.labelControl2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(231, 24);
            this.labelControl2.TabIndex = 1;
            this.labelControl2.Text = "Transaction Reference No.:";
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(14, 84);
            this.labelControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(142, 24);
            this.labelControl1.TabIndex = 2;
            this.labelControl1.Text = "Customer Name:";
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.labelControl3.Appearance.Options.UseFont = true;
            this.labelControl3.Location = new System.Drawing.Point(14, 122);
            this.labelControl3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(158, 24);
            this.labelControl3.TabIndex = 3;
            this.labelControl3.Text = "Advance Payment:";
            this.labelControl3.Visible = false;
            // 
            // txtcustname
            // 
            this.txtcustname.Location = new System.Drawing.Point(190, 79);
            this.txtcustname.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtcustname.Name = "txtcustname";
            this.txtcustname.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12.25F);
            this.txtcustname.Properties.Appearance.Options.UseFont = true;
            this.txtcustname.Size = new System.Drawing.Size(266, 30);
            this.txtcustname.TabIndex = 5;
            // 
            // btnonhold
            // 
            this.btnonhold.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.btnonhold.Appearance.Options.UseFont = true;
            this.btnonhold.Location = new System.Drawing.Point(190, 155);
            this.btnonhold.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnonhold.Name = "btnonhold";
            this.btnonhold.Size = new System.Drawing.Size(268, 52);
            this.btnonhold.TabIndex = 6;
            this.btnonhold.Text = "Confirm On-Hold Transaction";
            this.btnonhold.Click += new System.EventHandler(this.btnonhold_Click);
            // 
            // txtadvancepayment
            // 
            this.txtadvancepayment.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtadvancepayment.Location = new System.Drawing.Point(190, 117);
            this.txtadvancepayment.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtadvancepayment.Name = "txtadvancepayment";
            this.txtadvancepayment.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12.25F);
            this.txtadvancepayment.Properties.Appearance.Options.UseFont = true;
            this.txtadvancepayment.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtadvancepayment.Size = new System.Drawing.Size(266, 30);
            this.txtadvancepayment.TabIndex = 7;
            this.txtadvancepayment.Visible = false;
            // 
            // lblrefno
            // 
            this.lblrefno.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.lblrefno.Appearance.Options.UseFont = true;
            this.lblrefno.Location = new System.Drawing.Point(89, 47);
            this.lblrefno.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lblrefno.Name = "lblrefno";
            this.lblrefno.Size = new System.Drawing.Size(180, 24);
            this.lblrefno.TabIndex = 8;
            this.lblrefno.Text = "##################";
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.labelControl4.Appearance.Options.UseFont = true;
            this.labelControl4.Location = new System.Drawing.Point(14, 47);
            this.labelControl4.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(69, 24);
            this.labelControl4.TabIndex = 9;
            this.labelControl4.Text = "Order #:";
            // 
            // lbltranscode
            // 
            this.lbltranscode.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.lbltranscode.Appearance.Options.UseFont = true;
            this.lbltranscode.Location = new System.Drawing.Point(251, 15);
            this.lbltranscode.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lbltranscode.Name = "lbltranscode";
            this.lbltranscode.Size = new System.Drawing.Size(60, 24);
            this.lbltranscode.TabIndex = 10;
            this.lbltranscode.Text = "######";
            // 
            // POSOnHoldTransaction
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(488, 234);
            this.Controls.Add(this.lbltranscode);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.lblrefno);
            this.Controls.Add(this.txtadvancepayment);
            this.Controls.Add(this.btnonhold);
            this.Controls.Add(this.txtcustname);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.labelControl2);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "POSOnHoldTransaction";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "POSOnHoldTransaction";
            this.Load += new System.EventHandler(this.POSOnHoldTransaction_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtcustname.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtadvancepayment.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.TextEdit txtcustname;
        private DevExpress.XtraEditors.SimpleButton btnonhold;
        private DevExpress.XtraEditors.SpinEdit txtadvancepayment;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        public DevExpress.XtraEditors.LabelControl lblrefno;
        public DevExpress.XtraEditors.LabelControl lbltranscode;
    }
}