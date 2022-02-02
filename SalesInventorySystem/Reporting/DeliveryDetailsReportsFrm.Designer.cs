namespace SalesInventorySystem.Reporting
{
    partial class DeliveryDetailsReportsFrm
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
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.txteffectivitydate = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.radsalesorder = new System.Windows.Forms.RadioButton();
            this.radsts = new System.Windows.Forms.RadioButton();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txtbrcode = new System.Windows.Forms.TextBox();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.txtpo = new System.Windows.Forms.TextBox();
            this.simpleButton10 = new DevExpress.XtraEditors.SimpleButton();
            this.radsummaryview = new System.Windows.Forms.RadioButton();
            this.raddetailedview = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gridControl1.Location = new System.Drawing.Point(4, 19);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(1601, 708);
            this.gridControl1.TabIndex = 2;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridView1.Appearance.HeaderPanel.Options.UseFont = true;
            this.gridView1.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridView1.Appearance.Row.Options.UseFont = true;
            this.gridView1.AppearancePrint.FooterPanel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridView1.AppearancePrint.FooterPanel.Options.UseFont = true;
            this.gridView1.AppearancePrint.GroupFooter.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridView1.AppearancePrint.GroupFooter.Options.UseFont = true;
            this.gridView1.AppearancePrint.GroupRow.Font = new System.Drawing.Font("Tahoma", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridView1.AppearancePrint.GroupRow.ForeColor = System.Drawing.Color.Maroon;
            this.gridView1.AppearancePrint.GroupRow.Options.UseFont = true;
            this.gridView1.AppearancePrint.GroupRow.Options.UseForeColor = true;
            this.gridView1.AppearancePrint.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridView1.AppearancePrint.HeaderPanel.Options.UseFont = true;
            this.gridView1.DetailHeight = 431;
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsView.ColumnAutoWidth = false;
            this.gridView1.OptionsView.RowAutoHeight = true;
            this.gridView1.OptionsView.ShowFooter = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.labelControl2);
            this.groupBox1.Controls.Add(this.txteffectivitydate);
            this.groupBox1.Controls.Add(this.panel1);
            this.groupBox1.Controls.Add(this.labelControl1);
            this.groupBox1.Controls.Add(this.txtbrcode);
            this.groupBox1.Controls.Add(this.labelControl3);
            this.groupBox1.Controls.Add(this.txtpo);
            this.groupBox1.Controls.Add(this.simpleButton10);
            this.groupBox1.Controls.Add(this.radsummaryview);
            this.groupBox1.Controls.Add(this.raddetailedview);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Size = new System.Drawing.Size(1609, 90);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 9.25F);
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Location = new System.Drawing.Point(527, 54);
            this.labelControl2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(104, 18);
            this.labelControl2.TabIndex = 89;
            this.labelControl2.Text = "Effectivity Date:";
            // 
            // txteffectivitydate
            // 
            this.txteffectivitydate.Location = new System.Drawing.Point(653, 52);
            this.txteffectivitydate.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txteffectivitydate.Name = "txteffectivitydate";
            this.txteffectivitydate.Size = new System.Drawing.Size(132, 22);
            this.txteffectivitydate.TabIndex = 88;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.radsalesorder);
            this.panel1.Controls.Add(this.radsts);
            this.panel1.Location = new System.Drawing.Point(168, 49);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(255, 28);
            this.panel1.TabIndex = 87;
            this.panel1.Visible = false;
            // 
            // radsalesorder
            // 
            this.radsalesorder.AutoSize = true;
            this.radsalesorder.Checked = true;
            this.radsalesorder.Location = new System.Drawing.Point(129, 4);
            this.radsalesorder.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.radsalesorder.Name = "radsalesorder";
            this.radsalesorder.Size = new System.Drawing.Size(105, 21);
            this.radsalesorder.TabIndex = 83;
            this.radsalesorder.TabStop = true;
            this.radsalesorder.Text = "Sales Order";
            this.radsalesorder.UseVisualStyleBackColor = true;
            // 
            // radsts
            // 
            this.radsts.AutoSize = true;
            this.radsts.Location = new System.Drawing.Point(4, 4);
            this.radsts.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.radsts.Name = "radsts";
            this.radsts.Size = new System.Drawing.Size(56, 21);
            this.radsts.TabIndex = 82;
            this.radsts.Text = "STS";
            this.radsts.UseVisualStyleBackColor = true;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 9.25F);
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(796, 27);
            this.labelControl1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(88, 18);
            this.labelControl1.TabIndex = 86;
            this.labelControl1.Text = "Branch Code:";
            // 
            // txtbrcode
            // 
            this.txtbrcode.Location = new System.Drawing.Point(901, 22);
            this.txtbrcode.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtbrcode.Name = "txtbrcode";
            this.txtbrcode.Size = new System.Drawing.Size(132, 22);
            this.txtbrcode.TabIndex = 85;
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Tahoma", 9.25F);
            this.labelControl3.Appearance.Options.UseFont = true;
            this.labelControl3.Location = new System.Drawing.Point(527, 27);
            this.labelControl3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(40, 18);
            this.labelControl3.TabIndex = 84;
            this.labelControl3.Text = "PO #:";
            // 
            // txtpo
            // 
            this.txtpo.Location = new System.Drawing.Point(653, 22);
            this.txtpo.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtpo.Name = "txtpo";
            this.txtpo.Size = new System.Drawing.Size(132, 22);
            this.txtpo.TabIndex = 83;
            // 
            // simpleButton10
            // 
            this.simpleButton10.ImageOptions.Image = global::SalesInventorySystem.Properties.Resources.Print_32x32__2_;
            this.simpleButton10.Location = new System.Drawing.Point(16, 14);
            this.simpleButton10.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.simpleButton10.Name = "simpleButton10";
            this.simpleButton10.Size = new System.Drawing.Size(145, 64);
            this.simpleButton10.TabIndex = 82;
            this.simpleButton10.Text = "Print";
            this.simpleButton10.Click += new System.EventHandler(this.simpleButton10_Click);
            // 
            // radsummaryview
            // 
            this.radsummaryview.AutoSize = true;
            this.radsummaryview.Checked = true;
            this.radsummaryview.Location = new System.Drawing.Point(297, 23);
            this.radsummaryview.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.radsummaryview.Name = "radsummaryview";
            this.radsummaryview.Size = new System.Drawing.Size(121, 21);
            this.radsummaryview.TabIndex = 10;
            this.radsummaryview.TabStop = true;
            this.radsummaryview.Text = "Summary View";
            this.radsummaryview.UseVisualStyleBackColor = true;
            this.radsummaryview.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // raddetailedview
            // 
            this.raddetailedview.AutoSize = true;
            this.raddetailedview.Location = new System.Drawing.Point(172, 23);
            this.raddetailedview.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.raddetailedview.Name = "raddetailedview";
            this.raddetailedview.Size = new System.Drawing.Size(114, 21);
            this.raddetailedview.TabIndex = 9;
            this.raddetailedview.Text = "Detailed View";
            this.raddetailedview.UseVisualStyleBackColor = true;
            this.raddetailedview.CheckedChanged += new System.EventHandler(this.raddetailedview_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.gridControl1);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 90);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox2.Size = new System.Drawing.Size(1609, 731);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            // 
            // DeliveryDetailsReportsFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1609, 821);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "DeliveryDetailsReportsFrm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DeliveryDetailsReportsFrm";
            this.Load += new System.EventHandler(this.DeliveryDetailsReportsFrm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        public DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        public DevExpress.XtraGrid.GridControl gridControl1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        public System.Windows.Forms.RadioButton radsummaryview;
        public System.Windows.Forms.RadioButton raddetailedview;
        private DevExpress.XtraEditors.SimpleButton simpleButton10;
        public System.Windows.Forms.TextBox txtpo;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        public System.Windows.Forms.TextBox txtbrcode;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton radsalesorder;
        private System.Windows.Forms.RadioButton radsts;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        public System.Windows.Forms.TextBox txteffectivitydate;
    }
}