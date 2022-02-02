namespace SalesInventorySystem.POS
{
    partial class POSPaytoMerchant
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
            this.cmdConfirm = new System.Windows.Forms.Button();
            this.groupCreditCardDetails = new System.Windows.Forms.GroupBox();
            this.txtamount = new DevExpress.XtraEditors.SpinEdit();
            this.label2 = new System.Windows.Forms.Label();
            this.radcc = new System.Windows.Forms.RadioButton();
            this.radcash = new System.Windows.Forms.RadioButton();
            this.txtmerchant = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.searchLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.label1 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.txtrefno = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtvouchercode = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupCreditCardDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtamount.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtmerchant.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).BeginInit();
            this.SuspendLayout();
            // 
            // cmdConfirm
            // 
            this.cmdConfirm.BackColor = System.Drawing.Color.Gold;
            this.cmdConfirm.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.cmdConfirm.ForeColor = System.Drawing.Color.Black;
            this.cmdConfirm.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.cmdConfirm.Location = new System.Drawing.Point(12, 216);
            this.cmdConfirm.Margin = new System.Windows.Forms.Padding(4);
            this.cmdConfirm.Name = "cmdConfirm";
            this.cmdConfirm.Size = new System.Drawing.Size(469, 39);
            this.cmdConfirm.TabIndex = 123550;
            this.cmdConfirm.Text = "Submit";
            this.cmdConfirm.UseVisualStyleBackColor = false;
            this.cmdConfirm.Click += new System.EventHandler(this.cmdConfirm_Click);
            // 
            // groupCreditCardDetails
            // 
            this.groupCreditCardDetails.Controls.Add(this.txtamount);
            this.groupCreditCardDetails.Controls.Add(this.label2);
            this.groupCreditCardDetails.Controls.Add(this.radcc);
            this.groupCreditCardDetails.Controls.Add(this.radcash);
            this.groupCreditCardDetails.Controls.Add(this.txtmerchant);
            this.groupCreditCardDetails.Controls.Add(this.label1);
            this.groupCreditCardDetails.Controls.Add(this.label10);
            this.groupCreditCardDetails.Controls.Add(this.txtrefno);
            this.groupCreditCardDetails.Controls.Add(this.label4);
            this.groupCreditCardDetails.Controls.Add(this.txtvouchercode);
            this.groupCreditCardDetails.Controls.Add(this.label3);
            this.groupCreditCardDetails.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F);
            this.groupCreditCardDetails.Location = new System.Drawing.Point(13, 13);
            this.groupCreditCardDetails.Margin = new System.Windows.Forms.Padding(4);
            this.groupCreditCardDetails.Name = "groupCreditCardDetails";
            this.groupCreditCardDetails.Padding = new System.Windows.Forms.Padding(4);
            this.groupCreditCardDetails.Size = new System.Drawing.Size(468, 195);
            this.groupCreditCardDetails.TabIndex = 123549;
            this.groupCreditCardDetails.TabStop = false;
            // 
            // txtamount
            // 
            this.txtamount.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtamount.Location = new System.Drawing.Point(162, 153);
            this.txtamount.Name = "txtamount";
            this.txtamount.Properties.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.txtamount.Properties.Appearance.Options.UseFont = true;
            this.txtamount.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtamount.Size = new System.Drawing.Size(143, 26);
            this.txtamount.TabIndex = 20;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(13, 156);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 18);
            this.label2.TabIndex = 19;
            this.label2.Text = "Amount:";
            // 
            // radcc
            // 
            this.radcc.AutoSize = true;
            this.radcc.Checked = true;
            this.radcc.ForeColor = System.Drawing.Color.White;
            this.radcc.Location = new System.Drawing.Point(273, 20);
            this.radcc.Name = "radcc";
            this.radcc.Size = new System.Drawing.Size(105, 24);
            this.radcc.TabIndex = 18;
            this.radcc.TabStop = true;
            this.radcc.Text = "Credit (F2)";
            this.radcc.UseVisualStyleBackColor = true;
            // 
            // radcash
            // 
            this.radcash.AutoSize = true;
            this.radcash.ForeColor = System.Drawing.Color.White;
            this.radcash.Location = new System.Drawing.Point(162, 20);
            this.radcash.Name = "radcash";
            this.radcash.Size = new System.Drawing.Size(100, 24);
            this.radcash.TabIndex = 17;
            this.radcash.Text = "Cash (F1)";
            this.radcash.UseVisualStyleBackColor = true;
            // 
            // txtmerchant
            // 
            this.txtmerchant.Location = new System.Drawing.Point(162, 84);
            this.txtmerchant.Name = "txtmerchant";
            this.txtmerchant.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10.8F);
            this.txtmerchant.Properties.Appearance.Options.UseFont = true;
            this.txtmerchant.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtmerchant.Properties.NullText = "";
            this.txtmerchant.Properties.PopupView = this.searchLookUpEdit1View;
            this.txtmerchant.Size = new System.Drawing.Size(273, 28);
            this.txtmerchant.TabIndex = 16;
            // 
            // searchLookUpEdit1View
            // 
            this.searchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.searchLookUpEdit1View.Name = "searchLookUpEdit1View";
            this.searchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.searchLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(13, 90);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 18);
            this.label1.TabIndex = 15;
            this.label1.Text = "Merchant:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(35, 277);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(85, 20);
            this.label10.TabIndex = 14;
            this.label10.Text = "Card Type:";
            this.label10.Visible = false;
            // 
            // txtrefno
            // 
            this.txtrefno.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.txtrefno.Location = new System.Drawing.Point(162, 51);
            this.txtrefno.Margin = new System.Windows.Forms.Padding(4);
            this.txtrefno.Name = "txtrefno";
            this.txtrefno.Size = new System.Drawing.Size(273, 27);
            this.txtrefno.TabIndex = 11;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(13, 56);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(120, 18);
            this.label4.TabIndex = 10;
            this.label4.Text = "Reference No.:";
            // 
            // txtvouchercode
            // 
            this.txtvouchercode.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.txtvouchercode.Location = new System.Drawing.Point(162, 119);
            this.txtvouchercode.Margin = new System.Windows.Forms.Padding(4);
            this.txtvouchercode.Name = "txtvouchercode";
            this.txtvouchercode.Size = new System.Drawing.Size(273, 27);
            this.txtvouchercode.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(13, 123);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(115, 18);
            this.label3.TabIndex = 2;
            this.label3.Text = "Voucher Code:";
            // 
            // POSPaytoMerchant
            // 
            this.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(495, 271);
            this.Controls.Add(this.cmdConfirm);
            this.Controls.Add(this.groupCreditCardDetails);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "POSPaytoMerchant";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "POSPaytoMerchant";
            this.Load += new System.EventHandler(this.POSPaytoMerchant_Load);
            this.groupCreditCardDetails.ResumeLayout(false);
            this.groupCreditCardDetails.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtamount.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtmerchant.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.Button cmdConfirm;
        public System.Windows.Forms.GroupBox groupCreditCardDetails;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtrefno;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtvouchercode;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.SearchLookUpEdit txtmerchant;
        private DevExpress.XtraGrid.Views.Grid.GridView searchLookUpEdit1View;
        private System.Windows.Forms.RadioButton radcc;
        private System.Windows.Forms.RadioButton radcash;
        private System.Windows.Forms.Label label2;
        public DevExpress.XtraEditors.SpinEdit txtamount;
    }
}