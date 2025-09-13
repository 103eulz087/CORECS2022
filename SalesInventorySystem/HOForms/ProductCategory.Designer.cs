namespace SalesInventorySystem
{
    partial class ProductCategory
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
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.txtcatid = new System.Windows.Forms.TextBox();
            this.txtcattype = new System.Windows.Forms.TextBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.editDetailsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteCategoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.btncancel = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.updatebtn = new DevExpress.XtraEditors.SimpleButton();
            this.addbtn = new DevExpress.XtraEditors.SimpleButton();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(24, 61);
            this.labelControl1.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(224, 29);
            this.labelControl1.TabIndex = 19;
            this.labelControl1.Text = "Product Category ID:";
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Location = new System.Drawing.Point(24, 108);
            this.labelControl2.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(148, 29);
            this.labelControl2.TabIndex = 20;
            this.labelControl2.Text = "Product Type:";
            // 
            // txtcatid
            // 
            this.txtcatid.Enabled = false;
            this.txtcatid.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtcatid.Location = new System.Drawing.Point(251, 59);
            this.txtcatid.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.txtcatid.Name = "txtcatid";
            this.txtcatid.ReadOnly = true;
            this.txtcatid.Size = new System.Drawing.Size(196, 36);
            this.txtcatid.TabIndex = 21;
            // 
            // txtcattype
            // 
            this.txtcattype.Enabled = false;
            this.txtcattype.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtcattype.Location = new System.Drawing.Point(251, 105);
            this.txtcattype.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.txtcattype.Name = "txtcattype";
            this.txtcattype.Size = new System.Drawing.Size(298, 36);
            this.txtcattype.TabIndex = 22;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Enabled = false;
            this.checkBox1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBox1.Location = new System.Drawing.Point(561, 108);
            this.checkBox1.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(119, 33);
            this.checkBox1.TabIndex = 27;
            this.checkBox1.Text = "Is Vat?";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(3, 45);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(1966, 800);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.dataGridView1_MouseUp);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(36, 36);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editDetailsToolStripMenuItem,
            this.deleteCategoryToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(264, 76);
            // 
            // editDetailsToolStripMenuItem
            // 
            this.editDetailsToolStripMenuItem.Name = "editDetailsToolStripMenuItem";
            this.editDetailsToolStripMenuItem.Size = new System.Drawing.Size(263, 36);
            this.editDetailsToolStripMenuItem.Text = "Edit Details";
            this.editDetailsToolStripMenuItem.Click += new System.EventHandler(this.editDetailsToolStripMenuItem_Click);
            // 
            // deleteCategoryToolStripMenuItem
            // 
            this.deleteCategoryToolStripMenuItem.Name = "deleteCategoryToolStripMenuItem";
            this.deleteCategoryToolStripMenuItem.Size = new System.Drawing.Size(263, 36);
            this.deleteCategoryToolStripMenuItem.Text = "Delete Category";
            this.deleteCategoryToolStripMenuItem.Click += new System.EventHandler(this.deleteCategoryToolStripMenuItem_Click);
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Controls.Add(this.checkBox1);
            this.groupControl1.Controls.Add(this.txtcattype);
            this.groupControl1.Controls.Add(this.btncancel);
            this.groupControl1.Controls.Add(this.labelControl2);
            this.groupControl1.Controls.Add(this.txtcatid);
            this.groupControl1.Controls.Add(this.simpleButton1);
            this.groupControl1.Controls.Add(this.updatebtn);
            this.groupControl1.Controls.Add(this.addbtn);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(1972, 221);
            this.groupControl1.TabIndex = 28;
            // 
            // btncancel
            // 
            this.btncancel.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btncancel.Appearance.Options.UseFont = true;
            this.btncancel.Enabled = false;
            this.btncancel.ImageOptions.Image = global::SalesInventorySystem.Properties.Resources.Cancel_16x16__2_;
            this.btncancel.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btncancel.Location = new System.Drawing.Point(737, 152);
            this.btncancel.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.btncancel.Name = "btncancel";
            this.btncancel.Size = new System.Drawing.Size(150, 38);
            this.btncancel.TabIndex = 26;
            this.btncancel.Text = "Cancel";
            this.btncancel.Click += new System.EventHandler(this.btncancel_Click);
            // 
            // simpleButton1
            // 
            this.simpleButton1.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.simpleButton1.Appearance.Options.UseFont = true;
            this.simpleButton1.ImageOptions.Image = global::SalesInventorySystem.Properties.Resources.New_16x16__5_;
            this.simpleButton1.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.simpleButton1.Location = new System.Drawing.Point(251, 152);
            this.simpleButton1.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(150, 38);
            this.simpleButton1.TabIndex = 23;
            this.simpleButton1.Text = "New";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // updatebtn
            // 
            this.updatebtn.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.updatebtn.Appearance.Options.UseFont = true;
            this.updatebtn.Enabled = false;
            this.updatebtn.ImageOptions.Image = global::SalesInventorySystem.Properties.Resources.UpdateTable_16x16;
            this.updatebtn.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.updatebtn.Location = new System.Drawing.Point(575, 152);
            this.updatebtn.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.updatebtn.Name = "updatebtn";
            this.updatebtn.Size = new System.Drawing.Size(150, 38);
            this.updatebtn.TabIndex = 25;
            this.updatebtn.Text = "Update";
            this.updatebtn.Click += new System.EventHandler(this.updatebtn_Click);
            // 
            // addbtn
            // 
            this.addbtn.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addbtn.Appearance.Options.UseFont = true;
            this.addbtn.Enabled = false;
            this.addbtn.ImageOptions.Image = global::SalesInventorySystem.Properties.Resources.Add_16x16__2_1;
            this.addbtn.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.addbtn.Location = new System.Drawing.Point(413, 152);
            this.addbtn.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.addbtn.Name = "addbtn";
            this.addbtn.Size = new System.Drawing.Size(150, 38);
            this.addbtn.TabIndex = 24;
            this.addbtn.Text = "Add";
            this.addbtn.Click += new System.EventHandler(this.addbtn_Click);
            // 
            // groupControl2
            // 
            this.groupControl2.Controls.Add(this.dataGridView1);
            this.groupControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl2.Location = new System.Drawing.Point(0, 221);
            this.groupControl2.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(1972, 848);
            this.groupControl2.TabIndex = 29;
            // 
            // ProductCategory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1972, 1069);
            this.Controls.Add(this.groupControl2);
            this.Controls.Add(this.groupControl1);
            this.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.Name = "ProductCategory";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ProductCategory";
            this.Load += new System.EventHandler(this.ProductCategory_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private System.Windows.Forms.TextBox txtcatid;
        private System.Windows.Forms.TextBox txtcattype;
        private DevExpress.XtraEditors.SimpleButton btncancel;
        private DevExpress.XtraEditors.SimpleButton updatebtn;
        private DevExpress.XtraEditors.SimpleButton addbtn;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem editDetailsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteCategoryToolStripMenuItem;
        private System.Windows.Forms.CheckBox checkBox1;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.GroupControl groupControl2;
    }
}