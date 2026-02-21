namespace SalesInventorySystem.HOFormsDevEx
{
    partial class ReceivedSTSBatchMode
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
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.txtshipmentno = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            this.txtrefno = new DevExpress.XtraEditors.TextEdit();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.gridControlRcvd = new DevExpress.XtraGrid.GridControl();
            this.gridViewRcvd = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cancelLineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtshipmentno.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtrefno.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlRcvd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewRcvd)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupControl2
            // 
            this.groupControl2.Controls.Add(this.gridControlRcvd);
            this.groupControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl2.Location = new System.Drawing.Point(0, 127);
            this.groupControl2.Margin = new System.Windows.Forms.Padding(6, 3, 6, 3);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(2408, 1256);
            this.groupControl2.TabIndex = 28;
            // 
            // gridControl1
            // 
            this.gridControl1.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.gridControl1.Location = new System.Drawing.Point(761, 59);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(2403, 1215);
            this.gridControl1.TabIndex = 5;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            this.gridControl1.Visible = false;
            // 
            // gridView1
            // 
            this.gridView1.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridView1.Appearance.HeaderPanel.Options.UseFont = true;
            this.gridView1.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridView1.Appearance.Row.Options.UseFont = true;
            this.gridView1.DetailHeight = 673;
            this.gridView1.FixedLineWidth = 3;
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsSelection.MultiSelect = true;
            this.gridView1.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            this.gridView1.OptionsView.ColumnAutoWidth = false;
            this.gridView1.OptionsView.RowAutoHeight = true;
            this.gridView1.OptionsView.ShowIndicator = false;
            this.gridView1.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.gridView1_RowCellStyle);
            this.gridView1.ShowingEditor += new System.ComponentModel.CancelEventHandler(this.gridView1_ShowingEditor);
            // 
            // txtshipmentno
            // 
            this.txtshipmentno.Location = new System.Drawing.Point(148, 58);
            this.txtshipmentno.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.txtshipmentno.Name = "txtshipmentno";
            this.txtshipmentno.Properties.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtshipmentno.Properties.Appearance.Options.UseFont = true;
            this.txtshipmentno.Properties.MaxLength = 13;
            this.txtshipmentno.Properties.ReadOnly = true;
            this.txtshipmentno.Size = new System.Drawing.Size(150, 46);
            this.txtshipmentno.TabIndex = 19;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(26, 64);
            this.labelControl1.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(103, 29);
            this.labelControl1.TabIndex = 18;
            this.labelControl1.Text = "Order #:";
            // 
            // simpleButton2
            // 
            this.simpleButton2.ImageOptions.Image = global::SalesInventorySystem.Properties.Resources.Save_16x16__5_;
            this.simpleButton2.Location = new System.Drawing.Point(308, 58);
            this.simpleButton2.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new System.Drawing.Size(172, 41);
            this.simpleButton2.TabIndex = 91;
            this.simpleButton2.Text = "Save";
            this.simpleButton2.Click += new System.EventHandler(this.simpleButton2_Click);
            // 
            // txtrefno
            // 
            this.txtrefno.Location = new System.Drawing.Point(524, 53);
            this.txtrefno.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.txtrefno.Name = "txtrefno";
            this.txtrefno.Properties.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.txtrefno.Properties.Appearance.Options.UseFont = true;
            this.txtrefno.Properties.MaxLength = 13;
            this.txtrefno.Properties.ReadOnly = true;
            this.txtrefno.Size = new System.Drawing.Size(134, 52);
            this.txtrefno.TabIndex = 102;
            this.txtrefno.Visible = false;
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.gridControl1);
            this.groupControl1.Controls.Add(this.txtrefno);
            this.groupControl1.Controls.Add(this.simpleButton2);
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Controls.Add(this.txtshipmentno);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Margin = new System.Windows.Forms.Padding(6, 3, 6, 3);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(2408, 127);
            this.groupControl1.TabIndex = 27;
            this.groupControl1.Text = "Receive PO/Inventory";
            // 
            // gridControlRcvd
            // 
            this.gridControlRcvd.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControlRcvd.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.gridControlRcvd.Location = new System.Drawing.Point(3, 45);
            this.gridControlRcvd.MainView = this.gridViewRcvd;
            this.gridControlRcvd.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.gridControlRcvd.Name = "gridControlRcvd";
            this.gridControlRcvd.Size = new System.Drawing.Size(2402, 1208);
            this.gridControlRcvd.TabIndex = 4;
            this.gridControlRcvd.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewRcvd});
            this.gridControlRcvd.MouseUp += new System.Windows.Forms.MouseEventHandler(this.gridControlRcvd_MouseUp);
            // 
            // gridViewRcvd
            // 
            this.gridViewRcvd.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridViewRcvd.Appearance.HeaderPanel.Options.UseFont = true;
            this.gridViewRcvd.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridViewRcvd.Appearance.Row.Options.UseFont = true;
            this.gridViewRcvd.DetailHeight = 673;
            this.gridViewRcvd.FixedLineWidth = 3;
            this.gridViewRcvd.GridControl = this.gridControlRcvd;
            this.gridViewRcvd.Name = "gridViewRcvd";
            this.gridViewRcvd.OptionsBehavior.Editable = false;
            this.gridViewRcvd.OptionsBehavior.ReadOnly = true;
            this.gridViewRcvd.OptionsView.ColumnAutoWidth = false;
            this.gridViewRcvd.OptionsView.RowAutoHeight = true;
            this.gridViewRcvd.OptionsView.ShowFooter = true;
            this.gridViewRcvd.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.gridViewRcvd_RowCellStyle);
            this.gridViewRcvd.ShowingEditor += new System.ComponentModel.CancelEventHandler(this.gridViewRcvd_ShowingEditor);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cancelLineToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(213, 40);
            // 
            // cancelLineToolStripMenuItem
            // 
            this.cancelLineToolStripMenuItem.Name = "cancelLineToolStripMenuItem";
            this.cancelLineToolStripMenuItem.Size = new System.Drawing.Size(300, 36);
            this.cancelLineToolStripMenuItem.Text = "Cancel Line";
            this.cancelLineToolStripMenuItem.Click += new System.EventHandler(this.cancelLineToolStripMenuItem_Click);
            // 
            // ReceivedSTSBatchMode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(2408, 1383);
            this.Controls.Add(this.groupControl2);
            this.Controls.Add(this.groupControl1);
            this.Margin = new System.Windows.Forms.Padding(6, 4, 6, 4);
            this.Name = "ReceivedSTSBatchMode";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ReceivedSTSBatchMode";
            this.Load += new System.EventHandler(this.ReceivedSTSBatchMode_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtshipmentno.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtrefno.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlRcvd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewRcvd)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl2;
        public DevExpress.XtraGrid.GridControl gridControl1;
        public DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        public DevExpress.XtraEditors.TextEdit txtshipmentno;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
        private DevExpress.XtraEditors.TextEdit txtrefno;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        public DevExpress.XtraGrid.GridControl gridControlRcvd;
        public DevExpress.XtraGrid.Views.Grid.GridView gridViewRcvd;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem cancelLineToolStripMenuItem;
    }
}