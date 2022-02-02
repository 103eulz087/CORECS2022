namespace SalesInventorySystem.HotelManagement
{
    partial class HotelFrmReports
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
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.gridControlCheckin = new DevExpress.XtraGrid.GridControl();
            this.gridViewCheckin = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.txtreporttype = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.txtdateto = new DevExpress.XtraEditors.DateEdit();
            this.txtdatefrom = new DevExpress.XtraEditors.DateEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl13 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlCheckin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewCheckin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtreporttype.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtdateto.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtdateto.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtdatefrom.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtdatefrom.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.gridControlCheckin);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl1.Location = new System.Drawing.Point(0, 113);
            this.groupControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(1197, 586);
            this.groupControl1.TabIndex = 1;
            // 
            // gridControlCheckin
            // 
            this.gridControlCheckin.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControlCheckin.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gridControlCheckin.Location = new System.Drawing.Point(2, 25);
            this.gridControlCheckin.MainView = this.gridViewCheckin;
            this.gridControlCheckin.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gridControlCheckin.Name = "gridControlCheckin";
            this.gridControlCheckin.Size = new System.Drawing.Size(1193, 559);
            this.gridControlCheckin.TabIndex = 4;
            this.gridControlCheckin.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewCheckin});
            // 
            // gridViewCheckin
            // 
            this.gridViewCheckin.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridViewCheckin.Appearance.HeaderPanel.Options.UseFont = true;
            this.gridViewCheckin.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridViewCheckin.Appearance.Row.Options.UseFont = true;
            this.gridViewCheckin.GridControl = this.gridControlCheckin;
            this.gridViewCheckin.Name = "gridViewCheckin";
            this.gridViewCheckin.OptionsBehavior.Editable = false;
            this.gridViewCheckin.OptionsBehavior.ReadOnly = true;
            this.gridViewCheckin.OptionsView.ColumnAutoWidth = false;
            this.gridViewCheckin.OptionsView.RowAutoHeight = true;
            // 
            // groupControl2
            // 
            this.groupControl2.Controls.Add(this.button2);
            this.groupControl2.Controls.Add(this.button1);
            this.groupControl2.Controls.Add(this.txtreporttype);
            this.groupControl2.Controls.Add(this.labelControl2);
            this.groupControl2.Controls.Add(this.txtdateto);
            this.groupControl2.Controls.Add(this.txtdatefrom);
            this.groupControl2.Controls.Add(this.labelControl1);
            this.groupControl2.Controls.Add(this.labelControl13);
            this.groupControl2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupControl2.Location = new System.Drawing.Point(0, 0);
            this.groupControl2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(1197, 113);
            this.groupControl2.TabIndex = 2;
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Tahoma", 10F);
            this.button2.Location = new System.Drawing.Point(707, 36);
            this.button2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(125, 61);
            this.button2.TabIndex = 438;
            this.button2.Text = "Print";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Tahoma", 10F);
            this.button1.Location = new System.Drawing.Point(576, 36);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(125, 61);
            this.button1.TabIndex = 437;
            this.button1.Text = "Extract";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtreporttype
            // 
            this.txtreporttype.Location = new System.Drawing.Point(352, 37);
            this.txtreporttype.Name = "txtreporttype";
            this.txtreporttype.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10.25F);
            this.txtreporttype.Properties.Appearance.Options.UseFont = true;
            this.txtreporttype.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtreporttype.Properties.Items.AddRange(new object[] {
            "IN-HOUSE GUEST REPORT",
            "POLICE REPORT",
            "DAILY CASH COLLECTION REPORT",
            "ROOM SALES REPORT",
            "ROOM STATUS REPORT",
            "HOUSEKEEPING REPORT"});
            this.txtreporttype.Size = new System.Drawing.Size(218, 28);
            this.txtreporttype.TabIndex = 436;
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 10.25F);
            this.labelControl2.Location = new System.Drawing.Point(248, 40);
            this.labelControl2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(97, 21);
            this.labelControl2.TabIndex = 435;
            this.labelControl2.Text = "Report Type:";
            // 
            // txtdateto
            // 
            this.txtdateto.EditValue = null;
            this.txtdateto.Location = new System.Drawing.Point(103, 73);
            this.txtdateto.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtdateto.Name = "txtdateto";
            this.txtdateto.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10.25F);
            this.txtdateto.Properties.Appearance.Options.UseFont = true;
            this.txtdateto.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtdateto.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtdateto.Size = new System.Drawing.Size(125, 28);
            this.txtdateto.TabIndex = 434;
            // 
            // txtdatefrom
            // 
            this.txtdatefrom.EditValue = null;
            this.txtdatefrom.Location = new System.Drawing.Point(103, 37);
            this.txtdatefrom.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtdatefrom.Name = "txtdatefrom";
            this.txtdatefrom.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10.25F);
            this.txtdatefrom.Properties.Appearance.Options.UseFont = true;
            this.txtdatefrom.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtdatefrom.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtdatefrom.Size = new System.Drawing.Size(125, 28);
            this.txtdatefrom.TabIndex = 433;
            this.txtdatefrom.EditValueChanged += new System.EventHandler(this.txtdatefrom_EditValueChanged);
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 10.25F);
            this.labelControl1.Location = new System.Drawing.Point(12, 76);
            this.labelControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(66, 21);
            this.labelControl1.TabIndex = 37;
            this.labelControl1.Text = "Date To:";
            // 
            // labelControl13
            // 
            this.labelControl13.Appearance.Font = new System.Drawing.Font("Tahoma", 10.25F);
            this.labelControl13.Location = new System.Drawing.Point(12, 40);
            this.labelControl13.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.labelControl13.Name = "labelControl13";
            this.labelControl13.Size = new System.Drawing.Size(85, 21);
            this.labelControl13.TabIndex = 36;
            this.labelControl13.Text = "Date From:";
            // 
            // HotelFrmReports
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1197, 699);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.groupControl2);
            this.Name = "HotelFrmReports";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "HotelFrmReports";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlCheckin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewCheckin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            this.groupControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtreporttype.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtdateto.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtdateto.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtdatefrom.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtdatefrom.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraGrid.GridControl gridControlCheckin;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewCheckin;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl13;
        public DevExpress.XtraEditors.DateEdit txtdateto;
        public DevExpress.XtraEditors.DateEdit txtdatefrom;
        private DevExpress.XtraEditors.ComboBoxEdit txtreporttype;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}