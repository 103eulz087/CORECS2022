﻿namespace SalesInventorySystem.POS
{
    partial class POSPaymentDetails
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
            this.groupCreditCardDetails = new System.Windows.Forms.GroupBox();
            this.txtccbank = new System.Windows.Forms.ComboBox();
            this.cmdConfirm = new System.Windows.Forms.Button();
            this.txtcardtype = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.txtccrefno = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtccname = new System.Windows.Forms.TextBox();
            this.txtccnumber = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtexpirydate = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtccmerchant = new System.Windows.Forms.ComboBox();
            this.groupCheque = new System.Windows.Forms.GroupBox();
            this.txtcheckbankname = new System.Windows.Forms.TextBox();
            this.txtcheckname = new System.Windows.Forms.TextBox();
            this.txtchecknum = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.groupCreditCardDetails.SuspendLayout();
            this.groupCheque.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupCreditCardDetails
            // 
            this.groupCreditCardDetails.Controls.Add(this.txtccbank);
            this.groupCreditCardDetails.Controls.Add(this.cmdConfirm);
            this.groupCreditCardDetails.Controls.Add(this.txtcardtype);
            this.groupCreditCardDetails.Controls.Add(this.label13);
            this.groupCreditCardDetails.Controls.Add(this.label12);
            this.groupCreditCardDetails.Controls.Add(this.label11);
            this.groupCreditCardDetails.Controls.Add(this.label10);
            this.groupCreditCardDetails.Controls.Add(this.txtccrefno);
            this.groupCreditCardDetails.Controls.Add(this.label4);
            this.groupCreditCardDetails.Controls.Add(this.txtccname);
            this.groupCreditCardDetails.Controls.Add(this.txtccnumber);
            this.groupCreditCardDetails.Controls.Add(this.label3);
            this.groupCreditCardDetails.Controls.Add(this.label2);
            this.groupCreditCardDetails.Controls.Add(this.label1);
            this.groupCreditCardDetails.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F);
            this.groupCreditCardDetails.Location = new System.Drawing.Point(22, 22);
            this.groupCreditCardDetails.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.groupCreditCardDetails.Name = "groupCreditCardDetails";
            this.groupCreditCardDetails.Padding = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.groupCreditCardDetails.Size = new System.Drawing.Size(667, 386);
            this.groupCreditCardDetails.TabIndex = 0;
            this.groupCreditCardDetails.TabStop = false;
            this.groupCreditCardDetails.Visible = false;
            this.groupCreditCardDetails.Enter += new System.EventHandler(this.groupCreditCardDetails_Enter);
            // 
            // txtccbank
            // 
            this.txtccbank.FormattingEnabled = true;
            this.txtccbank.Items.AddRange(new object[] {
            "Mastercard",
            "Visa",
            "AMEX",
            "JCB",
            "BancNet",
            "Paymaya QR"});
            this.txtccbank.Location = new System.Drawing.Point(236, 92);
            this.txtccbank.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.txtccbank.Name = "txtccbank";
            this.txtccbank.Size = new System.Drawing.Size(374, 34);
            this.txtccbank.TabIndex = 16;
            // 
            // cmdConfirm
            // 
            this.cmdConfirm.BackColor = System.Drawing.Color.White;
            this.cmdConfirm.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdConfirm.ForeColor = System.Drawing.Color.Black;
            this.cmdConfirm.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.cmdConfirm.Location = new System.Drawing.Point(37, 298);
            this.cmdConfirm.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.cmdConfirm.Name = "cmdConfirm";
            this.cmdConfirm.Size = new System.Drawing.Size(576, 62);
            this.cmdConfirm.TabIndex = 123548;
            this.cmdConfirm.Text = "Submit";
            this.cmdConfirm.UseVisualStyleBackColor = false;
            this.cmdConfirm.Click += new System.EventHandler(this.cmdConfirm_Click);
            // 
            // txtcardtype
            // 
            this.txtcardtype.FormattingEnabled = true;
            this.txtcardtype.Items.AddRange(new object[] {
            "Mastercard",
            "Visa",
            "AMEX",
            "JCB",
            "BancNet",
            "Paymaya QR",
            "GiftCert"});
            this.txtcardtype.Location = new System.Drawing.Point(236, 248);
            this.txtcardtype.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.txtcardtype.Name = "txtcardtype";
            this.txtcardtype.Size = new System.Drawing.Size(374, 34);
            this.txtcardtype.TabIndex = 15;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Arial Black", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.White;
            this.label13.Location = new System.Drawing.Point(37, 250);
            this.label13.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(137, 30);
            this.label13.TabIndex = 17;
            this.label13.Text = "Card Type:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Arial Black", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.White;
            this.label12.Location = new System.Drawing.Point(37, 200);
            this.label12.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(95, 30);
            this.label12.TabIndex = 16;
            this.label12.Text = "Card #:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Arial Black", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.White;
            this.label11.Location = new System.Drawing.Point(37, 147);
            this.label11.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(148, 30);
            this.label11.TabIndex = 15;
            this.label11.Text = "Card Name:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(48, 416);
            this.label10.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(118, 26);
            this.label10.TabIndex = 14;
            this.label10.Text = "Card Type:";
            this.label10.Visible = false;
            // 
            // txtccrefno
            // 
            this.txtccrefno.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.txtccrefno.Location = new System.Drawing.Point(236, 39);
            this.txtccrefno.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.txtccrefno.Name = "txtccrefno";
            this.txtccrefno.Size = new System.Drawing.Size(374, 35);
            this.txtccrefno.TabIndex = 11;
            this.txtccrefno.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtccrefno_KeyDown);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Arial Black", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(37, 46);
            this.label4.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(182, 30);
            this.label4.TabIndex = 10;
            this.label4.Text = "Reference No.:";
            // 
            // txtccname
            // 
            this.txtccname.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.txtccname.Location = new System.Drawing.Point(238, 144);
            this.txtccname.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.txtccname.Name = "txtccname";
            this.txtccname.Size = new System.Drawing.Size(374, 35);
            this.txtccname.TabIndex = 5;
            // 
            // txtccnumber
            // 
            this.txtccnumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.txtccnumber.Location = new System.Drawing.Point(236, 196);
            this.txtccnumber.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.txtccnumber.Name = "txtccnumber";
            this.txtccnumber.Size = new System.Drawing.Size(374, 35);
            this.txtccnumber.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial Black", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(37, 98);
            this.label3.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(139, 30);
            this.label3.TabIndex = 2;
            this.label3.Text = "Card Bank:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(48, 508);
            this.label2.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(130, 26);
            this.label2.TabIndex = 1;
            this.label2.Text = "Card Name:";
            this.label2.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(48, 464);
            this.label1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(149, 26);
            this.label1.TabIndex = 0;
            this.label1.Text = "Card Number:";
            this.label1.Visible = false;
            // 
            // txtexpirydate
            // 
            this.txtexpirydate.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.txtexpirydate.Location = new System.Drawing.Point(600, 573);
            this.txtexpirydate.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.txtexpirydate.Name = "txtexpirydate";
            this.txtexpirydate.Size = new System.Drawing.Size(333, 35);
            this.txtexpirydate.TabIndex = 13;
            this.txtexpirydate.Visible = false;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Arial Black", 9F, System.Drawing.FontStyle.Bold);
            this.label9.ForeColor = System.Drawing.Color.White;
            this.label9.Location = new System.Drawing.Point(1027, 531);
            this.label9.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(248, 30);
            this.label9.TabIndex = 12;
            this.label9.Text = "Expiry Date (MMYY):";
            this.label9.Visible = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Arial Black", 9F, System.Drawing.FontStyle.Bold);
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(1027, 580);
            this.label5.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(193, 30);
            this.label5.TabIndex = 8;
            this.label5.Text = "Bank Merchant:";
            this.label5.Visible = false;
            // 
            // txtccmerchant
            // 
            this.txtccmerchant.FormattingEnabled = true;
            this.txtccmerchant.Location = new System.Drawing.Point(979, 462);
            this.txtccmerchant.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.txtccmerchant.Name = "txtccmerchant";
            this.txtccmerchant.Size = new System.Drawing.Size(374, 32);
            this.txtccmerchant.TabIndex = 9;
            this.txtccmerchant.Text = "Paymaya";
            this.txtccmerchant.Visible = false;
            // 
            // groupCheque
            // 
            this.groupCheque.Controls.Add(this.txtcheckbankname);
            this.groupCheque.Controls.Add(this.txtcheckname);
            this.groupCheque.Controls.Add(this.txtchecknum);
            this.groupCheque.Controls.Add(this.label6);
            this.groupCheque.Controls.Add(this.label7);
            this.groupCheque.Controls.Add(this.label8);
            this.groupCheque.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F);
            this.groupCheque.Location = new System.Drawing.Point(742, 22);
            this.groupCheque.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.groupCheque.Name = "groupCheque";
            this.groupCheque.Padding = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.groupCheque.Size = new System.Drawing.Size(667, 354);
            this.groupCheque.TabIndex = 1;
            this.groupCheque.TabStop = false;
            this.groupCheque.Text = "Cheque Payment Details";
            this.groupCheque.Visible = false;
            // 
            // txtcheckbankname
            // 
            this.txtcheckbankname.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.txtcheckbankname.Location = new System.Drawing.Point(236, 218);
            this.txtcheckbankname.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.txtcheckbankname.Name = "txtcheckbankname";
            this.txtcheckbankname.Size = new System.Drawing.Size(374, 35);
            this.txtcheckbankname.TabIndex = 6;
            // 
            // txtcheckname
            // 
            this.txtcheckname.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.txtcheckname.Location = new System.Drawing.Point(236, 142);
            this.txtcheckname.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.txtcheckname.Name = "txtcheckname";
            this.txtcheckname.Size = new System.Drawing.Size(374, 35);
            this.txtcheckname.TabIndex = 5;
            // 
            // txtchecknum
            // 
            this.txtchecknum.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.txtchecknum.Location = new System.Drawing.Point(236, 66);
            this.txtchecknum.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.txtchecknum.Name = "txtchecknum";
            this.txtchecknum.Size = new System.Drawing.Size(374, 35);
            this.txtchecknum.TabIndex = 4;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(48, 225);
            this.label6.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(133, 26);
            this.label6.TabIndex = 2;
            this.label6.Text = "Bank Name:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(48, 150);
            this.label7.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(145, 26);
            this.label7.TabIndex = 1;
            this.label7.Text = "Check Name:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(48, 74);
            this.label8.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(164, 26);
            this.label8.TabIndex = 0;
            this.label8.Text = "Check Number:";
            // 
            // POSPaymentDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(708, 440);
            this.Controls.Add(this.txtexpirydate);
            this.Controls.Add(this.groupCheque);
            this.Controls.Add(this.groupCreditCardDetails);
            this.Controls.Add(this.txtccmerchant);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label5);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.Name = "POSPaymentDetails";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "POSPaymentDetails";
            this.Load += new System.EventHandler(this.POSPaymentDetails_Load);
            this.groupCreditCardDetails.ResumeLayout(false);
            this.groupCreditCardDetails.PerformLayout();
            this.groupCheque.ResumeLayout(false);
            this.groupCheque.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox txtccname;
        private System.Windows.Forms.TextBox txtccnumber;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtcheckbankname;
        private System.Windows.Forms.TextBox txtcheckname;
        private System.Windows.Forms.TextBox txtchecknum;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        internal System.Windows.Forms.Button cmdConfirm;
        public System.Windows.Forms.GroupBox groupCheque;
        public System.Windows.Forms.GroupBox groupCreditCardDetails;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox txtccmerchant;
        private System.Windows.Forms.TextBox txtccrefno;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtexpirydate;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox txtcardtype;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ComboBox txtccbank;
    }
}