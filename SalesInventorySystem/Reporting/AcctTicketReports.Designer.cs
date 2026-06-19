namespace SalesInventorySystem.Reporting
{
    partial class AcctTicketReports
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
            this.components = new System.ComponentModel.Container();
            this.txtbrcode = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.searchLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lblcredit = new System.Windows.Forms.Label();
            this.lbldebit = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblbranchname = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.updateTicketToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.txtdate = new System.Windows.Forms.DateTimePicker();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.chckboxAllBranch = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.txtbrcode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtbrcode
            // 
            this.txtbrcode.Location = new System.Drawing.Point(149, 39);
            this.txtbrcode.Margin = new System.Windows.Forms.Padding(4);
            this.txtbrcode.Name = "txtbrcode";
            this.txtbrcode.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtbrcode.Properties.Appearance.Options.UseFont = true;
            this.txtbrcode.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtbrcode.Properties.NullText = "";
            this.txtbrcode.Properties.PopupView = this.searchLookUpEdit1View;
            this.txtbrcode.Size = new System.Drawing.Size(132, 24);
            this.txtbrcode.TabIndex = 13;
            this.txtbrcode.EditValueChanged += new System.EventHandler(this.txtbrcode_EditValueChanged);
            // 
            // searchLookUpEdit1View
            // 
            this.searchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.searchLookUpEdit1View.Name = "searchLookUpEdit1View";
            this.searchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.searchLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F, System.Drawing.FontStyle.Bold);
            this.label9.Location = new System.Drawing.Point(438, 70);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(58, 22);
            this.label9.TabIndex = 12;
            this.label9.Text = "User:";
            this.label9.Click += new System.EventHandler(this.label9_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.label8.Location = new System.Drawing.Point(374, 70);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 22);
            this.label8.TabIndex = 11;
            this.label8.Text = "User:";
            this.label8.Click += new System.EventHandler(this.label8_Click);
            // 
            // lblcredit
            // 
            this.lblcredit.AutoSize = true;
            this.lblcredit.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F, System.Drawing.FontStyle.Bold);
            this.lblcredit.Location = new System.Drawing.Point(1091, 70);
            this.lblcredit.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblcredit.Name = "lblcredit";
            this.lblcredit.Size = new System.Drawing.Size(49, 22);
            this.lblcredit.TabIndex = 10;
            this.lblcredit.Text = "0.00";
            this.lblcredit.Click += new System.EventHandler(this.lblcredit_Click);
            // 
            // lbldebit
            // 
            this.lbldebit.AutoSize = true;
            this.lbldebit.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F, System.Drawing.FontStyle.Bold);
            this.lbldebit.Location = new System.Drawing.Point(1091, 38);
            this.lbldebit.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbldebit.Name = "lbldebit";
            this.lbldebit.Size = new System.Drawing.Size(49, 22);
            this.lbldebit.TabIndex = 9;
            this.lbldebit.Text = "0.00";
            this.lbldebit.Click += new System.EventHandler(this.lbldebit_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.label5.Location = new System.Drawing.Point(975, 70);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(109, 22);
            this.label5.TabIndex = 8;
            this.label5.Text = "Total Credit:";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.label4.Location = new System.Drawing.Point(975, 38);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(103, 22);
            this.label4.TabIndex = 7;
            this.label4.Text = "Total Debit:";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // lblbranchname
            // 
            this.lblbranchname.AutoSize = true;
            this.lblbranchname.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F, System.Drawing.FontStyle.Bold);
            this.lblbranchname.Location = new System.Drawing.Point(145, 73);
            this.lblbranchname.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblbranchname.Name = "lblbranchname";
            this.lblbranchname.Size = new System.Drawing.Size(0, 22);
            this.lblbranchname.TabIndex = 4;
            this.lblbranchname.Click += new System.EventHandler(this.lblbranchname_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.label3.Location = new System.Drawing.Point(374, 38);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 22);
            this.label3.TabIndex = 2;
            this.label3.Text = "Date:";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.label2.Location = new System.Drawing.Point(16, 73);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(124, 22);
            this.label2.TabIndex = 1;
            this.label2.Text = "Branch Name:";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.label1.Location = new System.Drawing.Point(16, 41);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(120, 22);
            this.label1.TabIndex = 0;
            this.label1.Text = "Branch Code:";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.White;
            this.groupBox2.Controls.Add(this.gridControl1);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 114);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox2.Size = new System.Drawing.Size(1712, 724);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(4);
            this.gridControl1.Location = new System.Drawing.Point(4, 19);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Margin = new System.Windows.Forms.Padding(4);
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(1704, 701);
            this.gridControl1.TabIndex = 19;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            this.gridControl1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.gridControl1_MouseUp);
            // 
            // gridView1
            // 
            this.gridView1.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridView1.Appearance.HeaderPanel.Options.UseFont = true;
            this.gridView1.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridView1.Appearance.Row.Options.UseFont = true;
            this.gridView1.DetailHeight = 431;
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsBehavior.ReadOnly = true;
            this.gridView1.OptionsView.ColumnAutoWidth = false;
            this.gridView1.OptionsView.RowAutoHeight = true;
            this.gridView1.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.gridView1_RowStyle);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.updateTicketToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(171, 28);
            // 
            // updateTicketToolStripMenuItem
            // 
            this.updateTicketToolStripMenuItem.Name = "updateTicketToolStripMenuItem";
            this.updateTicketToolStripMenuItem.Size = new System.Drawing.Size(170, 24);
            this.updateTicketToolStripMenuItem.Text = "Update Ticket";
            this.updateTicketToolStripMenuItem.Click += new System.EventHandler(this.updateTicketToolStripMenuItem_Click);
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.chckboxAllBranch);
            this.groupControl1.Controls.Add(this.txtdate);
            this.groupControl1.Controls.Add(this.simpleButton1);
            this.groupControl1.Controls.Add(this.label1);
            this.groupControl1.Controls.Add(this.txtbrcode);
            this.groupControl1.Controls.Add(this.label4);
            this.groupControl1.Controls.Add(this.lblbranchname);
            this.groupControl1.Controls.Add(this.label9);
            this.groupControl1.Controls.Add(this.label5);
            this.groupControl1.Controls.Add(this.label8);
            this.groupControl1.Controls.Add(this.label3);
            this.groupControl1.Controls.Add(this.lbldebit);
            this.groupControl1.Controls.Add(this.lblcredit);
            this.groupControl1.Controls.Add(this.label2);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Margin = new System.Windows.Forms.Padding(4);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(1712, 114);
            this.groupControl1.TabIndex = 15;
            this.groupControl1.Paint += new System.Windows.Forms.PaintEventHandler(this.groupControl1_Paint);
            // 
            // txtdate
            // 
            this.txtdate.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.txtdate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.txtdate.Location = new System.Drawing.Point(423, 35);
            this.txtdate.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.txtdate.Name = "txtdate";
            this.txtdate.Size = new System.Drawing.Size(145, 27);
            this.txtdate.TabIndex = 16;
            // 
            // simpleButton1
            // 
            this.simpleButton1.Appearance.Font = new System.Drawing.Font("Tahoma", 9.25F);
            this.simpleButton1.Appearance.Options.UseFont = true;
            this.simpleButton1.ImageOptions.Image = global::SalesInventorySystem.Properties.Resources.Save_16x16__5_;
            this.simpleButton1.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.simpleButton1.Location = new System.Drawing.Point(821, 33);
            this.simpleButton1.Margin = new System.Windows.Forms.Padding(4);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(145, 58);
            this.simpleButton1.TabIndex = 15;
            this.simpleButton1.Text = "Submit";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // chckboxAllBranch
            // 
            this.chckboxAllBranch.AutoSize = true;
            this.chckboxAllBranch.Checked = true;
            this.chckboxAllBranch.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chckboxAllBranch.Location = new System.Drawing.Point(289, 43);
            this.chckboxAllBranch.Margin = new System.Windows.Forms.Padding(4);
            this.chckboxAllBranch.Name = "chckboxAllBranch";
            this.chckboxAllBranch.Size = new System.Drawing.Size(89, 21);
            this.chckboxAllBranch.TabIndex = 17;
            this.chckboxAllBranch.Text = "All Branch";
            this.chckboxAllBranch.UseVisualStyleBackColor = true;
            this.chckboxAllBranch.CheckedChanged += new System.EventHandler(this.chckboxAllBranch_CheckedChanged);
            // 
            // AcctTicketReports
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1712, 838);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupControl1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "AcctTicketReports";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AcctTicketReports";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.AcctTicketReports_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtbrcode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).EndInit();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblbranchname;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblcredit;
        private System.Windows.Forms.Label lbldebit;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem updateTicketToolStripMenuItem;
        private DevExpress.XtraEditors.SearchLookUpEdit txtbrcode;
        private DevExpress.XtraGrid.Views.Grid.GridView searchLookUpEdit1View;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private System.Windows.Forms.DateTimePicker txtdate;
        private System.Windows.Forms.CheckBox chckboxAllBranch;
    }
}