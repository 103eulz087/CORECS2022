namespace SalesInventorySystem.Accounting
{
    partial class ViewTicket
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.forchecking = new System.Windows.Forms.TabPage();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.forapproval = new System.Windows.Forms.TabPage();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.forupdating = new System.Windows.Forms.TabPage();
            this.dataGridView3 = new System.Windows.Forms.DataGridView();
            this.approvedtickets = new System.Windows.Forms.TabPage();
            this.dataGridView4 = new System.Windows.Forms.DataGridView();
            this.disapprovedtickets = new System.Windows.Forms.TabPage();
            this.dataGridView5 = new System.Windows.Forms.DataGridView();
            this.awaitingforapproval = new System.Windows.Forms.TabPage();
            this.dataGridView6 = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.approvedTicketToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.disApprovedTicketToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewTicketDetailsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.viewTicketDetailsToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip3 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl1.SuspendLayout();
            this.forchecking.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.forapproval.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.forupdating.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).BeginInit();
            this.approvedtickets.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView4)).BeginInit();
            this.disapprovedtickets.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView5)).BeginInit();
            this.awaitingforapproval.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView6)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.contextMenuStrip2.SuspendLayout();
            this.contextMenuStrip3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.forchecking);
            this.tabControl1.Controls.Add(this.forapproval);
            this.tabControl1.Controls.Add(this.forupdating);
            this.tabControl1.Controls.Add(this.approvedtickets);
            this.tabControl1.Controls.Add(this.disapprovedtickets);
            this.tabControl1.Controls.Add(this.awaitingforapproval);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1174, 574);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // forchecking
            // 
            this.forchecking.Controls.Add(this.dataGridView1);
            this.forchecking.Location = new System.Drawing.Point(4, 25);
            this.forchecking.Name = "forchecking";
            this.forchecking.Padding = new System.Windows.Forms.Padding(3);
            this.forchecking.Size = new System.Drawing.Size(1166, 545);
            this.forchecking.TabIndex = 0;
            this.forchecking.Text = "For Checking";
            this.forchecking.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(3, 3);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(1160, 539);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.dataGridView1_MouseUp);
            // 
            // forapproval
            // 
            this.forapproval.Controls.Add(this.dataGridView2);
            this.forapproval.Location = new System.Drawing.Point(4, 25);
            this.forapproval.Name = "forapproval";
            this.forapproval.Padding = new System.Windows.Forms.Padding(3);
            this.forapproval.Size = new System.Drawing.Size(1166, 545);
            this.forapproval.TabIndex = 1;
            this.forapproval.Text = "For Approval";
            this.forapproval.UseVisualStyleBackColor = true;
            // 
            // dataGridView2
            // 
            this.dataGridView2.AllowUserToAddRows = false;
            this.dataGridView2.AllowUserToDeleteRows = false;
            this.dataGridView2.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView2.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView2.Location = new System.Drawing.Point(3, 3);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.ReadOnly = true;
            this.dataGridView2.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView2.Size = new System.Drawing.Size(1160, 539);
            this.dataGridView2.TabIndex = 1;
            this.dataGridView2.MouseUp += new System.Windows.Forms.MouseEventHandler(this.dataGridView2_MouseUp);
            // 
            // forupdating
            // 
            this.forupdating.Controls.Add(this.dataGridView3);
            this.forupdating.Location = new System.Drawing.Point(4, 25);
            this.forupdating.Name = "forupdating";
            this.forupdating.Padding = new System.Windows.Forms.Padding(3);
            this.forupdating.Size = new System.Drawing.Size(1166, 545);
            this.forupdating.TabIndex = 2;
            this.forupdating.Text = "For Updating";
            this.forupdating.UseVisualStyleBackColor = true;
            // 
            // dataGridView3
            // 
            this.dataGridView3.AllowUserToAddRows = false;
            this.dataGridView3.AllowUserToDeleteRows = false;
            this.dataGridView3.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView3.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView3.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView3.Location = new System.Drawing.Point(3, 3);
            this.dataGridView3.Name = "dataGridView3";
            this.dataGridView3.ReadOnly = true;
            this.dataGridView3.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView3.Size = new System.Drawing.Size(1160, 539);
            this.dataGridView3.TabIndex = 1;
            this.dataGridView3.MouseUp += new System.Windows.Forms.MouseEventHandler(this.dataGridView3_MouseUp);
            // 
            // approvedtickets
            // 
            this.approvedtickets.Controls.Add(this.dataGridView4);
            this.approvedtickets.Location = new System.Drawing.Point(4, 25);
            this.approvedtickets.Name = "approvedtickets";
            this.approvedtickets.Padding = new System.Windows.Forms.Padding(3);
            this.approvedtickets.Size = new System.Drawing.Size(1166, 545);
            this.approvedtickets.TabIndex = 3;
            this.approvedtickets.Text = "Approve Tickets";
            this.approvedtickets.UseVisualStyleBackColor = true;
            // 
            // dataGridView4
            // 
            this.dataGridView4.AllowUserToAddRows = false;
            this.dataGridView4.AllowUserToDeleteRows = false;
            this.dataGridView4.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView4.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView4.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView4.Location = new System.Drawing.Point(3, 3);
            this.dataGridView4.Name = "dataGridView4";
            this.dataGridView4.ReadOnly = true;
            this.dataGridView4.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView4.Size = new System.Drawing.Size(1160, 539);
            this.dataGridView4.TabIndex = 1;
            // 
            // disapprovedtickets
            // 
            this.disapprovedtickets.Controls.Add(this.dataGridView5);
            this.disapprovedtickets.Location = new System.Drawing.Point(4, 25);
            this.disapprovedtickets.Name = "disapprovedtickets";
            this.disapprovedtickets.Padding = new System.Windows.Forms.Padding(3);
            this.disapprovedtickets.Size = new System.Drawing.Size(1166, 545);
            this.disapprovedtickets.TabIndex = 4;
            this.disapprovedtickets.Text = "DisApprove Tickets";
            this.disapprovedtickets.UseVisualStyleBackColor = true;
            // 
            // dataGridView5
            // 
            this.dataGridView5.AllowUserToAddRows = false;
            this.dataGridView5.AllowUserToDeleteRows = false;
            this.dataGridView5.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView5.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView5.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView5.Location = new System.Drawing.Point(3, 3);
            this.dataGridView5.Name = "dataGridView5";
            this.dataGridView5.ReadOnly = true;
            this.dataGridView5.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView5.Size = new System.Drawing.Size(1160, 539);
            this.dataGridView5.TabIndex = 1;
            // 
            // awaitingforapproval
            // 
            this.awaitingforapproval.Controls.Add(this.dataGridView6);
            this.awaitingforapproval.Location = new System.Drawing.Point(4, 25);
            this.awaitingforapproval.Name = "awaitingforapproval";
            this.awaitingforapproval.Padding = new System.Windows.Forms.Padding(3);
            this.awaitingforapproval.Size = new System.Drawing.Size(1166, 545);
            this.awaitingforapproval.TabIndex = 5;
            this.awaitingforapproval.Text = "AwaitingForApproval";
            this.awaitingforapproval.UseVisualStyleBackColor = true;
            // 
            // dataGridView6
            // 
            this.dataGridView6.AllowUserToAddRows = false;
            this.dataGridView6.AllowUserToDeleteRows = false;
            this.dataGridView6.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView6.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView6.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView6.Location = new System.Drawing.Point(3, 3);
            this.dataGridView6.Name = "dataGridView6";
            this.dataGridView6.ReadOnly = true;
            this.dataGridView6.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView6.Size = new System.Drawing.Size(1160, 539);
            this.dataGridView6.TabIndex = 2;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.approvedTicketToolStripMenuItem,
            this.disApprovedTicketToolStripMenuItem,
            this.viewTicketDetailsToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(178, 70);
            // 
            // approvedTicketToolStripMenuItem
            // 
            this.approvedTicketToolStripMenuItem.Name = "approvedTicketToolStripMenuItem";
            this.approvedTicketToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.approvedTicketToolStripMenuItem.Text = "Approved Ticket";
            this.approvedTicketToolStripMenuItem.Click += new System.EventHandler(this.approvedTicketToolStripMenuItem_Click);
            // 
            // disApprovedTicketToolStripMenuItem
            // 
            this.disApprovedTicketToolStripMenuItem.Name = "disApprovedTicketToolStripMenuItem";
            this.disApprovedTicketToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.disApprovedTicketToolStripMenuItem.Text = "DisApproved Ticket";
            this.disApprovedTicketToolStripMenuItem.Click += new System.EventHandler(this.disApprovedTicketToolStripMenuItem_Click);
            // 
            // viewTicketDetailsToolStripMenuItem
            // 
            this.viewTicketDetailsToolStripMenuItem.Name = "viewTicketDetailsToolStripMenuItem";
            this.viewTicketDetailsToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.viewTicketDetailsToolStripMenuItem.Text = "View TicketDetails";
            this.viewTicketDetailsToolStripMenuItem.Click += new System.EventHandler(this.viewTicketDetailsToolStripMenuItem_Click);
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.toolStripMenuItem2,
            this.viewTicketDetailsToolStripMenuItem1});
            this.contextMenuStrip2.Name = "contextMenuStrip1";
            this.contextMenuStrip2.Size = new System.Drawing.Size(178, 70);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(177, 22);
            this.toolStripMenuItem1.Text = "Approved Ticket";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(177, 22);
            this.toolStripMenuItem2.Text = "DisApproved Ticket";
            this.toolStripMenuItem2.Click += new System.EventHandler(this.toolStripMenuItem2_Click);
            // 
            // viewTicketDetailsToolStripMenuItem1
            // 
            this.viewTicketDetailsToolStripMenuItem1.Name = "viewTicketDetailsToolStripMenuItem1";
            this.viewTicketDetailsToolStripMenuItem1.Size = new System.Drawing.Size(177, 22);
            this.viewTicketDetailsToolStripMenuItem1.Text = "View TicketDetails";
            this.viewTicketDetailsToolStripMenuItem1.Click += new System.EventHandler(this.viewTicketDetailsToolStripMenuItem1_Click);
            // 
            // contextMenuStrip3
            // 
            this.contextMenuStrip3.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem3});
            this.contextMenuStrip3.Name = "contextMenuStrip1";
            this.contextMenuStrip3.Size = new System.Drawing.Size(148, 26);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(147, 22);
            this.toolStripMenuItem3.Text = "Update Ticket";
            this.toolStripMenuItem3.Click += new System.EventHandler(this.toolStripMenuItem3_Click);
            // 
            // ViewTicket
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1174, 574);
            this.Controls.Add(this.tabControl1);
            this.Name = "ViewTicket";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ViewTicket";
            this.Load += new System.EventHandler(this.ViewTicket_Load);
            this.tabControl1.ResumeLayout(false);
            this.forchecking.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.forapproval.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.forupdating.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).EndInit();
            this.approvedtickets.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView4)).EndInit();
            this.disapprovedtickets.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView5)).EndInit();
            this.awaitingforapproval.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView6)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.contextMenuStrip2.ResumeLayout(false);
            this.contextMenuStrip3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage forchecking;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TabPage forapproval;
        private System.Windows.Forms.TabPage forupdating;
        private System.Windows.Forms.TabPage approvedtickets;
        private System.Windows.Forms.TabPage disapprovedtickets;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.DataGridView dataGridView3;
        private System.Windows.Forms.DataGridView dataGridView4;
        private System.Windows.Forms.DataGridView dataGridView5;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem approvedTicketToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem disApprovedTicketToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip3;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem viewTicketDetailsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewTicketDetailsToolStripMenuItem1;
        private System.Windows.Forms.TabPage awaitingforapproval;
        private System.Windows.Forms.DataGridView dataGridView6;
    }
}