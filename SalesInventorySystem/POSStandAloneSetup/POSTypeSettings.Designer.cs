namespace SalesInventorySystem.POSStandAloneSetup
{
    partial class POSTypeSettings
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
            this.chckisretail = new System.Windows.Forms.CheckBox();
            this.chckisenableprinting = new System.Windows.Forms.CheckBox();
            this.btnupdate = new DevExpress.XtraEditors.SimpleButton();
            this.chckuploadpershifting = new System.Windows.Forms.CheckBox();
            this.chcksendemail = new System.Windows.Forms.CheckBox();
            this.chckinvoicelapsing = new System.Windows.Forms.CheckBox();
            this.chckisdatauploading = new System.Windows.Forms.CheckBox();
            this.chckislinkedserver = new System.Windows.Forms.CheckBox();
            this.chckcreditlimit = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtpostype = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtmachinename = new System.Windows.Forms.TextBox();
            this.txtlinkedservername = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.chckeodnotification = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtcashbegin = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // chckisretail
            // 
            this.chckisretail.AutoSize = true;
            this.chckisretail.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chckisretail.Location = new System.Drawing.Point(262, 178);
            this.chckisretail.Margin = new System.Windows.Forms.Padding(7, 7, 7, 7);
            this.chckisretail.Name = "chckisretail";
            this.chckisretail.Size = new System.Drawing.Size(142, 40);
            this.chckisretail.TabIndex = 453;
            this.chckisretail.Text = "isRetail";
            this.chckisretail.UseVisualStyleBackColor = true;
            // 
            // chckisenableprinting
            // 
            this.chckisenableprinting.AutoSize = true;
            this.chckisenableprinting.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chckisenableprinting.Location = new System.Drawing.Point(262, 245);
            this.chckisenableprinting.Margin = new System.Windows.Forms.Padding(7, 7, 7, 7);
            this.chckisenableprinting.Name = "chckisenableprinting";
            this.chckisenableprinting.Size = new System.Drawing.Size(260, 40);
            this.chckisenableprinting.TabIndex = 454;
            this.chckisenableprinting.Text = "isEnablePrinting";
            this.chckisenableprinting.UseVisualStyleBackColor = true;
            // 
            // btnupdate
            // 
            this.btnupdate.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.btnupdate.Appearance.Options.UseFont = true;
            this.btnupdate.Location = new System.Drawing.Point(329, 607);
            this.btnupdate.Margin = new System.Windows.Forms.Padding(7, 7, 7, 7);
            this.btnupdate.Name = "btnupdate";
            this.btnupdate.Size = new System.Drawing.Size(578, 105);
            this.btnupdate.TabIndex = 455;
            this.btnupdate.Text = "Update";
            this.btnupdate.Click += new System.EventHandler(this.btnupdate_Click);
            // 
            // chckuploadpershifting
            // 
            this.chckuploadpershifting.AutoSize = true;
            this.chckuploadpershifting.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chckuploadpershifting.Location = new System.Drawing.Point(551, 245);
            this.chckuploadpershifting.Margin = new System.Windows.Forms.Padding(7, 7, 7, 7);
            this.chckuploadpershifting.Name = "chckuploadpershifting";
            this.chckuploadpershifting.Size = new System.Drawing.Size(304, 40);
            this.chckuploadpershifting.TabIndex = 457;
            this.chckuploadpershifting.Text = "Upload Per Shifting";
            this.chckuploadpershifting.UseVisualStyleBackColor = true;
            // 
            // chcksendemail
            // 
            this.chcksendemail.AutoSize = true;
            this.chcksendemail.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chcksendemail.Location = new System.Drawing.Point(551, 178);
            this.chcksendemail.Margin = new System.Windows.Forms.Padding(7, 7, 7, 7);
            this.chcksendemail.Name = "chcksendemail";
            this.chcksendemail.Size = new System.Drawing.Size(354, 40);
            this.chcksendemail.TabIndex = 456;
            this.chcksendemail.Text = "Send Email Notification";
            this.chcksendemail.UseVisualStyleBackColor = true;
            // 
            // chckinvoicelapsing
            // 
            this.chckinvoicelapsing.AutoSize = true;
            this.chckinvoicelapsing.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chckinvoicelapsing.Location = new System.Drawing.Point(551, 304);
            this.chckinvoicelapsing.Margin = new System.Windows.Forms.Padding(7, 7, 7, 7);
            this.chckinvoicelapsing.Name = "chckinvoicelapsing";
            this.chckinvoicelapsing.Size = new System.Drawing.Size(333, 40);
            this.chckinvoicelapsing.TabIndex = 459;
            this.chckinvoicelapsing.Text = "Invoice Lapsing Term";
            this.chckinvoicelapsing.UseVisualStyleBackColor = true;
            // 
            // chckisdatauploading
            // 
            this.chckisdatauploading.AutoSize = true;
            this.chckisdatauploading.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chckisdatauploading.Location = new System.Drawing.Point(262, 304);
            this.chckisdatauploading.Margin = new System.Windows.Forms.Padding(7, 7, 7, 7);
            this.chckisdatauploading.Name = "chckisdatauploading";
            this.chckisdatauploading.Size = new System.Drawing.Size(252, 40);
            this.chckisdatauploading.TabIndex = 458;
            this.chckisdatauploading.Text = "Data Uploading";
            this.chckisdatauploading.UseVisualStyleBackColor = true;
            // 
            // chckislinkedserver
            // 
            this.chckislinkedserver.AutoSize = true;
            this.chckislinkedserver.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chckislinkedserver.Location = new System.Drawing.Point(262, 364);
            this.chckislinkedserver.Margin = new System.Windows.Forms.Padding(7, 7, 7, 7);
            this.chckislinkedserver.Name = "chckislinkedserver";
            this.chckislinkedserver.Size = new System.Drawing.Size(249, 40);
            this.chckislinkedserver.TabIndex = 461;
            this.chckislinkedserver.Text = "isLinked Server";
            this.chckislinkedserver.UseVisualStyleBackColor = true;
            // 
            // chckcreditlimit
            // 
            this.chckcreditlimit.AutoSize = true;
            this.chckcreditlimit.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chckcreditlimit.Location = new System.Drawing.Point(551, 364);
            this.chckcreditlimit.Margin = new System.Windows.Forms.Padding(7, 7, 7, 7);
            this.chckcreditlimit.Name = "chckcreditlimit";
            this.chckcreditlimit.Size = new System.Drawing.Size(298, 40);
            this.chckcreditlimit.TabIndex = 460;
            this.chckcreditlimit.Text = "Enable Credit Limit";
            this.chckcreditlimit.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.label1.Location = new System.Drawing.Point(33, 38);
            this.label1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(156, 36);
            this.label1.TabIndex = 462;
            this.label1.Text = "POS Type:";
            // 
            // txtpostype
            // 
            this.txtpostype.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.txtpostype.FormattingEnabled = true;
            this.txtpostype.Items.AddRange(new object[] {
            "1",
            "2",
            "3"});
            this.txtpostype.Location = new System.Drawing.Point(262, 38);
            this.txtpostype.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.txtpostype.Name = "txtpostype";
            this.txtpostype.Size = new System.Drawing.Size(314, 43);
            this.txtpostype.TabIndex = 463;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.label2.Location = new System.Drawing.Point(33, 100);
            this.label2.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(222, 36);
            this.label2.TabIndex = 464;
            this.label2.Text = "Machine Name:";
            // 
            // txtmachinename
            // 
            this.txtmachinename.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.txtmachinename.Location = new System.Drawing.Point(262, 100);
            this.txtmachinename.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.txtmachinename.Name = "txtmachinename";
            this.txtmachinename.Size = new System.Drawing.Size(641, 43);
            this.txtmachinename.TabIndex = 465;
            // 
            // txtlinkedservername
            // 
            this.txtlinkedservername.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.txtlinkedservername.Location = new System.Drawing.Point(329, 486);
            this.txtlinkedservername.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.txtlinkedservername.Name = "txtlinkedservername";
            this.txtlinkedservername.Size = new System.Drawing.Size(574, 43);
            this.txtlinkedservername.TabIndex = 467;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.label3.Location = new System.Drawing.Point(33, 491);
            this.label3.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(294, 36);
            this.label3.TabIndex = 466;
            this.label3.Text = "Linked Server Name:";
            // 
            // chckeodnotification
            // 
            this.chckeodnotification.AutoSize = true;
            this.chckeodnotification.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chckeodnotification.Location = new System.Drawing.Point(262, 424);
            this.chckeodnotification.Margin = new System.Windows.Forms.Padding(7, 7, 7, 7);
            this.chckeodnotification.Name = "chckeodnotification";
            this.chckeodnotification.Size = new System.Drawing.Size(345, 40);
            this.chckeodnotification.TabIndex = 468;
            this.chckeodnotification.Text = "EOD Email Notification";
            this.chckeodnotification.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.label4.Location = new System.Drawing.Point(33, 551);
            this.label4.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(174, 36);
            this.label4.TabIndex = 469;
            this.label4.Text = "Cash Begin:";
            // 
            // txtcashbegin
            // 
            this.txtcashbegin.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.txtcashbegin.Location = new System.Drawing.Point(329, 546);
            this.txtcashbegin.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.txtcashbegin.Name = "txtcashbegin";
            this.txtcashbegin.Size = new System.Drawing.Size(316, 43);
            this.txtcashbegin.TabIndex = 470;
            this.txtcashbegin.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtcashbegin_KeyPress);
            // 
            // POSTypeSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(14F, 29F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(940, 741);
            this.Controls.Add(this.txtcashbegin);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.chckeodnotification);
            this.Controls.Add(this.txtlinkedservername);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtmachinename);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtpostype);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.chckislinkedserver);
            this.Controls.Add(this.chckcreditlimit);
            this.Controls.Add(this.chckinvoicelapsing);
            this.Controls.Add(this.chckisdatauploading);
            this.Controls.Add(this.chckuploadpershifting);
            this.Controls.Add(this.chcksendemail);
            this.Controls.Add(this.btnupdate);
            this.Controls.Add(this.chckisenableprinting);
            this.Controls.Add(this.chckisretail);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "POSTypeSettings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "POSTypeSettings";
            this.Load += new System.EventHandler(this.POSTypeSettings_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chckisretail;
        private System.Windows.Forms.CheckBox chckisenableprinting;
        private DevExpress.XtraEditors.SimpleButton btnupdate;
        private System.Windows.Forms.CheckBox chckuploadpershifting;
        private System.Windows.Forms.CheckBox chcksendemail;
        private System.Windows.Forms.CheckBox chckinvoicelapsing;
        private System.Windows.Forms.CheckBox chckisdatauploading;
        private System.Windows.Forms.CheckBox chckislinkedserver;
        private System.Windows.Forms.CheckBox chckcreditlimit;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox txtpostype;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtmachinename;
        private System.Windows.Forms.TextBox txtlinkedservername;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox chckeodnotification;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtcashbegin;
    }
}