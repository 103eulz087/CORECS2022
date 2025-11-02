namespace SalesInventorySystem.POS
{
    partial class POSXread
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
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            this.txttransactiondate = new DevExpress.XtraEditors.TextEdit();
            this.lblDateOpen = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.txttransactionno = new DevExpress.XtraEditors.TextEdit();
            this.label30 = new System.Windows.Forms.Label();
            this.gridControl2 = new DevExpress.XtraGrid.GridControl();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.txttransactiondate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txttransactionno.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            this.SuspendLayout();
            // 
            // simpleButton2
            // 
            this.simpleButton2.ImageOptions.Image = global::SalesInventorySystem.Properties.Resources.Cancel_32x32;
            this.simpleButton2.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.simpleButton2.Location = new System.Drawing.Point(175, 92);
            this.simpleButton2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new System.Drawing.Size(125, 44);
            this.simpleButton2.TabIndex = 123644;
            this.simpleButton2.Text = "Cancel";
            // 
            // txttransactiondate
            // 
            this.txttransactiondate.Location = new System.Drawing.Point(143, 6);
            this.txttransactiondate.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txttransactiondate.Name = "txttransactiondate";
            this.txttransactiondate.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.txttransactiondate.Properties.Appearance.Options.UseFont = true;
            this.txttransactiondate.Size = new System.Drawing.Size(157, 24);
            this.txttransactiondate.TabIndex = 123641;
            // 
            // lblDateOpen
            // 
            this.lblDateOpen.AutoSize = true;
            this.lblDateOpen.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDateOpen.ForeColor = System.Drawing.Color.Red;
            this.lblDateOpen.Location = new System.Drawing.Point(11, 9);
            this.lblDateOpen.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblDateOpen.Name = "lblDateOpen";
            this.lblDateOpen.Size = new System.Drawing.Size(125, 18);
            this.lblDateOpen.TabIndex = 123638;
            this.lblDateOpen.Text = "Transaction Date:";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.ForeColor = System.Drawing.Color.Red;
            this.checkBox1.Location = new System.Drawing.Point(14, 69);
            this.checkBox1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(131, 17);
            this.checkBox1.TabIndex = 123640;
            this.checkBox1.Text = "Print Summary Report";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // simpleButton1
            // 
            this.simpleButton1.ImageOptions.Image = global::SalesInventorySystem.Properties.Resources.GenerateData_32x32;
            this.simpleButton1.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.simpleButton1.Location = new System.Drawing.Point(13, 92);
            this.simpleButton1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(157, 44);
            this.simpleButton1.TabIndex = 123643;
            this.simpleButton1.Text = "Submit";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // txttransactionno
            // 
            this.txttransactionno.Location = new System.Drawing.Point(143, 36);
            this.txttransactionno.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txttransactionno.Name = "txttransactionno";
            this.txttransactionno.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.txttransactionno.Properties.Appearance.Options.UseFont = true;
            this.txttransactionno.Size = new System.Drawing.Size(157, 24);
            this.txttransactionno.TabIndex = 123642;
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label30.ForeColor = System.Drawing.Color.Red;
            this.label30.Location = new System.Drawing.Point(11, 39);
            this.label30.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(105, 18);
            this.label30.TabIndex = 123639;
            this.label30.Text = "Transaction #:";
            // 
            // gridControl2
            // 
            this.gridControl2.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.gridControl2.Font = new System.Drawing.Font("Tahoma", 7.25F);
            this.gridControl2.Location = new System.Drawing.Point(11, 193);
            this.gridControl2.MainView = this.gridView2;
            this.gridControl2.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.gridControl2.Name = "gridControl2";
            this.gridControl2.Size = new System.Drawing.Size(262, 121);
            this.gridControl2.TabIndex = 123645;
            this.gridControl2.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView2});
            // 
            // gridView2
            // 
            this.gridView2.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridView2.Appearance.HeaderPanel.Options.UseFont = true;
            this.gridView2.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 10.25F);
            this.gridView2.Appearance.Row.Options.UseFont = true;
            this.gridView2.GridControl = this.gridControl2;
            this.gridView2.LevelIndent = 0;
            this.gridView2.Name = "gridView2";
            this.gridView2.OptionsBehavior.Editable = false;
            this.gridView2.OptionsBehavior.ReadOnly = true;
            this.gridView2.OptionsSelection.MultiSelect = true;
            this.gridView2.OptionsView.ColumnAutoWidth = false;
            this.gridView2.OptionsView.RowAutoHeight = true;
            this.gridView2.PreviewIndent = 0;
            // 
            // POSXread
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(314, 147);
            this.Controls.Add(this.gridControl2);
            this.Controls.Add(this.simpleButton2);
            this.Controls.Add(this.txttransactiondate);
            this.Controls.Add(this.lblDateOpen);
            this.Controls.Add(this.label30);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.txttransactionno);
            this.Controls.Add(this.simpleButton1);
            this.Name = "POSXread";
            this.Text = "POSXread";
            this.Load += new System.EventHandler(this.POSXread_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txttransactiondate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txttransactionno.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton simpleButton2;
        public DevExpress.XtraEditors.TextEdit txttransactiondate;
        internal System.Windows.Forms.Label lblDateOpen;
        private System.Windows.Forms.CheckBox checkBox1;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.TextEdit txttransactionno;
        internal System.Windows.Forms.Label label30;
        public DevExpress.XtraGrid.GridControl gridControl2;
        public DevExpress.XtraGrid.Views.Grid.GridView gridView2;
    }
}