namespace SalesInventorySystem.HOFormsDevEx
{
    partial class TransferByBarcode
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TransferByBarcode));
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.btnprint = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.btnsave = new DevExpress.XtraEditors.SimpleButton();
            this.btnadd = new DevExpress.XtraEditors.SimpleButton();
            this.txtdispatchno = new DevExpress.XtraEditors.TextEdit();
            this.txtbatchno = new DevExpress.XtraEditors.TextEdit();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.radtocomm = new System.Windows.Forms.RadioButton();
            this.radtobigblue = new System.Windows.Forms.RadioButton();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.txtbarcodeno = new DevExpress.XtraEditors.TextEdit();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cancelLineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtdispatchno.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtbatchno.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtbarcodeno.Properties)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gridView1
            // 
            this.gridView1.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridView1.Appearance.HeaderPanel.Options.UseFont = true;
            this.gridView1.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridView1.Appearance.Row.Options.UseFont = true;
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsBehavior.ReadOnly = true;
            this.gridView1.OptionsView.ColumnAutoWidth = false;
            this.gridView1.OptionsView.RowAutoHeight = true;
            this.gridView1.OptionsView.ShowFooter = true;
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gridControl1.Location = new System.Drawing.Point(2, 2);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(1042, 507);
            this.gridControl1.TabIndex = 20;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            this.gridControl1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.gridControl1_MouseUp);
            // 
            // btnprint
            // 
            this.btnprint.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnprint.ImageOptions.Image")));
            this.btnprint.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btnprint.Location = new System.Drawing.Point(822, 37);
            this.btnprint.Name = "btnprint";
            this.btnprint.Size = new System.Drawing.Size(108, 62);
            this.btnprint.TabIndex = 54;
            this.btnprint.Text = "Print";
            this.btnprint.Click += new System.EventHandler(this.btnprint_Click);
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.gridControl1);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 121);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1046, 511);
            this.panelControl1.TabIndex = 3;
            // 
            // btnsave
            // 
            this.btnsave.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnsave.ImageOptions.Image")));
            this.btnsave.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btnsave.Location = new System.Drawing.Point(706, 37);
            this.btnsave.Name = "btnsave";
            this.btnsave.Size = new System.Drawing.Size(108, 62);
            this.btnsave.TabIndex = 52;
            this.btnsave.Text = "Save";
            this.btnsave.Click += new System.EventHandler(this.btnsave_Click);
            // 
            // btnadd
            // 
            this.btnadd.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnadd.ImageOptions.Image")));
            this.btnadd.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btnadd.Location = new System.Drawing.Point(592, 37);
            this.btnadd.Name = "btnadd";
            this.btnadd.Size = new System.Drawing.Size(108, 62);
            this.btnadd.TabIndex = 50;
            this.btnadd.Text = "Add Items";
            this.btnadd.Click += new System.EventHandler(this.btnadd_Click);
            // 
            // txtdispatchno
            // 
            this.txtdispatchno.Location = new System.Drawing.Point(469, 70);
            this.txtdispatchno.Name = "txtdispatchno";
            this.txtdispatchno.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.8F);
            this.txtdispatchno.Properties.Appearance.Options.UseFont = true;
            this.txtdispatchno.Size = new System.Drawing.Size(110, 26);
            this.txtdispatchno.TabIndex = 49;
            // 
            // txtbatchno
            // 
            this.txtbatchno.Location = new System.Drawing.Point(469, 36);
            this.txtbatchno.Name = "txtbatchno";
            this.txtbatchno.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.8F);
            this.txtbatchno.Properties.Appearance.Options.UseFont = true;
            this.txtbatchno.Properties.ReadOnly = true;
            this.txtbatchno.Size = new System.Drawing.Size(110, 26);
            this.txtbatchno.TabIndex = 48;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(367, 74);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(96, 18);
            this.label5.TabIndex = 47;
            this.label5.Text = "Dispatch No.:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(367, 40);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 18);
            this.label3.TabIndex = 46;
            this.label3.Text = "Batch No.:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(18, 76);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 18);
            this.label2.TabIndex = 42;
            this.label2.Text = "Scan SKU:";
            // 
            // radtocomm
            // 
            this.radtocomm.AutoSize = true;
            this.radtocomm.Font = new System.Drawing.Font("Tahoma", 8.8F);
            this.radtocomm.Location = new System.Drawing.Point(181, 37);
            this.radtocomm.Name = "radtocomm";
            this.radtocomm.Size = new System.Drawing.Size(188, 22);
            this.radtocomm.TabIndex = 1;
            this.radtocomm.Text = "Transfer to Commissary";
            this.radtocomm.UseVisualStyleBackColor = true;
            // 
            // radtobigblue
            // 
            this.radtobigblue.AutoSize = true;
            this.radtobigblue.Checked = true;
            this.radtobigblue.Font = new System.Drawing.Font("Tahoma", 8.8F);
            this.radtobigblue.Location = new System.Drawing.Point(21, 37);
            this.radtobigblue.Name = "radtobigblue";
            this.radtobigblue.Size = new System.Drawing.Size(154, 22);
            this.radtobigblue.TabIndex = 0;
            this.radtobigblue.TabStop = true;
            this.radtobigblue.Text = "Transfer to BigBlue";
            this.radtobigblue.UseVisualStyleBackColor = true;
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.txtbarcodeno);
            this.groupControl1.Controls.Add(this.btnprint);
            this.groupControl1.Controls.Add(this.btnsave);
            this.groupControl1.Controls.Add(this.btnadd);
            this.groupControl1.Controls.Add(this.txtdispatchno);
            this.groupControl1.Controls.Add(this.txtbatchno);
            this.groupControl1.Controls.Add(this.label5);
            this.groupControl1.Controls.Add(this.label3);
            this.groupControl1.Controls.Add(this.label2);
            this.groupControl1.Controls.Add(this.radtocomm);
            this.groupControl1.Controls.Add(this.radtobigblue);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(1046, 121);
            this.groupControl1.TabIndex = 2;
            // 
            // txtbarcodeno
            // 
            this.txtbarcodeno.Location = new System.Drawing.Point(95, 70);
            this.txtbarcodeno.Name = "txtbarcodeno";
            this.txtbarcodeno.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.8F);
            this.txtbarcodeno.Properties.Appearance.Options.UseFont = true;
            this.txtbarcodeno.Size = new System.Drawing.Size(260, 26);
            this.txtbarcodeno.TabIndex = 55;
            this.txtbarcodeno.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtbarcodeno_KeyDown);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cancelLineToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(154, 28);
            // 
            // cancelLineToolStripMenuItem
            // 
            this.cancelLineToolStripMenuItem.Name = "cancelLineToolStripMenuItem";
            this.cancelLineToolStripMenuItem.Size = new System.Drawing.Size(153, 24);
            this.cancelLineToolStripMenuItem.Text = "Cancel Line";
            this.cancelLineToolStripMenuItem.Click += new System.EventHandler(this.cancelLineToolStripMenuItem_Click);
            // 
            // TransferByBarcode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1046, 632);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.groupControl1);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "TransferByBarcode";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TransferByBarcode";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.TransferByBarcode_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtdispatchno.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtbatchno.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtbarcodeno.Properties)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraEditors.SimpleButton btnprint;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SimpleButton btnsave;
        private DevExpress.XtraEditors.SimpleButton btnadd;
        private DevExpress.XtraEditors.TextEdit txtdispatchno;
        private DevExpress.XtraEditors.TextEdit txtbatchno;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton radtocomm;
        private System.Windows.Forms.RadioButton radtobigblue;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.TextEdit txtbarcodeno;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem cancelLineToolStripMenuItem;
    }
}