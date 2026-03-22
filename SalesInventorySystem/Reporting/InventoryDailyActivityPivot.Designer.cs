namespace SalesInventorySystem.Reporting
{
    partial class InventoryDailyActivityPivot
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
            this.dateto = new DevExpress.XtraEditors.DateEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.txtbranch = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.searchLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.labelbranch = new DevExpress.XtraEditors.LabelControl();
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            this.datefrom = new DevExpress.XtraEditors.DateEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pivotGridControl1)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dateto.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateto.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtbranch.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.datefrom.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.datefrom.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.pivotGridControl1);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 67);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox2.Size = new System.Drawing.Size(1151, 551);
            this.groupBox2.TabIndex = 7;
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
            this.pivotGridControl1.Location = new System.Drawing.Point(3, 16);
            this.pivotGridControl1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pivotGridControl1.Name = "pivotGridControl1";
            this.pivotGridControl1.OptionsBehavior.BestFitMode = ((DevExpress.XtraPivotGrid.PivotGridBestFitMode)((DevExpress.XtraPivotGrid.PivotGridBestFitMode.FieldValue | DevExpress.XtraPivotGrid.PivotGridBestFitMode.FieldHeader)));
            this.pivotGridControl1.Size = new System.Drawing.Size(1145, 533);
            this.pivotGridControl1.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dateto);
            this.groupBox1.Controls.Add(this.labelControl3);
            this.groupBox1.Controls.Add(this.txtbranch);
            this.groupBox1.Controls.Add(this.labelbranch);
            this.groupBox1.Controls.Add(this.simpleButton2);
            this.groupBox1.Controls.Add(this.datefrom);
            this.groupBox1.Controls.Add(this.labelControl1);
            this.groupBox1.Controls.Add(this.simpleButton1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Size = new System.Drawing.Size(1151, 67);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            // 
            // dateto
            // 
            this.dateto.EditValue = null;
            this.dateto.Location = new System.Drawing.Point(195, 38);
            this.dateto.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dateto.Name = "dateto";
            this.dateto.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateto.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateto.Size = new System.Drawing.Size(84, 20);
            this.dateto.TabIndex = 9;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(176, 41);
            this.labelControl3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(16, 13);
            this.labelControl3.TabIndex = 8;
            this.labelControl3.Text = "To:";
            // 
            // txtbranch
            // 
            this.txtbranch.Location = new System.Drawing.Point(87, 14);
            this.txtbranch.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtbranch.Name = "txtbranch";
            this.txtbranch.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 8.8F);
            this.txtbranch.Properties.Appearance.Options.UseFont = true;
            this.txtbranch.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtbranch.Properties.NullText = "";
            this.txtbranch.Properties.PopupView = this.searchLookUpEdit1View;
            this.txtbranch.Size = new System.Drawing.Size(193, 20);
            this.txtbranch.TabIndex = 7;
            this.txtbranch.EditValueChanged += new System.EventHandler(this.txtbranch_EditValueChanged);
            // 
            // searchLookUpEdit1View
            // 
            this.searchLookUpEdit1View.DetailHeight = 284;
            this.searchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.searchLookUpEdit1View.Name = "searchLookUpEdit1View";
            this.searchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.searchLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            // 
            // labelbranch
            // 
            this.labelbranch.Location = new System.Drawing.Point(10, 18);
            this.labelbranch.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.labelbranch.Name = "labelbranch";
            this.labelbranch.Size = new System.Drawing.Size(69, 13);
            this.labelbranch.TabIndex = 6;
            this.labelbranch.Text = "Select Branch:";
            // 
            // simpleButton2
            // 
            this.simpleButton2.Location = new System.Drawing.Point(361, 14);
            this.simpleButton2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new System.Drawing.Size(71, 42);
            this.simpleButton2.TabIndex = 5;
            this.simpleButton2.Text = "Print";
            // 
            // datefrom
            // 
            this.datefrom.EditValue = null;
            this.datefrom.Location = new System.Drawing.Point(87, 38);
            this.datefrom.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.datefrom.Name = "datefrom";
            this.datefrom.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.datefrom.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.datefrom.Size = new System.Drawing.Size(84, 20);
            this.datefrom.TabIndex = 4;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(10, 41);
            this.labelControl1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(54, 13);
            this.labelControl1.TabIndex = 3;
            this.labelControl1.Text = "Date From:";
            // 
            // simpleButton1
            // 
            this.simpleButton1.Location = new System.Drawing.Point(285, 14);
            this.simpleButton1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(71, 42);
            this.simpleButton1.TabIndex = 2;
            this.simpleButton1.Text = "Extract";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // InventoryDailyActivityPivot
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1151, 618);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "InventoryDailyActivityPivot";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "InventoryDailyActivityPivot";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.InventoryDailyActivityPivot_Load);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pivotGridControl1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dateto.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateto.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtbranch.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.datefrom.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.datefrom.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private DevExpress.XtraPivotGrid.PivotGridControl pivotGridControl1;
        private System.Windows.Forms.GroupBox groupBox1;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
        private DevExpress.XtraEditors.DateEdit datefrom;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.SearchLookUpEdit txtbranch;
        private DevExpress.XtraGrid.Views.Grid.GridView searchLookUpEdit1View;
        private DevExpress.XtraEditors.LabelControl labelbranch;
        private DevExpress.XtraEditors.DateEdit dateto;
        private DevExpress.XtraEditors.LabelControl labelControl3;
    }
}