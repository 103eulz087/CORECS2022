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
            this.gridView1.DetailHeight = 547;
            this.gridView1.FixedLineWidth = 3;
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
            this.gridControl1.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(6);
            this.gridControl1.Location = new System.Drawing.Point(3, 3);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Margin = new System.Windows.Forms.Padding(6);
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(1788, 792);
            this.gridControl1.TabIndex = 20;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            this.gridControl1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.gridControl1_MouseUp);
            // 
            // btnprint
            // 
            this.btnprint.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnprint.ImageOptions.Image")));
            this.btnprint.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btnprint.Location = new System.Drawing.Point(1409, 58);
            this.btnprint.Margin = new System.Windows.Forms.Padding(6, 4, 6, 4);
            this.btnprint.Name = "btnprint";
            this.btnprint.Size = new System.Drawing.Size(186, 97);
            this.btnprint.TabIndex = 54;
            this.btnprint.Text = "Print";
            this.btnprint.Click += new System.EventHandler(this.btnprint_Click);
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.gridControl1);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 189);
            this.panelControl1.Margin = new System.Windows.Forms.Padding(6, 4, 6, 4);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1794, 798);
            this.panelControl1.TabIndex = 3;
            // 
            // btnsave
            // 
            this.btnsave.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnsave.ImageOptions.Image")));
            this.btnsave.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btnsave.Location = new System.Drawing.Point(1211, 58);
            this.btnsave.Margin = new System.Windows.Forms.Padding(6, 4, 6, 4);
            this.btnsave.Name = "btnsave";
            this.btnsave.Size = new System.Drawing.Size(186, 97);
            this.btnsave.TabIndex = 52;
            this.btnsave.Text = "Save";
            this.btnsave.Click += new System.EventHandler(this.btnsave_Click);
            // 
            // btnadd
            // 
            this.btnadd.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnadd.ImageOptions.Image")));
            this.btnadd.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btnadd.Location = new System.Drawing.Point(1015, 58);
            this.btnadd.Margin = new System.Windows.Forms.Padding(6, 4, 6, 4);
            this.btnadd.Name = "btnadd";
            this.btnadd.Size = new System.Drawing.Size(186, 97);
            this.btnadd.TabIndex = 50;
            this.btnadd.Text = "Add Items";
            this.btnadd.Click += new System.EventHandler(this.btnadd_Click);
            // 
            // txtdispatchno
            // 
            this.txtdispatchno.Location = new System.Drawing.Point(804, 109);
            this.txtdispatchno.Margin = new System.Windows.Forms.Padding(6, 4, 6, 4);
            this.txtdispatchno.Name = "txtdispatchno";
            this.txtdispatchno.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.8F);
            this.txtdispatchno.Properties.Appearance.Options.UseFont = true;
            this.txtdispatchno.Size = new System.Drawing.Size(188, 46);
            this.txtdispatchno.TabIndex = 49;
            // 
            // txtbatchno
            // 
            this.txtbatchno.Location = new System.Drawing.Point(804, 57);
            this.txtbatchno.Margin = new System.Windows.Forms.Padding(6, 4, 6, 4);
            this.txtbatchno.Name = "txtbatchno";
            this.txtbatchno.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.8F);
            this.txtbatchno.Properties.Appearance.Options.UseFont = true;
            this.txtbatchno.Properties.ReadOnly = true;
            this.txtbatchno.Size = new System.Drawing.Size(188, 46);
            this.txtbatchno.TabIndex = 48;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(630, 115);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(155, 29);
            this.label5.TabIndex = 47;
            this.label5.Text = "Dispatch No.:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(630, 62);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(124, 29);
            this.label3.TabIndex = 46;
            this.label3.Text = "Batch No.:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(31, 118);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(122, 29);
            this.label2.TabIndex = 42;
            this.label2.Text = "Scan SKU:";
            // 
            // radtocomm
            // 
            this.radtocomm.AutoSize = true;
            this.radtocomm.Font = new System.Drawing.Font("Tahoma", 8.8F);
            this.radtocomm.Location = new System.Drawing.Point(310, 58);
            this.radtocomm.Margin = new System.Windows.Forms.Padding(6, 4, 6, 4);
            this.radtocomm.Name = "radtocomm";
            this.radtocomm.Size = new System.Drawing.Size(299, 33);
            this.radtocomm.TabIndex = 1;
            this.radtocomm.Text = "Transfer to Commissary";
            this.radtocomm.UseVisualStyleBackColor = true;
            // 
            // radtobigblue
            // 
            this.radtobigblue.AutoSize = true;
            this.radtobigblue.Checked = true;
            this.radtobigblue.Font = new System.Drawing.Font("Tahoma", 8.8F);
            this.radtobigblue.Location = new System.Drawing.Point(36, 58);
            this.radtobigblue.Margin = new System.Windows.Forms.Padding(6, 4, 6, 4);
            this.radtobigblue.Name = "radtobigblue";
            this.radtobigblue.Size = new System.Drawing.Size(248, 33);
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
            this.groupControl1.Margin = new System.Windows.Forms.Padding(6, 4, 6, 4);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(1794, 189);
            this.groupControl1.TabIndex = 2;
            // 
            // txtbarcodeno
            // 
            this.txtbarcodeno.Location = new System.Drawing.Point(163, 109);
            this.txtbarcodeno.Margin = new System.Windows.Forms.Padding(6, 4, 6, 4);
            this.txtbarcodeno.Name = "txtbarcodeno";
            this.txtbarcodeno.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.8F);
            this.txtbarcodeno.Properties.Appearance.Options.UseFont = true;
            this.txtbarcodeno.Size = new System.Drawing.Size(446, 46);
            this.txtbarcodeno.TabIndex = 55;
            this.txtbarcodeno.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtbarcodeno_KeyDown);
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
            this.cancelLineToolStripMenuItem.Size = new System.Drawing.Size(212, 36);
            this.cancelLineToolStripMenuItem.Text = "Cancel Line";
            this.cancelLineToolStripMenuItem.Click += new System.EventHandler(this.cancelLineToolStripMenuItem_Click);
            // 
            // TransferByBarcode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1794, 987);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.groupControl1);
            this.Name = "TransferByBarcode";
            this.Text = "TransferByBarcode";
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