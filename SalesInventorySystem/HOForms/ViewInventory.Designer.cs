namespace SalesInventorySystem
{
    partial class ViewInventory
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ViewInventory));
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.processToPrimalCutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showPrimalCutItemsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.refreshDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reprintBarcodeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.transferToCommisaryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gridControl1.Location = new System.Drawing.Point(3, 20);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(1439, 760);
            this.gridControl1.TabIndex = 0;
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
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsBehavior.ReadOnly = true;
            this.gridView1.OptionsView.ColumnAutoWidth = false;
            this.gridView1.OptionsView.RowAutoHeight = true;
            this.gridView1.OptionsView.ShowFooter = true;
            this.gridView1.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.gridView1_RowCellStyle);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.processToPrimalCutToolStripMenuItem,
            this.showPrimalCutItemsToolStripMenuItem,
            this.refreshDataToolStripMenuItem,
            this.reprintBarcodeToolStripMenuItem,
            this.transferToCommisaryToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(281, 154);
            // 
            // processToPrimalCutToolStripMenuItem
            // 
            this.processToPrimalCutToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("processToPrimalCutToolStripMenuItem.Image")));
            this.processToPrimalCutToolStripMenuItem.Name = "processToPrimalCutToolStripMenuItem";
            this.processToPrimalCutToolStripMenuItem.Size = new System.Drawing.Size(280, 30);
            this.processToPrimalCutToolStripMenuItem.Text = "Process to Primal-Cut";
            this.processToPrimalCutToolStripMenuItem.Click += new System.EventHandler(this.processToPrimalCutToolStripMenuItem_Click);
            // 
            // showPrimalCutItemsToolStripMenuItem
            // 
            this.showPrimalCutItemsToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("showPrimalCutItemsToolStripMenuItem.Image")));
            this.showPrimalCutItemsToolStripMenuItem.Name = "showPrimalCutItemsToolStripMenuItem";
            this.showPrimalCutItemsToolStripMenuItem.Size = new System.Drawing.Size(280, 30);
            this.showPrimalCutItemsToolStripMenuItem.Text = "Show Primal-Cut Items";
            this.showPrimalCutItemsToolStripMenuItem.Click += new System.EventHandler(this.showPrimalCutItemsToolStripMenuItem_Click);
            // 
            // refreshDataToolStripMenuItem
            // 
            this.refreshDataToolStripMenuItem.Name = "refreshDataToolStripMenuItem";
            this.refreshDataToolStripMenuItem.Size = new System.Drawing.Size(280, 30);
            this.refreshDataToolStripMenuItem.Text = "Refresh Data";
            this.refreshDataToolStripMenuItem.Click += new System.EventHandler(this.refreshDataToolStripMenuItem_Click);
            // 
            // reprintBarcodeToolStripMenuItem
            // 
            this.reprintBarcodeToolStripMenuItem.Name = "reprintBarcodeToolStripMenuItem";
            this.reprintBarcodeToolStripMenuItem.Size = new System.Drawing.Size(280, 30);
            this.reprintBarcodeToolStripMenuItem.Text = "Reprint Barcode";
            this.reprintBarcodeToolStripMenuItem.Click += new System.EventHandler(this.reprintBarcodeToolStripMenuItem_Click);
            // 
            // transferToCommisaryToolStripMenuItem
            // 
            this.transferToCommisaryToolStripMenuItem.Name = "transferToCommisaryToolStripMenuItem";
            this.transferToCommisaryToolStripMenuItem.Size = new System.Drawing.Size(280, 30);
            this.transferToCommisaryToolStripMenuItem.Text = "Transfer to Commisary";
            this.transferToCommisaryToolStripMenuItem.Click += new System.EventHandler(this.transferToCommisaryToolStripMenuItem_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.White;
            this.groupBox2.Controls.Add(this.gridControl1);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox2.Size = new System.Drawing.Size(1445, 784);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            // 
            // ViewInventory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1445, 784);
            this.Controls.Add(this.groupBox2);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "ViewInventory";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "BigBlue Inventory";
            this.Load += new System.EventHandler(this.ViewInventory_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem processToPrimalCutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showPrimalCutItemsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem refreshDataToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ToolStripMenuItem reprintBarcodeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem transferToCommisaryToolStripMenuItem;
    }
}