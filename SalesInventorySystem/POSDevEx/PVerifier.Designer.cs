namespace SalesInventorySystem.POSDevEx
{
    partial class PVerifier
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
            this.txtbarcode = new DevExpress.XtraEditors.TextEdit();
            this.txtprice = new DevExpress.XtraEditors.LabelControl();
            this.txtprodname = new System.Windows.Forms.RichTextBox();
            //this.tablePanel1 = new DevExpress.Utils.Layout.TablePanel();
            ((System.ComponentModel.ISupportInitialize)(this.txtbarcode.Properties)).BeginInit();
            //((System.ComponentModel.ISupportInitialize)(this.tablePanel1)).BeginInit();
            //this.tablePanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtbarcode
            // 
            //this.tablePanel1.SetColumn(this.txtbarcode, 2);
            this.txtbarcode.Location = new System.Drawing.Point(373, 266);
            this.txtbarcode.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtbarcode.Name = "txtbarcode";
            this.txtbarcode.Properties.Appearance.BackColor = System.Drawing.Color.PaleVioletRed;
            this.txtbarcode.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 27.8F);
            this.txtbarcode.Properties.Appearance.ForeColor = System.Drawing.Color.White;
            this.txtbarcode.Properties.Appearance.Options.UseBackColor = true;
            this.txtbarcode.Properties.Appearance.Options.UseFont = true;
            this.txtbarcode.Properties.Appearance.Options.UseForeColor = true;
            this.txtbarcode.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            //this.tablePanel1.SetRow(this.txtbarcode, 1);
            this.txtbarcode.Size = new System.Drawing.Size(442, 50);
            this.txtbarcode.TabIndex = 0;
            this.txtbarcode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtbarcode_KeyDown);
            // 
            // txtprice
            // 
            this.txtprice.Appearance.Font = new System.Drawing.Font("Tahoma", 40.8F);
            this.txtprice.Appearance.ForeColor = System.Drawing.Color.White;
            this.txtprice.Appearance.Options.UseFont = true;
            this.txtprice.Appearance.Options.UseForeColor = true;
            //this.tablePanel1.SetColumn(this.txtprice, 2);
            this.txtprice.Location = new System.Drawing.Point(373, 630);
            this.txtprice.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtprice.Name = "txtprice";
            //this.tablePanel1.SetRow(this.txtprice, 3);
            this.txtprice.Size = new System.Drawing.Size(103, 65);
            this.txtprice.TabIndex = 2;
            this.txtprice.Text = "0.00";
            this.txtprice.Click += new System.EventHandler(this.txtprice_Click);
            // 
            // txtprodname
            // 
            this.txtprodname.BackColor = System.Drawing.Color.PaleVioletRed;
            this.txtprodname.BorderStyle = System.Windows.Forms.BorderStyle.None;
            //this.tablePanel1.SetColumn(this.txtprodname, 2);
            this.txtprodname.Font = new System.Drawing.Font("Tahoma", 30.75F);
            this.txtprodname.ForeColor = System.Drawing.Color.White;
            this.txtprodname.Location = new System.Drawing.Point(373, 422);
            this.txtprodname.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtprodname.Name = "txtprodname";
            //this.tablePanel1.SetRow(this.txtprodname, 2);
            this.txtprodname.Size = new System.Drawing.Size(442, 137);
            this.txtprodname.TabIndex = 1;
            this.txtprodname.Text = "";
            // 
            // tablePanel1
            // 
            //this.tablePanel1.Columns.AddRange(new DevExpress.Utils.Layout.TablePanelColumn[] {
            //new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 27.56F),
            //new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 21.72F),
            //new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 59.74F),
            //new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 50.98F)});
            //this.tablePanel1.Controls.Add(this.txtprice);
            //this.tablePanel1.Controls.Add(this.txtprodname);
            //this.tablePanel1.Controls.Add(this.txtbarcode);
            //this.tablePanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            //this.tablePanel1.Location = new System.Drawing.Point(0, 0);
            //this.tablePanel1.Name = "tablePanel1";
            //this.tablePanel1.Rows.AddRange(new DevExpress.Utils.Layout.TablePanelRow[] {
            //new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 213F),
            //new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 157F),
            //new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 242F),
            //new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 101F),
            //new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 26F)});
            //this.tablePanel1.Size = new System.Drawing.Size(1200, 900);
            //this.tablePanel1.TabIndex = 3;
            // 
            // PVerifier
            // 
            this.Appearance.BackColor = System.Drawing.Color.Red;
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 900);
            //this.Controls.Add(this.tablePanel1);
            this.FormBorderEffect = DevExpress.XtraEditors.FormBorderEffect.None;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "PVerifier";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PVerifier";
            this.Load += new System.EventHandler(this.PVerifier_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtbarcode.Properties)).EndInit();
            //((System.ComponentModel.ISupportInitialize)(this.tablePanel1)).EndInit();
            //this.tablePanel1.ResumeLayout(false);
            //this.tablePanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.TextEdit txtbarcode;
        //private DevExpress.Utils.Layout.TablePanel tablePanel1;
        private DevExpress.XtraEditors.LabelControl txtprice;
        private System.Windows.Forms.RichTextBox txtprodname;
    }
}