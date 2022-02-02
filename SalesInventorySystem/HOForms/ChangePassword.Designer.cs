namespace SalesInventorySystem.HOForms
{
    partial class ChangePassword
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
            this.txtnewpass = new DevExpress.XtraEditors.TextEdit();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtconfirmnewpass = new DevExpress.XtraEditors.TextEdit();
            this.simpleButton4 = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.txtnewpass.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtconfirmnewpass.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // txtnewpass
            // 
            this.txtnewpass.Location = new System.Drawing.Point(412, 27);
            this.txtnewpass.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.txtnewpass.Name = "txtnewpass";
            this.txtnewpass.Properties.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtnewpass.Properties.Appearance.Options.UseFont = true;
            this.txtnewpass.Properties.MaxLength = 35;
            this.txtnewpass.Properties.PasswordChar = '*';
            this.txtnewpass.Size = new System.Drawing.Size(409, 50);
            this.txtnewpass.TabIndex = 111;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(39, 33);
            this.label2.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(249, 36);
            this.label2.TabIndex = 110;
            this.label2.Text = "New Password:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(39, 96);
            this.label1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(378, 36);
            this.label1.TabIndex = 112;
            this.label1.Text = "Confirm New Password:";
            // 
            // txtconfirmnewpass
            // 
            this.txtconfirmnewpass.Location = new System.Drawing.Point(412, 89);
            this.txtconfirmnewpass.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.txtconfirmnewpass.Name = "txtconfirmnewpass";
            this.txtconfirmnewpass.Properties.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtconfirmnewpass.Properties.Appearance.Options.UseFont = true;
            this.txtconfirmnewpass.Properties.MaxLength = 35;
            this.txtconfirmnewpass.Properties.PasswordChar = '*';
            this.txtconfirmnewpass.Size = new System.Drawing.Size(409, 50);
            this.txtconfirmnewpass.TabIndex = 113;
            // 
            // simpleButton4
            // 
            this.simpleButton4.Appearance.BackColor = System.Drawing.Color.RoyalBlue;
            this.simpleButton4.Appearance.ForeColor = System.Drawing.Color.White;
            this.simpleButton4.Appearance.Options.UseBackColor = true;
            this.simpleButton4.Appearance.Options.UseForeColor = true;
            this.simpleButton4.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Flat;
            this.simpleButton4.Location = new System.Drawing.Point(45, 152);
            this.simpleButton4.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.simpleButton4.Name = "simpleButton4";
            this.simpleButton4.Size = new System.Drawing.Size(776, 91);
            this.simpleButton4.TabIndex = 114;
            this.simpleButton4.Text = "Update";
            this.simpleButton4.Click += new System.EventHandler(this.simpleButton4_Click);
            // 
            // ChangePassword
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 29F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(890, 272);
            this.Controls.Add(this.simpleButton4);
            this.Controls.Add(this.txtconfirmnewpass);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtnewpass);
            this.Controls.Add(this.label2);
            this.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.Name = "ChangePassword";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ChangePassword";
            ((System.ComponentModel.ISupportInitialize)(this.txtnewpass.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtconfirmnewpass.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.TextEdit txtnewpass;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.TextEdit txtconfirmnewpass;
        private DevExpress.XtraEditors.SimpleButton simpleButton4;
    }
}