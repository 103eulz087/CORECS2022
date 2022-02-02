namespace SalesInventorySystem.HotelManagement
{
    partial class HotelFrmReservation
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
            this.txtroomnum = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtnameofguest = new System.Windows.Forms.TextBox();
            this.txtcontactno = new System.Windows.Forms.TextBox();
            this.txtnumdays = new System.Windows.Forms.TextBox();
            this.txtreservdate = new DevExpress.XtraEditors.DateEdit();
            this.button1 = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.txtreserveid = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.txtreservdate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtreservdate.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // txtroomnum
            // 
            this.txtroomnum.Font = new System.Drawing.Font("Tahoma", 10.25F);
            this.txtroomnum.Location = new System.Drawing.Point(172, 41);
            this.txtroomnum.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtroomnum.MaxLength = 10;
            this.txtroomnum.Name = "txtroomnum";
            this.txtroomnum.Size = new System.Drawing.Size(124, 28);
            this.txtroomnum.TabIndex = 424;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Tahoma", 10F);
            this.label6.Location = new System.Drawing.Point(12, 44);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(76, 21);
            this.label6.TabIndex = 423;
            this.label6.Text = "Room #:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 10F);
            this.label1.Location = new System.Drawing.Point(12, 80);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(126, 21);
            this.label1.TabIndex = 425;
            this.label1.Text = "Name of Guest:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 10F);
            this.label2.Location = new System.Drawing.Point(12, 116);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 21);
            this.label2.TabIndex = 426;
            this.label2.Text = "Contact #:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 10F);
            this.label3.Location = new System.Drawing.Point(12, 152);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(102, 21);
            this.label3.TabIndex = 427;
            this.label3.Text = "No. of Days:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 10F);
            this.label4.Location = new System.Drawing.Point(12, 188);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(145, 21);
            this.label4.TabIndex = 428;
            this.label4.Text = "Reservation Date:";
            // 
            // txtnameofguest
            // 
            this.txtnameofguest.Font = new System.Drawing.Font("Tahoma", 10.25F);
            this.txtnameofguest.Location = new System.Drawing.Point(172, 77);
            this.txtnameofguest.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtnameofguest.MaxLength = 80;
            this.txtnameofguest.Name = "txtnameofguest";
            this.txtnameofguest.Size = new System.Drawing.Size(282, 28);
            this.txtnameofguest.TabIndex = 429;
            // 
            // txtcontactno
            // 
            this.txtcontactno.Font = new System.Drawing.Font("Tahoma", 10.25F);
            this.txtcontactno.Location = new System.Drawing.Point(172, 113);
            this.txtcontactno.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtcontactno.MaxLength = 30;
            this.txtcontactno.Name = "txtcontactno";
            this.txtcontactno.Size = new System.Drawing.Size(282, 28);
            this.txtcontactno.TabIndex = 430;
            // 
            // txtnumdays
            // 
            this.txtnumdays.Font = new System.Drawing.Font("Tahoma", 10.25F);
            this.txtnumdays.Location = new System.Drawing.Point(172, 149);
            this.txtnumdays.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtnumdays.MaxLength = 3;
            this.txtnumdays.Name = "txtnumdays";
            this.txtnumdays.Size = new System.Drawing.Size(124, 28);
            this.txtnumdays.TabIndex = 431;
            this.txtnumdays.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtnumdays_KeyPress);
            // 
            // txtreservdate
            // 
            this.txtreservdate.EditValue = null;
            this.txtreservdate.Location = new System.Drawing.Point(172, 185);
            this.txtreservdate.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtreservdate.Name = "txtreservdate";
            this.txtreservdate.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10.25F);
            this.txtreservdate.Properties.Appearance.Options.UseFont = true;
            this.txtreservdate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtreservdate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtreservdate.Size = new System.Drawing.Size(125, 28);
            this.txtreservdate.TabIndex = 432;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Tahoma", 10F);
            this.button1.Location = new System.Drawing.Point(172, 221);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(125, 37);
            this.button1.TabIndex = 433;
            this.button1.Text = "Reserve";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Tahoma", 10F);
            this.label5.Location = new System.Drawing.Point(12, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(121, 21);
            this.label5.TabIndex = 434;
            this.label5.Text = "Reservation #:";
            // 
            // txtreserveid
            // 
            this.txtreserveid.Font = new System.Drawing.Font("Tahoma", 10.25F);
            this.txtreserveid.Location = new System.Drawing.Point(172, 6);
            this.txtreserveid.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtreserveid.MaxLength = 10;
            this.txtreserveid.Name = "txtreserveid";
            this.txtreserveid.Size = new System.Drawing.Size(124, 28);
            this.txtreserveid.TabIndex = 435;
            // 
            // HotelFrmReservation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(473, 274);
            this.Controls.Add(this.txtreserveid);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txtreservdate);
            this.Controls.Add(this.txtnumdays);
            this.Controls.Add(this.txtcontactno);
            this.Controls.Add(this.txtnameofguest);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtroomnum);
            this.Controls.Add(this.label6);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "HotelFrmReservation";
            this.Text = "HotelFrmReservation";
            this.Load += new System.EventHandler(this.HotelFrmReservation_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtreservdate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtreservdate.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        internal System.Windows.Forms.Label label6;
        internal System.Windows.Forms.Label label1;
        internal System.Windows.Forms.Label label2;
        internal System.Windows.Forms.Label label3;
        internal System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button1;
        public System.Windows.Forms.TextBox txtroomnum;
        public System.Windows.Forms.TextBox txtnameofguest;
        public System.Windows.Forms.TextBox txtcontactno;
        public System.Windows.Forms.TextBox txtnumdays;
        public DevExpress.XtraEditors.DateEdit txtreservdate;
        internal System.Windows.Forms.Label label5;
        public System.Windows.Forms.TextBox txtreserveid;
    }
}