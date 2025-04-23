namespace SalesInventorySystem.POS
{
    partial class POSplitBillFinal
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
            this.txtdiscount = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtinvoiceno = new System.Windows.Forms.TextBox();
            this.txtamountchange = new System.Windows.Forms.TextBox();
            this.txtamounttender = new DevExpress.XtraEditors.CalcEdit();
            this.txtamountpayable = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.btnconfirm = new DevExpress.XtraEditors.SimpleButton();
            this.lblvat = new DevExpress.XtraEditors.LabelControl();
            this.lblvatexemptsale = new DevExpress.XtraEditors.LabelControl();
            this.txttotalcredit = new System.Windows.Forms.TextBox();
            this.lblvatsale = new DevExpress.XtraEditors.LabelControl();
            this.label7 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txttotalcash = new System.Windows.Forms.TextBox();
            this.btncancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnpay = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.reppaymenttype = new DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit();
            this.repositoryItemSearchLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.repamount = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.repreferenceno = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addEntryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.txtamounttender.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.reppaymenttype)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSearchLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repamount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repreferenceno)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtdiscount
            // 
            this.txtdiscount.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.txtdiscount.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtdiscount.Location = new System.Drawing.Point(289, 234);
            this.txtdiscount.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.txtdiscount.MaxLength = 8;
            this.txtdiscount.Name = "txtdiscount";
            this.txtdiscount.ReadOnly = true;
            this.txtdiscount.Size = new System.Drawing.Size(505, 38);
            this.txtdiscount.TabIndex = 46;
            this.txtdiscount.Text = "0";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(33, 244);
            this.label1.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(129, 31);
            this.label1.TabIndex = 45;
            this.label1.Text = "Discount:";
            // 
            // txtinvoiceno
            // 
            this.txtinvoiceno.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtinvoiceno.Location = new System.Drawing.Point(289, 16);
            this.txtinvoiceno.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.txtinvoiceno.Name = "txtinvoiceno";
            this.txtinvoiceno.ReadOnly = true;
            this.txtinvoiceno.Size = new System.Drawing.Size(505, 38);
            this.txtinvoiceno.TabIndex = 42;
            // 
            // txtamountchange
            // 
            this.txtamountchange.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtamountchange.Location = new System.Drawing.Point(289, 181);
            this.txtamountchange.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.txtamountchange.Name = "txtamountchange";
            this.txtamountchange.ReadOnly = true;
            this.txtamountchange.Size = new System.Drawing.Size(505, 38);
            this.txtamountchange.TabIndex = 44;
            // 
            // txtamounttender
            // 
            this.txtamounttender.Location = new System.Drawing.Point(289, 127);
            this.txtamounttender.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.txtamounttender.Name = "txtamounttender";
            this.txtamounttender.Properties.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtamounttender.Properties.Appearance.Options.UseFont = true;
            this.txtamounttender.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtamounttender.Size = new System.Drawing.Size(507, 46);
            this.txtamounttender.TabIndex = 37;
            this.txtamounttender.EditValueChanged += new System.EventHandler(this.txtamounttender_EditValueChanged);
            // 
            // txtamountpayable
            // 
            this.txtamountpayable.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtamountpayable.Location = new System.Drawing.Point(289, 72);
            this.txtamountpayable.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.txtamountpayable.Name = "txtamountpayable";
            this.txtamountpayable.ReadOnly = true;
            this.txtamountpayable.Size = new System.Drawing.Size(505, 38);
            this.txtamountpayable.TabIndex = 43;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(30, 22);
            this.label3.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(131, 31);
            this.label3.TabIndex = 38;
            this.label3.Text = "Invoice #:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(30, 77);
            this.label4.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(220, 31);
            this.label4.TabIndex = 39;
            this.label4.Text = "Amount Payable:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(30, 187);
            this.label6.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(217, 31);
            this.label6.TabIndex = 41;
            this.label6.Text = "Amount Change:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(30, 133);
            this.label5.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(208, 31);
            this.label5.TabIndex = 40;
            this.label5.Text = "Amount Tender:";
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.btnconfirm);
            this.panelControl1.Controls.Add(this.lblvat);
            this.panelControl1.Controls.Add(this.lblvatexemptsale);
            this.panelControl1.Controls.Add(this.txttotalcredit);
            this.panelControl1.Controls.Add(this.lblvatsale);
            this.panelControl1.Controls.Add(this.label7);
            this.panelControl1.Controls.Add(this.label2);
            this.panelControl1.Controls.Add(this.txttotalcash);
            this.panelControl1.Controls.Add(this.btncancel);
            this.panelControl1.Controls.Add(this.btnpay);
            this.panelControl1.Controls.Add(this.label3);
            this.panelControl1.Controls.Add(this.label5);
            this.panelControl1.Controls.Add(this.txtdiscount);
            this.panelControl1.Controls.Add(this.label6);
            this.panelControl1.Controls.Add(this.label1);
            this.panelControl1.Controls.Add(this.label4);
            this.panelControl1.Controls.Add(this.txtinvoiceno);
            this.panelControl1.Controls.Add(this.txtamountpayable);
            this.panelControl1.Controls.Add(this.txtamountchange);
            this.panelControl1.Controls.Add(this.txtamounttender);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1560, 309);
            this.panelControl1.TabIndex = 47;
            // 
            // btnconfirm
            // 
            this.btnconfirm.Location = new System.Drawing.Point(859, 149);
            this.btnconfirm.Name = "btnconfirm";
            this.btnconfirm.Size = new System.Drawing.Size(169, 56);
            this.btnconfirm.TabIndex = 102;
            this.btnconfirm.Text = "Confirm";
            this.btnconfirm.Click += new System.EventHandler(this.simpleButton3_Click);
            // 
            // lblvat
            // 
            this.lblvat.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblvat.Appearance.ForeColor = System.Drawing.Color.MediumSeaGreen;
            this.lblvat.Appearance.Options.UseFont = true;
            this.lblvat.Appearance.Options.UseForeColor = true;
            this.lblvat.Location = new System.Drawing.Point(1449, 217);
            this.lblvat.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.lblvat.Name = "lblvat";
            this.lblvat.Size = new System.Drawing.Size(56, 31);
            this.lblvat.TabIndex = 101;
            this.lblvat.Text = "9999";
            // 
            // lblvatexemptsale
            // 
            this.lblvatexemptsale.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblvatexemptsale.Appearance.ForeColor = System.Drawing.Color.MediumSeaGreen;
            this.lblvatexemptsale.Appearance.Options.UseFont = true;
            this.lblvatexemptsale.Appearance.Options.UseForeColor = true;
            this.lblvatexemptsale.Location = new System.Drawing.Point(1476, 174);
            this.lblvatexemptsale.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.lblvatexemptsale.Name = "lblvatexemptsale";
            this.lblvatexemptsale.Size = new System.Drawing.Size(14, 31);
            this.lblvatexemptsale.TabIndex = 100;
            this.lblvatexemptsale.Text = "0";
            // 
            // txttotalcredit
            // 
            this.txttotalcredit.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txttotalcredit.Location = new System.Drawing.Point(1033, 80);
            this.txttotalcredit.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.txttotalcredit.Name = "txttotalcredit";
            this.txttotalcredit.ReadOnly = true;
            this.txttotalcredit.Size = new System.Drawing.Size(310, 38);
            this.txttotalcredit.TabIndex = 52;
            // 
            // lblvatsale
            // 
            this.lblvatsale.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblvatsale.Appearance.ForeColor = System.Drawing.Color.MediumSeaGreen;
            this.lblvatsale.Appearance.Options.UseFont = true;
            this.lblvatsale.Appearance.Options.UseForeColor = true;
            this.lblvatsale.Location = new System.Drawing.Point(1476, 134);
            this.lblvatsale.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.lblvatsale.Name = "lblvatsale";
            this.lblvatsale.Size = new System.Drawing.Size(14, 31);
            this.lblvatsale.TabIndex = 99;
            this.lblvatsale.Text = "0";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(865, 87);
            this.label7.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(163, 31);
            this.label7.TabIndex = 51;
            this.label7.Text = "Total Credit:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(865, 33);
            this.label2.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(154, 31);
            this.label2.TabIndex = 50;
            this.label2.Text = "Total Cash:";
            // 
            // txttotalcash
            // 
            this.txttotalcash.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txttotalcash.Location = new System.Drawing.Point(1033, 30);
            this.txttotalcash.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.txttotalcash.Name = "txttotalcash";
            this.txttotalcash.ReadOnly = true;
            this.txttotalcash.Size = new System.Drawing.Size(310, 38);
            this.txttotalcash.TabIndex = 49;
            // 
            // btncancel
            // 
            this.btncancel.Location = new System.Drawing.Point(1208, 149);
            this.btncancel.Name = "btncancel";
            this.btncancel.Size = new System.Drawing.Size(169, 56);
            this.btncancel.TabIndex = 48;
            this.btncancel.Text = "Cancel";
            this.btncancel.Click += new System.EventHandler(this.btncancel_Click);
            // 
            // btnpay
            // 
            this.btnpay.Location = new System.Drawing.Point(1033, 149);
            this.btnpay.Name = "btnpay";
            this.btnpay.Size = new System.Drawing.Size(169, 56);
            this.btnpay.TabIndex = 47;
            this.btnpay.Text = "Pay";
            this.btnpay.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.groupBox2);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl2.Location = new System.Drawing.Point(0, 309);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(1560, 693);
            this.panelControl2.TabIndex = 48;
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.White;
            this.groupBox2.Controls.Add(this.gridControl1);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(3, 3);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(5, 7, 5, 7);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(5, 7, 5, 7);
            this.groupBox2.Size = new System.Drawing.Size(1554, 687);
            this.groupBox2.TabIndex = 452;
            this.groupBox2.TabStop = false;
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(5, 7, 5, 7);
            this.gridControl1.Location = new System.Drawing.Point(5, 33);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Margin = new System.Windows.Forms.Padding(5, 7, 5, 7);
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.reppaymenttype,
            this.repamount,
            this.repreferenceno});
            this.gridControl1.Size = new System.Drawing.Size(1544, 647);
            this.gridControl1.TabIndex = 1;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            this.gridControl1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.gridControl1_MouseUp_1);
            // 
            // gridView1
            // 
            this.gridView1.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 9.25F, System.Drawing.FontStyle.Bold);
            this.gridView1.Appearance.HeaderPanel.Options.UseFont = true;
            this.gridView1.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 9.25F);
            this.gridView1.Appearance.Row.Options.UseFont = true;
            this.gridView1.DetailHeight = 674;
            this.gridView1.FixedLineWidth = 3;
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsView.ColumnAutoWidth = false;
            this.gridView1.OptionsView.RowAutoHeight = true;
            this.gridView1.OptionsView.ShowFooter = true;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.CustomRowCellEdit += new DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventHandler(this.gridView1_CustomRowCellEdit);
            this.gridView1.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.gridView1_CellValueChanged);
            // 
            // reppaymenttype
            // 
            this.reppaymenttype.AutoHeight = false;
            this.reppaymenttype.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFit;
            this.reppaymenttype.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.reppaymenttype.Name = "reppaymenttype";
            this.reppaymenttype.NullText = "";
            this.reppaymenttype.PopupView = this.repositoryItemSearchLookUpEdit1View;
            // 
            // repositoryItemSearchLookUpEdit1View
            // 
            this.repositoryItemSearchLookUpEdit1View.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.repositoryItemSearchLookUpEdit1View.Appearance.HeaderPanel.Options.UseFont = true;
            this.repositoryItemSearchLookUpEdit1View.DetailHeight = 547;
            this.repositoryItemSearchLookUpEdit1View.FixedLineWidth = 3;
            this.repositoryItemSearchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.repositoryItemSearchLookUpEdit1View.Name = "repositoryItemSearchLookUpEdit1View";
            this.repositoryItemSearchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.repositoryItemSearchLookUpEdit1View.OptionsView.ColumnAutoWidth = false;
            this.repositoryItemSearchLookUpEdit1View.OptionsView.RowAutoHeight = true;
            this.repositoryItemSearchLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            // 
            // repamount
            // 
            this.repamount.AutoHeight = false;
            this.repamount.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repamount.Name = "repamount";
            // 
            // repreferenceno
            // 
            this.repreferenceno.AutoHeight = false;
            this.repreferenceno.Name = "repreferenceno";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addEntryToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(195, 40);
            // 
            // addEntryToolStripMenuItem
            // 
            this.addEntryToolStripMenuItem.Name = "addEntryToolStripMenuItem";
            this.addEntryToolStripMenuItem.Size = new System.Drawing.Size(194, 36);
            this.addEntryToolStripMenuItem.Text = "Add Entry";
            this.addEntryToolStripMenuItem.Click += new System.EventHandler(this.addEntryToolStripMenuItem_Click);
            // 
            // POSplitBillFinal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1560, 1002);
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.panelControl1);
            this.Name = "POSplitBillFinal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "POSplitBillFinal";
            this.Load += new System.EventHandler(this.POSplitBillFinal_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtamounttender.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.reppaymenttype)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSearchLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repamount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repreferenceno)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.TextBox txtdiscount;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox txtamountchange;
        private DevExpress.XtraEditors.CalcEdit txtamounttender;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem addEntryToolStripMenuItem;
        public DevExpress.XtraGrid.GridControl gridControl1;
        public DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit reppaymenttype;
        private DevExpress.XtraGrid.Views.Grid.GridView repositoryItemSearchLookUpEdit1View;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit repamount;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repreferenceno;
        public System.Windows.Forms.TextBox txtinvoiceno;
        public System.Windows.Forms.TextBox txtamountpayable;
        private DevExpress.XtraEditors.SimpleButton btnpay;
        private DevExpress.XtraEditors.SimpleButton btncancel;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.TextBox txttotalcash;
        public System.Windows.Forms.TextBox txttotalcredit;
        private System.Windows.Forms.Label label7;
        public DevExpress.XtraEditors.LabelControl lblvat;
        public DevExpress.XtraEditors.LabelControl lblvatexemptsale;
        public DevExpress.XtraEditors.LabelControl lblvatsale;
        private DevExpress.XtraEditors.SimpleButton btnconfirm;
    }
}