namespace SalesInventorySystem
{
    partial class Login
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Login));
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.txtuserid = new DevExpress.XtraEditors.TextEdit();
            this.txtpassword = new DevExpress.XtraEditors.TextEdit();
            this.btnLogin = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.buttonLogin = new DevExpress.XtraEditors.SimpleButton();
            this.labelversion = new DevExpress.XtraEditors.LabelControl();
            this.btnclose = new DevExpress.XtraEditors.SimpleButton();
            this.PictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.txtuserid.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtpassword.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.BackColor = System.Drawing.Color.Black;
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.labelControl2.Appearance.ForeColor = System.Drawing.Color.White;
            this.labelControl2.Appearance.Options.UseBackColor = true;
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Appearance.Options.UseForeColor = true;
            this.labelControl2.Location = new System.Drawing.Point(108, 351);
            this.labelControl2.Margin = new System.Windows.Forms.Padding(2, 5, 2, 5);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(77, 21);
            this.labelControl2.TabIndex = 1;
            this.labelControl2.Text = "Password:";
            this.labelControl2.Visible = false;
            // 
            // txtuserid
            // 
            this.txtuserid.Location = new System.Drawing.Point(134, 227);
            this.txtuserid.Margin = new System.Windows.Forms.Padding(2, 5, 2, 5);
            this.txtuserid.Name = "txtuserid";
            this.txtuserid.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtuserid.Properties.Appearance.Options.UseFont = true;
            this.txtuserid.Size = new System.Drawing.Size(373, 30);
            this.txtuserid.TabIndex = 2;
            this.txtuserid.EditValueChanged += new System.EventHandler(this.txtuserid_EditValueChanged);
            this.txtuserid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtuserid_KeyDown);
            // 
            // txtpassword
            // 
            this.txtpassword.Location = new System.Drawing.Point(134, 287);
            this.txtpassword.Margin = new System.Windows.Forms.Padding(2, 5, 2, 5);
            this.txtpassword.Name = "txtpassword";
            this.txtpassword.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtpassword.Properties.Appearance.Options.UseFont = true;
            this.txtpassword.Properties.PasswordChar = '*';
            this.txtpassword.Size = new System.Drawing.Size(373, 30);
            this.txtpassword.TabIndex = 3;
            this.txtpassword.EditValueChanged += new System.EventHandler(this.txtpassword_EditValueChanged);
            this.txtpassword.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtpassword_KeyDown);
            // 
            // btnLogin
            // 
            this.btnLogin.Location = new System.Drawing.Point(0, 0);
            this.btnLogin.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(87, 29);
            this.btnLogin.TabIndex = 8;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.BackColor = System.Drawing.Color.Black;
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.labelControl1.Appearance.ForeColor = System.Drawing.Color.White;
            this.labelControl1.Appearance.Options.UseBackColor = true;
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Appearance.Options.UseForeColor = true;
            this.labelControl1.Location = new System.Drawing.Point(30, 351);
            this.labelControl1.Margin = new System.Windows.Forms.Padding(2, 5, 2, 5);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(81, 21);
            this.labelControl1.TabIndex = 7;
            this.labelControl1.Text = "Username:";
            this.labelControl1.Visible = false;
            // 
            // buttonLogin
            // 
            this.buttonLogin.Location = new System.Drawing.Point(236, 339);
            this.buttonLogin.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.buttonLogin.Name = "buttonLogin";
            this.buttonLogin.Size = new System.Drawing.Size(125, 43);
            this.buttonLogin.TabIndex = 18;
            this.buttonLogin.Text = "LOGIN";
            this.buttonLogin.Click += new System.EventHandler(this.buttonLogin_Click);
            // 
            // labelversion
            // 
            this.labelversion.Appearance.BackColor = System.Drawing.Color.Black;
            this.labelversion.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.labelversion.Appearance.ForeColor = System.Drawing.Color.White;
            this.labelversion.Appearance.Options.UseBackColor = true;
            this.labelversion.Appearance.Options.UseFont = true;
            this.labelversion.Appearance.Options.UseForeColor = true;
            this.labelversion.Location = new System.Drawing.Point(518, 354);
            this.labelversion.Margin = new System.Windows.Forms.Padding(2, 5, 2, 5);
            this.labelversion.Name = "labelversion";
            this.labelversion.Size = new System.Drawing.Size(71, 21);
            this.labelversion.TabIndex = 19;
            this.labelversion.Text = "ver: 1.8.1";
            // 
            // btnclose
            // 
            this.btnclose.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("simpleButton1.ImageOptions.SvgImage")));
            this.btnclose.Location = new System.Drawing.Point(551, 0);
            this.btnclose.Margin = new System.Windows.Forms.Padding(2);
            this.btnclose.Name = "btnclose";
            this.btnclose.Size = new System.Drawing.Size(46, 33);
            this.btnclose.TabIndex = 20;
            this.btnclose.Click += new System.EventHandler(this.btnclose_Click);
            // 
            // PictureBox1
            // 
            this.PictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.PictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox1.Image")));
            this.PictureBox1.Location = new System.Drawing.Point(0, 0);
            this.PictureBox1.Margin = new System.Windows.Forms.Padding(5);
            this.PictureBox1.Name = "PictureBox1";
            this.PictureBox1.Size = new System.Drawing.Size(597, 389);
            this.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PictureBox1.TabIndex = 17;
            this.PictureBox1.TabStop = false;
            this.PictureBox1.Click += new System.EventHandler(this.PictureBox1_Click_1);
            // 
            // Login
            // 
            this.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(596, 389);
            this.Controls.Add(this.btnclose);
            this.Controls.Add(this.labelversion);
            this.Controls.Add(this.buttonLogin);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.txtpassword);
            this.Controls.Add(this.txtuserid);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.PictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(2, 5, 2, 5);
            this.Name = "Login";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "IT CORE SYSTEM Inc. version 1.0";
            this.Load += new System.EventHandler(this.Login_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtuserid.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtpassword.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit txtpassword;
        private DevExpress.XtraEditors.SimpleButton btnLogin;
        private DevExpress.XtraEditors.LabelControl labelControl1;
       
        public DevExpress.XtraEditors.TextEdit txtuserid;
        internal System.Windows.Forms.PictureBox PictureBox1;
        private DevExpress.XtraEditors.SimpleButton buttonLogin;
        private DevExpress.XtraEditors.LabelControl labelversion;
        private DevExpress.XtraEditors.SimpleButton btnclose;
    }
}