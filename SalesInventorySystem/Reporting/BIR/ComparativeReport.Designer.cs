namespace SalesInventorySystem.Reporting.BIR
{
    partial class ComparativeReport
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.pivotGridControl1 = new DevExpress.XtraPivotGrid.PivotGridControl();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.simpleButton3 = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.txtdateto = new DevExpress.XtraEditors.DateEdit();
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            this.txtdatefrom = new DevExpress.XtraEditors.DateEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pivotGridControl1)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtdateto.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtdateto.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtdatefrom.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtdatefrom.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.pivotGridControl1);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 117);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox2.Size = new System.Drawing.Size(1458, 831);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            // 
            // pivotGridControl1
            // 
            this.pivotGridControl1.Appearance.ColumnHeaderArea.Font = new System.Drawing.Font("Tahoma", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pivotGridControl1.Appearance.ColumnHeaderArea.Options.UseFont = true;
            this.pivotGridControl1.Appearance.DataHeaderArea.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.pivotGridControl1.Appearance.DataHeaderArea.Options.UseForeColor = true;
            this.pivotGridControl1.Appearance.FieldHeader.ForeColor = System.Drawing.Color.DarkGreen;
            this.pivotGridControl1.Appearance.FieldHeader.Options.UseForeColor = true;
            this.pivotGridControl1.Appearance.FieldValue.ForeColor = System.Drawing.Color.Maroon;
            this.pivotGridControl1.Appearance.FieldValue.Options.UseForeColor = true;
            this.pivotGridControl1.Appearance.FieldValueGrandTotal.Font = new System.Drawing.Font("Tahoma", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pivotGridControl1.Appearance.FieldValueGrandTotal.ForeColor = System.Drawing.Color.DarkBlue;
            this.pivotGridControl1.Appearance.FieldValueGrandTotal.Options.UseFont = true;
            this.pivotGridControl1.Appearance.FieldValueGrandTotal.Options.UseForeColor = true;
            this.pivotGridControl1.Appearance.FieldValueTotal.BackColor = System.Drawing.Color.Gray;
            this.pivotGridControl1.Appearance.FieldValueTotal.Font = new System.Drawing.Font("Tahoma", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pivotGridControl1.Appearance.FieldValueTotal.Options.UseBackColor = true;
            this.pivotGridControl1.Appearance.FieldValueTotal.Options.UseFont = true;
            this.pivotGridControl1.Appearance.GrandTotalCell.ForeColor = System.Drawing.Color.Red;
            this.pivotGridControl1.Appearance.GrandTotalCell.Options.UseForeColor = true;
            this.pivotGridControl1.Appearance.RowHeaderArea.Font = new System.Drawing.Font("Tahoma", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pivotGridControl1.Appearance.RowHeaderArea.ForeColor = System.Drawing.Color.DarkOliveGreen;
            this.pivotGridControl1.Appearance.RowHeaderArea.Options.UseFont = true;
            this.pivotGridControl1.Appearance.RowHeaderArea.Options.UseForeColor = true;
            this.pivotGridControl1.Appearance.TotalCell.Font = new System.Drawing.Font("Tahoma", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pivotGridControl1.Appearance.TotalCell.ForeColor = System.Drawing.Color.Navy;
            this.pivotGridControl1.Appearance.TotalCell.Options.UseFont = true;
            this.pivotGridControl1.Appearance.TotalCell.Options.UseForeColor = true;
            this.pivotGridControl1.DataMember = "CustomSqlQuery";
            this.pivotGridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pivotGridControl1.Location = new System.Drawing.Point(4, 30);
            this.pivotGridControl1.Margin = new System.Windows.Forms.Padding(4);
            this.pivotGridControl1.Name = "pivotGridControl1";
            this.pivotGridControl1.OptionsBehavior.BestFitMode = ((DevExpress.XtraPivotGrid.PivotGridBestFitMode)((DevExpress.XtraPivotGrid.PivotGridBestFitMode.FieldValue | DevExpress.XtraPivotGrid.PivotGridBestFitMode.FieldHeader)));
            this.pivotGridControl1.OptionsView.FilterSeparatorBarPadding = 2;
            this.pivotGridControl1.OptionsView.ShowRowGrandTotals = false;
            this.pivotGridControl1.OptionsView.ShowRowTotals = false;
            this.pivotGridControl1.Size = new System.Drawing.Size(1450, 797);
            this.pivotGridControl1.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.simpleButton3);
            this.groupBox1.Controls.Add(this.labelControl2);
            this.groupBox1.Controls.Add(this.txtdateto);
            this.groupBox1.Controls.Add(this.simpleButton2);
            this.groupBox1.Controls.Add(this.txtdatefrom);
            this.groupBox1.Controls.Add(this.labelControl1);
            this.groupBox1.Controls.Add(this.simpleButton1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(1458, 117);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            // 
            // simpleButton3
            // 
            this.simpleButton3.Location = new System.Drawing.Point(672, 27);
            this.simpleButton3.Margin = new System.Windows.Forms.Padding(4);
            this.simpleButton3.Name = "simpleButton3";
            this.simpleButton3.Size = new System.Drawing.Size(196, 38);
            this.simpleButton3.TabIndex = 8;
            this.simpleButton3.Text = "Export to Excel";
            this.simpleButton3.Click += new System.EventHandler(this.simpleButton3_Click);
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(20, 73);
            this.labelControl2.Margin = new System.Windows.Forms.Padding(4);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(80, 25);
            this.labelControl2.TabIndex = 7;
            this.labelControl2.Text = "Date To:";
            // 
            // txtdateto
            // 
            this.txtdateto.EditValue = null;
            this.txtdateto.Location = new System.Drawing.Point(142, 69);
            this.txtdateto.Margin = new System.Windows.Forms.Padding(4);
            this.txtdateto.Name = "txtdateto";
            this.txtdateto.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtdateto.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtdateto.Properties.Mask.EditMask = "MM/dd/yyyy";
            this.txtdateto.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.txtdateto.Size = new System.Drawing.Size(214, 40);
            this.txtdateto.TabIndex = 6;
            // 
            // simpleButton2
            // 
            this.simpleButton2.Location = new System.Drawing.Point(520, 27);
            this.simpleButton2.Margin = new System.Windows.Forms.Padding(4);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new System.Drawing.Size(142, 38);
            this.simpleButton2.TabIndex = 5;
            this.simpleButton2.Text = "Print";
            // 
            // txtdatefrom
            // 
            this.txtdatefrom.EditValue = null;
            this.txtdatefrom.Location = new System.Drawing.Point(142, 31);
            this.txtdatefrom.Margin = new System.Windows.Forms.Padding(4);
            this.txtdatefrom.Name = "txtdatefrom";
            this.txtdatefrom.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtdatefrom.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtdatefrom.Properties.Mask.EditMask = "MM/dd/yyyy";
            this.txtdatefrom.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.txtdatefrom.Size = new System.Drawing.Size(214, 40);
            this.txtdatefrom.TabIndex = 4;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(20, 35);
            this.labelControl1.Margin = new System.Windows.Forms.Padding(4);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(105, 25);
            this.labelControl1.TabIndex = 3;
            this.labelControl1.Text = "Date From:";
            // 
            // simpleButton1
            // 
            this.simpleButton1.Location = new System.Drawing.Point(368, 27);
            this.simpleButton1.Margin = new System.Windows.Forms.Padding(4);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(142, 38);
            this.simpleButton1.TabIndex = 2;
            this.simpleButton1.Text = "Extract";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // ComparativeReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1458, 948);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.Name = "ComparativeReport";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ComparativeReport";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pivotGridControl1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtdateto.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtdateto.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtdatefrom.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtdatefrom.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private DevExpress.XtraPivotGrid.PivotGridControl pivotGridControl1;
        public System.Windows.Forms.GroupBox groupBox1;
        private DevExpress.XtraEditors.SimpleButton simpleButton3;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.DateEdit txtdateto;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
        private DevExpress.XtraEditors.DateEdit txtdatefrom;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
    }
}