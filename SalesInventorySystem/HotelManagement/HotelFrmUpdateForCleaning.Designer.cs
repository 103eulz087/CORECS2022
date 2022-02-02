namespace SalesInventorySystem.HotelManagement
{
    partial class HotelFrmUpdateForCleaning
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
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.txtattendant = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.searchLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txtstatus = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.txtroomno = new System.Windows.Forms.TextBox();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.txtremarks = new System.Windows.Forms.RichTextBox();
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.txtattendant.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtstatus.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl6
            // 
            this.labelControl6.Appearance.Font = new System.Drawing.Font("Tahoma", 10.25F);
            this.labelControl6.Location = new System.Drawing.Point(12, 57);
            this.labelControl6.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(80, 21);
            this.labelControl6.TabIndex = 44;
            this.labelControl6.Text = "Attendant:";
            // 
            // txtattendant
            // 
            this.txtattendant.EditValue = "";
            this.txtattendant.Location = new System.Drawing.Point(98, 54);
            this.txtattendant.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtattendant.Name = "txtattendant";
            this.txtattendant.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10.25F);
            this.txtattendant.Properties.Appearance.Options.UseFont = true;
            this.txtattendant.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtattendant.Properties.NullText = "";
            this.txtattendant.Properties.View = this.searchLookUpEdit1View;
            this.txtattendant.Size = new System.Drawing.Size(274, 28);
            this.txtattendant.TabIndex = 45;
            // 
            // searchLookUpEdit1View
            // 
            this.searchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.searchLookUpEdit1View.Name = "searchLookUpEdit1View";
            this.searchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.searchLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 10.25F);
            this.labelControl1.Location = new System.Drawing.Point(12, 92);
            this.labelControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(53, 21);
            this.labelControl1.TabIndex = 46;
            this.labelControl1.Text = "Status:";
            // 
            // txtstatus
            // 
            this.txtstatus.Location = new System.Drawing.Point(98, 89);
            this.txtstatus.Name = "txtstatus";
            this.txtstatus.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10.25F);
            this.txtstatus.Properties.Appearance.Options.UseFont = true;
            this.txtstatus.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtstatus.Properties.Items.AddRange(new object[] {
            "FOR CLEANING",
            "CLEANED"});
            this.txtstatus.Size = new System.Drawing.Size(157, 28);
            this.txtstatus.TabIndex = 47;
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 10.25F);
            this.labelControl2.Location = new System.Drawing.Point(12, 21);
            this.labelControl2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(66, 21);
            this.labelControl2.TabIndex = 48;
            this.labelControl2.Text = "Room #:";
            // 
            // txtroomno
            // 
            this.txtroomno.Font = new System.Drawing.Font("Tahoma", 10.25F);
            this.txtroomno.ForeColor = System.Drawing.Color.Red;
            this.txtroomno.Location = new System.Drawing.Point(98, 18);
            this.txtroomno.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtroomno.Name = "txtroomno";
            this.txtroomno.ReadOnly = true;
            this.txtroomno.Size = new System.Drawing.Size(157, 28);
            this.txtroomno.TabIndex = 91;
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Tahoma", 10.25F);
            this.labelControl3.Location = new System.Drawing.Point(12, 164);
            this.labelControl3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(71, 21);
            this.labelControl3.TabIndex = 92;
            this.labelControl3.Text = "Remarks:";
            // 
            // txtremarks
            // 
            this.txtremarks.Location = new System.Drawing.Point(98, 123);
            this.txtremarks.Name = "txtremarks";
            this.txtremarks.Size = new System.Drawing.Size(274, 116);
            this.txtremarks.TabIndex = 93;
            this.txtremarks.Text = "";
            // 
            // simpleButton2
            // 
            this.simpleButton2.Appearance.Font = new System.Drawing.Font("Tahoma", 10.25F);
            this.simpleButton2.Appearance.Options.UseFont = true;
            this.simpleButton2.Location = new System.Drawing.Point(197, 246);
            this.simpleButton2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new System.Drawing.Size(93, 32);
            this.simpleButton2.TabIndex = 95;
            this.simpleButton2.Text = "Clear";
            this.simpleButton2.Click += new System.EventHandler(this.simpleButton2_Click);
            // 
            // simpleButton1
            // 
            this.simpleButton1.Appearance.Font = new System.Drawing.Font("Tahoma", 10.25F);
            this.simpleButton1.Appearance.Options.UseFont = true;
            this.simpleButton1.Location = new System.Drawing.Point(98, 246);
            this.simpleButton1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(93, 32);
            this.simpleButton1.TabIndex = 94;
            this.simpleButton1.Text = "Submit";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // HotelFrmUpdateForCleaning
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(408, 300);
            this.Controls.Add(this.simpleButton2);
            this.Controls.Add(this.simpleButton1);
            this.Controls.Add(this.txtremarks);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.txtroomno);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.txtstatus);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.txtattendant);
            this.Controls.Add(this.labelControl6);
            this.Name = "HotelFrmUpdateForCleaning";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "HotelFrmUpdateForCleaning";
            this.Load += new System.EventHandler(this.HotelFrmUpdateForCleaning_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtattendant.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtstatus.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl6;
        public DevExpress.XtraEditors.SearchLookUpEdit txtattendant;
        private DevExpress.XtraGrid.Views.Grid.GridView searchLookUpEdit1View;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        public System.Windows.Forms.TextBox txtroomno;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        public DevExpress.XtraEditors.SimpleButton simpleButton2;
        public DevExpress.XtraEditors.SimpleButton simpleButton1;
        public DevExpress.XtraEditors.ComboBoxEdit txtstatus;
        public System.Windows.Forms.RichTextBox txtremarks;
    }
}