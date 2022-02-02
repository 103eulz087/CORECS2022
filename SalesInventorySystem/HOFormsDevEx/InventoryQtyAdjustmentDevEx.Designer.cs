namespace SalesInventorySystem.HOFormsDevEx
{
    partial class InventoryQtyAdjustmentDevEx
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
            this.txtsupplier = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.searchLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.lblsupplier = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.txtnewqty = new DevExpress.XtraEditors.TextEdit();
            this.txtqtyadj = new DevExpress.XtraEditors.TextEdit();
            this.txtcostkg = new DevExpress.XtraEditors.TextEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txtbranch = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.radadd = new System.Windows.Forms.RadioButton();
            this.raddeduct = new System.Windows.Forms.RadioButton();
            this.radlinktosupplier = new System.Windows.Forms.RadioButton();
            this.radintransit = new System.Windows.Forms.RadioButton();
            this.txtshipmentno = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.ShipmentNo = new DevExpress.XtraEditors.LabelControl();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.txtproduct = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.gridView3 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.txtqty = new DevExpress.XtraEditors.TextEdit();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.txtorigqty = new DevExpress.XtraEditors.TextEdit();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.txtsupplier.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtnewqty.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtqtyadj.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtcostkg.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtbranch.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtshipmentno.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtproduct.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtqty.Properties)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtorigqty.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // txtsupplier
            // 
            this.txtsupplier.Location = new System.Drawing.Point(131, 123);
            this.txtsupplier.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtsupplier.Name = "txtsupplier";
            this.txtsupplier.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtsupplier.Properties.Appearance.Options.UseFont = true;
            this.txtsupplier.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtsupplier.Properties.NullText = "";
            this.txtsupplier.Properties.PopupView = this.searchLookUpEdit1View;
            this.txtsupplier.Size = new System.Drawing.Size(278, 28);
            this.txtsupplier.TabIndex = 80;
            this.txtsupplier.EditValueChanged += new System.EventHandler(this.txtsupplier_EditValueChanged);
            // 
            // searchLookUpEdit1View
            // 
            this.searchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.searchLookUpEdit1View.Name = "searchLookUpEdit1View";
            this.searchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.searchLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            // 
            // lblsupplier
            // 
            this.lblsupplier.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.lblsupplier.Appearance.Options.UseFont = true;
            this.lblsupplier.Location = new System.Drawing.Point(57, 127);
            this.lblsupplier.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lblsupplier.Name = "lblsupplier";
            this.lblsupplier.Size = new System.Drawing.Size(65, 21);
            this.lblsupplier.TabIndex = 79;
            this.lblsupplier.Text = "Supplier:";
            // 
            // labelControl5
            // 
            this.labelControl5.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.labelControl5.Appearance.Options.UseFont = true;
            this.labelControl5.Location = new System.Drawing.Point(69, 50);
            this.labelControl5.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(57, 21);
            this.labelControl5.TabIndex = 72;
            this.labelControl5.Text = "Branch:";
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Tahoma", 10F);
            this.button2.Location = new System.Drawing.Point(225, 411);
            this.button2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(87, 36);
            this.button2.TabIndex = 71;
            this.button2.Text = "Cancel";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Tahoma", 10F);
            this.button1.Location = new System.Drawing.Point(131, 411);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(87, 36);
            this.button1.TabIndex = 70;
            this.button1.Text = "Save";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtnewqty
            // 
            this.txtnewqty.EditValue = "0";
            this.txtnewqty.Location = new System.Drawing.Point(131, 375);
            this.txtnewqty.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtnewqty.Name = "txtnewqty";
            this.txtnewqty.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtnewqty.Properties.Appearance.Options.UseFont = true;
            this.txtnewqty.Properties.ReadOnly = true;
            this.txtnewqty.Size = new System.Drawing.Size(116, 28);
            this.txtnewqty.TabIndex = 69;
            // 
            // txtqtyadj
            // 
            this.txtqtyadj.EditValue = "0";
            this.txtqtyadj.Location = new System.Drawing.Point(131, 337);
            this.txtqtyadj.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtqtyadj.Name = "txtqtyadj";
            this.txtqtyadj.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtqtyadj.Properties.Appearance.Options.UseFont = true;
            this.txtqtyadj.Size = new System.Drawing.Size(116, 28);
            this.txtqtyadj.TabIndex = 68;
            this.txtqtyadj.EditValueChanged += new System.EventHandler(this.txtqtyadj_EditValueChanged);
            // 
            // txtcostkg
            // 
            this.txtcostkg.EditValue = "";
            this.txtcostkg.Location = new System.Drawing.Point(131, 226);
            this.txtcostkg.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtcostkg.Name = "txtcostkg";
            this.txtcostkg.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtcostkg.Properties.Appearance.Options.UseFont = true;
            this.txtcostkg.Properties.ReadOnly = true;
            this.txtcostkg.Size = new System.Drawing.Size(116, 28);
            this.txtcostkg.TabIndex = 66;
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.labelControl4.Appearance.Options.UseFont = true;
            this.labelControl4.Location = new System.Drawing.Point(52, 379);
            this.labelControl4.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(70, 21);
            this.labelControl4.TabIndex = 65;
            this.labelControl4.Text = "New Qty:";
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.labelControl3.Appearance.Options.UseFont = true;
            this.labelControl3.Location = new System.Drawing.Point(7, 341);
            this.labelControl3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(123, 21);
            this.labelControl3.TabIndex = 64;
            this.labelControl3.Text = "Qty Adjustment:";
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(61, 230);
            this.labelControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(63, 21);
            this.labelControl1.TabIndex = 62;
            this.labelControl1.Text = "Cost Kg:";
            // 
            // txtbranch
            // 
            this.txtbranch.Location = new System.Drawing.Point(132, 47);
            this.txtbranch.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtbranch.Name = "txtbranch";
            this.txtbranch.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtbranch.Properties.Appearance.Options.UseFont = true;
            this.txtbranch.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtbranch.Properties.NullText = "";
            this.txtbranch.Properties.PopupView = this.gridView1;
            this.txtbranch.Size = new System.Drawing.Size(280, 28);
            this.txtbranch.TabIndex = 81;
            this.txtbranch.EditValueChanged += new System.EventHandler(this.txtbranch_EditValueChanged);
            // 
            // gridView1
            // 
            this.gridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // radadd
            // 
            this.radadd.AutoSize = true;
            this.radadd.Font = new System.Drawing.Font("Tahoma", 9.25F);
            this.radadd.Location = new System.Drawing.Point(2, 4);
            this.radadd.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.radadd.Name = "radadd";
            this.radadd.Size = new System.Drawing.Size(63, 23);
            this.radadd.TabIndex = 82;
            this.radadd.Text = "ADD";
            this.radadd.UseVisualStyleBackColor = true;
            this.radadd.CheckedChanged += new System.EventHandler(this.radadd_CheckedChanged);
            // 
            // raddeduct
            // 
            this.raddeduct.AutoSize = true;
            this.raddeduct.Font = new System.Drawing.Font("Tahoma", 9.25F);
            this.raddeduct.Location = new System.Drawing.Point(69, 4);
            this.raddeduct.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.raddeduct.Name = "raddeduct";
            this.raddeduct.Size = new System.Drawing.Size(92, 23);
            this.raddeduct.TabIndex = 83;
            this.raddeduct.TabStop = true;
            this.raddeduct.Text = "DEDUCT";
            this.raddeduct.UseVisualStyleBackColor = true;
            this.raddeduct.CheckedChanged += new System.EventHandler(this.raddeduct_CheckedChanged);
            // 
            // radlinktosupplier
            // 
            this.radlinktosupplier.AutoSize = true;
            this.radlinktosupplier.Font = new System.Drawing.Font("Tahoma", 9.25F);
            this.radlinktosupplier.Location = new System.Drawing.Point(97, 4);
            this.radlinktosupplier.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.radlinktosupplier.Name = "radlinktosupplier";
            this.radlinktosupplier.Size = new System.Drawing.Size(141, 23);
            this.radlinktosupplier.TabIndex = 85;
            this.radlinktosupplier.TabStop = true;
            this.radlinktosupplier.Text = "Link to Supplier";
            this.radlinktosupplier.UseVisualStyleBackColor = true;
            this.radlinktosupplier.CheckedChanged += new System.EventHandler(this.radlinktosupplier_CheckedChanged);
            // 
            // radintransit
            // 
            this.radintransit.AutoSize = true;
            this.radintransit.Font = new System.Drawing.Font("Tahoma", 9.25F);
            this.radintransit.Location = new System.Drawing.Point(3, 4);
            this.radintransit.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.radintransit.Name = "radintransit";
            this.radintransit.Size = new System.Drawing.Size(94, 23);
            this.radintransit.TabIndex = 84;
            this.radintransit.TabStop = true;
            this.radintransit.Text = "InTransit";
            this.radintransit.UseVisualStyleBackColor = true;
            this.radintransit.CheckedChanged += new System.EventHandler(this.radintransit_CheckedChanged);
            // 
            // txtshipmentno
            // 
            this.txtshipmentno.Location = new System.Drawing.Point(131, 158);
            this.txtshipmentno.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtshipmentno.Name = "txtshipmentno";
            this.txtshipmentno.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtshipmentno.Properties.Appearance.Options.UseFont = true;
            this.txtshipmentno.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtshipmentno.Properties.NullText = "";
            this.txtshipmentno.Properties.PopupView = this.gridView2;
            this.txtshipmentno.Size = new System.Drawing.Size(278, 28);
            this.txtshipmentno.TabIndex = 87;
            this.txtshipmentno.EditValueChanged += new System.EventHandler(this.txtshipmentno_EditValueChanged);
            // 
            // gridView2
            // 
            this.gridView2.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView2.Name = "gridView2";
            this.gridView2.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView2.OptionsView.ShowGroupPanel = false;
            // 
            // ShipmentNo
            // 
            this.ShipmentNo.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.ShipmentNo.Appearance.Options.UseFont = true;
            this.ShipmentNo.Location = new System.Drawing.Point(31, 161);
            this.ShipmentNo.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ShipmentNo.Name = "ShipmentNo";
            this.ShipmentNo.Size = new System.Drawing.Size(95, 21);
            this.ShipmentNo.TabIndex = 86;
            this.ShipmentNo.Text = "ShipmentNo:";
            // 
            // labelControl7
            // 
            this.labelControl7.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.labelControl7.Appearance.Options.UseFont = true;
            this.labelControl7.Location = new System.Drawing.Point(62, 196);
            this.labelControl7.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(62, 21);
            this.labelControl7.TabIndex = 88;
            this.labelControl7.Text = "Product:";
            // 
            // txtproduct
            // 
            this.txtproduct.Location = new System.Drawing.Point(131, 192);
            this.txtproduct.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtproduct.Name = "txtproduct";
            this.txtproduct.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtproduct.Properties.Appearance.Options.UseFont = true;
            this.txtproduct.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtproduct.Properties.NullText = "";
            this.txtproduct.Properties.PopupView = this.gridView3;
            this.txtproduct.Size = new System.Drawing.Size(278, 28);
            this.txtproduct.TabIndex = 89;
            this.txtproduct.EditValueChanged += new System.EventHandler(this.txtproduct_EditValueChanged);
            // 
            // gridView3
            // 
            this.gridView3.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView3.Name = "gridView3";
            this.gridView3.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView3.OptionsView.ShowGroupPanel = false;
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Location = new System.Drawing.Point(24, 302);
            this.labelControl2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(104, 21);
            this.labelControl2.TabIndex = 63;
            this.labelControl2.Text = "Available Qty:";
            // 
            // txtqty
            // 
            this.txtqty.EditValue = "";
            this.txtqty.Location = new System.Drawing.Point(131, 298);
            this.txtqty.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtqty.Name = "txtqty";
            this.txtqty.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtqty.Properties.Appearance.Options.UseFont = true;
            this.txtqty.Properties.ReadOnly = true;
            this.txtqty.Size = new System.Drawing.Size(116, 28);
            this.txtqty.TabIndex = 67;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.radintransit);
            this.panel1.Controls.Add(this.radlinktosupplier);
            this.panel1.Location = new System.Drawing.Point(132, 81);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(261, 34);
            this.panel1.TabIndex = 90;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.raddeduct);
            this.panel2.Controls.Add(this.radadd);
            this.panel2.Location = new System.Drawing.Point(132, 7);
            this.panel2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(234, 32);
            this.panel2.TabIndex = 91;
            // 
            // txtorigqty
            // 
            this.txtorigqty.EditValue = "";
            this.txtorigqty.Location = new System.Drawing.Point(131, 262);
            this.txtorigqty.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtorigqty.Name = "txtorigqty";
            this.txtorigqty.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtorigqty.Properties.Appearance.Options.UseFont = true;
            this.txtorigqty.Properties.ReadOnly = true;
            this.txtorigqty.Size = new System.Drawing.Size(116, 28);
            this.txtorigqty.TabIndex = 92;
            // 
            // labelControl6
            // 
            this.labelControl6.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.labelControl6.Appearance.Options.UseFont = true;
            this.labelControl6.Location = new System.Drawing.Point(54, 265);
            this.labelControl6.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(68, 21);
            this.labelControl6.TabIndex = 93;
            this.labelControl6.Text = "Orig Qty:";
            // 
            // InventoryQtyAdjustmentDevEx
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(432, 457);
            this.Controls.Add(this.labelControl6);
            this.Controls.Add(this.txtorigqty);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.txtproduct);
            this.Controls.Add(this.labelControl7);
            this.Controls.Add(this.txtshipmentno);
            this.Controls.Add(this.ShipmentNo);
            this.Controls.Add(this.txtbranch);
            this.Controls.Add(this.txtsupplier);
            this.Controls.Add(this.lblsupplier);
            this.Controls.Add(this.labelControl5);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txtnewqty);
            this.Controls.Add(this.txtqtyadj);
            this.Controls.Add(this.txtqty);
            this.Controls.Add(this.txtcostkg);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl1);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "InventoryQtyAdjustmentDevEx";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "InventoryQtyAdjustmentDevEx";
            this.Load += new System.EventHandler(this.InventoryQtyAdjustmentDevEx_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtsupplier.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtnewqty.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtqtyadj.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtcostkg.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtbranch.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtshipmentno.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtproduct.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtqty.Properties)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtorigqty.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SearchLookUpEdit txtsupplier;
        private DevExpress.XtraGrid.Views.Grid.GridView searchLookUpEdit1View;
        private DevExpress.XtraEditors.LabelControl lblsupplier;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        public DevExpress.XtraEditors.TextEdit txtnewqty;
        public DevExpress.XtraEditors.TextEdit txtqtyadj;
        public DevExpress.XtraEditors.TextEdit txtcostkg;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.SearchLookUpEdit txtbranch;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private System.Windows.Forms.RadioButton radadd;
        private System.Windows.Forms.RadioButton raddeduct;
        private System.Windows.Forms.RadioButton radlinktosupplier;
        private System.Windows.Forms.RadioButton radintransit;
        private DevExpress.XtraEditors.SearchLookUpEdit txtshipmentno;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private DevExpress.XtraEditors.LabelControl ShipmentNo;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.SearchLookUpEdit txtproduct;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView3;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        public DevExpress.XtraEditors.TextEdit txtqty;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        public DevExpress.XtraEditors.TextEdit txtorigqty;
        private DevExpress.XtraEditors.LabelControl labelControl6;
    }
}