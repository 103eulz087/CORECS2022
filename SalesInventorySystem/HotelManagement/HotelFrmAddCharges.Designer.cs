namespace SalesInventorySystem.HotelManagement
{
    partial class HotelFrmAddCharges
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
            this.txtrate = new System.Windows.Forms.TextBox();
            this.txtservices = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.searchLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.lblbookingid = new DevExpress.XtraEditors.LabelControl();
            this.labelControl9 = new DevExpress.XtraEditors.LabelControl();
            this.lblroomno = new DevExpress.XtraEditors.LabelControl();
            this.lblguestid = new DevExpress.XtraEditors.LabelControl();
            this.lblguestname = new DevExpress.XtraEditors.LabelControl();
            this.lblcheckindate = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.txttotalamount = new System.Windows.Forms.TextBox();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.button1 = new System.Windows.Forms.Button();
            this.txtqty = new System.Windows.Forms.TextBox();
            this.labelControl11 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.txtservices.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtrate
            // 
            this.txtrate.Font = new System.Drawing.Font("Tahoma", 9.25F);
            this.txtrate.Location = new System.Drawing.Point(159, 69);
            this.txtrate.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtrate.Name = "txtrate";
            this.txtrate.ReadOnly = true;
            this.txtrate.Size = new System.Drawing.Size(194, 26);
            this.txtrate.TabIndex = 62;
            // 
            // txtservices
            // 
            this.txtservices.Location = new System.Drawing.Point(159, 39);
            this.txtservices.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtservices.Name = "txtservices";
            this.txtservices.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.25F);
            this.txtservices.Properties.Appearance.Options.UseFont = true;
            this.txtservices.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtservices.Properties.NullText = "";
            this.txtservices.Properties.View = this.searchLookUpEdit1View;
            this.txtservices.Size = new System.Drawing.Size(383, 24);
            this.txtservices.TabIndex = 59;
            this.txtservices.EditValueChanged += new System.EventHandler(this.txtservices_EditValueChanged);
            // 
            // searchLookUpEdit1View
            // 
            this.searchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.searchLookUpEdit1View.Name = "searchLookUpEdit1View";
            this.searchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.searchLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.lblbookingid);
            this.groupControl1.Controls.Add(this.labelControl9);
            this.groupControl1.Controls.Add(this.lblroomno);
            this.groupControl1.Controls.Add(this.lblguestid);
            this.groupControl1.Controls.Add(this.lblguestname);
            this.groupControl1.Controls.Add(this.lblcheckindate);
            this.groupControl1.Controls.Add(this.labelControl4);
            this.groupControl1.Controls.Add(this.labelControl3);
            this.groupControl1.Controls.Add(this.labelControl2);
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(771, 161);
            this.groupControl1.TabIndex = 64;
            this.groupControl1.Text = "Booking Details";
            // 
            // lblbookingid
            // 
            this.lblbookingid.Appearance.Font = new System.Drawing.Font("Tahoma", 9.25F, System.Drawing.FontStyle.Bold);
            this.lblbookingid.Location = new System.Drawing.Point(115, 119);
            this.lblbookingid.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lblbookingid.Name = "lblbookingid";
            this.lblbookingid.Size = new System.Drawing.Size(92, 18);
            this.lblbookingid.TabIndex = 68;
            this.lblbookingid.Text = "lblbookingid";
            // 
            // labelControl9
            // 
            this.labelControl9.Appearance.Font = new System.Drawing.Font("Tahoma", 9.25F);
            this.labelControl9.Location = new System.Drawing.Point(27, 119);
            this.labelControl9.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.labelControl9.Name = "labelControl9";
            this.labelControl9.Size = new System.Drawing.Size(76, 18);
            this.labelControl9.TabIndex = 67;
            this.labelControl9.Text = "Booking ID:";
            // 
            // lblroomno
            // 
            this.lblroomno.Appearance.Font = new System.Drawing.Font("Tahoma", 9.25F, System.Drawing.FontStyle.Bold);
            this.lblroomno.Location = new System.Drawing.Point(115, 82);
            this.lblroomno.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lblroomno.Name = "lblroomno";
            this.lblroomno.Size = new System.Drawing.Size(103, 18);
            this.lblroomno.TabIndex = 66;
            this.lblroomno.Text = "Checkin Date:";
            // 
            // lblguestid
            // 
            this.lblguestid.Appearance.Font = new System.Drawing.Font("Tahoma", 9.25F, System.Drawing.FontStyle.Bold);
            this.lblguestid.Location = new System.Drawing.Point(115, 44);
            this.lblguestid.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lblguestid.Name = "lblguestid";
            this.lblguestid.Size = new System.Drawing.Size(74, 18);
            this.lblguestid.TabIndex = 65;
            this.lblguestid.Text = "lblguestid";
            // 
            // lblguestname
            // 
            this.lblguestname.Appearance.Font = new System.Drawing.Font("Tahoma", 9.25F, System.Drawing.FontStyle.Bold);
            this.lblguestname.Location = new System.Drawing.Point(485, 82);
            this.lblguestname.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lblguestname.Name = "lblguestname";
            this.lblguestname.Size = new System.Drawing.Size(103, 18);
            this.lblguestname.TabIndex = 64;
            this.lblguestname.Text = "Checkin Date:";
            // 
            // lblcheckindate
            // 
            this.lblcheckindate.Appearance.Font = new System.Drawing.Font("Tahoma", 9.25F, System.Drawing.FontStyle.Bold);
            this.lblcheckindate.Location = new System.Drawing.Point(485, 44);
            this.lblcheckindate.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lblcheckindate.Name = "lblcheckindate";
            this.lblcheckindate.Size = new System.Drawing.Size(103, 18);
            this.lblcheckindate.TabIndex = 63;
            this.lblcheckindate.Text = "Checkin Date:";
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Font = new System.Drawing.Font("Tahoma", 9.25F);
            this.labelControl4.Location = new System.Drawing.Point(390, 44);
            this.labelControl4.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(90, 18);
            this.labelControl4.TabIndex = 62;
            this.labelControl4.Text = "Checkin Date:";
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Tahoma", 9.25F);
            this.labelControl3.Location = new System.Drawing.Point(390, 82);
            this.labelControl3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(87, 18);
            this.labelControl3.TabIndex = 61;
            this.labelControl3.Text = "Guest Name:";
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 9.25F);
            this.labelControl2.Location = new System.Drawing.Point(27, 82);
            this.labelControl2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(71, 18);
            this.labelControl2.TabIndex = 60;
            this.labelControl2.Text = "Room No.:";
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 9.25F);
            this.labelControl1.Location = new System.Drawing.Point(27, 44);
            this.labelControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(64, 18);
            this.labelControl1.TabIndex = 59;
            this.labelControl1.Text = "Guest ID:";
            // 
            // groupControl2
            // 
            this.groupControl2.Controls.Add(this.txttotalamount);
            this.groupControl2.Controls.Add(this.labelControl7);
            this.groupControl2.Controls.Add(this.button1);
            this.groupControl2.Controls.Add(this.txtqty);
            this.groupControl2.Controls.Add(this.labelControl11);
            this.groupControl2.Controls.Add(this.labelControl6);
            this.groupControl2.Controls.Add(this.labelControl5);
            this.groupControl2.Controls.Add(this.txtservices);
            this.groupControl2.Controls.Add(this.txtrate);
            this.groupControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl2.Location = new System.Drawing.Point(0, 161);
            this.groupControl2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(771, 233);
            this.groupControl2.TabIndex = 65;
            this.groupControl2.Text = "HouseKeeping Services";
            // 
            // txttotalamount
            // 
            this.txttotalamount.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.txttotalamount.Font = new System.Drawing.Font("Tahoma", 9.25F);
            this.txttotalamount.Location = new System.Drawing.Point(159, 138);
            this.txttotalamount.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txttotalamount.Name = "txttotalamount";
            this.txttotalamount.Size = new System.Drawing.Size(194, 26);
            this.txttotalamount.TabIndex = 69;
            // 
            // labelControl7
            // 
            this.labelControl7.Appearance.Font = new System.Drawing.Font("Tahoma", 9.25F);
            this.labelControl7.Location = new System.Drawing.Point(25, 142);
            this.labelControl7.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(94, 18);
            this.labelControl7.TabIndex = 68;
            this.labelControl7.Text = "Total Amount:";
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Tahoma", 9.25F);
            this.button1.Location = new System.Drawing.Point(159, 173);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(195, 42);
            this.button1.TabIndex = 67;
            this.button1.Text = "Submit";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtqty
            // 
            this.txtqty.Font = new System.Drawing.Font("Tahoma", 9.25F);
            this.txtqty.Location = new System.Drawing.Point(159, 104);
            this.txtqty.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtqty.Name = "txtqty";
            this.txtqty.Size = new System.Drawing.Size(194, 26);
            this.txtqty.TabIndex = 66;
            this.txtqty.TextChanged += new System.EventHandler(this.txtqty_TextChanged);
            this.txtqty.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtqty_KeyPress);
            // 
            // labelControl11
            // 
            this.labelControl11.Appearance.Font = new System.Drawing.Font("Tahoma", 9.25F);
            this.labelControl11.Location = new System.Drawing.Point(25, 107);
            this.labelControl11.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.labelControl11.Name = "labelControl11";
            this.labelControl11.Size = new System.Drawing.Size(29, 18);
            this.labelControl11.TabIndex = 65;
            this.labelControl11.Text = "Qty:";
            // 
            // labelControl6
            // 
            this.labelControl6.Appearance.Font = new System.Drawing.Font("Tahoma", 9.25F);
            this.labelControl6.Location = new System.Drawing.Point(25, 73);
            this.labelControl6.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(35, 18);
            this.labelControl6.TabIndex = 64;
            this.labelControl6.Text = "Rate:";
            // 
            // labelControl5
            // 
            this.labelControl5.Appearance.Font = new System.Drawing.Font("Tahoma", 9.25F);
            this.labelControl5.Location = new System.Drawing.Point(25, 42);
            this.labelControl5.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(58, 18);
            this.labelControl5.TabIndex = 63;
            this.labelControl5.Text = "Services:";
            // 
            // HotelFrmAddCharges
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(771, 394);
            this.Controls.Add(this.groupControl2);
            this.Controls.Add(this.groupControl1);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "HotelFrmAddCharges";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "HotelFrmAddCharges";
            this.Load += new System.EventHandler(this.HotelFrmAddCharges_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtservices.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            this.groupControl2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        public System.Windows.Forms.TextBox txtrate;
        public DevExpress.XtraEditors.SearchLookUpEdit txtservices;
        private DevExpress.XtraGrid.Views.Grid.GridView searchLookUpEdit1View;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        public System.Windows.Forms.TextBox txtqty;
        private DevExpress.XtraEditors.LabelControl labelControl11;
        private System.Windows.Forms.Button button1;
        public DevExpress.XtraEditors.GroupControl groupControl1;
        public DevExpress.XtraEditors.LabelControl lblroomno;
        public DevExpress.XtraEditors.LabelControl lblguestid;
        public DevExpress.XtraEditors.LabelControl lblguestname;
        public DevExpress.XtraEditors.LabelControl lblcheckindate;
        public System.Windows.Forms.TextBox txttotalamount;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        public DevExpress.XtraEditors.LabelControl lblbookingid;
        private DevExpress.XtraEditors.LabelControl labelControl9;
    }
}