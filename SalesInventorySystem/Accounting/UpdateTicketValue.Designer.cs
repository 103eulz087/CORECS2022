namespace SalesInventorySystem.Accounting
{
    partial class UpdateTicketValue
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
            this.txtdebitvalue = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtdebitacctcode = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.searchLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.txtcreditacctcode = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtcreditvalue = new System.Windows.Forms.TextBox();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.txtdebitacctcode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtcreditacctcode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // txtdebitvalue
            // 
            this.txtdebitvalue.Font = new System.Drawing.Font("Tahoma", 10.25F);
            this.txtdebitvalue.Location = new System.Drawing.Point(166, 66);
            this.txtdebitvalue.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtdebitvalue.MaxLength = 200;
            this.txtdebitvalue.Name = "txtdebitvalue";
            this.txtdebitvalue.Size = new System.Drawing.Size(129, 28);
            this.txtdebitvalue.TabIndex = 425;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.label3.Location = new System.Drawing.Point(22, 36);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(145, 22);
            this.label3.TabIndex = 424;
            this.label3.Text = "Debit Acct Code:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.label1.Location = new System.Drawing.Point(22, 122);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(151, 22);
            this.label1.TabIndex = 426;
            this.label1.Text = "Credit Acct Code:";
            // 
            // txtdebitacctcode
            // 
            this.txtdebitacctcode.Location = new System.Drawing.Point(166, 34);
            this.txtdebitacctcode.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtdebitacctcode.Name = "txtdebitacctcode";
            this.txtdebitacctcode.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.25F);
            this.txtdebitacctcode.Properties.Appearance.Options.UseFont = true;
            this.txtdebitacctcode.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtdebitacctcode.Properties.PopupView = this.searchLookUpEdit1View;
            this.txtdebitacctcode.Size = new System.Drawing.Size(244, 24);
            this.txtdebitacctcode.TabIndex = 427;
            // 
            // searchLookUpEdit1View
            // 
            this.searchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.searchLookUpEdit1View.Name = "searchLookUpEdit1View";
            this.searchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.searchLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            // 
            // txtcreditacctcode
            // 
            this.txtcreditacctcode.Location = new System.Drawing.Point(166, 121);
            this.txtcreditacctcode.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtcreditacctcode.Name = "txtcreditacctcode";
            this.txtcreditacctcode.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.25F);
            this.txtcreditacctcode.Properties.Appearance.Options.UseFont = true;
            this.txtcreditacctcode.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtcreditacctcode.Properties.PopupView = this.gridView1;
            this.txtcreditacctcode.Size = new System.Drawing.Size(244, 24);
            this.txtcreditacctcode.TabIndex = 428;
            // 
            // gridView1
            // 
            this.gridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.label2.Location = new System.Drawing.Point(22, 70);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(108, 22);
            this.label2.TabIndex = 429;
            this.label2.Text = "Debit Value:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.label4.Location = new System.Drawing.Point(22, 156);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(114, 22);
            this.label4.TabIndex = 430;
            this.label4.Text = "Credit Value:";
            // 
            // txtcreditvalue
            // 
            this.txtcreditvalue.Font = new System.Drawing.Font("Tahoma", 10.25F);
            this.txtcreditvalue.Location = new System.Drawing.Point(166, 153);
            this.txtcreditvalue.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtcreditvalue.MaxLength = 200;
            this.txtcreditvalue.Name = "txtcreditvalue";
            this.txtcreditvalue.Size = new System.Drawing.Size(129, 28);
            this.txtcreditvalue.TabIndex = 431;
            // 
            // simpleButton1
            // 
            this.simpleButton1.Location = new System.Drawing.Point(166, 198);
            this.simpleButton1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(244, 46);
            this.simpleButton1.TabIndex = 432;
            this.simpleButton1.Text = "Save Changes";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // UpdateTicketValue
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(433, 258);
            this.Controls.Add(this.simpleButton1);
            this.Controls.Add(this.txtcreditvalue);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtcreditacctcode);
            this.Controls.Add(this.txtdebitacctcode);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtdebitvalue);
            this.Controls.Add(this.label3);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "UpdateTicketValue";
            this.Text = "UpdateTicketValue";
            this.Load += new System.EventHandler(this.UpdateTicketValue_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtdebitacctcode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtcreditacctcode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraGrid.Views.Grid.GridView searchLookUpEdit1View;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        public System.Windows.Forms.TextBox txtdebitvalue;
        public DevExpress.XtraEditors.SearchLookUpEdit txtdebitacctcode;
        public DevExpress.XtraEditors.SearchLookUpEdit txtcreditacctcode;
        public System.Windows.Forms.TextBox txtcreditvalue;
    }
}