﻿namespace SalesInventorySystem.Reporting
{
    partial class SalesInvoiceDexEx
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
            this.gridControl4 = new DevExpress.XtraGrid.GridControl();
            this.gridView4 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtdiscount = new System.Windows.Forms.TextBox();
            this.txtcusttin = new System.Windows.Forms.TextBox();
            this.txtcustkey = new System.Windows.Forms.TextBox();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.txtterm = new System.Windows.Forms.TextBox();
            this.txtcustaddress = new System.Windows.Forms.TextBox();
            this.txtcustname = new System.Windows.Forms.TextBox();
            this.txttotalamountdue = new System.Windows.Forms.TextBox();
            this.txtaddvat = new System.Windows.Forms.TextBox();
            this.txtamountdue = new System.Windows.Forms.TextBox();
            this.txtamountnetofvat = new System.Windows.Forms.TextBox();
            this.txtlessvat = new System.Windows.Forms.TextBox();
            this.txttotalsales = new System.Windows.Forms.TextBox();
            this.txtvatamount = new System.Windows.Forms.TextBox();
            this.txtvatexemptsale = new System.Windows.Forms.TextBox();
            this.txtvatablesale = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtpono = new System.Windows.Forms.TextBox();
            this.txtzeroratedsale = new System.Windows.Forms.TextBox();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView4)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.gridControl4);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 57);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1167, 551);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            // 
            // gridControl4
            // 
            this.gridControl4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl4.Location = new System.Drawing.Point(3, 17);
            this.gridControl4.MainView = this.gridView4;
            this.gridControl4.Name = "gridControl4";
            this.gridControl4.Size = new System.Drawing.Size(1161, 531);
            this.gridControl4.TabIndex = 4;
            this.gridControl4.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView4});
            // 
            // gridView4
            // 
            this.gridView4.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridView4.Appearance.HeaderPanel.Options.UseFont = true;
            this.gridView4.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridView4.Appearance.Row.Options.UseFont = true;
            this.gridView4.AppearancePrint.FooterPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.gridView4.AppearancePrint.FooterPanel.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridView4.AppearancePrint.FooterPanel.Options.UseBackColor = true;
            this.gridView4.AppearancePrint.FooterPanel.Options.UseFont = true;
            this.gridView4.GridControl = this.gridControl4;
            this.gridView4.Name = "gridView4";
            this.gridView4.OptionsPrint.PrintFooter = false;
            this.gridView4.OptionsPrint.PrintGroupFooter = false;
            this.gridView4.OptionsPrint.PrintHeader = false;
            this.gridView4.OptionsPrint.PrintHorzLines = false;
            this.gridView4.OptionsPrint.PrintVertLines = false;
            this.gridView4.OptionsView.ColumnAutoWidth = false;
            this.gridView4.OptionsView.RowAutoHeight = true;
            this.gridView4.OptionsView.ShowColumnHeaders = false;
            this.gridView4.OptionsView.ShowFooter = true;
            this.gridView4.OptionsView.ShowGroupPanel = false;
            this.gridView4.OptionsView.ShowHorizontalLines = DevExpress.Utils.DefaultBoolean.False;
            this.gridView4.OptionsView.ShowVerticalLines = DevExpress.Utils.DefaultBoolean.False;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtzeroratedsale);
            this.groupBox1.Controls.Add(this.txtdiscount);
            this.groupBox1.Controls.Add(this.txtcusttin);
            this.groupBox1.Controls.Add(this.txtcustkey);
            this.groupBox1.Controls.Add(this.simpleButton1);
            this.groupBox1.Controls.Add(this.txtterm);
            this.groupBox1.Controls.Add(this.txtcustaddress);
            this.groupBox1.Controls.Add(this.txtcustname);
            this.groupBox1.Controls.Add(this.txttotalamountdue);
            this.groupBox1.Controls.Add(this.txtaddvat);
            this.groupBox1.Controls.Add(this.txtamountdue);
            this.groupBox1.Controls.Add(this.txtamountnetofvat);
            this.groupBox1.Controls.Add(this.txtlessvat);
            this.groupBox1.Controls.Add(this.txttotalsales);
            this.groupBox1.Controls.Add(this.txtvatamount);
            this.groupBox1.Controls.Add(this.txtvatexemptsale);
            this.groupBox1.Controls.Add(this.txtvatablesale);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtpono);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1167, 57);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // txtdiscount
            // 
            this.txtdiscount.Location = new System.Drawing.Point(1006, 41);
            this.txtdiscount.Name = "txtdiscount";
            this.txtdiscount.ReadOnly = true;
            this.txtdiscount.Size = new System.Drawing.Size(114, 21);
            this.txtdiscount.TabIndex = 21;
            this.txtdiscount.Text = "0";
            this.txtdiscount.Visible = false;
            // 
            // txtcusttin
            // 
            this.txtcusttin.Location = new System.Drawing.Point(502, 135);
            this.txtcusttin.Name = "txtcusttin";
            this.txtcusttin.ReadOnly = true;
            this.txtcusttin.Size = new System.Drawing.Size(114, 21);
            this.txtcusttin.TabIndex = 20;
            this.txtcusttin.Visible = false;
            // 
            // txtcustkey
            // 
            this.txtcustkey.Location = new System.Drawing.Point(384, 135);
            this.txtcustkey.Name = "txtcustkey";
            this.txtcustkey.ReadOnly = true;
            this.txtcustkey.Size = new System.Drawing.Size(114, 21);
            this.txtcustkey.TabIndex = 19;
            this.txtcustkey.Visible = false;
            // 
            // simpleButton1
            // 
            this.simpleButton1.ImageOptions.Image = global::SalesInventorySystem.Properties.Resources.Print_32x32__2_;
            this.simpleButton1.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.simpleButton1.Location = new System.Drawing.Point(6, 11);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(154, 36);
            this.simpleButton1.TabIndex = 18;
            this.simpleButton1.Text = "Print";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // txtterm
            // 
            this.txtterm.Location = new System.Drawing.Point(434, 110);
            this.txtterm.Name = "txtterm";
            this.txtterm.ReadOnly = true;
            this.txtterm.Size = new System.Drawing.Size(114, 21);
            this.txtterm.TabIndex = 17;
            this.txtterm.Visible = false;
            // 
            // txtcustaddress
            // 
            this.txtcustaddress.Location = new System.Drawing.Point(384, 85);
            this.txtcustaddress.Name = "txtcustaddress";
            this.txtcustaddress.ReadOnly = true;
            this.txtcustaddress.Size = new System.Drawing.Size(114, 21);
            this.txtcustaddress.TabIndex = 16;
            this.txtcustaddress.Visible = false;
            // 
            // txtcustname
            // 
            this.txtcustname.Location = new System.Drawing.Point(384, 67);
            this.txtcustname.Name = "txtcustname";
            this.txtcustname.ReadOnly = true;
            this.txtcustname.Size = new System.Drawing.Size(114, 21);
            this.txtcustname.TabIndex = 15;
            this.txtcustname.Visible = false;
            // 
            // txttotalamountdue
            // 
            this.txttotalamountdue.Location = new System.Drawing.Point(1006, 16);
            this.txttotalamountdue.Name = "txttotalamountdue";
            this.txttotalamountdue.ReadOnly = true;
            this.txttotalamountdue.Size = new System.Drawing.Size(114, 21);
            this.txttotalamountdue.TabIndex = 14;
            this.txttotalamountdue.Text = "0";
            this.txttotalamountdue.Visible = false;
            // 
            // txtaddvat
            // 
            this.txtaddvat.Location = new System.Drawing.Point(886, 41);
            this.txtaddvat.Name = "txtaddvat";
            this.txtaddvat.ReadOnly = true;
            this.txtaddvat.Size = new System.Drawing.Size(114, 21);
            this.txtaddvat.TabIndex = 13;
            this.txtaddvat.Text = "0";
            this.txtaddvat.Visible = false;
            // 
            // txtamountdue
            // 
            this.txtamountdue.Location = new System.Drawing.Point(886, 16);
            this.txtamountdue.Name = "txtamountdue";
            this.txtamountdue.ReadOnly = true;
            this.txtamountdue.Size = new System.Drawing.Size(114, 21);
            this.txtamountdue.TabIndex = 12;
            this.txtamountdue.Text = "0";
            this.txtamountdue.Visible = false;
            // 
            // txtamountnetofvat
            // 
            this.txtamountnetofvat.Location = new System.Drawing.Point(749, 41);
            this.txtamountnetofvat.Name = "txtamountnetofvat";
            this.txtamountnetofvat.ReadOnly = true;
            this.txtamountnetofvat.Size = new System.Drawing.Size(114, 21);
            this.txtamountnetofvat.TabIndex = 11;
            this.txtamountnetofvat.Text = "0";
            this.txtamountnetofvat.Visible = false;
            // 
            // txtlessvat
            // 
            this.txtlessvat.Location = new System.Drawing.Point(749, 16);
            this.txtlessvat.Name = "txtlessvat";
            this.txtlessvat.ReadOnly = true;
            this.txtlessvat.Size = new System.Drawing.Size(114, 21);
            this.txtlessvat.TabIndex = 10;
            this.txtlessvat.Text = "0";
            this.txtlessvat.Visible = false;
            // 
            // txttotalsales
            // 
            this.txttotalsales.Location = new System.Drawing.Point(617, 41);
            this.txttotalsales.Name = "txttotalsales";
            this.txttotalsales.ReadOnly = true;
            this.txttotalsales.Size = new System.Drawing.Size(114, 21);
            this.txttotalsales.TabIndex = 9;
            this.txttotalsales.Text = "0";
            this.txttotalsales.Visible = false;
            // 
            // txtvatamount
            // 
            this.txtvatamount.Location = new System.Drawing.Point(617, 16);
            this.txtvatamount.Name = "txtvatamount";
            this.txtvatamount.ReadOnly = true;
            this.txtvatamount.Size = new System.Drawing.Size(114, 21);
            this.txtvatamount.TabIndex = 8;
            this.txtvatamount.Text = "0";
            this.txtvatamount.Visible = false;
            // 
            // txtvatexemptsale
            // 
            this.txtvatexemptsale.Location = new System.Drawing.Point(479, 41);
            this.txtvatexemptsale.Name = "txtvatexemptsale";
            this.txtvatexemptsale.ReadOnly = true;
            this.txtvatexemptsale.Size = new System.Drawing.Size(114, 21);
            this.txtvatexemptsale.TabIndex = 7;
            this.txtvatexemptsale.Text = "0";
            this.txtvatexemptsale.Visible = false;
            // 
            // txtvatablesale
            // 
            this.txtvatablesale.Location = new System.Drawing.Point(479, 16);
            this.txtvatablesale.Name = "txtvatablesale";
            this.txtvatablesale.ReadOnly = true;
            this.txtvatablesale.Size = new System.Drawing.Size(114, 21);
            this.txtvatablesale.TabIndex = 6;
            this.txtvatablesale.Text = "0";
            this.txtvatablesale.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(166, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "PO #:";
            // 
            // txtpono
            // 
            this.txtpono.Location = new System.Drawing.Point(213, 18);
            this.txtpono.Name = "txtpono";
            this.txtpono.ReadOnly = true;
            this.txtpono.Size = new System.Drawing.Size(114, 21);
            this.txtpono.TabIndex = 4;
            // 
            // txtzeroratedsale
            // 
            this.txtzeroratedsale.Location = new System.Drawing.Point(359, 16);
            this.txtzeroratedsale.Name = "txtzeroratedsale";
            this.txtzeroratedsale.ReadOnly = true;
            this.txtzeroratedsale.Size = new System.Drawing.Size(114, 21);
            this.txtzeroratedsale.TabIndex = 22;
            this.txtzeroratedsale.Text = "0";
            this.txtzeroratedsale.Visible = false;
            // 
            // SalesInvoiceDexEx
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1167, 608);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "SalesInvoiceDexEx";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SalesInvoiceDexEx";
            this.Load += new System.EventHandler(this.SalesInvoiceDexEx_Load);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView4)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        public DevExpress.XtraGrid.GridControl gridControl4;
        public DevExpress.XtraGrid.Views.Grid.GridView gridView4;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.TextBox txtpono;
        public System.Windows.Forms.TextBox txtaddvat;
        public System.Windows.Forms.TextBox txtamountdue;
        public System.Windows.Forms.TextBox txtamountnetofvat;
        public System.Windows.Forms.TextBox txtlessvat;
        public System.Windows.Forms.TextBox txttotalsales;
        public System.Windows.Forms.TextBox txtvatamount;
        public System.Windows.Forms.TextBox txtvatexemptsale;
        public System.Windows.Forms.TextBox txtvatablesale;
        public System.Windows.Forms.TextBox txttotalamountdue;
        public System.Windows.Forms.TextBox txtterm;
        public System.Windows.Forms.TextBox txtcustaddress;
        public System.Windows.Forms.TextBox txtcustname;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        public System.Windows.Forms.TextBox txtcusttin;
        public System.Windows.Forms.TextBox txtcustkey;
        public System.Windows.Forms.TextBox txtdiscount;
        public System.Windows.Forms.TextBox txtzeroratedsale;
    }
}