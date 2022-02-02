namespace SalesInventorySystem.HOFormsDevEx
{
    partial class ConfirmOrderUpdateSPriceDevEx
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConfirmOrderUpdateSPriceDevEx));
            this.txtdesc = new DevExpress.XtraEditors.TextEdit();
            this.txtseqno = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl11 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.txtsprice = new DevExpress.XtraEditors.SpinEdit();
            this.txtpono = new DevExpress.XtraEditors.TextEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.checkapplytoall = new System.Windows.Forms.CheckBox();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.txtdesc.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtseqno.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtsprice.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtpono.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // txtdesc
            // 
            this.txtdesc.Location = new System.Drawing.Point(117, 76);
            this.txtdesc.Name = "txtdesc";
            this.txtdesc.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.25F);
            this.txtdesc.Properties.Appearance.Options.UseFont = true;
            this.txtdesc.Properties.ReadOnly = true;
            this.txtdesc.Size = new System.Drawing.Size(171, 24);
            this.txtdesc.TabIndex = 28;
            // 
            // txtseqno
            // 
            this.txtseqno.Location = new System.Drawing.Point(117, 45);
            this.txtseqno.Name = "txtseqno";
            this.txtseqno.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.25F);
            this.txtseqno.Properties.Appearance.Options.UseFont = true;
            this.txtseqno.Properties.ReadOnly = true;
            this.txtseqno.Size = new System.Drawing.Size(171, 24);
            this.txtseqno.TabIndex = 27;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 9.25F);
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(14, 80);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(75, 18);
            this.labelControl1.TabIndex = 26;
            this.labelControl1.Text = "Description:";
            // 
            // labelControl11
            // 
            this.labelControl11.Appearance.Font = new System.Drawing.Font("Tahoma", 9.25F);
            this.labelControl11.Appearance.Options.UseFont = true;
            this.labelControl11.Location = new System.Drawing.Point(14, 49);
            this.labelControl11.Name = "labelControl11";
            this.labelControl11.Size = new System.Drawing.Size(96, 18);
            this.labelControl11.TabIndex = 25;
            this.labelControl11.Text = "Sequence No.:";
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 9.25F);
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Location = new System.Drawing.Point(14, 109);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(78, 18);
            this.labelControl2.TabIndex = 29;
            this.labelControl2.Text = "Selling Price:";
            // 
            // txtsprice
            // 
            this.txtsprice.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtsprice.Location = new System.Drawing.Point(117, 106);
            this.txtsprice.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtsprice.Name = "txtsprice";
            this.txtsprice.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.25F);
            this.txtsprice.Properties.Appearance.Options.UseFont = true;
            this.txtsprice.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtsprice.Size = new System.Drawing.Size(171, 24);
            this.txtsprice.TabIndex = 30;
            this.txtsprice.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtsprice_KeyDown);
            // 
            // txtpono
            // 
            this.txtpono.Location = new System.Drawing.Point(117, 17);
            this.txtpono.Name = "txtpono";
            this.txtpono.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.25F);
            this.txtpono.Properties.Appearance.Options.UseFont = true;
            this.txtpono.Properties.ReadOnly = true;
            this.txtpono.Size = new System.Drawing.Size(171, 24);
            this.txtpono.TabIndex = 34;
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Tahoma", 9.25F);
            this.labelControl3.Appearance.Options.UseFont = true;
            this.labelControl3.Location = new System.Drawing.Point(14, 21);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(52, 18);
            this.labelControl3.TabIndex = 33;
            this.labelControl3.Text = "PO No.:";
            // 
            // checkapplytoall
            // 
            this.checkapplytoall.AutoSize = true;
            this.checkapplytoall.Location = new System.Drawing.Point(14, 143);
            this.checkapplytoall.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.checkapplytoall.Name = "checkapplytoall";
            this.checkapplytoall.Size = new System.Drawing.Size(100, 21);
            this.checkapplytoall.TabIndex = 35;
            this.checkapplytoall.Text = "Apply To All";
            this.checkapplytoall.UseVisualStyleBackColor = true;
            // 
            // simpleButton1
            // 
            this.simpleButton1.Appearance.Font = new System.Drawing.Font("Tahoma", 8.5F);
            this.simpleButton1.Appearance.Options.UseFont = true;
            this.simpleButton1.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("simpleButton1.ImageOptions.Image")));
            this.simpleButton1.Location = new System.Drawing.Point(157, 137);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(131, 44);
            this.simpleButton1.TabIndex = 32;
            this.simpleButton1.Text = "Submit";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // ConfirmOrderUpdateSPriceDevEx
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(304, 201);
            this.Controls.Add(this.checkapplytoall);
            this.Controls.Add(this.txtpono);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.simpleButton1);
            this.Controls.Add(this.txtsprice);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.txtdesc);
            this.Controls.Add(this.txtseqno);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.labelControl11);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "ConfirmOrderUpdateSPriceDevEx";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ConfirmOrderUpdateSPriceDevEx";
            this.Load += new System.EventHandler(this.ConfirmOrderUpdateSPriceDevEx_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtdesc.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtseqno.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtsprice.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtpono.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public DevExpress.XtraEditors.TextEdit txtdesc;
        public DevExpress.XtraEditors.TextEdit txtseqno;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl11;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        public DevExpress.XtraEditors.SpinEdit txtsprice;
        public DevExpress.XtraEditors.TextEdit txtpono;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private System.Windows.Forms.CheckBox checkapplytoall;
    }
}