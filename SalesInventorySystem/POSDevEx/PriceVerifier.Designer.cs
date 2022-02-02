namespace SalesInventorySystem.POSDevEx
{
    partial class PriceVerifier
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PriceVerifier));
            this.txtprodname = new System.Windows.Forms.RichTextBox();
            this.txtprice = new DevExpress.XtraEditors.LabelControl();
            this.txtbarcode = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.txtbarcode.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // txtprodname
            // 
            this.txtprodname.BackColor = System.Drawing.Color.IndianRed;
            this.txtprodname.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtprodname.Font = new System.Drawing.Font("Tahoma", 37.75F);
            this.txtprodname.ForeColor = System.Drawing.Color.White;
            this.txtprodname.Location = new System.Drawing.Point(406, 604);
            this.txtprodname.Name = "txtprodname";
            this.txtprodname.Size = new System.Drawing.Size(650, 188);
            this.txtprodname.TabIndex = 4;
            this.txtprodname.Text = "";
            // 
            // txtprice
            // 
            this.txtprice.Appearance.Font = new System.Drawing.Font("Tahoma", 37.8F, System.Drawing.FontStyle.Bold);
            this.txtprice.Appearance.ForeColor = System.Drawing.Color.White;
            this.txtprice.Appearance.Options.UseFont = true;
            this.txtprice.Appearance.Options.UseForeColor = true;
            this.txtprice.Location = new System.Drawing.Point(680, 812);
            this.txtprice.Name = "txtprice";
            this.txtprice.Size = new System.Drawing.Size(140, 76);
            this.txtprice.TabIndex = 3;
            this.txtprice.Text = "0.00";
            // 
            // txtbarcode
            // 
            this.txtbarcode.Location = new System.Drawing.Point(445, 495);
            this.txtbarcode.Name = "txtbarcode";
            this.txtbarcode.Properties.Appearance.BackColor = System.Drawing.Color.IndianRed;
            this.txtbarcode.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 27.8F);
            this.txtbarcode.Properties.Appearance.ForeColor = System.Drawing.Color.White;
            this.txtbarcode.Properties.Appearance.Options.UseBackColor = true;
            this.txtbarcode.Properties.Appearance.Options.UseFont = true;
            this.txtbarcode.Properties.Appearance.Options.UseForeColor = true;
            this.txtbarcode.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.txtbarcode.Size = new System.Drawing.Size(700, 60);
            this.txtbarcode.TabIndex = 1;
            this.txtbarcode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtbarcode_KeyDown);
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 37.8F, System.Drawing.FontStyle.Bold);
            this.labelControl2.Appearance.ForeColor = System.Drawing.Color.White;
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Appearance.Options.UseForeColor = true;
            this.labelControl2.Location = new System.Drawing.Point(620, 812);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(41, 76);
            this.labelControl2.TabIndex = 5;
            this.labelControl2.Text = "P";
            // 
            // PriceVerifier
            // 
            this.Appearance.BackColor = System.Drawing.Color.White;
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayoutStore = System.Windows.Forms.ImageLayout.Stretch;
            this.BackgroundImageStore = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImageStore")));
            this.ClientSize = new System.Drawing.Size(1600, 1174);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.txtprice);
            this.Controls.Add(this.txtprodname);
            this.Controls.Add(this.txtbarcode);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "PriceVerifier";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PriceVerifier";
            this.Load += new System.EventHandler(this.PriceVerifier_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtbarcode.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox txtprodname;
        private DevExpress.XtraEditors.LabelControl txtprice;
        private DevExpress.XtraEditors.TextEdit txtbarcode;
        private DevExpress.XtraEditors.LabelControl labelControl2;
    }
}