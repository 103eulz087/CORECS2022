namespace SalesInventorySystem.Orders
{
    partial class AddBranchOrder2
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.txtproduct = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtweight = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnD = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtskuno = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.btnaddinventory = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.txtprodcode = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtbatchcode = new System.Windows.Forms.TextBox();
            this.button4 = new System.Windows.Forms.Button();
            this.txtports = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtavailableqty = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.isprimalcuts = new System.Windows.Forms.CheckBox();
            this.button5 = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtproduct
            // 
            this.txtproduct.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F);
            this.txtproduct.FormattingEnabled = true;
            this.txtproduct.Location = new System.Drawing.Point(420, 155);
            this.txtproduct.Margin = new System.Windows.Forms.Padding(2);
            this.txtproduct.Name = "txtproduct";
            this.txtproduct.Size = new System.Drawing.Size(375, 39);
            this.txtproduct.TabIndex = 9;
            this.txtproduct.SelectedIndexChanged += new System.EventHandler(this.txtproduct_SelectedIndexChanged);
            this.txtproduct.TextUpdate += new System.EventHandler(this.txtproduct_TextUpdate);
            this.txtproduct.DropDownClosed += new System.EventHandler(this.txtproduct_DropDownClosed);
            this.txtproduct.TextChanged += new System.EventHandler(this.txtproduct_TextChanged);
            this.txtproduct.Click += new System.EventHandler(this.txtproduct_Click);
            this.txtproduct.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtproduct_KeyDown);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 30.25F);
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(11, 147);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(405, 47);
            this.label4.TabIndex = 8;
            this.label4.Text = "SELECT PRODUCT:";
            // 
            // txtweight
            // 
            this.txtweight.Font = new System.Drawing.Font("Microsoft Sans Serif", 24.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtweight.ForeColor = System.Drawing.Color.Blue;
            this.txtweight.Location = new System.Drawing.Point(374, 208);
            this.txtweight.Margin = new System.Windows.Forms.Padding(2);
            this.txtweight.MaxLength = 6;
            this.txtweight.Name = "txtweight";
            this.txtweight.Size = new System.Drawing.Size(240, 45);
            this.txtweight.TabIndex = 11;
            this.txtweight.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtweight_KeyDown);
            this.txtweight.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtweight_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 30.25F);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(14, 205);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(368, 47);
            this.label2.TabIndex = 10;
            this.label2.Text = "ACTUAL WEIGHT:";
            // 
            // btnD
            // 
            this.btnD.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnD.Location = new System.Drawing.Point(619, 206);
            this.btnD.Margin = new System.Windows.Forms.Padding(2);
            this.btnD.Name = "btnD";
            this.btnD.Size = new System.Drawing.Size(176, 48);
            this.btnD.TabIndex = 12;
            this.btnD.Text = "GET Weight";
            this.btnD.UseVisualStyleBackColor = true;
            this.btnD.Click += new System.EventHandler(this.btnD_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 30.25F);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(14, 264);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(234, 47);
            this.label1.TabIndex = 13;
            this.label1.Text = "BARCODE:";
            // 
            // txtskuno
            // 
            this.txtskuno.Font = new System.Drawing.Font("Microsoft Sans Serif", 24.75F);
            this.txtskuno.ForeColor = System.Drawing.Color.Blue;
            this.txtskuno.Location = new System.Drawing.Point(241, 267);
            this.txtskuno.Margin = new System.Windows.Forms.Padding(2);
            this.txtskuno.MaxLength = 13;
            this.txtskuno.Name = "txtskuno";
            this.txtskuno.Size = new System.Drawing.Size(373, 45);
            this.txtskuno.TabIndex = 14;
            this.txtskuno.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtskuno_KeyDown);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(1090, 396);
            this.dataGridView1.TabIndex = 25;
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 18.25F);
            this.button2.Location = new System.Drawing.Point(8, 538);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(242, 59);
            this.button2.TabIndex = 27;
            this.button2.Text = "Upload(F10)";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.button1.Location = new System.Drawing.Point(619, 266);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(176, 47);
            this.button1.TabIndex = 26;
            this.button1.Text = "Print Barcode(F8)";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnaddinventory
            // 
            this.btnaddinventory.Font = new System.Drawing.Font("Microsoft Sans Serif", 18.25F);
            this.btnaddinventory.Location = new System.Drawing.Point(7, 333);
            this.btnaddinventory.Name = "btnaddinventory";
            this.btnaddinventory.Size = new System.Drawing.Size(242, 59);
            this.btnaddinventory.TabIndex = 28;
            this.btnaddinventory.Text = "Add Inventory";
            this.btnaddinventory.UseVisualStyleBackColor = true;
            this.btnaddinventory.Click += new System.EventHandler(this.btnaddinventory_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 30.25F);
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(11, 86);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(476, 47);
            this.label5.TabIndex = 29;
            this.label5.Text = "PRODUCT CATEGORY:";
            // 
            // button3
            // 
            this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 18.25F);
            this.button3.Location = new System.Drawing.Point(7, 403);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(242, 57);
            this.button3.TabIndex = 31;
            this.button3.Text = "Cancel Line(Del)";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // txtprodcode
            // 
            this.txtprodcode.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtprodcode.ForeColor = System.Drawing.Color.Blue;
            this.txtprodcode.Location = new System.Drawing.Point(1129, 146);
            this.txtprodcode.Margin = new System.Windows.Forms.Padding(2);
            this.txtprodcode.MaxLength = 6;
            this.txtprodcode.Name = "txtprodcode";
            this.txtprodcode.Size = new System.Drawing.Size(120, 53);
            this.txtprodcode.TabIndex = 32;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.25F);
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(9, 86);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(155, 25);
            this.label6.TabIndex = 33;
            this.label6.Text = "BATCH CODE:";
            // 
            // txtbatchcode
            // 
            this.txtbatchcode.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F);
            this.txtbatchcode.ForeColor = System.Drawing.Color.Blue;
            this.txtbatchcode.Location = new System.Drawing.Point(168, 83);
            this.txtbatchcode.Margin = new System.Windows.Forms.Padding(2);
            this.txtbatchcode.MaxLength = 6;
            this.txtbatchcode.Name = "txtbatchcode";
            this.txtbatchcode.Size = new System.Drawing.Size(81, 31);
            this.txtbatchcode.TabIndex = 34;
            // 
            // button4
            // 
            this.button4.Font = new System.Drawing.Font("Microsoft Sans Serif", 18.25F);
            this.button4.Location = new System.Drawing.Point(7, 471);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(242, 56);
            this.button4.TabIndex = 35;
            this.button4.Text = "Clear(F6)";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // txtports
            // 
            this.txtports.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.txtports.FormattingEnabled = true;
            this.txtports.Location = new System.Drawing.Point(14, 36);
            this.txtports.Margin = new System.Windows.Forms.Padding(2);
            this.txtports.Name = "txtports";
            this.txtports.Size = new System.Drawing.Size(235, 33);
            this.txtports.TabIndex = 36;
            this.txtports.SelectedIndexChanged += new System.EventHandler(this.txtports_SelectedIndexChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txtavailableqty);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.button4);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.txtports);
            this.panel1.Controls.Add(this.btnaddinventory);
            this.panel1.Controls.Add(this.button3);
            this.panel1.Controls.Add(this.txtbatchcode);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(1090, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(260, 729);
            this.panel1.TabIndex = 37;
            // 
            // txtavailableqty
            // 
            this.txtavailableqty.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F);
            this.txtavailableqty.ForeColor = System.Drawing.Color.Blue;
            this.txtavailableqty.Location = new System.Drawing.Point(169, 120);
            this.txtavailableqty.Margin = new System.Windows.Forms.Padding(2);
            this.txtavailableqty.MaxLength = 6;
            this.txtavailableqty.Name = "txtavailableqty";
            this.txtavailableqty.Size = new System.Drawing.Size(81, 31);
            this.txtavailableqty.TabIndex = 44;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.25F);
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(9, 123);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(145, 25);
            this.label8.TabIndex = 44;
            this.label8.Text = "Available Qty:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.25F);
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(14, 9);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(235, 25);
            this.label3.TabIndex = 37;
            this.label3.Text = "COM PORT SETTINGS";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1090, 333);
            this.panel2.TabIndex = 38;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.isprimalcuts);
            this.panel3.Controls.Add(this.button5);
            this.panel3.Controls.Add(this.label7);
            this.panel3.Controls.Add(this.button1);
            this.panel3.Controls.Add(this.txtskuno);
            this.panel3.Controls.Add(this.btnD);
            this.panel3.Controls.Add(this.label1);
            this.panel3.Controls.Add(this.label5);
            this.panel3.Controls.Add(this.label4);
            this.panel3.Controls.Add(this.txtproduct);
            this.panel3.Controls.Add(this.txtweight);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1338, 333);
            this.panel3.TabIndex = 37;
            // 
            // isprimalcuts
            // 
            this.isprimalcuts.AutoSize = true;
            this.isprimalcuts.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.isprimalcuts.ForeColor = System.Drawing.Color.White;
            this.isprimalcuts.Location = new System.Drawing.Point(799, 123);
            this.isprimalcuts.Name = "isprimalcuts";
            this.isprimalcuts.Size = new System.Drawing.Size(124, 24);
            this.isprimalcuts.TabIndex = 43;
            this.isprimalcuts.Text = "isPrimalCuts";
            this.isprimalcuts.UseVisualStyleBackColor = true;
            // 
            // button5
            // 
            this.button5.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button5.Location = new System.Drawing.Point(799, 154);
            this.button5.Margin = new System.Windows.Forms.Padding(2);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(165, 39);
            this.button5.TabIndex = 42;
            this.button5.Text = "Search Items";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Arial Black", 34F, System.Drawing.FontStyle.Bold);
            this.label7.ForeColor = System.Drawing.Color.Red;
            this.label7.Location = new System.Drawing.Point(11, 13);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(1061, 65);
            this.label7.TabIndex = 40;
            this.label7.Text = "ENZO MEAT MARKET WEIGHING SCALE";
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.dataGridView1);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 333);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1090, 396);
            this.panel4.TabIndex = 39;
            // 
            // AddBranchOrder2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(1350, 729);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.txtprodcode);
            this.Name = "AddBranchOrder2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Add Orders";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AddPrimalCutInventory_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.AddPrimalCutInventory_FormClosed);
            this.Load += new System.EventHandler(this.AddBranchOrder2_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox txtproduct;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtweight;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnD;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtskuno;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.IO.Ports.SerialPort serialPort1;
        private System.Windows.Forms.Button btnaddinventory;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TextBox txtprodcode;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtbatchcode;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.ComboBox txtports;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.CheckBox isprimalcuts;
        private System.Windows.Forms.TextBox txtavailableqty;
        private System.Windows.Forms.Label label8;
    }
}