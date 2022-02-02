namespace SalesInventorySystem.HotelManagement
{
    partial class HotelFrmGuestLedger
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabledger = new System.Windows.Forms.TabPage();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.gridControlLEdger = new DevExpress.XtraGrid.GridControl();
            this.gridViewLedger = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.txtacctbalance = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtacctname = new System.Windows.Forms.TextBox();
            this.tabControl1.SuspendLayout();
            this.tabledger.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlLEdger)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewLedger)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabledger);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.tabControl1.Location = new System.Drawing.Point(0, 95);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(4);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1322, 603);
            this.tabControl1.TabIndex = 1;
            // 
            // tabledger
            // 
            this.tabledger.Controls.Add(this.groupBox4);
            this.tabledger.Location = new System.Drawing.Point(4, 29);
            this.tabledger.Margin = new System.Windows.Forms.Padding(4);
            this.tabledger.Name = "tabledger";
            this.tabledger.Padding = new System.Windows.Forms.Padding(4);
            this.tabledger.Size = new System.Drawing.Size(1314, 570);
            this.tabledger.TabIndex = 0;
            this.tabledger.Text = "Account History";
            this.tabledger.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.gridControlLEdger);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox4.Location = new System.Drawing.Point(4, 4);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox4.Size = new System.Drawing.Size(1306, 562);
            this.groupBox4.TabIndex = 10;
            this.groupBox4.TabStop = false;
            // 
            // gridControlLEdger
            // 
            this.gridControlLEdger.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControlLEdger.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(4);
            this.gridControlLEdger.Location = new System.Drawing.Point(4, 24);
            this.gridControlLEdger.MainView = this.gridViewLedger;
            this.gridControlLEdger.Margin = new System.Windows.Forms.Padding(4);
            this.gridControlLEdger.Name = "gridControlLEdger";
            this.gridControlLEdger.Size = new System.Drawing.Size(1298, 534);
            this.gridControlLEdger.TabIndex = 5;
            this.gridControlLEdger.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewLedger});
            // 
            // gridViewLedger
            // 
            this.gridViewLedger.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridViewLedger.Appearance.HeaderPanel.Options.UseFont = true;
            this.gridViewLedger.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridViewLedger.Appearance.Row.Options.UseFont = true;
            this.gridViewLedger.GridControl = this.gridControlLEdger;
            this.gridViewLedger.Name = "gridViewLedger";
            this.gridViewLedger.OptionsBehavior.Editable = false;
            this.gridViewLedger.OptionsBehavior.ReadOnly = true;
            this.gridViewLedger.OptionsPrint.AutoWidth = false;
            this.gridViewLedger.OptionsView.ColumnAutoWidth = false;
            this.gridViewLedger.OptionsView.RowAutoHeight = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.txtacctbalance);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.txtacctname);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1322, 95);
            this.panel1.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label2.Location = new System.Drawing.Point(13, 13);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(145, 28);
            this.label2.TabIndex = 426;
            this.label2.Text = "Account Name:";
            // 
            // txtacctbalance
            // 
            this.txtacctbalance.Font = new System.Drawing.Font("Tahoma", 10.25F);
            this.txtacctbalance.Location = new System.Drawing.Point(168, 49);
            this.txtacctbalance.Margin = new System.Windows.Forms.Padding(4);
            this.txtacctbalance.MaxLength = 10;
            this.txtacctbalance.Name = "txtacctbalance";
            this.txtacctbalance.ReadOnly = true;
            this.txtacctbalance.Size = new System.Drawing.Size(231, 28);
            this.txtacctbalance.TabIndex = 429;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label4.Location = new System.Drawing.Point(11, 49);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(159, 28);
            this.label4.TabIndex = 427;
            this.label4.Text = "Account Balance:";
            // 
            // txtacctname
            // 
            this.txtacctname.Font = new System.Drawing.Font("Tahoma", 10.25F);
            this.txtacctname.Location = new System.Drawing.Point(168, 13);
            this.txtacctname.Margin = new System.Windows.Forms.Padding(4);
            this.txtacctname.MaxLength = 10;
            this.txtacctname.Name = "txtacctname";
            this.txtacctname.ReadOnly = true;
            this.txtacctname.Size = new System.Drawing.Size(325, 28);
            this.txtacctname.TabIndex = 428;
            // 
            // HotelFrmGuestLedger
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1322, 698);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.panel1);
            this.Name = "HotelFrmGuestLedger";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "HotelFrmGuestLedger";
            this.Load += new System.EventHandler(this.HotelFrmGuestLedger_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabledger.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlLEdger)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewLedger)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabledger;
        private System.Windows.Forms.GroupBox groupBox4;
        public DevExpress.XtraGrid.GridControl gridControlLEdger;
        public DevExpress.XtraGrid.Views.Grid.GridView gridViewLedger;
        private System.Windows.Forms.Panel panel1;
        internal System.Windows.Forms.Label label2;
        public System.Windows.Forms.TextBox txtacctbalance;
        internal System.Windows.Forms.Label label4;
        public System.Windows.Forms.TextBox txtacctname;
    }
}