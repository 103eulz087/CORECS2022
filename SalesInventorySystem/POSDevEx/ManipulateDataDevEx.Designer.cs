namespace SalesInventorySystem.POSDevEx
{
    partial class ManipulateDataDevEx
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
            this.groupControl4 = new DevExpress.XtraEditors.GroupControl();
            this.gridControl2 = new DevExpress.XtraGrid.GridControl();
            this.gridView3 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.groupControl3 = new DevExpress.XtraEditors.GroupControl();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.textEdit1 = new DevExpress.XtraEditors.TextEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.txtcalcres = new DevExpress.XtraEditors.TextEdit();
            this.btncalc = new DevExpress.XtraEditors.SimpleButton();
            this.txtcttrto = new DevExpress.XtraEditors.TextEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.txtcttrfrom = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.txtgoal = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txtnewtotalamount = new DevExpress.XtraEditors.TextEdit();
            this.txtvariance = new DevExpress.XtraEditors.TextEdit();
            this.labelControl13 = new DevExpress.XtraEditors.LabelControl();
            this.btnanalyze = new DevExpress.XtraEditors.SimpleButton();
            this.txttargetsales = new DevExpress.XtraEditors.TextEdit();
            this.txtrefundamount = new DevExpress.XtraEditors.TextEdit();
            this.labelControl11 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl10 = new DevExpress.XtraEditors.LabelControl();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl4)).BeginInit();
            this.groupControl4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).BeginInit();
            this.groupControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtcalcres.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtcttrto.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtcttrfrom.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtgoal.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtnewtotalamount.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtvariance.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txttargetsales.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtrefundamount.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl4
            // 
            this.groupControl4.Controls.Add(this.gridControl2);
            this.groupControl4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl4.Location = new System.Drawing.Point(0, 303);
            this.groupControl4.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.groupControl4.Name = "groupControl4";
            this.groupControl4.Size = new System.Drawing.Size(2724, 1030);
            this.groupControl4.TabIndex = 3;
            // 
            // gridControl2
            // 
            this.gridControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl2.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.gridControl2.Location = new System.Drawing.Point(3, 45);
            this.gridControl2.MainView = this.gridView3;
            this.gridControl2.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.gridControl2.Name = "gridControl2";
            this.gridControl2.Size = new System.Drawing.Size(2718, 982);
            this.gridControl2.TabIndex = 1;
            this.gridControl2.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView3});
            // 
            // gridView3
            // 
            this.gridView3.Appearance.FooterPanel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridView3.Appearance.FooterPanel.Options.UseFont = true;
            this.gridView3.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridView3.Appearance.HeaderPanel.Options.UseFont = true;
            this.gridView3.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridView3.Appearance.Row.Options.UseFont = true;
            this.gridView3.DetailHeight = 547;
            this.gridView3.FixedLineWidth = 3;
            this.gridView3.GridControl = this.gridControl2;
            this.gridView3.Name = "gridView3";
            this.gridView3.OptionsView.ColumnAutoWidth = false;
            this.gridView3.OptionsView.RowAutoHeight = true;
            this.gridView3.OptionsView.ShowFooter = true;
            this.gridView3.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.gridView3_RowCellStyle);
            this.gridView3.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.gridView3_RowStyle);
            this.gridView3.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.gridView3_CellValueChanged);
            // 
            // groupControl3
            // 
            this.groupControl3.Appearance.Font = new System.Drawing.Font("Arial", 9.25F);
            this.groupControl3.Appearance.Options.UseFont = true;
            this.groupControl3.Controls.Add(this.simpleButton1);
            this.groupControl3.Controls.Add(this.labelControl5);
            this.groupControl3.Controls.Add(this.textEdit1);
            this.groupControl3.Controls.Add(this.labelControl4);
            this.groupControl3.Controls.Add(this.txtcalcres);
            this.groupControl3.Controls.Add(this.btncalc);
            this.groupControl3.Controls.Add(this.txtcttrto);
            this.groupControl3.Controls.Add(this.labelControl3);
            this.groupControl3.Controls.Add(this.txtcttrfrom);
            this.groupControl3.Controls.Add(this.labelControl2);
            this.groupControl3.Controls.Add(this.progressBar1);
            this.groupControl3.Controls.Add(this.txtgoal);
            this.groupControl3.Controls.Add(this.labelControl1);
            this.groupControl3.Controls.Add(this.txtnewtotalamount);
            this.groupControl3.Controls.Add(this.txtvariance);
            this.groupControl3.Controls.Add(this.labelControl13);
            this.groupControl3.Controls.Add(this.btnanalyze);
            this.groupControl3.Controls.Add(this.txttargetsales);
            this.groupControl3.Controls.Add(this.txtrefundamount);
            this.groupControl3.Controls.Add(this.labelControl11);
            this.groupControl3.Controls.Add(this.labelControl10);
            this.groupControl3.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupControl3.Location = new System.Drawing.Point(0, 0);
            this.groupControl3.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.groupControl3.Name = "groupControl3";
            this.groupControl3.Size = new System.Drawing.Size(2724, 303);
            this.groupControl3.TabIndex = 4;
            // 
            // simpleButton1
            // 
            this.simpleButton1.Appearance.Font = new System.Drawing.Font("Arial", 10.25F);
            this.simpleButton1.Appearance.Options.UseFont = true;
            this.simpleButton1.Location = new System.Drawing.Point(620, 179);
            this.simpleButton1.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(158, 104);
            this.simpleButton1.TabIndex = 47;
            this.simpleButton1.Text = "Reset";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // labelControl5
            // 
            this.labelControl5.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl5.Appearance.Options.UseFont = true;
            this.labelControl5.Location = new System.Drawing.Point(906, 177);
            this.labelControl5.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(39, 36);
            this.labelControl5.TabIndex = 46;
            this.labelControl5.Text = "Hit:";
            // 
            // textEdit1
            // 
            this.textEdit1.EditValue = "0";
            this.textEdit1.Location = new System.Drawing.Point(958, 172);
            this.textEdit1.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.textEdit1.Name = "textEdit1";
            this.textEdit1.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.textEdit1.Properties.Appearance.Options.UseFont = true;
            this.textEdit1.Size = new System.Drawing.Size(200, 50);
            this.textEdit1.TabIndex = 45;
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl4.Appearance.Options.UseFont = true;
            this.labelControl4.Location = new System.Drawing.Point(78, 246);
            this.labelControl4.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(77, 36);
            this.labelControl4.TabIndex = 44;
            this.labelControl4.Text = "Result:";
            // 
            // txtcalcres
            // 
            this.txtcalcres.EditValue = "0";
            this.txtcalcres.Location = new System.Drawing.Point(168, 240);
            this.txtcalcres.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.txtcalcres.Name = "txtcalcres";
            this.txtcalcres.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.txtcalcres.Properties.Appearance.Options.UseFont = true;
            this.txtcalcres.Size = new System.Drawing.Size(274, 50);
            this.txtcalcres.TabIndex = 43;
            // 
            // btncalc
            // 
            this.btncalc.Appearance.Font = new System.Drawing.Font("Arial", 10.25F);
            this.btncalc.Appearance.Options.UseFont = true;
            this.btncalc.Location = new System.Drawing.Point(452, 179);
            this.btncalc.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.btncalc.Name = "btncalc";
            this.btncalc.Size = new System.Drawing.Size(158, 104);
            this.btncalc.TabIndex = 42;
            this.btncalc.Text = "Calculate";
            this.btncalc.Click += new System.EventHandler(this.btncalc_Click);
            // 
            // txtcttrto
            // 
            this.txtcttrto.EditValue = "0";
            this.txtcttrto.Location = new System.Drawing.Point(332, 185);
            this.txtcttrto.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.txtcttrto.Name = "txtcttrto";
            this.txtcttrto.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.txtcttrto.Properties.Appearance.Options.UseFont = true;
            this.txtcttrto.Size = new System.Drawing.Size(110, 50);
            this.txtcttrto.TabIndex = 41;
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl3.Appearance.Options.UseFont = true;
            this.labelControl3.Location = new System.Drawing.Point(288, 189);
            this.labelControl3.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(35, 36);
            this.labelControl3.TabIndex = 40;
            this.labelControl3.Text = "To:";
            // 
            // txtcttrfrom
            // 
            this.txtcttrfrom.EditValue = "0";
            this.txtcttrfrom.Location = new System.Drawing.Point(168, 185);
            this.txtcttrfrom.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.txtcttrfrom.Name = "txtcttrfrom";
            this.txtcttrfrom.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.txtcttrfrom.Properties.Appearance.Options.UseFont = true;
            this.txtcttrfrom.Size = new System.Drawing.Size(110, 50);
            this.txtcttrfrom.TabIndex = 39;
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Location = new System.Drawing.Point(46, 189);
            this.labelControl2.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(106, 36);
            this.labelControl2.TabIndex = 38;
            this.labelControl2.Text = "Ctr From:";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(1170, 61);
            this.progressBar1.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(488, 97);
            this.progressBar1.TabIndex = 37;
            // 
            // txtgoal
            // 
            this.txtgoal.EditValue = "0";
            this.txtgoal.Location = new System.Drawing.Point(958, 115);
            this.txtgoal.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.txtgoal.Name = "txtgoal";
            this.txtgoal.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.txtgoal.Properties.Appearance.Options.UseFont = true;
            this.txtgoal.Size = new System.Drawing.Size(200, 50);
            this.txtgoal.TabIndex = 28;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(886, 123);
            this.labelControl1.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(58, 36);
            this.labelControl1.TabIndex = 27;
            this.labelControl1.Text = "Goal:";
            // 
            // txtnewtotalamount
            // 
            this.txtnewtotalamount.EditValue = "0";
            this.txtnewtotalamount.Location = new System.Drawing.Point(958, 64);
            this.txtnewtotalamount.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.txtnewtotalamount.Name = "txtnewtotalamount";
            this.txtnewtotalamount.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.txtnewtotalamount.Properties.Appearance.Options.UseFont = true;
            this.txtnewtotalamount.Size = new System.Drawing.Size(200, 50);
            this.txtnewtotalamount.TabIndex = 26;
            this.txtnewtotalamount.Visible = false;
            // 
            // txtvariance
            // 
            this.txtvariance.EditValue = "0";
            this.txtvariance.Location = new System.Drawing.Point(216, 115);
            this.txtvariance.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.txtvariance.Name = "txtvariance";
            this.txtvariance.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.txtvariance.Properties.Appearance.Options.UseFont = true;
            this.txtvariance.Size = new System.Drawing.Size(200, 50);
            this.txtvariance.TabIndex = 25;
            // 
            // labelControl13
            // 
            this.labelControl13.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl13.Appearance.Options.UseFont = true;
            this.labelControl13.Location = new System.Drawing.Point(98, 122);
            this.labelControl13.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.labelControl13.Name = "labelControl13";
            this.labelControl13.Size = new System.Drawing.Size(104, 36);
            this.labelControl13.TabIndex = 24;
            this.labelControl13.Text = "Variance:";
            // 
            // btnanalyze
            // 
            this.btnanalyze.Appearance.Font = new System.Drawing.Font("Arial", 10.25F);
            this.btnanalyze.Appearance.Options.UseFont = true;
            this.btnanalyze.Location = new System.Drawing.Point(428, 114);
            this.btnanalyze.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.btnanalyze.Name = "btnanalyze";
            this.btnanalyze.Size = new System.Drawing.Size(446, 52);
            this.btnanalyze.TabIndex = 21;
            this.btnanalyze.Text = "Update Changes";
            this.btnanalyze.Click += new System.EventHandler(this.btnanalyze_Click);
            // 
            // txttargetsales
            // 
            this.txttargetsales.EditValue = "0";
            this.txttargetsales.Location = new System.Drawing.Point(674, 64);
            this.txttargetsales.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.txttargetsales.Name = "txttargetsales";
            this.txttargetsales.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.txttargetsales.Properties.Appearance.Options.UseFont = true;
            this.txttargetsales.Size = new System.Drawing.Size(200, 50);
            this.txttargetsales.TabIndex = 20;
            // 
            // txtrefundamount
            // 
            this.txtrefundamount.EditValue = "0";
            this.txtrefundamount.Location = new System.Drawing.Point(216, 64);
            this.txtrefundamount.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.txtrefundamount.Name = "txtrefundamount";
            this.txtrefundamount.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.txtrefundamount.Properties.Appearance.Options.UseFont = true;
            this.txtrefundamount.Size = new System.Drawing.Size(200, 50);
            this.txtrefundamount.TabIndex = 19;
            // 
            // labelControl11
            // 
            this.labelControl11.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl11.Appearance.Options.UseFont = true;
            this.labelControl11.Location = new System.Drawing.Point(428, 68);
            this.labelControl11.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.labelControl11.Name = "labelControl11";
            this.labelControl11.Size = new System.Drawing.Size(230, 36);
            this.labelControl11.TabIndex = 15;
            this.labelControl11.Text = "Target Z-Reset Sale:";
            // 
            // labelControl10
            // 
            this.labelControl10.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl10.Appearance.Options.UseFont = true;
            this.labelControl10.Location = new System.Drawing.Point(24, 68);
            this.labelControl10.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.labelControl10.Name = "labelControl10";
            this.labelControl10.Size = new System.Drawing.Size(189, 36);
            this.labelControl10.TabIndex = 14;
            this.labelControl10.Text = "Refund Amount:";
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // ManipulateDataDevEx
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(2724, 1333);
            this.Controls.Add(this.groupControl4);
            this.Controls.Add(this.groupControl3);
            this.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.Name = "ManipulateDataDevEx";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ManipulateDataDevEx";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.ManipulateDataDevEx_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl4)).EndInit();
            this.groupControl4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).EndInit();
            this.groupControl3.ResumeLayout(false);
            this.groupControl3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtcalcres.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtcttrto.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtcttrfrom.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtgoal.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtnewtotalamount.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtvariance.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txttargetsales.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtrefundamount.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl4;
        public DevExpress.XtraGrid.Views.Grid.GridView gridView3;
        public DevExpress.XtraGrid.GridControl gridControl2;
        private DevExpress.XtraEditors.GroupControl groupControl3;
        private DevExpress.XtraEditors.LabelControl labelControl13;
        private DevExpress.XtraEditors.SimpleButton btnanalyze;
        private DevExpress.XtraEditors.LabelControl labelControl11;
        private DevExpress.XtraEditors.LabelControl labelControl10;
        public DevExpress.XtraEditors.TextEdit txtvariance;
        public DevExpress.XtraEditors.TextEdit txttargetsales;
        public DevExpress.XtraEditors.TextEdit txtrefundamount;
        public DevExpress.XtraEditors.TextEdit txtnewtotalamount;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        public DevExpress.XtraEditors.TextEdit txtgoal;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Timer timer1;
        public DevExpress.XtraEditors.TextEdit txtcttrto;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        public DevExpress.XtraEditors.TextEdit txtcttrfrom;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.SimpleButton btncalc;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        public DevExpress.XtraEditors.TextEdit txtcalcres;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        public DevExpress.XtraEditors.TextEdit textEdit1;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
    }
}