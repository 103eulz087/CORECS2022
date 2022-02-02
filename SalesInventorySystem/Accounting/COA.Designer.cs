namespace SalesInventorySystem.Accounting
{
    partial class COA
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtbranch = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.simpleButton4 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton3 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.txtlevelnum = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.txtaccttype = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.txtmotheracct = new DevExpress.XtraEditors.TextEdit();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.txtaccttitle = new DevExpress.XtraEditors.TextEdit();
            this.txtacctcode = new DevExpress.XtraEditors.TextEdit();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.refreshDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtbranch.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtlevelnum.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtaccttype.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtmotheracct.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtaccttitle.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtacctcode.Properties)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.White;
            this.groupBox1.Controls.Add(this.treeView1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(408, 482);
            this.groupBox1.TabIndex = 15;
            this.groupBox1.TabStop = false;
            // 
            // treeView1
            // 
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.Font = new System.Drawing.Font("Tahoma", 10.25F);
            this.treeView1.Location = new System.Drawing.Point(3, 16);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(402, 463);
            this.treeView1.TabIndex = 0;
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            this.treeView1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.treeView1_MouseUp);
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.White;
            this.groupBox2.Controls.Add(this.txtbranch);
            this.groupBox2.Controls.Add(this.labelControl8);
            this.groupBox2.Controls.Add(this.radioButton2);
            this.groupBox2.Controls.Add(this.radioButton1);
            this.groupBox2.Controls.Add(this.labelControl7);
            this.groupBox2.Controls.Add(this.labelControl6);
            this.groupBox2.Controls.Add(this.simpleButton4);
            this.groupBox2.Controls.Add(this.simpleButton3);
            this.groupBox2.Controls.Add(this.simpleButton2);
            this.groupBox2.Controls.Add(this.simpleButton1);
            this.groupBox2.Controls.Add(this.labelControl1);
            this.groupBox2.Controls.Add(this.labelControl2);
            this.groupBox2.Controls.Add(this.txtlevelnum);
            this.groupBox2.Controls.Add(this.labelControl3);
            this.groupBox2.Controls.Add(this.txtaccttype);
            this.groupBox2.Controls.Add(this.labelControl4);
            this.groupBox2.Controls.Add(this.txtmotheracct);
            this.groupBox2.Controls.Add(this.labelControl5);
            this.groupBox2.Controls.Add(this.txtaccttitle);
            this.groupBox2.Controls.Add(this.txtacctcode);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(408, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(450, 482);
            this.groupBox2.TabIndex = 16;
            this.groupBox2.TabStop = false;
            // 
            // txtbranch
            // 
            this.txtbranch.Location = new System.Drawing.Point(126, 348);
            this.txtbranch.Name = "txtbranch";
            this.txtbranch.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10.25F);
            this.txtbranch.Properties.Appearance.Options.UseFont = true;
            this.txtbranch.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtbranch.Properties.ReadOnly = true;
            this.txtbranch.Size = new System.Drawing.Size(100, 24);
            this.txtbranch.TabIndex = 20;
            this.txtbranch.Click += new System.EventHandler(this.txtbranch_Click);
            // 
            // labelControl8
            // 
            this.labelControl8.Appearance.Font = new System.Drawing.Font("Tahoma", 10.25F);
            this.labelControl8.Location = new System.Drawing.Point(72, 351);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(48, 17);
            this.labelControl8.TabIndex = 19;
            this.labelControl8.Text = "Branch:";
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Enabled = false;
            this.radioButton2.Font = new System.Drawing.Font("Tahoma", 10.25F);
            this.radioButton2.Location = new System.Drawing.Point(180, 263);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(41, 21);
            this.radioButton2.TabIndex = 18;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "SL";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Enabled = false;
            this.radioButton1.Font = new System.Drawing.Font("Tahoma", 10.25F);
            this.radioButton1.Location = new System.Drawing.Point(137, 263);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(42, 21);
            this.radioButton1.TabIndex = 17;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "GL";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // labelControl7
            // 
            this.labelControl7.Appearance.Font = new System.Drawing.Font("Tahoma", 10.25F);
            this.labelControl7.Location = new System.Drawing.Point(71, 265);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(49, 17);
            this.labelControl7.TabIndex = 16;
            this.labelControl7.Text = "GL / SL:";
            // 
            // labelControl6
            // 
            this.labelControl6.Appearance.Font = new System.Drawing.Font("Tahoma", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl6.Location = new System.Drawing.Point(110, 20);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(239, 33);
            this.labelControl6.TabIndex = 15;
            this.labelControl6.Text = "Chart of Accounts";
            // 
            // simpleButton4
            // 
            this.simpleButton4.Location = new System.Drawing.Point(336, 404);
            this.simpleButton4.Name = "simpleButton4";
            this.simpleButton4.Size = new System.Drawing.Size(86, 38);
            this.simpleButton4.TabIndex = 14;
            this.simpleButton4.Text = "Cancel";
            this.simpleButton4.Click += new System.EventHandler(this.simpleButton4_Click);
            // 
            // simpleButton3
            // 
            this.simpleButton3.Enabled = false;
            this.simpleButton3.Location = new System.Drawing.Point(234, 404);
            this.simpleButton3.Name = "simpleButton3";
            this.simpleButton3.Size = new System.Drawing.Size(86, 38);
            this.simpleButton3.TabIndex = 13;
            this.simpleButton3.Text = "Edit Account";
            this.simpleButton3.Click += new System.EventHandler(this.simpleButton3_Click);
            // 
            // simpleButton2
            // 
            this.simpleButton2.Enabled = false;
            this.simpleButton2.Location = new System.Drawing.Point(135, 404);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new System.Drawing.Size(86, 38);
            this.simpleButton2.TabIndex = 12;
            this.simpleButton2.Text = "Add Account";
            this.simpleButton2.Click += new System.EventHandler(this.simpleButton2_Click);
            // 
            // simpleButton1
            // 
            this.simpleButton1.Location = new System.Drawing.Point(34, 404);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(86, 38);
            this.simpleButton1.TabIndex = 11;
            this.simpleButton1.Text = "Add New";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 10.25F);
            this.labelControl1.Location = new System.Drawing.Point(28, 82);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(92, 17);
            this.labelControl1.TabIndex = 1;
            this.labelControl1.Text = "Account Code:";
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 10.25F);
            this.labelControl2.Location = new System.Drawing.Point(36, 130);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(84, 17);
            this.labelControl2.TabIndex = 2;
            this.labelControl2.Text = "Account Title:";
            // 
            // txtlevelnum
            // 
            this.txtlevelnum.Location = new System.Drawing.Point(126, 218);
            this.txtlevelnum.Name = "txtlevelnum";
            this.txtlevelnum.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10.25F);
            this.txtlevelnum.Properties.Appearance.Options.UseFont = true;
            this.txtlevelnum.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtlevelnum.Properties.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8"});
            this.txtlevelnum.Properties.ReadOnly = true;
            this.txtlevelnum.Size = new System.Drawing.Size(100, 24);
            this.txtlevelnum.TabIndex = 10;
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Tahoma", 10.25F);
            this.labelControl3.Location = new System.Drawing.Point(29, 175);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(91, 17);
            this.labelControl3.TabIndex = 3;
            this.labelControl3.Text = "Account Type:";
            // 
            // txtaccttype
            // 
            this.txtaccttype.Location = new System.Drawing.Point(126, 172);
            this.txtaccttype.Name = "txtaccttype";
            this.txtaccttype.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10.25F);
            this.txtaccttype.Properties.Appearance.Options.UseFont = true;
            this.txtaccttype.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtaccttype.Properties.Items.AddRange(new object[] {
            "Summary Account",
            "Posting Account"});
            this.txtaccttype.Properties.ReadOnly = true;
            this.txtaccttype.Size = new System.Drawing.Size(100, 24);
            this.txtaccttype.TabIndex = 9;
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Font = new System.Drawing.Font("Tahoma", 10.25F);
            this.labelControl4.Location = new System.Drawing.Point(31, 221);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(89, 17);
            this.labelControl4.TabIndex = 4;
            this.labelControl4.Text = "Level Number:";
            // 
            // txtmotheracct
            // 
            this.txtmotheracct.Location = new System.Drawing.Point(126, 308);
            this.txtmotheracct.Name = "txtmotheracct";
            this.txtmotheracct.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10.25F);
            this.txtmotheracct.Properties.Appearance.Options.UseFont = true;
            this.txtmotheracct.Properties.ReadOnly = true;
            this.txtmotheracct.Size = new System.Drawing.Size(100, 24);
            this.txtmotheracct.TabIndex = 8;
            // 
            // labelControl5
            // 
            this.labelControl5.Appearance.Font = new System.Drawing.Font("Tahoma", 10.25F);
            this.labelControl5.Location = new System.Drawing.Point(17, 311);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(103, 17);
            this.labelControl5.TabIndex = 5;
            this.labelControl5.Text = "Mother Account:";
            // 
            // txtaccttitle
            // 
            this.txtaccttitle.Location = new System.Drawing.Point(126, 127);
            this.txtaccttitle.Name = "txtaccttitle";
            this.txtaccttitle.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10.25F);
            this.txtaccttitle.Properties.Appearance.Options.UseFont = true;
            this.txtaccttitle.Properties.ReadOnly = true;
            this.txtaccttitle.Size = new System.Drawing.Size(301, 24);
            this.txtaccttitle.TabIndex = 7;
            // 
            // txtacctcode
            // 
            this.txtacctcode.Location = new System.Drawing.Point(126, 79);
            this.txtacctcode.Name = "txtacctcode";
            this.txtacctcode.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10.25F);
            this.txtacctcode.Properties.Appearance.Options.UseFont = true;
            this.txtacctcode.Properties.ReadOnly = true;
            this.txtacctcode.Size = new System.Drawing.Size(100, 24);
            this.txtacctcode.TabIndex = 6;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.refreshDataToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(141, 26);
            // 
            // refreshDataToolStripMenuItem
            // 
            this.refreshDataToolStripMenuItem.Name = "refreshDataToolStripMenuItem";
            this.refreshDataToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.refreshDataToolStripMenuItem.Text = "Refresh Data";
            this.refreshDataToolStripMenuItem.Click += new System.EventHandler(this.refreshDataToolStripMenuItem_Click);
            // 
            // COA
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(858, 482);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "COA";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "COA";
            this.Load += new System.EventHandler(this.COA_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtbranch.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtlevelnum.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtaccttype.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtmotheracct.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtaccttitle.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtacctcode.Properties)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.GroupBox groupBox2;
        private DevExpress.XtraEditors.ComboBoxEdit txtbranch;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton1;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.SimpleButton simpleButton4;
        private DevExpress.XtraEditors.SimpleButton simpleButton3;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.ComboBoxEdit txtlevelnum;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.ComboBoxEdit txtaccttype;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.TextEdit txtmotheracct;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.TextEdit txtaccttitle;
        private DevExpress.XtraEditors.TextEdit txtacctcode;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem refreshDataToolStripMenuItem;
    }
}