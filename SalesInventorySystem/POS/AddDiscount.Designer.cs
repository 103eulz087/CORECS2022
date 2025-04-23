namespace SalesInventorySystem
{
    partial class AddDiscount
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
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.txtcontrolno = new System.Windows.Forms.TextBox();
            this.txtname = new System.Windows.Forms.TextBox();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txtdiscountamount = new System.Windows.Forms.TextBox();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.txtamnttobediscount = new System.Windows.Forms.TextBox();
            this.panelsenior = new System.Windows.Forms.Panel();
            this.txtpercentageamount = new System.Windows.Forms.TextBox();
            this.labelControl12 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl11 = new DevExpress.XtraEditors.LabelControl();
            this.panelothers = new System.Windows.Forms.Panel();
            this.labelControl15 = new DevExpress.XtraEditors.LabelControl();
            this.txtotherdiscountamount = new System.Windows.Forms.TextBox();
            this.txtotherspercent = new System.Windows.Forms.TextBox();
            this.labelControl14 = new DevExpress.XtraEditors.LabelControl();
            this.button1 = new System.Windows.Forms.Button();
            this.txtremarks = new System.Windows.Forms.RichTextBox();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.txtothersamount = new System.Windows.Forms.TextBox();
            this.panelpwd = new System.Windows.Forms.Panel();
            this.txtpwdamount = new System.Windows.Forms.TextBox();
            this.labelControl10 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl9 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.txtpwdpercent = new System.Windows.Forms.TextBox();
            this.btnPwdDiscount = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.txtpwddiscountamount = new System.Windows.Forms.TextBox();
            this.txtpwdidno = new System.Windows.Forms.TextBox();
            this.txtpwdname = new System.Windows.Forms.TextBox();
            this.txtvatadj = new System.Windows.Forms.TextBox();
            this.labelControl13 = new DevExpress.XtraEditors.LabelControl();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.txtcashiertansno = new System.Windows.Forms.TextBox();
            this.txtorderno = new System.Windows.Forms.TextBox();
            this.txttransactionno = new System.Windows.Forms.TextBox();
            this.txtvatexadj = new System.Windows.Forms.TextBox();
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.btnshowdiscounteditems = new DevExpress.XtraEditors.SimpleButton();
            this.panelsenior.SuspendLayout();
            this.panelothers.SuspendLayout();
            this.panelpwd.SuspendLayout();
            this.SuspendLayout();
            // 
            // simpleButton1
            // 
            this.simpleButton1.Location = new System.Drawing.Point(553, 135);
            this.simpleButton1.Margin = new System.Windows.Forms.Padding(5, 7, 5, 7);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(265, 147);
            this.simpleButton1.TabIndex = 13;
            this.simpleButton1.Text = "SET";
            this.simpleButton1.Visible = false;
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 12.25F);
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Location = new System.Drawing.Point(20, 24);
            this.labelControl2.Margin = new System.Windows.Forms.Padding(5, 7, 5, 7);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(177, 40);
            this.labelControl2.TabIndex = 14;
            this.labelControl2.Text = "Control No.:";
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Checked = true;
            this.radioButton1.Font = new System.Drawing.Font("Tahoma", 10.25F);
            this.radioButton1.Location = new System.Drawing.Point(24, 24);
            this.radioButton1.Margin = new System.Windows.Forms.Padding(5, 7, 5, 7);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(275, 38);
            this.radioButton1.TabIndex = 15;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "Senior Citizen (F1)";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Font = new System.Drawing.Font("Tahoma", 10.25F);
            this.radioButton2.Location = new System.Drawing.Point(337, 24);
            this.radioButton2.Margin = new System.Windows.Forms.Padding(5, 7, 5, 7);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(166, 38);
            this.radioButton2.TabIndex = 16;
            this.radioButton2.Text = "PWD (F2)";
            this.radioButton2.UseVisualStyleBackColor = true;
            this.radioButton2.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Tahoma", 12.25F);
            this.labelControl3.Appearance.Options.UseFont = true;
            this.labelControl3.Location = new System.Drawing.Point(20, 82);
            this.labelControl3.Margin = new System.Windows.Forms.Padding(5, 7, 5, 7);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(96, 40);
            this.labelControl3.TabIndex = 17;
            this.labelControl3.Text = "Name:";
            // 
            // txtcontrolno
            // 
            this.txtcontrolno.Font = new System.Drawing.Font("Tahoma", 10.25F);
            this.txtcontrolno.Location = new System.Drawing.Point(209, 22);
            this.txtcontrolno.Margin = new System.Windows.Forms.Padding(5, 7, 5, 7);
            this.txtcontrolno.Name = "txtcontrolno";
            this.txtcontrolno.Size = new System.Drawing.Size(609, 40);
            this.txtcontrolno.TabIndex = 18;
            this.txtcontrolno.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtcontrolno_KeyDown);
            // 
            // txtname
            // 
            this.txtname.Font = new System.Drawing.Font("Tahoma", 10.25F);
            this.txtname.Location = new System.Drawing.Point(209, 79);
            this.txtname.Margin = new System.Windows.Forms.Padding(5, 7, 5, 7);
            this.txtname.Name = "txtname";
            this.txtname.Size = new System.Drawing.Size(609, 40);
            this.txtname.TabIndex = 19;
            this.txtname.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtname_KeyDown);
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 12.25F);
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(20, 22);
            this.labelControl1.Margin = new System.Windows.Forms.Padding(5, 7, 5, 7);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(125, 40);
            this.labelControl1.TabIndex = 20;
            this.labelControl1.Text = "Amount:";
            // 
            // txtdiscountamount
            // 
            this.txtdiscountamount.Font = new System.Drawing.Font("Tahoma", 10.25F);
            this.txtdiscountamount.Location = new System.Drawing.Point(341, 237);
            this.txtdiscountamount.Margin = new System.Windows.Forms.Padding(5, 7, 5, 7);
            this.txtdiscountamount.Name = "txtdiscountamount";
            this.txtdiscountamount.ReadOnly = true;
            this.txtdiscountamount.Size = new System.Drawing.Size(200, 40);
            this.txtdiscountamount.TabIndex = 21;
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Font = new System.Drawing.Font("Tahoma", 12.25F);
            this.labelControl4.Appearance.Options.UseFont = true;
            this.labelControl4.Location = new System.Drawing.Point(12, 235);
            this.labelControl4.Margin = new System.Windows.Forms.Padding(5, 7, 5, 7);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(295, 40);
            this.labelControl4.TabIndex = 22;
            this.labelControl4.Text = "Discounted Amount:";
            // 
            // txtamnttobediscount
            // 
            this.txtamnttobediscount.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.txtamnttobediscount.Enabled = false;
            this.txtamnttobediscount.Font = new System.Drawing.Font("Tahoma", 10.25F);
            this.txtamnttobediscount.Location = new System.Drawing.Point(341, 135);
            this.txtamnttobediscount.Margin = new System.Windows.Forms.Padding(5, 7, 5, 7);
            this.txtamnttobediscount.Name = "txtamnttobediscount";
            this.txtamnttobediscount.Size = new System.Drawing.Size(200, 40);
            this.txtamnttobediscount.TabIndex = 23;
            this.txtamnttobediscount.TextChanged += new System.EventHandler(this.txtamnttobediscount_TextChanged);
            this.txtamnttobediscount.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtamnttobediscount_KeyDown);
            // 
            // panelsenior
            // 
            this.panelsenior.Controls.Add(this.txtpercentageamount);
            this.panelsenior.Controls.Add(this.labelControl12);
            this.panelsenior.Controls.Add(this.labelControl11);
            this.panelsenior.Controls.Add(this.labelControl2);
            this.panelsenior.Controls.Add(this.txtamnttobediscount);
            this.panelsenior.Controls.Add(this.simpleButton1);
            this.panelsenior.Controls.Add(this.labelControl4);
            this.panelsenior.Controls.Add(this.labelControl3);
            this.panelsenior.Controls.Add(this.txtdiscountamount);
            this.panelsenior.Controls.Add(this.txtcontrolno);
            this.panelsenior.Controls.Add(this.txtname);
            this.panelsenior.Location = new System.Drawing.Point(20, 75);
            this.panelsenior.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.panelsenior.Name = "panelsenior";
            this.panelsenior.Size = new System.Drawing.Size(848, 368);
            this.panelsenior.TabIndex = 24;
            // 
            // txtpercentageamount
            // 
            this.txtpercentageamount.Font = new System.Drawing.Font("Tahoma", 10.25F);
            this.txtpercentageamount.Location = new System.Drawing.Point(341, 185);
            this.txtpercentageamount.Margin = new System.Windows.Forms.Padding(5, 7, 5, 7);
            this.txtpercentageamount.Name = "txtpercentageamount";
            this.txtpercentageamount.Size = new System.Drawing.Size(200, 40);
            this.txtpercentageamount.TabIndex = 26;
            this.txtpercentageamount.Text = "0";
            this.txtpercentageamount.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            this.txtpercentageamount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox1_KeyPress);
            // 
            // labelControl12
            // 
            this.labelControl12.Appearance.Font = new System.Drawing.Font("Tahoma", 12.25F);
            this.labelControl12.Appearance.Options.UseFont = true;
            this.labelControl12.Location = new System.Drawing.Point(100, 182);
            this.labelControl12.Margin = new System.Windows.Forms.Padding(5, 7, 5, 7);
            this.labelControl12.Name = "labelControl12";
            this.labelControl12.Size = new System.Drawing.Size(214, 40);
            this.labelControl12.TabIndex = 25;
            this.labelControl12.Text = "Percentage %:";
            // 
            // labelControl11
            // 
            this.labelControl11.Appearance.Font = new System.Drawing.Font("Tahoma", 12.25F);
            this.labelControl11.Appearance.Options.UseFont = true;
            this.labelControl11.Location = new System.Drawing.Point(16, 133);
            this.labelControl11.Margin = new System.Windows.Forms.Padding(5, 7, 5, 7);
            this.labelControl11.Name = "labelControl11";
            this.labelControl11.Size = new System.Drawing.Size(299, 40);
            this.labelControl11.TabIndex = 24;
            this.labelControl11.Text = "Amount to Discount:";
            // 
            // panelothers
            // 
            this.panelothers.Controls.Add(this.labelControl15);
            this.panelothers.Controls.Add(this.txtotherdiscountamount);
            this.panelothers.Controls.Add(this.txtotherspercent);
            this.panelothers.Controls.Add(this.labelControl14);
            this.panelothers.Controls.Add(this.button1);
            this.panelothers.Controls.Add(this.txtremarks);
            this.panelothers.Controls.Add(this.labelControl5);
            this.panelothers.Controls.Add(this.txtothersamount);
            this.panelothers.Controls.Add(this.labelControl1);
            this.panelothers.Location = new System.Drawing.Point(20, 75);
            this.panelothers.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.panelothers.Name = "panelothers";
            this.panelothers.Size = new System.Drawing.Size(848, 368);
            this.panelothers.TabIndex = 25;
            this.panelothers.Visible = false;
            // 
            // labelControl15
            // 
            this.labelControl15.Appearance.Font = new System.Drawing.Font("Tahoma", 12.25F);
            this.labelControl15.Appearance.Options.UseFont = true;
            this.labelControl15.Location = new System.Drawing.Point(20, 136);
            this.labelControl15.Margin = new System.Windows.Forms.Padding(5, 7, 5, 7);
            this.labelControl15.Name = "labelControl15";
            this.labelControl15.Size = new System.Drawing.Size(295, 40);
            this.labelControl15.TabIndex = 28;
            this.labelControl15.Text = "Discounted Amount:";
            // 
            // txtotherdiscountamount
            // 
            this.txtotherdiscountamount.Font = new System.Drawing.Font("Tahoma", 10.25F);
            this.txtotherdiscountamount.Location = new System.Drawing.Point(341, 136);
            this.txtotherdiscountamount.Margin = new System.Windows.Forms.Padding(5, 7, 5, 7);
            this.txtotherdiscountamount.Name = "txtotherdiscountamount";
            this.txtotherdiscountamount.ReadOnly = true;
            this.txtotherdiscountamount.Size = new System.Drawing.Size(183, 40);
            this.txtotherdiscountamount.TabIndex = 27;
            this.txtotherdiscountamount.Text = "0";
            // 
            // txtotherspercent
            // 
            this.txtotherspercent.BackColor = System.Drawing.Color.White;
            this.txtotherspercent.Font = new System.Drawing.Font("Tahoma", 10.25F);
            this.txtotherspercent.Location = new System.Drawing.Point(265, 79);
            this.txtotherspercent.Margin = new System.Windows.Forms.Padding(5, 7, 5, 7);
            this.txtotherspercent.Name = "txtotherspercent";
            this.txtotherspercent.Size = new System.Drawing.Size(257, 40);
            this.txtotherspercent.TabIndex = 26;
            this.txtotherspercent.Text = "0";
            this.txtotherspercent.TextChanged += new System.EventHandler(this.txtotherspercent_TextChanged);
            // 
            // labelControl14
            // 
            this.labelControl14.Appearance.Font = new System.Drawing.Font("Tahoma", 12.25F);
            this.labelControl14.Appearance.Options.UseFont = true;
            this.labelControl14.Location = new System.Drawing.Point(20, 76);
            this.labelControl14.Margin = new System.Windows.Forms.Padding(5, 7, 5, 7);
            this.labelControl14.Name = "labelControl14";
            this.labelControl14.Size = new System.Drawing.Size(214, 40);
            this.labelControl14.TabIndex = 25;
            this.labelControl14.Text = "Percentage %:";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(572, 136);
            this.button1.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(251, 43);
            this.button1.TabIndex = 24;
            this.button1.Text = "Update";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtremarks
            // 
            this.txtremarks.Location = new System.Drawing.Point(265, 197);
            this.txtremarks.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.txtremarks.Name = "txtremarks";
            this.txtremarks.Size = new System.Drawing.Size(555, 101);
            this.txtremarks.TabIndex = 23;
            this.txtremarks.Text = "";
            // 
            // labelControl5
            // 
            this.labelControl5.Appearance.Font = new System.Drawing.Font("Tahoma", 12.25F);
            this.labelControl5.Appearance.Options.UseFont = true;
            this.labelControl5.Location = new System.Drawing.Point(20, 224);
            this.labelControl5.Margin = new System.Windows.Forms.Padding(5, 7, 5, 7);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(137, 40);
            this.labelControl5.TabIndex = 22;
            this.labelControl5.Text = "Remarks:";
            // 
            // txtothersamount
            // 
            this.txtothersamount.Font = new System.Drawing.Font("Tahoma", 10.25F);
            this.txtothersamount.Location = new System.Drawing.Point(265, 24);
            this.txtothersamount.Margin = new System.Windows.Forms.Padding(5, 7, 5, 7);
            this.txtothersamount.Name = "txtothersamount";
            this.txtothersamount.Size = new System.Drawing.Size(257, 40);
            this.txtothersamount.TabIndex = 21;
            // 
            // panelpwd
            // 
            this.panelpwd.Controls.Add(this.txtpwdamount);
            this.panelpwd.Controls.Add(this.labelControl10);
            this.panelpwd.Controls.Add(this.labelControl9);
            this.panelpwd.Controls.Add(this.labelControl6);
            this.panelpwd.Controls.Add(this.txtpwdpercent);
            this.panelpwd.Controls.Add(this.btnPwdDiscount);
            this.panelpwd.Controls.Add(this.labelControl7);
            this.panelpwd.Controls.Add(this.labelControl8);
            this.panelpwd.Controls.Add(this.txtpwddiscountamount);
            this.panelpwd.Controls.Add(this.txtpwdidno);
            this.panelpwd.Controls.Add(this.txtpwdname);
            this.panelpwd.Location = new System.Drawing.Point(20, 75);
            this.panelpwd.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.panelpwd.Name = "panelpwd";
            this.panelpwd.Size = new System.Drawing.Size(848, 368);
            this.panelpwd.TabIndex = 26;
            this.panelpwd.Visible = false;
            // 
            // txtpwdamount
            // 
            this.txtpwdamount.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.txtpwdamount.Enabled = false;
            this.txtpwdamount.Font = new System.Drawing.Font("Tahoma", 10.25F);
            this.txtpwdamount.Location = new System.Drawing.Point(352, 135);
            this.txtpwdamount.Margin = new System.Windows.Forms.Padding(5, 7, 5, 7);
            this.txtpwdamount.Name = "txtpwdamount";
            this.txtpwdamount.ReadOnly = true;
            this.txtpwdamount.Size = new System.Drawing.Size(189, 40);
            this.txtpwdamount.TabIndex = 26;
            this.txtpwdamount.Text = "0";
            this.txtpwdamount.TextChanged += new System.EventHandler(this.txtpwdamount_TextChanged);
            // 
            // labelControl10
            // 
            this.labelControl10.Appearance.Font = new System.Drawing.Font("Tahoma", 12.25F);
            this.labelControl10.Appearance.Options.UseFont = true;
            this.labelControl10.Location = new System.Drawing.Point(20, 133);
            this.labelControl10.Margin = new System.Windows.Forms.Padding(5, 7, 5, 7);
            this.labelControl10.Name = "labelControl10";
            this.labelControl10.Size = new System.Drawing.Size(299, 40);
            this.labelControl10.TabIndex = 25;
            this.labelControl10.Text = "Amount to Discount:";
            // 
            // labelControl9
            // 
            this.labelControl9.Appearance.Font = new System.Drawing.Font("Tahoma", 12.25F);
            this.labelControl9.Appearance.Options.UseFont = true;
            this.labelControl9.Location = new System.Drawing.Point(19, 243);
            this.labelControl9.Margin = new System.Windows.Forms.Padding(5, 7, 5, 7);
            this.labelControl9.Name = "labelControl9";
            this.labelControl9.Size = new System.Drawing.Size(295, 40);
            this.labelControl9.TabIndex = 24;
            this.labelControl9.Text = "Discounted Amount:";
            // 
            // labelControl6
            // 
            this.labelControl6.Appearance.Font = new System.Drawing.Font("Tahoma", 12.25F);
            this.labelControl6.Appearance.Options.UseFont = true;
            this.labelControl6.Location = new System.Drawing.Point(20, 24);
            this.labelControl6.Margin = new System.Windows.Forms.Padding(5, 7, 5, 7);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(160, 40);
            this.labelControl6.TabIndex = 14;
            this.labelControl6.Text = "PWD ID #:";
            // 
            // txtpwdpercent
            // 
            this.txtpwdpercent.BackColor = System.Drawing.Color.White;
            this.txtpwdpercent.Font = new System.Drawing.Font("Tahoma", 10.25F);
            this.txtpwdpercent.Location = new System.Drawing.Point(352, 187);
            this.txtpwdpercent.Margin = new System.Windows.Forms.Padding(5, 7, 5, 7);
            this.txtpwdpercent.Name = "txtpwdpercent";
            this.txtpwdpercent.Size = new System.Drawing.Size(189, 40);
            this.txtpwdpercent.TabIndex = 23;
            this.txtpwdpercent.Text = "0";
            this.txtpwdpercent.TextChanged += new System.EventHandler(this.txtpwdpercent_TextChanged);
            // 
            // btnPwdDiscount
            // 
            this.btnPwdDiscount.Location = new System.Drawing.Point(553, 135);
            this.btnPwdDiscount.Margin = new System.Windows.Forms.Padding(5, 7, 5, 7);
            this.btnPwdDiscount.Name = "btnPwdDiscount";
            this.btnPwdDiscount.Size = new System.Drawing.Size(265, 154);
            this.btnPwdDiscount.TabIndex = 13;
            this.btnPwdDiscount.Text = "SET";
            this.btnPwdDiscount.Visible = false;
            this.btnPwdDiscount.Click += new System.EventHandler(this.btnPwdDiscount_Click);
            // 
            // labelControl7
            // 
            this.labelControl7.Appearance.Font = new System.Drawing.Font("Tahoma", 12.25F);
            this.labelControl7.Appearance.Options.UseFont = true;
            this.labelControl7.Location = new System.Drawing.Point(107, 185);
            this.labelControl7.Margin = new System.Windows.Forms.Padding(5, 7, 5, 7);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(214, 40);
            this.labelControl7.TabIndex = 22;
            this.labelControl7.Text = "Percentage %:";
            // 
            // labelControl8
            // 
            this.labelControl8.Appearance.Font = new System.Drawing.Font("Tahoma", 12.25F);
            this.labelControl8.Appearance.Options.UseFont = true;
            this.labelControl8.Location = new System.Drawing.Point(20, 82);
            this.labelControl8.Margin = new System.Windows.Forms.Padding(5, 7, 5, 7);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(96, 40);
            this.labelControl8.TabIndex = 17;
            this.labelControl8.Text = "Name:";
            // 
            // txtpwddiscountamount
            // 
            this.txtpwddiscountamount.Font = new System.Drawing.Font("Tahoma", 10.25F);
            this.txtpwddiscountamount.Location = new System.Drawing.Point(352, 246);
            this.txtpwddiscountamount.Margin = new System.Windows.Forms.Padding(5, 7, 5, 7);
            this.txtpwddiscountamount.Name = "txtpwddiscountamount";
            this.txtpwddiscountamount.ReadOnly = true;
            this.txtpwddiscountamount.Size = new System.Drawing.Size(189, 40);
            this.txtpwddiscountamount.TabIndex = 21;
            this.txtpwddiscountamount.Text = "0";
            // 
            // txtpwdidno
            // 
            this.txtpwdidno.Font = new System.Drawing.Font("Tahoma", 10.25F);
            this.txtpwdidno.Location = new System.Drawing.Point(209, 22);
            this.txtpwdidno.Margin = new System.Windows.Forms.Padding(5, 7, 5, 7);
            this.txtpwdidno.Name = "txtpwdidno";
            this.txtpwdidno.Size = new System.Drawing.Size(609, 40);
            this.txtpwdidno.TabIndex = 18;
            this.txtpwdidno.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtpwdidno_KeyDown);
            // 
            // txtpwdname
            // 
            this.txtpwdname.Font = new System.Drawing.Font("Tahoma", 10.25F);
            this.txtpwdname.Location = new System.Drawing.Point(209, 79);
            this.txtpwdname.Margin = new System.Windows.Forms.Padding(5, 7, 5, 7);
            this.txtpwdname.Name = "txtpwdname";
            this.txtpwdname.Size = new System.Drawing.Size(609, 40);
            this.txtpwdname.TabIndex = 19;
            this.txtpwdname.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtpwdname_KeyDown);
            // 
            // txtvatadj
            // 
            this.txtvatadj.Font = new System.Drawing.Font("Tahoma", 10.25F);
            this.txtvatadj.Location = new System.Drawing.Point(349, 782);
            this.txtvatadj.Margin = new System.Windows.Forms.Padding(5, 7, 5, 7);
            this.txtvatadj.Name = "txtvatadj";
            this.txtvatadj.ReadOnly = true;
            this.txtvatadj.Size = new System.Drawing.Size(189, 40);
            this.txtvatadj.TabIndex = 28;
            this.txtvatadj.Text = "0";
            // 
            // labelControl13
            // 
            this.labelControl13.Appearance.Font = new System.Drawing.Font("Tahoma", 12.25F);
            this.labelControl13.Appearance.Options.UseFont = true;
            this.labelControl13.Location = new System.Drawing.Point(79, 782);
            this.labelControl13.Margin = new System.Windows.Forms.Padding(5, 7, 5, 7);
            this.labelControl13.Name = "labelControl13";
            this.labelControl13.Size = new System.Drawing.Size(235, 40);
            this.labelControl13.TabIndex = 27;
            this.labelControl13.Text = "Vat Adjustment:";
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.Font = new System.Drawing.Font("Tahoma", 10.25F);
            this.radioButton3.Location = new System.Drawing.Point(532, 24);
            this.radioButton3.Margin = new System.Windows.Forms.Padding(5, 7, 5, 7);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(225, 38);
            this.radioButton3.TabIndex = 27;
            this.radioButton3.Text = "REGULAR (F3)";
            this.radioButton3.UseVisualStyleBackColor = true;
            this.radioButton3.CheckedChanged += new System.EventHandler(this.radioButton3_CheckedChanged);
            // 
            // txtcashiertansno
            // 
            this.txtcashiertansno.Font = new System.Drawing.Font("Tahoma", 10.25F);
            this.txtcashiertansno.Location = new System.Drawing.Point(257, 554);
            this.txtcashiertansno.Margin = new System.Windows.Forms.Padding(5, 7, 5, 7);
            this.txtcashiertansno.Name = "txtcashiertansno";
            this.txtcashiertansno.ReadOnly = true;
            this.txtcashiertansno.Size = new System.Drawing.Size(609, 40);
            this.txtcashiertansno.TabIndex = 29;
            // 
            // txtorderno
            // 
            this.txtorderno.Font = new System.Drawing.Font("Tahoma", 10.25F);
            this.txtorderno.Location = new System.Drawing.Point(257, 611);
            this.txtorderno.Margin = new System.Windows.Forms.Padding(5, 7, 5, 7);
            this.txtorderno.Name = "txtorderno";
            this.txtorderno.ReadOnly = true;
            this.txtorderno.Size = new System.Drawing.Size(609, 40);
            this.txtorderno.TabIndex = 30;
            // 
            // txttransactionno
            // 
            this.txttransactionno.Font = new System.Drawing.Font("Tahoma", 10.25F);
            this.txttransactionno.Location = new System.Drawing.Point(257, 667);
            this.txttransactionno.Margin = new System.Windows.Forms.Padding(5, 7, 5, 7);
            this.txttransactionno.Name = "txttransactionno";
            this.txttransactionno.ReadOnly = true;
            this.txttransactionno.Size = new System.Drawing.Size(609, 40);
            this.txttransactionno.TabIndex = 31;
            // 
            // txtvatexadj
            // 
            this.txtvatexadj.Font = new System.Drawing.Font("Tahoma", 10.25F);
            this.txtvatexadj.Location = new System.Drawing.Point(365, 465);
            this.txtvatexadj.Margin = new System.Windows.Forms.Padding(5, 7, 5, 7);
            this.txtvatexadj.Name = "txtvatexadj";
            this.txtvatexadj.ReadOnly = true;
            this.txtvatexadj.Size = new System.Drawing.Size(196, 40);
            this.txtvatexadj.TabIndex = 29;
            this.txtvatexadj.Text = "0";
            this.txtvatexadj.Visible = false;
            // 
            // simpleButton2
            // 
            this.simpleButton2.Appearance.Font = new System.Drawing.Font("Tahoma", 8.8F);
            this.simpleButton2.Appearance.Options.UseFont = true;
            this.simpleButton2.ImageOptions.Image = global::SalesInventorySystem.Properties.Resources.ShowProduct_16x16;
            this.simpleButton2.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.simpleButton2.Location = new System.Drawing.Point(791, 24);
            this.simpleButton2.Margin = new System.Windows.Forms.Padding(5, 7, 5, 7);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new System.Drawing.Size(79, 42);
            this.simpleButton2.TabIndex = 29;
            this.simpleButton2.Text = "...";
            this.simpleButton2.Click += new System.EventHandler(this.simpleButton2_Click);
            // 
            // btnSave
            // 
            this.btnSave.ImageOptions.Image = global::SalesInventorySystem.Properties.Resources.Save_16x16__5_;
            this.btnSave.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btnSave.Location = new System.Drawing.Point(575, 454);
            this.btnSave.Margin = new System.Windows.Forms.Padding(5, 7, 5, 7);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(295, 64);
            this.btnSave.TabIndex = 27;
            this.btnSave.Text = "SAVE";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnshowdiscounteditems
            // 
            this.btnshowdiscounteditems.Appearance.Font = new System.Drawing.Font("Tahoma", 8.8F);
            this.btnshowdiscounteditems.Appearance.Options.UseFont = true;
            this.btnshowdiscounteditems.ImageOptions.Image = global::SalesInventorySystem.Properties.Resources.ShowProduct_16x16;
            this.btnshowdiscounteditems.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btnshowdiscounteditems.Location = new System.Drawing.Point(20, 454);
            this.btnshowdiscounteditems.Margin = new System.Windows.Forms.Padding(5, 7, 5, 7);
            this.btnshowdiscounteditems.Name = "btnshowdiscounteditems";
            this.btnshowdiscounteditems.Size = new System.Drawing.Size(335, 64);
            this.btnshowdiscounteditems.TabIndex = 28;
            this.btnshowdiscounteditems.Text = "Show Discounted Items";
            this.btnshowdiscounteditems.Visible = false;
            this.btnshowdiscounteditems.Click += new System.EventHandler(this.btnshowdiscounteditems_Click);
            // 
            // AddDiscount
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 532);
            this.Controls.Add(this.txtvatadj);
            this.Controls.Add(this.simpleButton2);
            this.Controls.Add(this.labelControl13);
            this.Controls.Add(this.txtvatexadj);
            this.Controls.Add(this.txttransactionno);
            this.Controls.Add(this.txtorderno);
            this.Controls.Add(this.txtcashiertansno);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnshowdiscounteditems);
            this.Controls.Add(this.radioButton3);
            this.Controls.Add(this.panelpwd);
            this.Controls.Add(this.panelothers);
            this.Controls.Add(this.panelsenior);
            this.Controls.Add(this.radioButton2);
            this.Controls.Add(this.radioButton1);
            this.Margin = new System.Windows.Forms.Padding(5, 7, 5, 7);
            this.Name = "AddDiscount";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Discount";
            this.Load += new System.EventHandler(this.AddDiscount_Load);
            this.panelsenior.ResumeLayout(false);
            this.panelsenior.PerformLayout();
            this.panelothers.ResumeLayout(false);
            this.panelothers.PerformLayout();
            this.panelpwd.ResumeLayout(false);
            this.panelpwd.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        public System.Windows.Forms.TextBox txtcontrolno;
        public System.Windows.Forms.TextBox txtname;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        public System.Windows.Forms.TextBox txtdiscountamount;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        public System.Windows.Forms.TextBox txtamnttobediscount;
        private System.Windows.Forms.Panel panelsenior;
        private System.Windows.Forms.Panel panelothers;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.RichTextBox txtremarks;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        public System.Windows.Forms.TextBox txtothersamount;
        private System.Windows.Forms.Panel panelpwd;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        public System.Windows.Forms.TextBox txtpwdpercent;
        private DevExpress.XtraEditors.SimpleButton btnPwdDiscount;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        public System.Windows.Forms.TextBox txtpwddiscountamount;
        public System.Windows.Forms.TextBox txtpwdidno;
        public System.Windows.Forms.TextBox txtpwdname;
        private DevExpress.XtraEditors.LabelControl labelControl9;
        public System.Windows.Forms.TextBox txtpwdamount;
        private DevExpress.XtraEditors.LabelControl labelControl10;
        private DevExpress.XtraEditors.LabelControl labelControl11;
        public System.Windows.Forms.TextBox txtpercentageamount;
        private DevExpress.XtraEditors.LabelControl labelControl12;
        private DevExpress.XtraEditors.SimpleButton btnshowdiscounteditems;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        public System.Windows.Forms.TextBox txtcashiertansno;
        public System.Windows.Forms.TextBox txtorderno;
        public System.Windows.Forms.TextBox txttransactionno;
        public System.Windows.Forms.TextBox txtvatadj;
        private DevExpress.XtraEditors.LabelControl labelControl13;
        public System.Windows.Forms.TextBox txtvatexadj;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
        public System.Windows.Forms.TextBox txtotherspercent;
        private DevExpress.XtraEditors.LabelControl labelControl14;
        private DevExpress.XtraEditors.LabelControl labelControl15;
        public System.Windows.Forms.TextBox txtotherdiscountamount;
        public System.Windows.Forms.RadioButton radioButton1;
        public System.Windows.Forms.RadioButton radioButton2;
        public System.Windows.Forms.RadioButton radioButton3;
    }
}